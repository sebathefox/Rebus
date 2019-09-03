using System.Net;
using RebusCore.Client;

namespace RebusClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            
            client.Connect(new IPEndPoint(IPAddress.Loopback, 1337));
            client.Initialize();
            
        }
    }
}