using System.Net;
using RebusCore;
using RebusCore.Server;

namespace RebusServer
{
    internal class Program
    {
        public static int Main(string[] args)
        {
            Server server = new Server();
            
            server.Initialize(args);

            return server.Run();
        }
    }
}