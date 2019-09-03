using System.Collections.Generic;

namespace RebusCore.Commands
{
    public class CommandHandler
    {
        #region Fields

        private List<ICommand> _commands;

        #endregion

        public CommandHandler()
        {
            _commands = new List<ICommand>();
        }
        
        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void AddCommands(ICollection<ICommand> commands)
        {
            _commands.AddRange(commands);
        }
    }
}