using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using DiscordBot;
using Discord.WebSocket;
using System.Text.RegularExpressions;

/* Import custom module functions */
using DiscordBot.Modules.PROFANITY_MODULE;

namespace UmiChanBot
{

    public class Program
    {
        private DiscordSocketClient _client;
        public string botname = "UmiChanBot: ";

        public static void Main(string[] args)       
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Console.WriteLine("DISCORD Bot name: UmiChanBot");
            Console.WriteLine("VERSION: 1.0.0");

            _client = new DiscordSocketClient();
            _client.MessageReceived += MessageReceived;
            _client.Log += Log;


            string token = "";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);

        }

        private async Task MessageReceived(SocketMessage socketMessage)
        {

            _Botphrases _Botphrases = new _Botphrases();
            _Botemotes _Botemotes = new _Botemotes();
            _ProfanityFilter profanityFilter = new _ProfanityFilter();

            Random random = new Random();

            int RANDOM_PHRASE = random.Next(0, 5);
            string RANDOM_PHRASE_RESULT = null;

            switch (RANDOM_PHRASE)
            {
                case 0:
                    RANDOM_PHRASE_RESULT = _Botphrases.PING_PONG;
                    break;
                case 1:
                    RANDOM_PHRASE_RESULT = _Botphrases.UMI_HAPPY_MESSAGE_1;
                    break;
                case 2:
                    RANDOM_PHRASE_RESULT = _Botphrases.UMI_HAPPY_MESSAGE_2;
                    break;
                case 3:
                    RANDOM_PHRASE_RESULT = _Botphrases.UMI_CATCH_PHRASE;
                    break;
                case 4:
                    RANDOM_PHRASE_RESULT = _Botphrases.UMI_HAPPY_MESSAGE_3;
                    break;
                case 5:
                    RANDOM_PHRASE_RESULT = _Botphrases.UMI_HAPPY_MESSAGE_4;
                    break;
            }

            Random random_1 = new Random();
            int RANDOM_MIMORIN_PICTURE = random_1.Next(0, 4);
            string RANDOM_MIMO_PICTURE_RESULT = null;

            switch (RANDOM_MIMORIN_PICTURE)
            {
                case 0:
                    RANDOM_MIMO_PICTURE_RESULT = _Botphrases.UMI_MIMORIN_1;
                    break;
                case 1:
                    RANDOM_MIMO_PICTURE_RESULT = _Botphrases.UMI_MIMORIN_2;
                    break;
                case 2:
                    RANDOM_MIMO_PICTURE_RESULT = _Botphrases.UMI_MIMORIN_3;
                    break;
                case 3:
                    RANDOM_MIMO_PICTURE_RESULT = _Botphrases.UMI_MIMORIN_4;
                    break;
                case 4:
                    RANDOM_MIMO_PICTURE_RESULT = _Botphrases.UMI_MIMORIN_5;
                    break;
            }

            if (socketMessage.Content == "!ping")
            {
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + socketMessage);

                if (RANDOM_PHRASE == 0)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }
                
                if (RANDOM_PHRASE == 1)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }

                if (RANDOM_PHRASE == 2)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }

                if (RANDOM_PHRASE == 3)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }

                if (RANDOM_PHRASE == 4)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }

                if (RANDOM_PHRASE == 5)
                {
                    await socketMessage.Channel.SendMessageAsync(RANDOM_PHRASE_RESULT);
                }
            }

            if (socketMessage.Content == "海未ちゃん大好き")
            {
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + socketMessage);
                await socketMessage.Channel.SendMessageAsync(_Botphrases.UMI_RESPOND_TO_SUKI);
            }

            string[] mimomimo = { "!mimorin", "Mimorin", "!みもりん", "!三森すずこ" };

            foreach (string x in mimomimo)
            {
                if (socketMessage.Content == x)
                {
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + "sent attachment to ");

                    if (RANDOM_MIMORIN_PICTURE == 0)
                    {
                        await socketMessage.Channel.SendMessageAsync(RANDOM_MIMO_PICTURE_RESULT);
                    }

                    if (RANDOM_MIMORIN_PICTURE == 1)
                    {
                        await socketMessage.Channel.SendMessageAsync(RANDOM_MIMO_PICTURE_RESULT);
                    }

                    if (RANDOM_MIMORIN_PICTURE == 2)
                    {
                        await socketMessage.Channel.SendMessageAsync(RANDOM_MIMO_PICTURE_RESULT);
                    }

                    if (RANDOM_MIMORIN_PICTURE == 3)
                    {
                        await socketMessage.Channel.SendMessageAsync(RANDOM_MIMO_PICTURE_RESULT);
                    }

                    if (RANDOM_MIMORIN_PICTURE == 4)
                    {
                        await socketMessage.Channel.SendMessageAsync(RANDOM_MIMO_PICTURE_RESULT);
                    }
                }
            }

            string[] yozorawanandemo = { "Yozora wa nandemo", "yozora wa", "Yozora wa nandemo shitteru" };

            foreach (string x in yozorawanandemo)
            {
                if (socketMessage.Content == x)
                {
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + "sent attachment");
                    await socketMessage.Channel.SendMessageAsync("https://www.youtube.com/watch?v=DkoSxdfFdeI");
                }
            }


            foreach (string x in profanityFilter.filteredWords)
            {

                string badwords;

                badwords = socketMessage.Content.ToString();

                for (int i = 0; i < x.Length; i++)
                {
                    if (badwords.Contains(x))
                    {
                        Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + "Profanity has been detected in chat.");
                        await socketMessage.Channel.SendMessageAsync("Please tone down the profanity. Thank you! (^ ^)/");
                        return;
                    }
                }

            }
      
           

            string[] iHateUmi = { "I hate Umi", "Umi makes me angry", "I hate you Umi" };

            foreach (string x in iHateUmi)
            {
                if (socketMessage.Content == x)
                {
                    await socketMessage.Channel.SendMessageAsync(_Botemotes.EMOTE_BROKEN_HEART);

                    SocketGuildUser user;
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + "EMOTE_BROKEN_HEART");
                }
            }

            if (socketMessage.Content == "umi.seiyuu")
            {
                Console.WriteLine(DateTime.Now.ToString("hh:mm:ss : ") + botname + "!seiyuu command");
                await socketMessage.Channel.SendMessageAsync(_Botphrases.UMI_MY_SEIYUU_1);
            }

        }

        public Task DeleteAsync(RequestOptions options = null)
        {
            return null;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
