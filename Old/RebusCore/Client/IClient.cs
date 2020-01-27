using System;
using System.Net;

namespace RebusCore.Client
{
    /// <summary>
    /// The interface defining the design of the client.
    /// </summary>
    public interface IClient : IDisposable
    {
        /// <summary>
        /// Initializes the client and makes it work.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Connects the client to a remote or local server.
        /// </summary>
        /// <param name="ep">The server endpoint to connect to.</param>
        void Connect(IPEndPoint ep);

        /// <summary>
        /// Disconnects the client from the server.
        /// </summary>
        void Disconnect();
    }
}