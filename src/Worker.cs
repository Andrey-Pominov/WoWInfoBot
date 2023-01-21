using System.Net.NetworkInformation;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WowInfoBot
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ArmoryBotConfig _config;
        private ArmoryBot _armoryBot;

        public Worker(ILogger<Worker> logger, ArmoryBotConfig config)
        {
            _logger = logger;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CheckForInternet(); 
            void CheckForInternet()
            {
                _logger.LogWarning("Waiting for network connection...");
                using var ping = new Ping();
                string[] urls = { $"{_config.Region}.battle.net", "discord.com", "google.com" }; 
                while (true)
                {
                    foreach (var url in urls)
                    {
                        try
                        {
                            stoppingToken.ThrowIfCancellationRequested();
                            var reply = ping.Send(url); 
                            if (reply.Status is IPStatus.Success) return; 
                        }
                        catch (OperationCanceledException) { throw; }
                        catch
                        {
                            // ignored
                        }
                        finally { Thread.Sleep(250); } 
                    }
                }
            }
            _logger.LogInformation("Starting up WowInfoBot...");
            _armoryBot = new ArmoryBot(_logger, _config);
            await _armoryBot.DiscordStartupAsync();
            await Task.Delay(-1, stoppingToken); 
        }
    }
}