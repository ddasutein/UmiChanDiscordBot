using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mamahiru.Terminal.Modules.Commands
{
    public class UserCommands : ModuleBase
    {
        [Command("hello")]
        private async Task Hello()
        {
            var sb = new StringBuilder();
            var user = Context.User;

            sb.AppendLine($"Hi there, -> {user}");
            sb.AppendLine("I am Mamahiru Bot! Yoroshiku Onegaishimasu :wave:");

            await ReplyAsync(sb.ToString());
        }
    }
}
