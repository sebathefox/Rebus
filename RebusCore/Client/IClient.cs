using System;
using System.Net;

namespace RebusCore.Client
{
    public interface IClient : IDisposable
    {
        void Initialize();

        void Connect(IPEndPoint ep);

        void Disconnect();
    }
}