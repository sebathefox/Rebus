using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RebusCore
{
    public static class ConfigLoader
    {
        public static Dictionary<string, object> LoadConfig(string pathToFile)
        {
            // If config doesn't exist create default.
            if(!File.Exists(pathToFile))
                GenerateDefaultConfig(pathToFile);

            return DeserializeConfig(File.ReadAllText(pathToFile));
        }

        public static void GenerateDefaultConfig(string pathToFile, Dictionary<string, object> customConfig = null)
        {
            if (customConfig != null)
            {
                string serializedData = SerializeConfig(customConfig);
                
                File.WriteAllText(pathToFile, serializedData, Encoding.UTF8);
            }
            else
            {
                customConfig = new Dictionary<string, object>();
                
                customConfig.Add("port", 1337);
                
                File.WriteAllText(pathToFile, SerializeConfig(customConfig), Encoding.UTF8);
            }
            
        }

        private static string SerializeConfig(Dictionary<string, object> data)
        {
            string config = String.Empty;

            foreach (KeyValuePair<string,object> pair in data)
            {
                config += pair.Key + "=" + pair.Value + "\r";
            }

            return config;
        }

        private static Dictionary<string, object> DeserializeConfig(string data)
        {
            Dictionary<string, object> config = new Dictionary<string, object>();

            string[] lines = data.Split('\r');

            foreach (string line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;
                
                config.Add(line.Substring(0, line.IndexOf('=')), line.Substring(line.IndexOf('=')));

                Console.WriteLine(line);
            }

            return config;
        }
    }
}