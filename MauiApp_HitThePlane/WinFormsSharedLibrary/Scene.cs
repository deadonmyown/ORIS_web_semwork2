using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsSharedLibrary
{
    public static class Scene
    {
        public static AirPlane MyPlane { get; set; }
        public static AirPlane EnemyPlane { get; set; }
        public static float GravityValue { get; set; }
        public static float AirResistance { get; set; }
        public static HashSet<Bullet> Bullets { get; set; }
        public static Rectangle House { get; set; }
        public static Rectangle Ground { get; set; }
        public static int GroundHeigth { get; set; }
    }
}
