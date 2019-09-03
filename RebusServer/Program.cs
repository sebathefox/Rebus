using System.Net;
using RebusCore;
using RebusCore.Server;

namespace RebusServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //TODO: Argument handling.
            
            Server server = new Server();
            
            server.Initialize(new IPEndPoint(IPAddress.Any, 1337));

            ConfigLoader.LoadConfig("./kek.txt");
        }
    }
}