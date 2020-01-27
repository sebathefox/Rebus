namespace Rebus.Debug
{
    /// <summary>
    /// Base interface for all loggers.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a debug message to the log.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Debug(object message);

        /// <summary>
        /// Writes a info message to the log.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Info(object message);

        /// <summary>
        /// Writes a warning message to the log.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Warning(object message);
        
        /// <summary>
        /// Writes a error message to the log.
        /// </summary>
        /// <param name="message">The message to log</param>
        void Error(object message);
    }
}