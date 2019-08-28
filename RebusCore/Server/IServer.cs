using System;
using System.Net;

namespace RebusCore.Server
{
    public interface IServer : IDisposable
    {
        void Initialize(IPEndPoint ep);
    }
}