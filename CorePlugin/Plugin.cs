using System.Collections.Generic;
using CorePlugin.Commands;
using RebusCore.Commands;
using RebusCore.Plugin;

namespace CorePlugin
{
    public class Plugin : IPlugin
    {
        private List<ICommand> _commands;
        
        public void Initialize()
        {
            _commands = new List<ICommand>();
            _commands.Add(new PingCommand());
        }

        public ICollection<ICommand> GetCommands()
        {
            return _commands;
        }
    }
}