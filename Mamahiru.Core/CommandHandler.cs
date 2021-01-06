using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Discord;

namespace Mamahiru.Core
{
    public class CommandHandler
    {
        private static string argumentStr = "?";

        private DiscordShardedClient discordShardedClient;
        private CommandService commandService;

        private readonly DiscordSocketClient discordSocketClient;
        private readonly IServiceProvider services;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            services = serviceProvider;
            commandService = serviceProvider.GetRequiredService<CommandService>();
            discordShardedClient = serviceProvider.GetRequiredService<DiscordShardedClient>();

            commandService.CommandExecuted += CommandExecutedAsync;
            discordSocketClient.MessageReceived += MessageReceivedAsync;
        }

        private async Task MessageReceivedAsync(SocketMessage rawMessage)
        {
            // ensures we don't process system/other bot messages
            if (!(rawMessage is SocketUserMessage message))
            {
                return;
            }

            if (message.Source != MessageSource.User)
            {
                return;
            }

            // sets the argument position away from the prefix we set
            var argPos = 0;

            // get prefix from the configuration file
            char prefix = Char.Parse("?");

            // determine if the message has a valid prefix, and adjust argPos based on prefix
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }

            var context = new SocketCommandContext(_client, message);

            // execute command if one is found that matches
            await _commands.ExecuteAsync(context, argPos, _services);
        }

        private Task CommandExecutedAsync(Optional<CommandInfo> arg1, ICommandContext arg2, IResult arg3)
        {
            throw new NotImplementedException();
        }



        //public static async Task MessageReceived(SocketMessage socketMessage)
        //{
        //    if (socketMessage.Content == argumentStr + "purephrase")
        //    {
        //        await socketMessage.Channel.SendMessageAsync("https://www.youtube.com/watch?v=l_a5iCwRBlU");
        //    }
        //}
    }
}
