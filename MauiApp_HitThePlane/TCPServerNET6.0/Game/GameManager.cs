using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TCPServerNET6._0.Game
{
    public enum PlaneDirection
    {
        Up, Forward, Down
    }

    public enum PlaneState
    {
        Takeoff, Flight, Destroyed
    }

    public static class GameManager
    {
        private const int ScaleRatio = 2;
        public static Size ModelSize => new Size(52 * ScaleRatio, 22 * ScaleRatio);


        public static float SpeedBoost { get; set; }
        public static float MaxSpeed { get; set; }
        public static float AngleChange { get; set; }
    }
}
