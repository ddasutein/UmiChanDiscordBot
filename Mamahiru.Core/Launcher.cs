using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Mamahiru.Core.Modules.BMS;
using Microsoft.Extensions.DependencyInjection;

namespace Mamahiru.Core
{
    public class Launcher
    {
        public static string Hello = "Hello World!";

        private static DiscordSocketClient _client;
        public string botname = "Mamahiru";

        public static async Task MainAsync()
        {
            JsonConfig.LoadJSON();
            Console.WriteLine("Discord Bot: Mamahiru");
            Console.WriteLine("Version 0.0.1-alphadev");

            //_client = new DiscordSocketClient();
            //_client.MessageReceived += CommandHandler.MessageReceived;
            //_client.Log += Log;

            if (JsonConfig.settings.token == null)
            {
                Console.WriteLine("token empty");
                Console.ReadKey();
                return;
            }

            var services = new ServiceCollection()
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    DefaultRunMode = RunMode.Async,
                    LogLevel = LogSeverity.Verbose,
                    CaseSensitiveCommands = false,
                    ThrowOnError = false
                }))
                .AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
                {
                    //LogLevel = LogSeverity.Debug,
                    LogLevel = LogSeverity.Verbose,
                    MessageCacheSize = 1000
                }))
                .AddSingleton<StartUpService>()
                .AddSingleton<CommandHandler>();

            var serviceProvider = services.BuildServiceProvider();

            // Start the Bot
            await serviceProvider.GetRequiredService<StartUpService>().StartAsync();

            // Load up Modules and Services Here
            serviceProvider.GetRequiredService<CommandHandler>();

            //string token = JsonConfig.settings.token;
            //await _client.LoginAsync(TokenType.Bot, token);
            //await _client.StartAsync();
            await Task.Delay(-1);

            //JsonConfig.SaveJson();

        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }


}
