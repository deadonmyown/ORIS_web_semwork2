using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPClient;
using TCPServer;
using XProtocol.Serializator;
using XProtocol;
using System.Net.Sockets;
using System.Numerics;

namespace HitThePlane.Entities
{
    public class NetworkManager
    {
        private static readonly Lazy<NetworkManager> lazy =
        new Lazy<NetworkManager>(() => new NetworkManager());

        public static NetworkManager Instance => lazy.Value;

        private NetworkManager()
        {
        }

        public XClient Client { get; private set; }

        public Player Player { get; set; }

        public void Start(int port)
        {
            Client = new XClient();
            Client.OnPacketRecieve += OnPacketRecieve;
            Client.Connect("127.0.0.1", port);
        }

        private void OnPacketRecieve(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case XPacketType.Player:
                    ProcessPlayerSpawn(packet);
                    break;
                case XPacketType.PlayerController:
                    ProcessPlayerMovement(packet);
                    break;
                case XPacketType.PlayerInputResult:
                    ProcessPlayerInputGet(packet);
                    break;
                case XPacketType.PlayerDisconnect:
                    ProcessPlayerDisconnect(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessHandshake(XPacket packet)
        {
            var playerConnection = XPacketConverter.Deserialize<XPacketHandshake>(packet);

            Client.Id = playerConnection.Id;
            Console.WriteLine($"player with id = {playerConnection.Id} connected");

        }

        private void ProcessPlayerSpawn(XPacket packet)
        {
            var playerSpawn = XPacketConverter.Deserialize<XPacketPlayer>(packet);

            Player.Spawn(playerSpawn.Id, playerSpawn.Level, playerSpawn.Position, playerSpawn.Name.ToString());
        }

        private void ProcessPlayerMovement(XPacket packet)
        {
            var playerMovement = XPacketConverter.Deserialize<XPacketPlayerController>(packet);

            if (Player.Players[playerMovement.Id].IsLocal)
            {
                Player.Plane.Move(playerMovement);
            }
            Player.Players[playerMovement.Id].Plane.Move(playerMovement);
        }

        private void ProcessPlayerInputGet(XPacket packet)
        {
            var playerInputRes = XPacketConverter.Deserialize<XPacketPlayerInputResult>(packet);

            if (Player.Players[playerInputRes.Id].IsLocal)
            {
                Player.Plane.GetInputResult(playerInputRes);
            }
            Player.Players[playerInputRes.Id].Plane.GetInputResult(playerInputRes);
        }

        private void ProcessPlayerDisconnect(XPacket packet)
        {
            var disc = XPacketConverter.Deserialize<XPacketPlayerDisconnect>(packet);
            PlayerDisconnect(disc.Id);
        }

        public void PlayerDisconnect(int id)
        {
            if (Player.Players.TryGetValue(id, out Player player))
            {
                Console.WriteLine($"{player} has disconnected from the server");
                Player.OnDestroy(id);

                if (id == Player.Id)
                    Player = null;
            }
        }

    }
}
