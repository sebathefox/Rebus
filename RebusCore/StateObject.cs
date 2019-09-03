using System.Net.Sockets;
using System.Text;

namespace RebusCore
{
    public class StateObject
    {
        public StateObject()
        {
            Client = null;
            Buffer = new byte[BufferSize];
            StringBuilder = new StringBuilder();
        }
        
        public Socket Client { get; set; }

        public byte[] Buffer { get; set; }
        
        public StringBuilder StringBuilder { get; set; }
        
        public static int BufferSize = 2048;
    }
}