using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RebusCore.Client
{
    public class Client : IClient
    {
        private Socket _client;

        private string _data = String.Empty;

        public event EventHandler<string> OnDataReceived;
        
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
                if (_client.Available > 0)
                {
                    StateObject state = new StateObject();

                    state.Client = _client;
                    
                    _client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback),
                        state);
                }
            }
        }

        public void Send(string message)
        {
            StateObject state = new StateObject();

            state.Client = _client;
                
            byte[] buff = Encoding.UTF8.GetBytes(message);
                    
            _client.BeginSend(buff, 0, buff.Length, SocketFlags.None, new AsyncCallback(SendCallback), state);
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

        private void ReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject) ar.AsyncState;

            state.StringBuilder.Append(Encoding.UTF8.GetString(state.Buffer));
            
            if (state.Client.Available > 0)
            {
                state.Client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, SocketFlags.None,
                    new AsyncCallback(ReceiveCallback), state);
            }
            else
            {
                _data = state.StringBuilder.ToString();
                OnDataReceived?.Invoke(this, state.StringBuilder.ToString());
            }
        }
    }
}