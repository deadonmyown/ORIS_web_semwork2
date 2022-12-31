using System.Drawing;

namespace TCPServer.Game
{
    public static class GameManager
    {
        private const int ScaleRatio = 2;
        public static Size ModelSize => new Size(52 * ScaleRatio, 22 * ScaleRatio);


        public static float SpeedBoost { get; set; }
        public static float MaxSpeed { get; set; }
        public static float AngleChange { get; set; }
        public static int ReloadTime { get; set; }
    }
}
