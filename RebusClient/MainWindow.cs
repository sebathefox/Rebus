using System;
using System.IO;
using System.Net;
using System.Threading;
using Gdk;
using Gtk;
using RebusCore.Client;
using Window = Gtk.Window;

namespace RebusClient
{
    public class MainWindow : Window
    {
        #region Fields
        
        private Fixed _container;
        private Button _send;
        private Label _output;
        private Entry _input;

        private Button _connect;
        private Entry _connString;

        private byte[] _buffer;

        private Pixbuf _pixbuf;
        
        private Image _icon;

        private Thread _clientThread;

        private Client _client;
        
        #endregion
        
        public MainWindow(string title) : base(title)
        {
            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);
            Resizable = false;
            SetIconFromFile("icon.png");

            DeleteEvent += OnDelete;
            
            _container = new Fixed();
            
            _send = new Button("Button");
            _send.Label = "Send";
            
            _send.Clicked += OnSendClicked;
            
            _output = new Label("");
            _output.SetSizeRequest(650, 400);
            _output.LineWrap = true;
            _output.Justify = Justification.Left;

            _input = new Entry();
            _input.SetSizeRequest(600, 25);

            _buffer = File.ReadAllBytes("icon.png");
            
            _pixbuf = new Pixbuf(_buffer, 64, 64);

            _icon = new Image(_pixbuf);
            _icon.SetSizeRequest(64, 64);

            _connect = new Button(_icon);
            _connect.SetSizeRequest(64, 64);
            

            _connect.Clicked += Connect;
            
            _container.Put(_connect, 0, 0);
            
            _connString = new Entry();
            
            _container.Put(_connString, 192, 0);
            
            _container.Put(_icon, 0, 0);
            
            _container.Put(_input, 50, 550);
            _container.Put(_output, 50, 50);
            _container.Put(_send, 700, 550);
            
            
            Add(_container);
            
            
            _client = new Client();
            
            ShowAll();
        }

        private void Connect(object sender, EventArgs e)
        {
            //TODO: Make it dynamically connect to a server through an interface.
            _client.Connect(new IPEndPoint(IPAddress.Parse(_connString.Text), 1337));
            
            _client.OnDataReceived += OnReceived;
            _clientThread = new Thread(ClientThread);
            
            _clientThread.Start();
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            if (!_client.Send(_input.Text + "<EOF>"))
            {
                _output.Text += "ERROR: COULD NOT SEND TO SERVER!\n";
            }
        }

        private void OnDelete(object sender, DeleteEventArgs args)
        {
//            _clientThread.Interrupt();
            _clientThread.Abort();
            _client.Disconnect();
            _client.Dispose();
            
            Application.Quit();
        }

        private void ClientThread(object args)
        {
            _client.Initialize();
        }

        private void OnReceived(object sender, string message)
        {
            _output.Text += message += "\n";
        }
    }
}