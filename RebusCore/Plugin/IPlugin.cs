using System.Collections.Generic;
using RebusCore.Commands;

namespace RebusCore.Plugin
{
    public interface IPlugin
    {
        void Initialize();

        ICollection<ICommand> GetCommands();
    }
}