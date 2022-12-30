using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketPlayerInput
    {
        [XField(1)]
        public bool isAdown;
        [XField(2)]
        public bool isDdown;
        [XField(3)]
        public bool isWdown;
        [XField(4)]
        public bool isSdown;
        [XField(5)]
        public float Speed;
        [XField(6)]
        public int Id;

        public XPacketPlayerInput(float speed, int id, params bool[] inputs)
        {
            Speed = speed;
            Id = id;
            isAdown = inputs[0];
            isDdown = inputs[1];
            isWdown = inputs[2];
            isSdown = inputs[3];
        }

        public XPacketPlayerInput() { }
    }
}