using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XProtocol.Serializator;

namespace XProtocol
{
    public class XPacketPlayerTest
    {
        [XField(1)]
        public double PosX;
        [XField(2)]
        public double PosY;
        [XField(3)]
        public double Speed;
        [XField(4)]
        public double Rotation;
        [XField(5)]
        public double DeltaTime;
        [XField(6)]
        public int Index;
    }
}