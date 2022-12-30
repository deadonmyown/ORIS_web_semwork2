using System;
using System.Collections.Generic;
using XProtocol.Serializator;
using XProtocol;
using System.Numerics;
using ClassLibrary;

namespace TCPServer
{
    public class Player
    {
        public static Dictionary<int, Player> Players = new Dictionary<int, Player>();

        public int Id { get; set; }
        public Vector2 Position { get; set; }

        public PlayerController Movement { get; set; }

        public Player(int id, Vector2 position)
        {
            Id = id;
            Position = position;
        }

        public Player(int id,  Vector2 position, PlayerController movement)
        {
            Id = id;
            Position = position;
            Movement = movement;
        }

        public static void Spawn(int id, Vector2 position, LevelStruct scene)
        {
            foreach (var otherPlayer in Players.Values)
                otherPlayer.SendSpawned(id, otherPlayer, scene);

            Player player = new Player(id, position);

            player.SendSpawned(scene);
            Players.Add(id, player);
        }

        public static void OnDestroy(int id) => Players.Remove(id);

        private void SendSpawned(LevelStruct scene)
        {
            ConnectedClient.SendPacketsToAll(XPacketConverter.Serialize(XPacketType.Player, new XPacketPlayer(Id, Position, scene)).ToPacket());
        }

        private void SendSpawned(int currPlayer, Player player, LevelStruct scene)
        {
            ConnectedClient.SendPacketsToClient(XPacketConverter.Serialize(XPacketType.Player, new XPacketPlayer(player.Id, player.Position, scene)).ToPacket(), currPlayer);
        }
    }
}
