# WoWInfoBot

.NET Core Discord Bot for World of Warcraft Armory Lookups.

## Current Status: Working in Shadowlands 9.x and Dragonflight 10.0

### Setup:
1. Sign up for [Blizzard API Access](https://develop.battle.net/), and register an application. You will need a `client_id` and `client_secret`.
2. Register a new [Discord Application](https://discord.com/developers/applications). Subsequently create a "Bot" for your application, and take note of the `token` for your created bot.
3. On the Oauth2 page of your discord application, use the supplied URL to join your bot to your server(s). The URL should look like `https://discord.com/api/oauth2/authorize?client_id=YOURCLIENTID&permissions=281600&scope=bot`   where YOURCLIENTID is the id listed on the "General Information" page.
4. Modify `appsettings.json` with the above parameters that are highlighted in parts 1 & 2. See [WoW Localizations](https://develop.battle.net/documentation/world-of-warcraft/guides/localization) for localization info, or leave as default (en_US). Non-English localizations are not fully supported.
5. Make sure you have [.NET 6.0](https://dotnet.microsoft.com/download) installed on the system that will be running your bot (.NET 6.0 is [cross-platform](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) so you can run this on Windows/macOS/Linux).


 ## Discord Usage:
```bash
!armory pve/pvp character-realm    ## character-realmslug  --> !armory pvp/pve ассонанс-blackscar
!armory token    ## WoW Token
!armory help    ## Help Command
```

Create file appsetting.json 
```bash
{
	"BotConfig": {
		"DiscordToken": "DiscordToken",
		"CmdPrefix": "!",
		"ClientId": "BlizzardCLientId",
		"ClientSecret": "BlizzardCLientSecret",
		"locale": "en_GB",           ## if region US en_US
		"Timezone": "Europe/Paris"   ## if region US America/New_York
	},
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft": "Warning",
			"Microsoft.Hosting.Lifetime": "Information"
		},
		"Console": {
			"TimestampFormat": "[M/d/y HH:mm:ss]"
		}
	}
}
```