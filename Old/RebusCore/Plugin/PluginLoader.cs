using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using RebusCore.Debug;

namespace RebusCore.Plugin
{
    /// <summary>
    /// Contains the methods for dynamic loading of plugins.
    /// </summary>
    public static class PluginLoader
    {
        /// <summary>
        /// Loads the plugin assemblies and returns them as a collection.
        /// </summary>
        /// <param name="path">The path to the folder to search for plugins.</param>
        /// <returns>Returns the collection of found plugins.</returns>
        public static ICollection<IPlugin> LoadPlugins(string path)
        {
            string[] pluginNames = null;

            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/" + path))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/" + path);
            
            if (Directory.Exists(Directory.GetCurrentDirectory() + "/" + path))
            {
                pluginNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/" + path,  "*.dll");
                
                ICollection<Assembly> assemblies = new List<Assembly>(pluginNames.Length);

                foreach (string pluginName in pluginNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(pluginName);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();

                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if(!(type.GetInterface("IPlugin") != null))
                                continue;
                            
                            if(type.IsInterface || type.IsAbstract)
                                continue;
                            if(type.GetInterface(pluginType.FullName) != null)
                                pluginTypes.Add(type);
                        }
                    }
                }
                ICollection<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);

                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin) Activator.CreateInstance(type);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }
    }
}