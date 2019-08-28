using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace RebusCore.Server
{
    public class Server : IServer
    {
        private Socket _listener;

        private ConcurrentBag<Socket> _clients;
        
        public void Dispose()
        {
            
        }

        public void Initialize(IPEndPoint ep)
        {
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new ConcurrentBag<Socket>();
            
            _listener.Bind(ep);

            _listener.Listen(10);
            
            while (true)
            {


                _listener.BeginAccept(new AsyncCallback(Accept), null);

                foreach (Socket client in _clients)
                {
                    if (client.Available > 0)
                    {
                        StateObject state = new StateObject();

                        client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(Receive), state);
                    }
                }
                
            }
        }

        private void Accept(IAsyncResult ar)
        {
            _clients.Add(_listener.EndAccept(ar));
        }

        private void Receive(IAsyncResult ar)
        {
            
        }
    }
}