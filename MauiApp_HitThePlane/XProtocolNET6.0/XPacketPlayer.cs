using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using XProtocol.Serializator;
using ClassLibrary;

namespace XProtocol
{
    public class XPacketPlayer
    {
        [XField(1)]
        public int Id;
        [XField(2)]
        public Vector2 Position;
        [XField(4)]
        public LevelStruct Level;
        [XField(5)]
        public char Name;

        public XPacketPlayer(int id, Vector2 position, LevelStruct level, char name)
        {
            Id = id;
            Position = position;
            Level = level;
            Name = name;
        }

        public XPacketPlayer() { }
    }
}
