using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Start()
        {
            Server = new XServer();
            Server.Start(2, 4910);
        }

        public void PlayerDisconnect(int id)
        {
            if (Player.list.TryGetValue(id, out Player player))
            {
                Console.WriteLine($"{player.Name} has disconnected from the server");
                Player.OnDestroy(id);
                XServer.ClientDisconnect(id);
            }
        }

    }
}
