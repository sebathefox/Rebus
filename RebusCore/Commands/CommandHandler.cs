using System;
using System.Collections.Generic;

namespace RebusCore.Commands
{
    public class CommandHandler
    {
        #region Fields

        private List<ICommand> _commands;

        private string _result;
        
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

        public void RunCommand(string cmd)
        {
            foreach (ICommand command in _commands)
            {
                if (command.Execute(cmd))
                {
                    _result = command.Result;
                    break;
                }
            }
        }

        public string Result
        {
            get
            {
                string tmp = _result;
                _result = String.Empty;
                return tmp;
            }
        }
    }
}