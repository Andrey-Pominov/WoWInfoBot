using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WowInfoBot
{
    public class ArmoryBot
    {
        private readonly ILogger<Worker> _logger;
        private readonly ArmoryBotConfig _config;
        private readonly BlizzardApi _blizzApi;
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public ArmoryBot(ILogger<Worker> logger, ArmoryBotConfig config)
        {
            _logger = logger;
            _config = config;
            _blizzApi = new BlizzardApi(logger, config);
        }

        public async Task DiscordStartupAsync() // Discord bot подключение
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands)
                .BuildServiceProvider();
            
            _client.Log += DiscordLog;
            _client.MessageReceived += DiscordHandleCommandAsync;
            
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            await _client.LoginAsync(TokenType.Bot, _config.DiscordToken);
            await _client.StartAsync();
            await _client.SetGameAsync($"{_config.CmdPrefix}armory help", null, ActivityType.Listening);
        }

        private async Task DiscordHandleCommandAsync(SocketMessage messageParam)
        {
            if (messageParam is null) return;
            if (messageParam is not SocketUserMessage message) return;
            if (message.Source is not MessageSource.User) return;
            
            var argPos = 0;
            
            if (!message.HasCharPrefix(_config.CmdPrefix, ref argPos)) return;
            await Task.Run(async delegate
            {
                await _commands.ExecuteAsync(new ArmoryCommandContext(_client, message, _blizzApi, _config.CmdPrefix, _logger), argPos, _services);
            });
        }

        private async Task DiscordLog(LogMessage message)
        {
            string messageOut = $"Discord: {message.Message}{message.Exception}";
            switch (message.Severity)
            {
                case LogSeverity.Critical:
                    _logger.LogCritical(messageOut);
                    break;
                case LogSeverity.Error:
                    _logger.LogError(messageOut);
                    break;
                case LogSeverity.Warning:
                    _logger.LogWarning(messageOut);
                    break;
                case LogSeverity.Debug:
                    _logger.LogDebug(messageOut);
                    break;
                default:
                    _logger.LogInformation(messageOut);
                    break;
            }
        }
    }
}