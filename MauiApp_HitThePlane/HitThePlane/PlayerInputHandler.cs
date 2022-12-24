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

        public static void KeyboardPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Plane.Rotate(PlaneDirection.Down);
                    break;

                case Keys.D:
                    Plane.Rotate(PlaneDirection.Up);
                    break;

                case Keys.W:
                    Plane.Speed += Plane.SpeedBoost;
                    break;

                case Keys.S:
                    Plane.Speed -= Plane.SpeedBoost;
                    break;

                case Keys.Space:
                    Plane.Shoot();
                    break;
            }
        }
    }
}
