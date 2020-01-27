using System;
using RebusCore.Commands;

namespace CorePlugin.Commands
{
    public class PingCommand : ICommand
    {
        
        public bool Execute(string command)
        {
            if (command.Contains("/ping"))
            {
                Result = "Pong";
                return true;
            }

            return false;
        }

        public string Result { get; set; }
    }
}