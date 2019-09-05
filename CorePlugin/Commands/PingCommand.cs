using System;
using RebusCore.Commands;

namespace CorePlugin.Commands
{
    public class PingCommand : ICommand
    {
        
        public bool Execute(string command)
        {
            if(command.Contains("/ping"))
                Console.WriteLine("PONG");
            return true;
        }
    }
}