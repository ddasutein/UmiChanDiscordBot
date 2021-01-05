using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Mamahiru.Core.Modules.BMS;

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
            Console.WriteLine("DISCORD Bot name: Mamahiru");
            Console.WriteLine("VERSION: 0.0.1-alpha");

            _client = new DiscordSocketClient();
            _client.MessageReceived += CommandHandler.MessageReceived;
            _client.Log += Log;

            if (JsonConfig.settings.token == null)
            {
                Console.WriteLine("token empty");
                Console.ReadKey();
                return;
            }

            string token = JsonConfig.settings.token;
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);

            JsonConfig.SaveJson();

        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}
