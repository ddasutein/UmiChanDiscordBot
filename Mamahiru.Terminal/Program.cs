using System;
using Mamahiru.Core;

namespace Mamahiru.Terminal
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                Launcher.MainAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
