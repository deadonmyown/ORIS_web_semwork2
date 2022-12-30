using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPServer.Game;
using XProtocol;
using XProtocol.Serializator;

namespace TCPServer
{
    public class NetworkManager
    {
        private static readonly Lazy<NetworkManager> lazy =
        new Lazy<NetworkManager>(() => new NetworkManager());

        public static NetworkManager Instance => lazy.Value;

        private NetworkManager()
        {
        }

        public XServer Server { get; private set; }

        public void Start(int maxPlayer, int port)
        {
            Server = new XServer();
            Server.Start(maxPlayer, port);
            Server.AcceptClients();
        }

        public void PlayerDisconnect(int id)
        {
            if (Player.Players.TryGetValue(id, out Player player))
            {
                foreach(var (clientId, client) in XServer.Clients)
                    client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.PlayerDisconnect, new XPacketPlayerDisconnect() { Id = id}).ToPacket());
                Console.WriteLine($"{player} has disconnected from the server");
                Player.OnDestroy(id);
                XServer.ClientDisconnect(id);
            }
        }

    }
}
