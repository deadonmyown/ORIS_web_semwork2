using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using TCPServerNET6._0.Game;
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
                    ProcessHandshake(packet);
                    break;
                case XPacketType.PlayerTest:
                    ProcessPlayerTest(packet);
                    break;
                case XPacketType.PlayerInput:
                    ProcessInput(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ProcessHandshake(XPacket packet)
        {
            Console.WriteLine("Recieved handshake packet.");

            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);
            handshake.MagicHandshakeNumber -= 15;

            Console.WriteLine("Answering..");

            QueuePacketSend(XPacketConverter.Serialize(XPacketType.Handshake, handshake).ToPacket());
        }

        private void ProcessPlayerTest(XPacket packet)
        {
            Console.WriteLine("Received player packet.");

            var playerTest = XPacketConverter.Deserialize<XPacketPlayerTest>(packet);

            playerTest.PosX = playerTest.PosX + playerTest.Speed * Math.Cos(playerTest.Rotation * Math.PI / 180) * playerTest.DeltaTime;
            playerTest.PosY = playerTest.PosY + playerTest.Speed * Math.Sin(playerTest.Rotation * Math.PI / 180) * playerTest.DeltaTime;

            Console.WriteLine("Answering...");

            SendPacketsToAll(XPacketConverter.Serialize(XPacketType.PlayerTest, playerTest).ToPacket());
        }

        private void ProcessInput(XPacket packet)
        {
            Console.WriteLine("Get input");

            var input = XPacketConverter.Deserialize<XPacketPlayerInput>(packet);

            PlaneDirection direction = PlaneDirection.Forward; 
            float speed = input.Speed;

            if (input.isAdown) direction = PlaneDirection.Down;
            if (input.isDdown) direction = PlaneDirection.Up;
            if (input.isWdown) speed += GameManager.SpeedBoost;
            if (input.isSdown) speed -= GameManager.SpeedBoost;

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