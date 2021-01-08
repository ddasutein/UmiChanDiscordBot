using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mamahiru.Core.Modules.BMS
{
    public class CommandManager : ModuleBase
    {
        [Command("hello")]
        public async Task HelloCommand()
        {
            // initialize empty string builder for reply
            var sb = new StringBuilder();

            // get user info from the Context
            var user = Context.User;

            // build out the reply
            sb.AppendLine($"Hello World!");
            // send simple string reply
            await ReplyAsync(sb.ToString());
        }
    }
}
