using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace TCPServer
{
    public class XServer
    {
        public int MaxPlayers { get; private set; }
        public int Port { get; private set; }

        private readonly Socket _socket;
        private readonly Dictionary<int, ConnectedClient> _clients;

        private bool _listening;
        private bool _stopListening;

        public XServer()
        {
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
            var ipAddress = ipHostInfo.AddressList[0];

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new Dictionary<int, ConnectedClient>();
        }

        public void Start(int maxPlayer, int port)
        {
            if (_listening)
            {
                throw new Exception("Server is already listening incoming requests.");
            }

            MaxPlayers= maxPlayer;
            Port= port;

            Console.WriteLine("Start server");

            _socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            _socket.Listen(10);

            _listening = true;
        }

        public void Stop()
        {
            if (!_listening)
            {
                throw new Exception("Server is already not listening incoming requests.");
            }

            Console.WriteLine("Stop server");

            _stopListening = true;
            _socket.Shutdown(SocketShutdown.Both);
            _listening = false;
        }

        public void AcceptClients()
        {
            while (true)
            {
                if (_stopListening)
                {
                    return;
                }

                Socket client;

                try
                {
                    client = _socket.Accept();
                } catch { return; }

                Console.WriteLine($"[!] Accepted client from {(IPEndPoint) client.RemoteEndPoint}");

                var c = new ConnectedClient(client);
                for (int i = 1; i <= MaxPlayers; i++)
                {
                    if (!_clients.ContainsKey(i) || !_clients.TryGetValue(i, out _))
                    {
                        _clients.Add(i, c);
                        break;
                    }
                }
            }
        }
    }
}
