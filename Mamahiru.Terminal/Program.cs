using System;

namespace Mamahiru.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                new MainLauncher().StartAsync().GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
