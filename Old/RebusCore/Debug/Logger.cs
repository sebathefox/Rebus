using System;
using System.IO;
using System.Text;

namespace RebusCore.Debug
{
    /// <summary>
    /// A simple logger that logs to the debug, console and a specific logfile.
    /// </summary>
    public static class Logger
    {
        private static string _logLocation;
        private static FileStream _file;

        static Logger()
        {
            _logLocation = String.Empty;
        }
        
        /// <summary>
        /// Initializes the logger.
        /// </summary>
        /// <param name="path">The path to the logfile.</param>
        public static void Init(string path)
        {
            _logLocation = path;

            if (!File.Exists(_logLocation))
                File.Create(_logLocation);
            
            _file = File.Open(_logLocation, FileMode.Open, FileAccess.Write);
        }

        /// <summary>
        /// Logs a object through this logger.
        /// </summary>
        /// <param name="message">The <see cref="object"/> to log using it's ToString() method.</param>
        public static void Log(object message)
        {
            string msg = message.ToString();
            
            Console.WriteLine(msg);
            System.Diagnostics.Debug.WriteLine(msg);
            
            msg += "\r\n";
            
            _file.Write(Encoding.UTF8.GetBytes(msg), 0, msg.Length);
            _file.Flush(true);
        }
    }
}