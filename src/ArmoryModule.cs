using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace WowInfoBot;

[Group("armory")]
public class ArmoryModule : ModuleBase<ArmoryCommandContext>
{
    [Command]
    public async Task HandleCmd(params string[] args)
    {

        var identity = "";

        for (int i = 1; i < args.Length; i++)
        {
            identity += args[i];
        }

        if (args.Length < 1)
        {
            await SendErrorResponse("No command arguments provided!");
            return;
        }

        if (args[0] == "help")
        {
            await CommandResponseHelp();
        }
        else if (args[0] == "token")
        {
            await CommandResponseToken();
        }
        else if (args[0] == "pve" | args[0] == "pvp")
        {
            if (args.Length < 2)
            {
                await SendErrorResponse("The name or server of the character was entered incorrectly!");
                return;
            }

            if (args[0] == "pve")
            {
                await CommandResponseArmory(identity, LookupType.Pve);
            }
            else if (args[0] == "pvp")
            {
                await CommandResponseArmory(identity, LookupType.Pvp);
            }
        }
        else await SendErrorResponse("wrong command!");
    }

    // !armory pvp/pve nickname-realm  --> !armory pvp/pve ассонанс-Черный Шрам
    private async Task CommandResponseArmory(string identity, LookupType type)
    {
        try
        {
            Context.Logger.LogInformation($"Request from user {Context.Message.Author}");

            var character = identity.ToLower().Split(new[] { '-' }, 2);
            
            var info = await Context.Api.ArmoryLookup(character[0], character[1], type);

            var messageOnDiscord = new EmbedBuilder();

            messageOnDiscord.WithTitle(info.CharInfo.Name);
            messageOnDiscord.WithDescription($"{info.CharInfo.ItemLevel}");

            switch (type)
            {
                case LookupType.Pve:
                    if (info.RaidInfo.Raids.Count == 0)
                    {
                        messageOnDiscord.AddField("Raids", "None", true);
                    }
                    else
                    {
                        foreach (var raid in info.RaidInfo.Raids)
                        {
                            messageOnDiscord.AddField(raid.Name, raid.ToString(), true);
                        }   
                    }
                        
                    messageOnDiscord.AddField("Mythic+", info.MythicPlus);
                    messageOnDiscord.AddField("PVE Achievements", info.Achievements);
                    break;
                    
                case LookupType.Pvp:
                    messageOnDiscord.AddField("Rated PVP", info.PvpRating);
                    messageOnDiscord.AddField("PVP Stats", info.PvpStats);
                    messageOnDiscord.AddField("PVP Achievements", info.Achievements);
                    break;
            }

            messageOnDiscord.WithFooter($"{Context.Prefix}armory help");
            messageOnDiscord.WithThumbnailUrl(info.AvatarUrl);
            messageOnDiscord.WithUrl(info.CharInfo.ArmoryUrl);
            await Context.Message.Channel.SendMessageAsync("", false, messageOnDiscord.Build());
        }
        catch (Exception ex)
        {
            await SendErrorResponse(ex.ToString());
        }
    }

    //  !armory token
    private async Task CommandResponseToken()
    {
        try
        {
            Context.Logger.LogInformation($"Request from user {Context.Message.Author}");
            var token = await Context.Api.WoWTokenLookup();
            var messageOnDiscord = new EmbedBuilder();
            messageOnDiscord.WithTitle("WoW Token");
            messageOnDiscord.WithDescription(
                $"• Price: {token.Price}\n LastUpdated: {token.LastUpdated}");
            messageOnDiscord.WithFooter($"{Context.Prefix}armory help");
            messageOnDiscord.WithThumbnailUrl(Globals.WowTokenIconUrl);
            await Context.Message.Channel.SendMessageAsync("", false, messageOnDiscord.Build());
        }
        catch (Exception ex)
        {
            await SendErrorResponse(ex.ToString());
        }
    }

    //!armory help
    private async Task CommandResponseHelp()
    {
        try
        {
            Context.Logger.LogInformation($"Request from user {Context.Message.Author}");
            var messageOnDiscord = new EmbedBuilder();
            messageOnDiscord.WithTitle("ArmoryBot Help");
            messageOnDiscord.WithDescription(
                $"To view pvp or pve stats: `{Context.Prefix}armory pve/pvp CharacterName-Realm`\n• To find out how much a token is worth: `{Context.Prefix}armory token");
            messageOnDiscord.WithFooter($"Info");
            messageOnDiscord.WithThumbnailUrl(Globals.TomeIconUrl);
            await Context.Message.Channel.SendMessageAsync("", false, messageOnDiscord.Build());
        }
        catch (Exception ex)
        {
            await SendErrorResponse(ex.ToString());
        }
    }

 
    private async Task SendErrorResponse(string error)
    {
        try
        {
            Context.Logger.LogWarning($"{Context.Message} от {Context.Message.Author}: {error}");
            var messageOnDiscord = new EmbedBuilder();
            messageOnDiscord.WithDescription(
                $"**Error** the command was entered incorrectly `{Context.Message}`\nPlease check your spelling and try again..");
            messageOnDiscord.WithFooter($"{Context.Prefix}armory help");
            messageOnDiscord.WithThumbnailUrl(Globals.QuestionMarkIconUrl);
            await Context.Message.Channel.SendMessageAsync("", false, messageOnDiscord.Build());
        }
        catch (Exception ex)
        {
            Context.Logger.LogError(ex.ToString());
        }
    }
    
}