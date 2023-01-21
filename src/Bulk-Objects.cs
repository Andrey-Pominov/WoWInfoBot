using System.Text.Json.Serialization;

namespace WowInfoBot
{
    /// <summary>
    /// https://us.api.blizzard.com/profile/wow/character/{realm}/{character}/encounters/raids?namespace=profile-us
    /// Added via https://app.quicktype.io/
    /// </summary>
    public class RaidJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("character")] public Character Character { get; set; }

        [JsonPropertyName("expansions")] public Expansion[] ExpansionsExpansions { get; set; }
    }

    public class Character
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }
    }

    public class Self
    {
        [JsonPropertyName("href")] public Uri Href { get; set; }
    }

    public class Realm
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("slug")] public string Slug { get; set; }
    }

    public class Expansion
    {
        [JsonPropertyName("expansion")] public Character ExpansionExpansion { get; set; }

        [JsonPropertyName("instances")] public Instance[] Instances { get; set; }
    }

    public class Instance
    {
        [JsonPropertyName("instance")] public Character InstanceInstance { get; set; }

        [JsonPropertyName("modes")] public Mode[] Modes { get; set; }
    }

    public class Mode
    {
        [JsonPropertyName("difficulty")] public Difficulty Difficulty { get; set; }

        [JsonPropertyName("status")] public Difficulty Status { get; set; }

        [JsonPropertyName("progress")] public Progress Progress { get; set; }
    }

    public class Difficulty
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
    }

    public class Progress
    {
        [JsonPropertyName("completed_count")] public long CompletedCount { get; set; }

        [JsonPropertyName("total_count")] public long TotalCount { get; set; }

        [JsonPropertyName("encounters")] public Encounter[] Encounters { get; set; }
    }

    public class Encounter
    {
        [JsonPropertyName("encounter")] public Character EncounterEncounter { get; set; }

        [JsonPropertyName("completed_count")] public long CompletedCount { get; set; }

        [JsonPropertyName("last_kill_timestamp")]
        public long LastKillTimestamp { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("self")] public Self Self { get; set; }
    }


    public class CharacterSummaryJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("race")] public ActiveSpec Race { get; set; }

        [JsonPropertyName("character_class")] public ActiveSpec CharacterClass { get; set; }

        [JsonPropertyName("active_spec")] public ActiveSpec ActiveSpec { get; set; }

        [JsonPropertyName("level")] public long Level { get; set; }

        [JsonPropertyName("equipped_item_level")]
        public long EquippedItemLevel { get; set; }

        [JsonPropertyName("covenant_progress")]
        public CovenantProgress CovenantProgress { get; set; }
    }

    public class Achievements
    {
        [JsonPropertyName("href")] public Uri Href { get; set; }
    }

    public class ActiveSpec
    {
        [JsonPropertyName("key")] public Achievements Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("display_string")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string DisplayString { get; set; }

        [JsonPropertyName("slug")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string Slug { get; set; }
    }

    public class CovenantProgress
    {
        [JsonPropertyName("chosen_covenant")] public ActiveSpec ChosenCovenant { get; set; }

        [JsonPropertyName("renown_level")] public long RenownLevel { get; set; }

        [JsonPropertyName("soulbinds")] public Achievements Soulbinds { get; set; }
    }

    public class Faction
    {
        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
    }


    public class CharacterMediaJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("character")] public Character Character { get; set; }

        [JsonPropertyName("assets")] public Asset[] Assets { get; set; }
    }

    public class Asset
    {
        [JsonPropertyName("key")] public string Key { get; set; }

        [JsonPropertyName("value")] public Uri Value { get; set; }
    }


    public class MythicPlusJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("current_period")] public CurrentPeriod CurrentPeriod { get; set; }

        [JsonPropertyName("seasons")] public Period[] Seasons { get; set; }

        [JsonPropertyName("character")] public MythicPlusInfoCharacter Character { get; set; }
    }

    public class MythicPlusInfoCharacter
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("realm")] public Realm Realm { get; set; }
    }

    public class CurrentPeriod
    {
        [JsonPropertyName("period")] public Period Period { get; set; }

        [JsonPropertyName("best_runs")] public BestRun[] BestRuns { get; set; }
    }

    public class BestRun
    {
        [JsonPropertyName("completed_timestamp")]
        public long CompletedTimestamp { get; set; }

        [JsonPropertyName("duration")] public long Duration { get; set; }

        [JsonPropertyName("keystone_level")] public long KeystoneLevel { get; set; }

        [JsonPropertyName("keystone_affixes")] public Realm[] KeystoneAffixes { get; set; }

        [JsonPropertyName("members")] public Member[] Members { get; set; }

        [JsonPropertyName("dungeon")] public Realm Dungeon { get; set; }

        [JsonPropertyName("is_completed_within_time")]
        public bool IsCompletedWithinTime { get; set; }
    }

    public class Member
    {
        [JsonPropertyName("character")] public MemberCharacter Character { get; set; }

        [JsonPropertyName("specialization")] public Realm Specialization { get; set; }

        [JsonPropertyName("race")] public Realm Race { get; set; }

        [JsonPropertyName("equipped_item_level")]
        public long EquippedItemLevel { get; set; }
    }

    public class MemberCharacter
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("realm")] public Realm Realm { get; set; }
    }

    public class Period
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }
    }

    public class MPlusSeasonInfoJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("season")] public Season Season { get; set; }

        [JsonPropertyName("best_runs")] public BestRun[] BestRuns { get; set; }

        [JsonPropertyName("character")] public MythicPlusSeasonInfoCharacter Character { get; set; }
    }


    public class Dungeon
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }
    }


    public class MythicPlusSeasonInfoCharacter
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("realm")] public Realm Realm { get; set; }
    }


    public partial class Season
    {
        [JsonPropertyName("key")] public Self Key { get; set; }

        [JsonPropertyName("id")] public long Id { get; set; }
    }

    public class PvpBracketInfo
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("character")] public Character Character { get; set; }

        [JsonPropertyName("faction")] public Faction Faction { get; set; }

        [JsonPropertyName("bracket")] public Bracket Bracket { get; set; }

        [JsonPropertyName("rating")] public long Rating { get; set; }

        [JsonPropertyName("season")] public Season Season { get; set; }

        [JsonPropertyName("tier")] public Season Tier { get; set; }

        [JsonPropertyName("season_match_statistics")]
        public MatchStatistics SeasonMatchStatistics { get; set; }

        [JsonPropertyName("weekly_match_statistics")]
        public MatchStatistics WeeklyMatchStatistics { get; set; }

        [JsonPropertyName("specialization")] public Character Specialization { get; set; }

        [JsonPropertyName("season_round_statistics")]
        public MatchStatistics SeasonRoundStatistics { get; set; }

        [JsonPropertyName("weekly_round_statistics")]
        public MatchStatistics WeeklyRoundStatistics { get; set; }
    }

    public class Bracket
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }
    }

    public class MatchStatistics
    {
        [JsonPropertyName("played")] public long Played { get; set; }

        [JsonPropertyName("won")] public long Won { get; set; }

        [JsonPropertyName("lost")] public long Lost { get; set; }
    }

    public class AchievementSummaryJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; }
    }

    public class Achievement
    {
        [JsonPropertyName("id")] public long Id { get; set; }

        [JsonPropertyName("achievement")] public Character AchievementAchievement { get; set; }
    }

    public class MPlusSeasonIndexJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("seasons")] public Season[] Seasons { get; set; }

        [JsonPropertyName("current_season")] public Season CurrentSeason { get; set; }
    }

    public class CharacterStatsJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("health")] public long Health { get; set; }

        [JsonPropertyName("versatility")] public double Versatility { get; set; }

        [JsonPropertyName("versatility_damage_done_bonus")]
        public double VersatilityDamageDoneBonus { get; set; }

        [JsonPropertyName("versatility_healing_done_bonus")]
        public double VersatilityHealingDoneBonus { get; set; }

        [JsonPropertyName("versatility_damage_taken_bonus")]
        public double VersatilityDamageTakenBonus { get; set; }
    }

    public class WoWTokenJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("last_updated_timestamp")]
        public long LastUpdatedTimestamp { get; set; }

        [JsonPropertyName("price")] public long Price { get; set; }
    }

    public class CheckTokenJson
    {
        [JsonPropertyName("scope")] public object[] Scope { get; set; }

        [JsonPropertyName("account_authorities")]
        public object[] AccountAuthorities { get; set; }

        [JsonPropertyName("exp")] public long Exp { get; set; }

        [JsonPropertyName("client_authorities")]
        public object[] ClientAuthorities { get; set; }

        [JsonPropertyName("authorities")] public Authority[] Authorities { get; set; }

        [JsonPropertyName("client_id")] public string ClientId { get; set; }
    }

    public class Authority
    {
        [JsonPropertyName("authority")] public string AuthorityAuthority { get; set; }
    }

    public class PvpSummaryJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("brackets")] public Self[] Brackets { get; set; }

        [JsonPropertyName("honor_level")] public long HonorLevel { get; set; }

        [JsonPropertyName("honorable_kills")] public long HonorableKills { get; set; }
    }

    public class MPlusSummaryJson
    {
        [JsonPropertyName("_links")] public Links Links { get; set; }

        [JsonPropertyName("current_period")] public CurrentPeriod CurrentPeriod { get; set; }

        [JsonPropertyName("seasons")] public Period[] Seasons { get; set; }

        [JsonPropertyName("character")] public Character Character { get; set; }

        [JsonPropertyName("current_mythic_rating")]
        public MythicRating CurrentMythicRating { get; set; }
    }

    public class MythicRating
    {
        [JsonPropertyName("rating")] public double Rating { get; set; }
    }
}