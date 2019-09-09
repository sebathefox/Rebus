using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RebusCore.Commands;
using RebusCore.Debug;
using RebusCore.Plugin;

namespace RebusCore.Server
{
    /// <summary>
    /// A basic self sustainable server class.
    /// </summary>
    public sealed class Server
    {
        #region Fields

        // The raw underlying socket used for server communication.
        private Socket _listener;

        // A thread safe unlinked list that holds the clients.
        private ConcurrentBag<Socket> _clients;

        // Used for shutting down the main loop.
        private bool _isRunning;

        // A list of used plugins.
        private ICollection<IPlugin> _plugins = null;

        private CommandHandler _commandHandler;

        private Dictionary<string, object> _config;

        private ManualResetEvent allDone = new ManualResetEvent(false);

        private Thread _receiverThread;
        
        #endregion

        public void Dispose()
        {
            _listener.Close();
            _listener.Dispose();
            
            foreach (Socket client in _clients)
            {
                client.Disconnect(false);
                client.Close(2);
                client.Dispose();
            }
        }

        /// <summary>
        /// Initializes the server with it's default parameters and sets it to listen on the specified endpoint.
        /// </summary>
        /// <param name="args">The program arguments if any.</param>
        public void Initialize(string[] args)
        {
            _config = ConfigLoader.LoadConfig("./config.txt");
            
            Logger.Init(_config["logLocation"].ToString());
            
            _isRunning = true;
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            Logger.Log("Created Socket");
            
            _clients = new ConcurrentBag<Socket>();
            _commandHandler = new CommandHandler();
            
            Logger.Log("Loading Plugins...");
            
            _plugins = PluginLoader.LoadPlugins(_config["pluginFolder"].ToString());
            
            foreach (IPlugin plugin in _plugins)
            {
                plugin.Initialize();

                Logger.Log("LOADED PLUGIN: " + plugin.GetType().ToString());
                
                _commandHandler.AddCommands(plugin.GetCommands());
            }
            
                
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(_config["listenAddress"].ToString()),
            int.Parse(_config["port"].ToString()));
            
            
            Logger.Log("Binding to Endpoint: " + ep.ToString());
            _listener.Bind(ep);

            Logger.Log("Bound");
            _listener.Listen(10);
            Logger.Log("Listening...");
        }

        public int Run()
        {
            _receiverThread = new Thread(ReceiveThread);

            _receiverThread.IsBackground = true;
            
            _receiverThread.Start();
            
            while (_isRunning)
            {
                allDone.Reset();

                _listener.BeginAccept(new AsyncCallback(Accept), null);
                allDone.WaitOne();
            }
            
            Dispose();

            return 0;
        }

        private void Accept(IAsyncResult ar)
        {
            _clients.Add(_listener.EndAccept(ar));
        }

        private void Receive(IAsyncResult ar)
        {
            String content = String.Empty;

            StateObject state = (StateObject) ar.AsyncState;

            Socket client = state.Client;

            int bytesRead = client.EndReceive(ar);

            state.StringBuilder.Append(Encoding.UTF8.GetString(state.Buffer, 0, bytesRead));

            content = state.StringBuilder.ToString();

            if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
            {
                content = content.Remove(content.IndexOf("<EOF>", StringComparison.Ordinal));
                
                _commandHandler.RunCommand(content);
                
                string tmpContent = _commandHandler.Result;

                Console.WriteLine(content);

                if (content.StartsWith("/"))
                {
                    content = tmpContent;
                }
                
                Console.WriteLine("Read: " + content);

                foreach (Socket socket in _clients)
                {
                    Send(socket, content);
                }
            }
            else
            {
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(Receive), state);
            }
        }

        private void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.UTF8.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket) ar.AsyncState;

                int bytesSent = client.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ReceiveThread(object args)
        {
            while (true)
            {
                foreach (Socket client in _clients)
                {
                    if (client.Available > 0)
                    {
                        StateObject state = new StateObject();

                        state.Client = client;

                        client.BeginReceive(state.Buffer,
                            0,
                            StateObject.BufferSize,
                            0,
                            new AsyncCallback(Receive),
                            state);
                    }
                }
            }
        }

        private void LoadPlugins(string path)
        {
            _plugins = PluginLoader.LoadPlugins(path);
        }
    }
}