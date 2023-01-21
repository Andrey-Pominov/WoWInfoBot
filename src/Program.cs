using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WowInfoBot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) => 
           Host.CreateDefaultBuilder(args)
               .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration.GetSection("BotConfig").Get<ArmoryBotConfig>();
                    services.AddSingleton(config);
                    services.AddHostedService<Worker>();
                });
    }
}