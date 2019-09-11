using System;
using System.Net;

namespace RebusCore.Server
{
    /// <summary>
    /// The server interface (NOT FINISHED).
    /// </summary>
    public interface IServer : IDisposable
    {
        /// <summary>
        /// Initializes a new Server.
        /// </summary>
        /// <param name="ep">The listening ip and port.</param>
        void Initialize(IPEndPoint ep);
    }
}