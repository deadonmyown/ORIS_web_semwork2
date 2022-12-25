using HitThePlane.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HitThePlane
{
    public static class PlayerInputHandler
    {
        private static AirPlane Plane => Scene.MyPlane;

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
        }

        public static void MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mustShoot = true;
        }

        public static void Apply()
        {
            if (isAdowm) Plane.Rotate(PlaneDirection.Down);
            if (isDdown) Plane.Rotate(PlaneDirection.Up);
            if (isWdowm) Plane.Speed += Plane.SpeedBoost;
            if (isSdown) Plane.Speed -= Plane.SpeedBoost;
            if (mustShoot)
            {
                Plane.Shoot();
                mustShoot = false;
            }
        }
    }
}
