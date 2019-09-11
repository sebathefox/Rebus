using System.Collections.Generic;
using RebusCore.Commands;

namespace RebusCore.Plugin
{
    /// <summary>
    /// The plugin interface that all plugins must follow to get loaded.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initializes the plugin.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Gets the plugin specific commands.
        /// </summary>
        /// <returns>Returns the collection of commands if any.</returns>
        ICollection<ICommand> GetCommands();
    }
}