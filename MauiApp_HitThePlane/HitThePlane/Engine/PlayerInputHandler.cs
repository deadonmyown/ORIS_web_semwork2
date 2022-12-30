using HitThePlane.Entities;

namespace HitThePlane.Engine
{
    public static class PlayerInputHandler
    {
        private static AirPlane _plane;

        public static void Bind(AirPlane plane)
        {
            _plane = plane;
        }

        private static bool isAdowm = false;
        private static bool isDdown = false;
        private static bool isWdowm = false;
        private static bool isSdown = false;
        private static bool mustShoot = false;

        public static void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isAdowm = true;
            if (e.KeyCode == Keys.D)
                isDdown = true;
            if (e.KeyCode == Keys.W)
                isWdowm = true;
            if (e.KeyCode == Keys.S)
                isSdown = true;
            if (e.KeyCode == Keys.K)
                mustShoot = true;
        }

        public static void KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
                isAdowm = false;
            if (e.KeyCode == Keys.D)
                isDdown = false;
            if (e.KeyCode == Keys.W)
                isWdowm = false;
            if (e.KeyCode == Keys.S)
                isSdown = false;
            if (e.KeyCode == Keys.K)
                mustShoot = false;
        }

        //не работает
        public static void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mustShoot = true;
        }

        public static void Apply()
        {
            if (isAdowm) _plane.Rotate(PlaneDirection.Up);
            if (isDdown) _plane.Rotate(PlaneDirection.Down);
            if (isWdowm) _plane.Speed += _plane.SpeedBoost;
            if (isSdown) _plane.Speed -= _plane.SpeedBoost;
            //TODO
            if (mustShoot) _plane.Shoot();
        }
    }
}
