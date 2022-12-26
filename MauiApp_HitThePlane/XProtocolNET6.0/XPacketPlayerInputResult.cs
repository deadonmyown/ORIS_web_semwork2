using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketPlayerInputResult
    {
        [XField(1)]
        public int Rotation;
        [XField(2)]
        public float Speed;
        [XField(3)]
        public int Id;

        public XPacketPlayerInputResult(int rotation, float speed, int id)
        {
            Rotation = rotation;
            Speed = speed;
            Id = id;
        }   
    }
}
