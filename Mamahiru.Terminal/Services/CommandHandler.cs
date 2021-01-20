using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Mamahiru.Core.Services
{
    public class CommandHandler
    {
        private CommandService commandService;
        private DiscordShardedClient discordShardedClient;
        private readonly IServiceProvider serviceProvider;

        public CommandHandler(IServiceProvider services)
        {
            serviceProvider = services;
            discordShardedClient = services.GetRequiredService<DiscordShardedClient>();
            commandService = services.GetRequiredService<CommandService>();
            discordShardedClient.MessageReceived += HandleCommand;
        }

        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            // Don't handle the command if it is a system message
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            // Don't listen to bots
            if (message.Source != MessageSource.User)
            {
                return;
            }

            // Mark where the prefix ends and the command begins
            int argPos = 0;

            // Create a Command Context
            var context = new ShardedCommandContext(discordShardedClient, message);

            char prefix = Char.Parse("?");


            // Determine if the message has a valid prefix, adjust argPos
            if (!(message.HasMentionPrefix(discordShardedClient.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) return;


            // Execute the Command, store the result            
            var result = await commandService.ExecuteAsync(context, argPos, serviceProvider);

            await LogCommandUsage(context, result);
            // If the command failed, notify the user
            if (!result.IsSuccess)
            {
                if (result.ErrorReason != "Unknown command.")
                {
                    await message.Channel.SendMessageAsync($"**Error:** {result.ErrorReason}");
                }
            }
        }


        private async Task LogCommandUsage(SocketCommandContext context, IResult result)
        {
            await Task.Run(async () =>
            {
                if (context.Channel is IGuildChannel)
                {
                    var logTxt = $"User: [{context.User.Username}]<->[{context.User.Id}] Discord Server: [{context.Guild.Name}] -> [{context.Message.Content}]";
                    Console.WriteLine(logTxt);
                }
                else
                {
                    var logTxt = $"User: [{context.User.Username}]<->[{context.User.Id}] -> [{context.Message.Content}]";
                    Console.WriteLine(logTxt);
                }
            });


        }
    }      
}
