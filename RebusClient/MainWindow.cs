using System;
using System.Net;
using System.Threading;
using Gtk;
using RebusCore.Client;

namespace RebusClient
{
    public class MainWindow : Window
    {
        private Fixed _container;
        private Button _send;
        private Label _output;
        private Entry _input;

        private Thread _clientThread;

        private Client _client;
        
        public MainWindow(string title) : base(title)
        {
            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);
            Resizable = false;
            //SetIconFromFile("icon.png");

            DeleteEvent += OnDelete;
            
            _container = new Fixed();
            
            _send = new Button("Button");
            _send.Label = "Send";
            
            _send.Clicked += OnSendClicked;
            
            _output = new Label("");
            _output.SetSizeRequest(650, 400);
            
            _input = new Entry();
            _input.SetSizeRequest(600, 25);

            _container.Put(_input, 50, 550);
            _container.Put(_output, 50, 50);
            _container.Put(_send, 700, 550);
            
            
            Add(_container);
            
            
            _client = new Client();
            
            _client.Connect(new IPEndPoint(IPAddress.Loopback, 1337));
            
            _client.OnDataReceived += OnReceived;
            _clientThread = new Thread(ClientThread);
            
            _clientThread.Start();
            
            ShowAll();
        }

        private void OnSendClicked(object sender, EventArgs e)
        {
            _client.Send(_input.Text + "<EOF>");
        }

        private void OnDelete(object sender, DeleteEventArgs args)
        {
            _clientThread.Interrupt();
            
            Application.Quit();
        }

        private void ClientThread(object args)
        {
            _client.Initialize();
        }

        private void OnReceived(object sender, string message)
        {
            _output.Text += message += "\r\n";
        }
    }
}