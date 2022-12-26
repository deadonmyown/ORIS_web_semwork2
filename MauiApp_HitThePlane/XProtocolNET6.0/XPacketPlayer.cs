using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketPlayer
    {
        [XField(1)]
        public int Id;
        [XField(2)]
        public string Name;
        [XField(3)]
        public Vector2 Position;

        public XPacketPlayer(int id, string name, Vector2 position)
        {
            Id = id;
            Name = name;
            Position = position;
        }

        public XPacketPlayer() { }
    }
}
