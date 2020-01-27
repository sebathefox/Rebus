using System.Net.Sockets;
using System.Text;

namespace RebusCore
{
    /// <summary>
    /// The object that controls the state of sockets.
    /// </summary>
    public class StateObject
    {
        public StateObject()
        {
            Client = null;
            Buffer = new byte[BufferSize];
            StringBuilder = new StringBuilder();
        }
        
        /// <summary>
        /// The client socket.
        /// </summary>
        public Socket Client { get; set; }

        /// <summary>
        /// The byte buffer used as storage.
        /// </summary>
        public byte[] Buffer { get; set; }
        
        /// <summary>
        /// The stringbuilder that will bind all of the bytes together.
        /// </summary>
        public StringBuilder StringBuilder { get; set; }
        
        /// <summary>
        /// The size in bytes of the buffer.
        /// </summary>
        public static int BufferSize = 2048;
    }
}