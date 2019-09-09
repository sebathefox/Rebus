namespace RebusCore.Commands
{
    /// <summary>
    /// Defines the basic structure of a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command through the <see cref="ICommand"/> Interface.
        /// </summary>
        /// <returns>Whether it was a valid command.</returns>
        bool Execute(string command);
        
        string Result { get; set; }
    }
}