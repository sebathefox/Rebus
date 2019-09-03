using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RebusCore.Client
{
    public class Client : IClient
    {
        private Socket _client;

        public Client()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        
        public void Dispose()
        {
            
        }

        public void Initialize()
        {
            while (true)
            {
                Console.Write("Write message: ");
                string content = Console.ReadLine();
                
                StateObject state = new StateObject();

                state.Client = _client;
                
                byte[] buff = Encoding.UTF8.GetBytes(content);

                _client.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), state);
            }
        }

        public void Connect(IPEndPoint ep)
        {
            _client.BeginConnect(ep, new AsyncCallback(ConnectCallback), null);
        }

        public void Disconnect()
        {
            
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            _client.EndConnect(ar);
            Console.WriteLine("Connected");
        }

        private void SendCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject) ar.AsyncState;
            Socket client = state.Client;

            client.EndSend(ar);
        }
    }
}