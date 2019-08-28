using System.Net.Sockets;

namespace RebusCore
{
    public class StateObject
    {
        public StateObject()
        {
            Client = null;
            Buffer = new byte[BufferSize];
        }
        
        public Socket Client { get; set; }

        public byte[] Buffer { get; set; }
        
        public static int BufferSize = 2048;
    }
}