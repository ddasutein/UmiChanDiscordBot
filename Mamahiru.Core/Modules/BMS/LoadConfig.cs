using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mamahiru.Core.Modules.BMS
{
    public class LoadConfig
    {
        private static string fileName = "config.json";
        private static bool firstRun = !File.Exists(fileName);
        public static AppConfig settings = new AppConfig();


        public static void LoadJSON()
        {
            Console.WriteLine(firstRun);
            if (!firstRun)
            {
                // Read existing JSON file
                string jsonText = File.ReadAllText(fileName);
                settings = JsonConvert.DeserializeObject<AppConfig>(jsonText);
                Console.WriteLine(jsonText);
            }
            else
            {
                // Create empty JSON file 
                string jsonFormat = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(fileName, jsonFormat);
            }
        }

        public static async void SaveJson()
        {
            await Task.Run(() =>
            {
                try
                {
                    string jsonText = JsonConvert.SerializeObject(settings, Formatting.Indented);
                    File.WriteAllText(fileName, jsonText);
                }
                catch (IOException e)
                {
                    //Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
                }

            });

        }
    }
}
