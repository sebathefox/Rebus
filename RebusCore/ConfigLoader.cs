using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RebusCore
{
    /// <summary>
    /// Loader class that loads the configs.
    /// </summary>
    public static class ConfigLoader
    {
        private static Dictionary<string, object> _config;
        
        /// <summary>
        /// Loads the config at the specified location.
        /// </summary>
        /// <param name="pathToFile">The location of the config.</param>
        /// <returns>A dictionary containing key value pairs from the config.</returns>
        public static Dictionary<string, object> LoadConfig(string pathToFile)
        {
            if (_config != null)
                return _config;
            
            // If config doesn't exist create default.
            if(!File.Exists(pathToFile))
                GenerateDefaultConfig(pathToFile);

            return _config = DeserializeConfig(File.ReadAllText(pathToFile));
        }

        /// <summary>
        /// Generates a new default config with the default settings.
        /// </summary>
        /// <param name="pathToFile">The location of the file.</param>
        /// <param name="customConfig">The overrides to the config if any.</param>
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
                
                customConfig.Add("listenAddress", "0.0.0.0");
                customConfig.Add("port", 1337);
                customConfig.Add("pluginFolder", "plugins");
                customConfig.Add("logLocation", "output.log");
                
                File.WriteAllText(pathToFile, SerializeConfig(customConfig), Encoding.UTF8);
            }
            
        }

        /// <summary>
        /// Serializes the config to a string that can be written to the config.
        /// </summary>
        /// <param name="data">The config to write.</param>
        /// <returns>The serialized string version of the config.</returns>
        private static string SerializeConfig(Dictionary<string, object> data)
        {
            string config = String.Empty;

            foreach (KeyValuePair<string,object> pair in data)
            {
                config += pair.Key + "=" + pair.Value + "\r";
            }

            return config;
        }

        /// <summary>
        /// Deserializes the config from a <see cref="string"/> to a <see cref="Dictionary{TKey,TValue}"/> object.
        /// </summary>
        /// <param name="data">The raw string from the config.</param>
        /// <returns>The <see cref="Dictionary{TKey,TValue}"/> object.</returns>
        private static Dictionary<string, object> DeserializeConfig(string data)
        {
            Dictionary<string, object> config = new Dictionary<string, object>();

            string[] lines = data.Split('\r');

            foreach (string line in lines)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;
                
                config.Add(line.Substring(0, line.IndexOf('=')), line.Substring(line.IndexOf('=') + 1));

                Console.WriteLine(line);
            }

            return config;
        }
    }
}