using System;
using System.Collections.Generic;

namespace RebusCore.Commands
{
    /// <summary>
    /// The handler that controls the calling of commands.
    /// </summary>
    public class CommandHandler
    {
        #region Fields

        private List<ICommand> _commands;

        private string _result;
        
        #endregion

        /// <summary>
        /// Creates a new <see cref="CommandHandler"/> and sets it up.
        /// </summary>
        public CommandHandler()
        {
            _commands = new List<ICommand>();
        }
        
        /// <summary>
        /// Adds a single command.
        /// </summary>
        /// <param name="command">The command to add.</param>
        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        /// <summary>
        /// Adds a collection of commands.
        /// </summary>
        /// <param name="commands">The collection of commands to add.</param>
        public void AddCommands(ICollection<ICommand> commands)
        {
            _commands.AddRange(commands);
        }

        /// <summary>
        /// Runs each of the internally stored commands until there is one that succeeds.
        /// </summary>
        /// <param name="cmd">The command string.</param>
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

        /// <summary>
        /// The result of the last called command.
        /// </summary>
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