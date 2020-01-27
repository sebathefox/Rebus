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
        
        /// <summary>
        /// The result to show to the user as a string.
        /// </summary>
        string Result { get; set; }
    }
}