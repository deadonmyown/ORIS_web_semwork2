using System;
using System.Threading;
using XProtocol;
using XProtocol.Serializator;

namespace TCPClient
{
    internal class Program
    {
        private static int _handshakeMagic;

        private static void Main()
        {
            Console.Title = "XClient";
            Console.ForegroundColor = ConsoleColor.White;
            
            var client = new XClient();
            client.OnPacketRecieve += OnPacketRecieve;
            client.Connect("127.0.0.1", 4910);

            /*var rand = new Random();
            _handshakeMagic = rand.Next();*/

            Thread.Sleep(1000);
            
            /*Console.WriteLine("Sending handshake packet..");

            client.QueuePacketSend(
                XPacketConverter.Serialize(
                    XPacketType.Handshake,
                    new XPacketHandshake
                    {
                        MagicHandshakeNumber = _handshakeMagic
                    })
                    .ToPacket());*/

            client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Player, new XPacketPlayer { PosX = 0}).ToPacket());

            while(true) {}
        }

        private static void OnPacketRecieve(byte[] packet)
        {
            var parsed = XPacket.Parse(packet);

            if (parsed != null)
            {
                ProcessIncomingPacket(parsed);
            }
        }

        private static void ProcessIncomingPacket(XPacket packet)
        {
            var type = XPacketTypeManager.GetTypeFromPacket(packet);

            switch (type)
            {
                case XPacketType.Handshake:
                    ProcessHandshake(packet);
                    break;
                case XPacketType.Player:
                    ProcessPlayer(packet);
                    break;
                case XPacketType.Unknown:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void ProcessPlayer(XPacket packet)
        {
            var player = XPacketConverter.Deserialize<XPacketPlayer>(packet);

            Console.WriteLine("Get player packet");
            if(player.PosX != 0)
                Console.WriteLine("player move");
        }

        private static void ProcessHandshake(XPacket packet)
        {
            var handshake = XPacketConverter.Deserialize<XPacketHandshake>(packet);

            if (_handshakeMagic - handshake.MagicHandshakeNumber == 15)
            {
                Console.WriteLine("Handshake successful!");
            }
        }
    }
}
