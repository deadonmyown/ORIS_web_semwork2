using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServerNET6._0.Game
{
    public static class Scene
    {
        public static float GravityValue { get; set; }
        public static float AirResistance { get; set; }
        public static HashSet<Bullet> Bullets { get; set; }
        public static Rectangle House { get; set; }
        public static Rectangle Ground { get; set; }
        public static int GroundHeigth { get; set; }

        public static void Initialize(float gravityValue, float airResistance, HashSet<Bullet> bullets, Rectangle house, Rectangle ground, int groundHeigth)
        {
            GravityValue = gravityValue;
            AirResistance = airResistance;
            Bullets = bullets;
            House = house;
            Ground = ground;
            GroundHeigth = groundHeigth;
        }
    }
}
