using System.Drawing;

namespace ClassLibrary
{
    public struct SceneStruct
    {
        public float GravityValue { get; set; }
        public float AirResistance { get; set; }
        //public HashSet<Bullet> Bullets { get; set; }
        public Rectangle House { get; set; }
        public Rectangle Ground { get; set; }
        public int GroundHeigth { get; set; }
    }
}