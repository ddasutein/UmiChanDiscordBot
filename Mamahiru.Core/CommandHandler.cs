using Discord.WebSocket;
using System.Threading.Tasks;

namespace Mamahiru.Core
{
    public class CommandHandler
    {
        private static string argumentStr = "?";

        public static async Task MessageReceived(SocketMessage socketMessage)
        {
            if (socketMessage.Content == argumentStr + "purephrase")
            {
                await socketMessage.Channel.SendMessageAsync("https://www.youtube.com/watch?v=l_a5iCwRBlU");
            }
        }
    }
}
