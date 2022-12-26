using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketPlayerMovement
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
        public float GravityValue;

        public XPacketPlayerMovement(Vector2 position, float directionAngle, float speed, int state, int direction, float gravityValue)
        {
            Position = position;
            DirectionAngle = directionAngle;
            Speed = speed;
            State = state;
            Direction = direction;
            GravityValue = gravityValue;
        }
    }
}
