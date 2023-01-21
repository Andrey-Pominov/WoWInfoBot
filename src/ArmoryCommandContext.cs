using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;

namespace WowInfoBot;

public class ArmoryCommandContext : ICommandContext
{
    public readonly BlizzardApi Api;
    public readonly char Prefix;
    public readonly ILogger<Worker> Logger;

    public ArmoryCommandContext(IDiscordClient client, SocketUserMessage message, BlizzardApi api, char prefix,
        ILogger<Worker> logger)
    {
        Client = client;
        Guild = (message.Channel as IGuildChannel)?.Guild;
        Channel = message.Channel;
        User = message.Author;
        Message = message;
        Api = api;
        Prefix = prefix;
        Logger = logger;
    }

    public IDiscordClient Client { get; }
    public IUserMessage Message { get; }
    public IUser User { get; }
    public IGuild Guild { get; }
    public IMessageChannel Channel { get; }
}