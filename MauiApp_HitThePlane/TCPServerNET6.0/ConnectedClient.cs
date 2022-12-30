using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using TCPServer.Game;
using XProtocol;
using XProtocol.Serializator;

namespace TCPServer
{
    public class ConnectedClient
    {
        public Socket Client { get; }

        private readonly Queue<byte[]> _packetSendingQueue = new Queue<byte[]>();

        public ConnectedClient(Socket client)
        {
            Client = client;

            Task.Run((Action)ProcessIncomingPackets);
            Task.Run((Action)SendPackets);
        }

        private void ProcessIncomingPackets()
        {
            while (true) // Слушаем пакеты, пока клиент не отключится.
            {
                var buff = new byte[256]; // Максимальный размер пакета - 256 байт.
                Client.Receive(buff);

                buff = buff.TakeWhile((b, i) =>
                {
                    if (b != 0xFF) return true;
                    return buff[i + 1] != 0;
                }).Concat(new byte[] { 0xFF, 0 }).ToArray();

                var parsed = XPacket.Parse(buff);

                if (parsed != null)
                {
                    ProcessIncomingPacket(parsed);
                }
            }
        }

        private void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.Handshake:
                    ProcessPlayerHandshake(packet);
                    break;
                case XPacketType.Player:
                    ProcessPlayerSpawn(packet);
                    break;
                case XPacketType.PlayerController:
                    ProcessPlayerMovement(packet);
                    break;
                case XPacketType.PlayerInput:
                    ProcessPlayerInput(packet);
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

        private void ProcessPlayerHandshake(XPacket packet)
        {
            Console.WriteLine("cho");
        }

        private void ProcessPlayerDisconnect(XPacket packet)
        {
            var disconnect = XPacketConverter.Deserialize<XPacketPlayerDisconnect>(packet);

            NetworkManager.Instance.PlayerDisconnect(disconnect.Id);
        }

        private void ProcessPlayerSpawn(XPacket packet)
        {
            var player = XPacketConverter.Deserialize<XPacketPlayer>(packet);

            Player.Spawn(player.Id, player.Position, player.Level);
        }

        private void ProcessPlayerMovement(XPacket packet)
        {
            var playerMove = XPacketConverter.Deserialize<XPacketPlayerController>(packet);

            PlayerController move = new PlayerController(playerMove.Position, playerMove.Speed, playerMove.GravityValue, playerMove.DirectionAngle, (PlaneState)playerMove.State, (PlaneDirection)playerMove.Direction, playerMove.Scene);
            Console.WriteLine($"player start moving: {move.Position} {move.GravityValue}");

            move.Move(playerMove.FormX);

            Console.WriteLine($"player moved: {move.Position} {move.GravityValue}");

            SendPacketsToAll(XPacketConverter.Serialize(XPacketType.PlayerController, 
                new XPacketPlayerController(move.Position, move.DirectionAngle, move.Speed, 
                (int)move.State, (int)move.Direction, move.GravityValue, playerMove.Id, playerMove.FormX, playerMove.Scene)).ToPacket()); 
        }

        private void ProcessPlayerInput(XPacket packet)
        {

            var input = XPacketConverter.Deserialize<XPacketPlayerInput>(packet);

            PlaneDirection direction = PlaneDirection.Forward; 
            float speed = input.Speed;
            Console.WriteLine($"Get input: {input.Id} {input.Speed}, A:{input.isAdown}, D:{input.isDdown}, W:{input.isWdown}, S:{input.isSdown}");

            if (input.isAdown) direction = PlaneDirection.Down;
            if (input.isDdown) direction = PlaneDirection.Up;
            if (input.isWdown) speed += GameManager.SpeedBoost;
            if (input.isSdown) speed -= GameManager.SpeedBoost;

            Console.WriteLine($"Speed: {speed}, Direction:{direction}");

            SendPacketsToClient(XPacketConverter.Serialize(XPacketType.PlayerInputResult, new XPacketPlayerInputResult((int)direction, speed, input.Id)).ToPacket(), input.Id);
        }

        public void QueuePacketSend(byte[] packet)
        {
            if (packet.Length > 256)
            {
                throw new Exception("Max packet size is 256 bytes.");
            }

            _packetSendingQueue.Enqueue(packet);
        }

        private void SendPackets()
        {
            while (true)
            {
                if (_packetSendingQueue.Count == 0)
                {
                    Thread.Sleep(10);
                    continue;
                }

                var packet = _packetSendingQueue.Dequeue();
                Client.Send(packet);

                Thread.Sleep(10);
            }
        }

        public static void SendPacketsToAll(byte[] packet)
        {
            foreach (var kvp in XServer.Clients)
            {
                var index = kvp.Key;
                var client = kvp.Value;
                client.Client.Send(packet);
            }
        }

        public static void SendPacketsToClient(byte[] packet, int id)
        {
            if (XServer.Clients.TryGetValue(id, out ConnectedClient client))
                client.Client.Send(packet);
        }
    }
}