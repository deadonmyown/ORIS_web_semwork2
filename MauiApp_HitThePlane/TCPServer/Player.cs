using System;
using System.Collections.Generic;
using XProtocol.Serializator;
using XProtocol;
using System.Numerics;

namespace TCPServer
{
    public class Player
    {
        public static Dictionary<int, Player> list = new Dictionary<int, Player>();

        public int Id { get; set; }
        public string Name { get; set; }
        public Vector2 Position { get; set; }

        public Player(int id,string name, Vector2 position)
        {
            Id = id;
            Name = name;
            Position = position;
        }

        public static void Spawn(int id, string name, Vector2 position)
        {
            foreach (var otherPlayer in list.Values)
                otherPlayer.SendSpawned(id, otherPlayer);

            Player player = new Player(id, string.IsNullOrEmpty(name) ? $"anon {id}": name, position);

            player.SendSpawned();
            list.Add(id, player);
        }

        public static void OnDestroy(int id) => list.Remove(id);

        private void SendSpawned()
        {
            ConnectedClient.SendPacketsToAll(XPacketConverter.Serialize(XPacketType.Player, new XPacketPlayer(Id, Name, Position)).ToPacket());
        }

        private void SendSpawned(int currPlayer, Player player)
        {
            ConnectedClient.SendPacketsToClient(XPacketConverter.Serialize(XPacketType.Player, new XPacketPlayer(player.Id, player.Name, player.Position)).ToPacket(), currPlayer);
        }
    }
}
