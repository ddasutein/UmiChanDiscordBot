using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Mamahiru.Core.Modules.BMS;
using Mamahiru.Core.Services;
using Mamahiru.Terminal.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mamahiru.Terminal
{
    public class MainLauncher
    {
        public static string Hello = "Hello World!";
        public string botname = "Mamahiru";

        private bool IsInitialSetupComplete = false;

        public async Task StartAsync()
        {

            // Before the Bot can start, check if the token has been set in the config file
            JsonConfig.LoadJSON();
            if (string.IsNullOrWhiteSpace(JsonConfig.settings.token))
            {
                do
                {
                    InitialSetup();

                } while (IsInitialSetupComplete == false);
            }

            //Configure services
            var services = new ServiceCollection()
                .AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
                {
                    //LogLevel = LogSeverity.Debug,
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    DefaultRunMode = RunMode.Async,
                    LogLevel = LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    ThrowOnError = false
                }))
                .AddHttpClient()
                .AddSingleton<CommandHandler>()
                .AddSingleton<StartUpService>();

            //Add logging      
            //ConfigureServices(services);

            //Build services
            var serviceProvider = services.BuildServiceProvider();

            //Start the bot
            await serviceProvider.GetRequiredService<StartUpService>().StartAsync();

            //Load up services
            serviceProvider.GetRequiredService<CommandHandler>();

            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


        private void InitialSetup()
        {
            string token;
            Console.WriteLine("Please enter your Discord Bot Token");
            token = Console.ReadLine();
            JsonConfig.settings.token = token;
            JsonConfig.SaveJson();
            Console.ReadKey();
            IsInitialSetupComplete = true;

        }

    }
}
