using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Timers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Timer = System.Timers.Timer;

namespace WowInfoBot
{
    public class BlizzardApi
    {
        private readonly ILogger<Worker> _logger;
        private readonly ArmoryBotConfig _config;
        private readonly IServiceCollection _services;
        private readonly IHttpClientFactory? _clientFactory;
        private BlizzardAccessToken _token;
        private Timer _tokenExpTimer;
        private HttpRequestMessage request;

        private long _mPlusSeasonId = -1;

        public BlizzardApi(ILogger<Worker> logger, ArmoryBotConfig config)
        {
            _logger = logger;
            _config = config;
            _services = new ServiceCollection();

            _services.AddHttpClient("default")
                .AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError()
                    .OrResult(response => (int)response.StatusCode != 200)
                    .WaitAndRetryAsync(3, delay => TimeSpan.FromMilliseconds(250)));


            _clientFactory = _services.BuildServiceProvider().GetService<IHttpClientFactory>();
            RequestToken();
        }

        public async Task<ArmoryData> ArmoryLookup(string character, string realm, LookupType type)
        {
            try
            {
                var info = new ArmoryData();
                var realmSlug = GetRealmSlug(realm);

                var charInfo = GetCharacter(character, realmSlug.Result);
                var avatarInfo = GetAvatar(character, realmSlug.Result);
                var achievementInfo = GetAchievements(character, realmSlug.Result, type);
                switch (type)
                {
                    case LookupType.Pve:
                        var raidInfo = GetRaids(character, realmSlug.Result);
                        var mythicPlus = GetMythicPlus(character, realmSlug.Result);
                        await Task.WhenAll(raidInfo, mythicPlus);
                        info.RaidInfo = raidInfo.Result;
                        info.MythicPlus = mythicPlus.Result;
                        break;
                    case LookupType.Pvp:
                        var pvpInfo = GetPvp(character, realmSlug.Result);
                        var pvpStats = GetPvpStats(character, realmSlug.Result);
                        await Task.WhenAll(pvpInfo, pvpStats);
                        info.PvpRating = pvpInfo.Result;
                        info.PvpStats = pvpStats.Result;
                        break;
                }

                await Task.WhenAll(charInfo, avatarInfo, achievementInfo);
                info.CharInfo = charInfo.Result;
                info.AvatarUrl = avatarInfo.Result;
                info.Achievements = achievementInfo.Result;
                return info;
            }
            catch
            {
                await CheckToken();
                throw;
            }
        }

        public async Task<WoWToken> WoWTokenLookup()
        {
            try
            {
                var info = new WoWToken();
                WoWTokenJson wowToken =
                    await Call($"https://{_config.Region}.api.blizzard.com/data/wow/token/index",
                        Namespace.Dynamic, typeof(WoWTokenJson));
                info.Price = (wowToken.Price / 10000).ToString();

                var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var lastUpdate = origin.AddMilliseconds(wowToken.LastUpdatedTimestamp).ToUniversalTime();
                var span = DateTime.Now.ToUniversalTime() - lastUpdate;

                info.LastUpdated = $"{lastUpdate} ({span.Minutes} minutes ago)";
                return info;
            }
            catch
            {
                await CheckToken();
                throw;
            }
        }

        private async Task<CharacterInfo> GetCharacter(string character, string realm)
        {
            var info = new CharacterInfo();

            CharacterSummaryJson summary = await Call($"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}", Namespace.Profile, typeof(CharacterSummaryJson));


            info.Name = $"{summary.Name} {summary.Level} {summary.Race.Name} {summary.ActiveSpec.Name} {summary.CharacterClass.Name}";
            info.ItemLevel = $"\n**Item Level: {summary.EquippedItemLevel}**";
            info.ArmoryUrl = $"https://worldofwarcraft.com/character/{_config.Region}/{realm}/{character}";

            return info;
        }

        //TODO: need get all realms one times
        private async Task<string> GetRealmSlug(string characterRealm)
        {
            try
            {
                var realms = new RealmsWow();

                var page = 1;

                do
                {
                    RealmsJson summary = await Call($"https://{_config.Region}.api.blizzard.com/data/wow/search/realm", Namespace.Dynamic, typeof(RealmsJson), page);

                    realms.Results = summary.Results;
                    realms.PageCount = summary.PageCount;

                    foreach (var realm in realms.Results)
                    {
                        var tempRealm = realm.Data.Name;

                        if (characterRealm.Equals(tempRealm.EnGb.ToLower().Replace(" ", "")) ||
                            characterRealm.Equals(tempRealm.RuRu.ToLower().Replace(" ", "")))
                        {
                            return realm.Data.Slug;
                        }
                    }

                    page++;
                } while (page <= realms.PageCount);

                return characterRealm;
            }
            catch
            {
                throw new NullReferenceException("Realm value is null.");
            }
        }

        private async Task<string> GetAvatar(string character, string realm)
        {
            CharacterMediaJson characterMedia = await Call(
                $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/character-media",
                Namespace.Profile, typeof(CharacterMediaJson));

            foreach (var asset in characterMedia.Assets)
            {
                if (asset.Key.ToLower() == "avatar")
                {
                    return asset.Value.ToString();
                }
            }

            throw new NullReferenceException("Avatar value is null.");
        }

        private async Task<RaidData> GetRaids(string character, string realm)
        {
            var raids = new RaidData(_config.Locale);

            RaidJson raidInfo = await Call(
                $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/encounters/raids",
                Namespace.Profile, typeof(RaidJson));

            if (raidInfo.ExpansionsExpansions != null)
            {
                foreach (var expansion in raidInfo.ExpansionsExpansions)
                {
                    switch (expansion.ExpansionExpansion.Id)
                    {
                        case (long)Id.AdditionDragonflight:
                            foreach (var raid in expansion.Instances)
                            {
                                raids.Add(raid);
                            }

                            break;

                        // if need progress on Shadowlands 9.0+
                        // case (long)Id.AdditionShadowlands:
                        //     foreach (var raid in expansion.Instances)
                        //     {
                        //         raids.Add(raid);
                        //     }
                        // break;
                    }
                }
            }

            return raids;
        }

        private async Task<string> GetMythicPlus(string character, string realm)
        {
            MPlusSummaryJson summary = await Call(
                $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/mythic-keystone-profile",
                Namespace.Profile, typeof(MPlusSummaryJson));

            var hasCurrentSeason = false;

            if (summary.Seasons != null)
            {
                foreach (var season in summary.Seasons)
                {
                    if (season.Id == _mPlusSeasonId)
                    {
                        hasCurrentSeason = true;
                        break;
                    }
                }
            }

            if (hasCurrentSeason)
            {
                var data = new MythicPlusData();

                MPlusSeasonInfoJson mPlusSeasonInfo = await Call(
                    $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/mythic-keystone-profile/season/{_mPlusSeasonId}",
                    Namespace.Profile, typeof(MPlusSeasonInfoJson));

                data.Parse(summary, mPlusSeasonInfo);
                return data.ToString();
            }

            return "None";
        }

        private async Task<string> GetAchievements(string character, string realm, LookupType type)
        {
            var list = new AchievementsList();
            AchievementSummaryJson achievementInfo =
                await Call(
                    $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/achievements",
                    Namespace.Profile, typeof(AchievementSummaryJson));
            switch (type)
            {
                case LookupType.Pve:
                    foreach (var achievement in achievementInfo.Achievements)
                    {
                        if (Globals.AchievementsPve.ContainsKey(achievement.Id))
                            list.Add(achievement.Id, achievement.AchievementAchievement.Name, type);
                    }

                    break;
                case LookupType.Pvp:
                    foreach (var achievement in achievementInfo.Achievements)
                    {
                        if (Globals.AchievementsPvp.ContainsKey(achievement.Id))
                            list.Add(achievement.Id, achievement.AchievementAchievement.Name, type);
                    }

                    break;
            }

            return list.ToString();
        }

        private async Task<string> GetPvp(string character, string realm)
        {
            var output = "";

            PvpSummaryJson summary =
                await Call(
                    $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/pvp-summary",
                    Namespace.Profile, typeof(PvpSummaryJson));

            if (summary.Brackets != null)
            {
                foreach (var item in summary.Brackets)
                {
                    var uri = item.Href.ToString().Split('?');
                    PvpBracketInfo bracket = await Call(uri[0], Namespace.Profile, typeof(PvpBracketInfo));

                    var pvpBracket = bracket.Bracket.Type;

                    switch (pvpBracket.ToLower())
                    {
                        case "arena_2v2":
                            pvpBracket = "2х2";
                            break;
                        case "arena_3v3":
                            pvpBracket = "3х3";
                            break;
                        case "battlegrounds":
                            pvpBracket = "RBG";
                            break;
                        case "shuffle":
                            pvpBracket = $"{bracket.Specialization.Name} - solo shuffle";
                            break;
                        default:
                            continue;
                    }

                    if (bracket.SeasonMatchStatistics.Played > 0)
                    {
                        var winPercent = 0;
                        if (bracket.SeasonMatchStatistics.Won > 0)
                        {
                            if (bracket.Bracket.Type.ToLower().Equals("shuffle"))
                            {
                                winPercent = (int)(bracket.SeasonRoundStatistics.Won /
                                    (double)bracket.SeasonRoundStatistics.Played * 100);
                            }
                            else
                            {
                                winPercent = (int)(bracket.SeasonMatchStatistics.Won /
                                    (double)bracket.SeasonMatchStatistics.Played * 100);
                            }
                        }

                        output += $"{pvpBracket} Rating: {bracket.Rating}, Win {winPercent}%)\n";
                    }
                }
            }
            else
            {
                output += "None";
            }

            output += $"\n Honor Level {summary.HonorLevel}. Honorable Kills {summary.HonorableKills}";
            return output;
        }

        private async Task<string> GetPvpStats(string character, string realm)
        {
            CharacterStatsJson stats =
                await Call(
                    $"https://{_config.Region}.api.blizzard.com/profile/wow/character/{realm}/{character}/statistics",
                    Namespace.Profile, typeof(CharacterStatsJson));
            return $" Health: {stats.Health}\n Versatility: {Math.Round(stats.VersatilityDamageDoneBonus, 2)}%";
        }

        private async Task GetGameData()
        {
            MPlusSeasonIndexJson jsonSeason = await Call(
                $"https://{_config.Region}.api.blizzard.com/data/wow/mythic-keystone/season/index",
                Namespace.Dynamic, typeof(MPlusSeasonIndexJson));
            _mPlusSeasonId = jsonSeason.CurrentSeason.Id;
        }

        private async Task RequestToken()
        {
            try
            {
                _logger.LogInformation("Requesting new BlizzAPI Token...");
                using var request = new HttpRequestMessage(new HttpMethod("POST"),
                    $"https://{_config.Region}.battle.net/oauth/token");

                var base64Authorization =
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_config.ClientId}:{_config.ClientSecret}"));

                request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64Authorization}");
                request.Content = new StringContent("grant_type=client_credentials");
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var client = _clientFactory.CreateClient("default");
                using var response = await client.SendAsync(request);
                await using var content = await response.Content.ReadAsStreamAsync();


                _token = await JsonSerializer.DeserializeAsync<BlizzardAccessToken>(content,
                    new JsonSerializerOptions { IgnoreNullValues = true });

                if (_token.AccessToken is null)
                {
                    throw new Exception($"Error obtaining token:\n{response}");
                }

                TokenExpTimerStart();
                _logger.LogInformation($"BlizzAPI Token obtained! Valid until {_token.ExpireDate} (Auto-Renewing).");

                await GetGameData();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
            }
        }

        private async Task CheckToken()
        {
            try
            {
                _logger.LogWarning("Checking BlizzAPI Token...");
                using var request = new HttpRequestMessage(new HttpMethod("POST"),
                    $"https://{_config.Region}.battle.net/oauth/check_token");

                var contentList = new List<string>();

                contentList.Add($"token={_token.AccessToken}");
                request.Content = new StringContent(string.Join("&", contentList));
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                var client = _clientFactory.CreateClient("default");
                using var response = await client.SendAsync(request);
                await using var content = await response.Content.ReadAsStreamAsync();

                var json = await JsonSerializer.DeserializeAsync<CheckTokenJson>(content,
                    new JsonSerializerOptions { IgnoreNullValues = true });

                if (json.ClientId is null)
                {
                    throw new Exception($"BlizzAPI Token is no longer valid!\n{response}");
                }

                _logger.LogInformation($"BlizzAPI Token is valid! Valid until {_token.ExpireDate} (Auto-Renewing).");

                if (_mPlusSeasonId == -1)
                    await GetGameData();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.ToString());
                await RequestToken();
            }
        }

        private async Task<dynamic> Call(string uri, Namespace space, Type jsonType, int api = 0)
        {
            if (api == 0)
            {
                request = new HttpRequestMessage(new HttpMethod("GET"), uri + $"?locale={_config.Locale}");
            }
            else
            {
                request = new HttpRequestMessage(new HttpMethod("GET"), uri + $"?timezone={_config.Timezone}" + $"&orderby=id&_page={api}&");
            }


            switch (space)
            {
                case Namespace.Profile:
                    request.Headers.TryAddWithoutValidation("Battlenet-Namespace", $"profile-{_config.Region}");
                    break;
                case Namespace.Static:
                    request.Headers.TryAddWithoutValidation("Battlenet-Namespace", $"static-{_config.Region}");
                    break;
                case Namespace.Dynamic:
                    request.Headers.TryAddWithoutValidation("Battlenet-Namespace", $"dynamic-{_config.Region}");
                    break;
            }

            request.Headers.TryAddWithoutValidation("Authorization",
                $"Bearer {_token.AccessToken}");

            var client = _clientFactory.CreateClient("default");
            using var response = await client.SendAsync(request);
            await using var content = await response.Content.ReadAsStreamAsync();

            return (await JsonSerializer.DeserializeAsync(content, jsonType))!;
        }

        private void TokenExpTimerStart()
        {
            _tokenExpTimer = new Timer(_token.ExpiresIn * 1000);
            _tokenExpTimer.AutoReset = false;
            _tokenExpTimer.Elapsed += TokenExpTimerElapsed;
            _tokenExpTimer.Start();
        }

        private async void TokenExpTimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.LogWarning("BlizzAPI Token expired!");
            await RequestToken();
        }
    }
}