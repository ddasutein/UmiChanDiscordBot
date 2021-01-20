using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Mamahiru.Core.Modules.BMS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mamahiru.Terminal.Services
{
    public class StartUpService
    {
        private readonly DiscordShardedClient discordShardedClient;
        private readonly CommandService commandService;
        private readonly IServiceProvider serviceProvider;

        public StartUpService(IServiceProvider services)
        {
            serviceProvider = services;
            discordShardedClient = serviceProvider.GetRequiredService<DiscordShardedClient>();
            commandService = serviceProvider.GetRequiredService<CommandService>();
        }

        public async Task StartAsync()
        {
            JsonConfig.LoadJSON();
            string discordToken = JsonConfig.settings.token;
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new Exception("Token missing from config.json! Please enter your token there (root directory)");
            }
            Console.WriteLine("Start Async ");
            await discordShardedClient.LoginAsync(TokenType.Bot, discordToken);
            await discordShardedClient.StartAsync();
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }

    }
}
