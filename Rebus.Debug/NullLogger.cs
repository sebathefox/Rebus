namespace Rebus.Debug
{
    /// <summary>
    /// NullLogger is a logger used for development purpose and doesn't log anything.
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <inheritdoc />
        public void Debug(object message)
        {
            
        }

        /// <inheritdoc />
        public void Info(object message)
        {
            
        }

        /// <inheritdoc />
        public void Warning(object message)
        {
            
        }

        /// <inheritdoc />
        public void Error(object message)
        {
            
        }
    }
}