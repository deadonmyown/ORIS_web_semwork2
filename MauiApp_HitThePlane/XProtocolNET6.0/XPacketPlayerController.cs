using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using XProtocol.Serializator;
using ClassLibrary;

namespace XProtocol
{
    public class XPacketPlayerController
    {
        [XField(1)]
        public Vector2 Position;
        [XField(2)]
        public float DirectionAngle;
        [XField(3)]
        public float Speed;
        [XField(4)]
        public int State;
        [XField(5)]
        public int Direction;
        [XField(6)]
        public int Id;
        [XField(7)]
        public int Width;
        [XField(8)]
        public int Height;
        [XField(9)]
        public LevelStruct Level;

        public XPacketPlayerController() { }

        public XPacketPlayerController(Vector2 position, float directionAngle, float speed, int state, int direction, int id, int width, int height, LevelStruct scene)
        {
            Position = position;
            DirectionAngle = directionAngle;
            Speed = speed;
            State = state;
            Direction = direction;
            Id = id;
            Width = width;
            Height = height;
            Level = scene;
        }
    }
}
