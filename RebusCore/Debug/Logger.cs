using System;
using System.IO;
using System.Text;

namespace RebusCore.Debug
{
    public static class Logger
    {
        private static string _logLocation;
        private static FileStream _file;

        static Logger()
        {
            _logLocation = String.Empty;
        }
        
        public static void Init(string path)
        {
            _logLocation = path;
            _file = File.Open(_logLocation, FileMode.Open, FileAccess.Write);
        }

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