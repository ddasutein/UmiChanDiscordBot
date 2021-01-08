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

namespace Mamahiru.Core
{
    public class StartUpService
    {
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        public StartUpService(IServiceProvider services)
        {
            _services = services;
            _discord = _services.GetRequiredService<DiscordShardedClient>();
            _commands = _services.GetRequiredService<CommandService>();
        }

        public async Task StartAsync()
        {
            string discordToken = JsonConfig.settings.token;
            if (string.IsNullOrWhiteSpace(discordToken))
            {
                throw new Exception("Token missing from config.json! Please enter your token there (root directory)");
            }

            await _discord.LoginAsync(TokenType.Bot, discordToken);
            await _discord.StartAsync();
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

    }
}
