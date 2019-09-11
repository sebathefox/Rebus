using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RebusCore.Client
{
    /// <summary>
    /// The client class used by the official Rebus client.
    /// </summary>
    public class Client : IClient
    {
        private Socket _client;

        private string _data = String.Empty;

        /// <summary>
        /// Called when data is received.
        /// </summary>
        public event EventHandler<string> OnDataReceived;
        
        /// <summary>
        /// Creates a new client.
        /// </summary>
        public Client()
        {
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        
        /// <summary>
        /// Disposes the client.
        /// </summary>
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

        /// <summary>
        /// Sends a message to the server.
        /// </summary>
        /// <param name="message">The string message to send.</param>
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

        /// <summary>
        /// Callback to run when the client have connected successfully.
        /// </summary>
        /// <param name="ar">The async state object.</param>
        private void ConnectCallback(IAsyncResult ar)
        {
            _client.EndConnect(ar);
            Console.WriteLine("Connected");
        }

        /// <summary>
        /// Callback to run when the client have sent data successfully.
        /// </summary>
        /// <param name="ar">The async state object.</param>
        private void SendCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject) ar.AsyncState;
            Socket client = state.Client;

            client.EndSend(ar);
        }

        /// <summary>
        /// Callback to run when the client have received data from the server.
        /// </summary>
        /// <param name="ar">The async state object.</param>
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