using System.Drawing;

namespace ClassLibrary
{
    public struct LevelStruct
    {
        public int GroundHeight { get; set; }
        public float GravityValue { get; set; }
        public float AirResistance { get; set; }
        public Collider House { get; set; }
        public Collider Ground { get; set; }
    }

    public enum PlaneDirection
    {
        Up, Forward, Down
    }

    public enum PlaneState
    {
        Takeoff, Flight, Destroyed
    }
}