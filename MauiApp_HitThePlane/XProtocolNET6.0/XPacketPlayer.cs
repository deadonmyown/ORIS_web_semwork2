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
        [XField(3)]
        public SceneStruct Scene;

        public XPacketPlayer(int id, Vector2 position, SceneStruct scene)
        {
            Id = id;
            Position = position;
            Scene = scene;
        }

        public XPacketPlayer() { }
    }
}
