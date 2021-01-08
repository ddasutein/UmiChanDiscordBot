using Discord.WebSocket;
using Discord.Commands;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using System.Reflection;

namespace Mamahiru.Core
{
    public class CommandHandler
    {
        private static string argumentStr = "?";

        private static DiscordShardedClient discordShardedClient;
        private static CommandService commandService;

        //private static DiscordSocketClient discordSocketClient;
        private static IServiceProvider services;

        public CommandHandler(IServiceProvider serviceProvider)
        {
            services = serviceProvider;
            commandService = serviceProvider.GetRequiredService<CommandService>();
            discordShardedClient = serviceProvider.GetRequiredService<DiscordShardedClient>();

            commandService.CommandExecuted += CommandExecutedAsync;
            discordShardedClient.MessageReceived += MessageReceivedAsync;
        }

        public static async Task InitializeAsync()
        {
            // register modules that are public and inherit ModuleBase<T>.
            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }


        private static async Task MessageReceivedAsync(SocketMessage socketMessage)
        {
            var message = socketMessage as SocketUserMessage;

            if (message.Source != MessageSource.User)
            {
                return;
            }

            var context = new ShardedCommandContext(discordShardedClient, message);

            // sets the argument position away from the prefix we set
            int argPos = 0;

            // get prefix from the configuration file
            char prefix = Char.Parse("?");

            Console.WriteLine(context);

            // determine if the message has a valid prefix, and adjust argPos based on prefix
            if (!(message.HasMentionPrefix(discordShardedClient.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos)))
            {
                return;
            }

            

            // execute command if one is found that matches
            await commandService.ExecuteAsync(context, argPos, services);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // if a command isn't found, log that info to console and exit this method
            if (!command.IsSpecified)
            {
                System.Console.WriteLine($"Command failed to execute for [] <-> []!");
                return;
            }


            // log success to the console and exit this method
            if (result.IsSuccess)
            {
                System.Console.WriteLine($"Command [] executed for -> []");
                return;
            }


            // failure scenario, let's let the user know
            await context.Channel.SendMessageAsync($"Sorry, ... something went wrong -> []!");
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
