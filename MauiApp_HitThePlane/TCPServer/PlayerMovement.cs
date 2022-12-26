using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TCPServer
{
    public class PlayerMovement
    {
        /*public PlaneState State { get; private set; } = PlaneState.Takeoff;

        public float SpeedBoost;
        private float _maxSpeed;
        private float _speed;

        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0 || State == PlaneState.Destroyed)
                    _speed = 0;
                else if (value > _maxSpeed)
                    _speed = _maxSpeed;
                else
                    _speed = value;
            }
        }

        public float _angleChange { get; set; }


        private Vector2 DisplacementVector =>
            new Vector2((float)(Speed * Math.Cos(DirectionAngle * Math.PI / 180)),
                    Speed * (float)(Math.Sin(DirectionAngle * Math.PI / 180)));

        private float _gravityValue;

        private Vector2 GravityVector =>
            new Vector2(0, _gravityValue * (Speed < 1 ? 1 : 1 / (float)Math.Sqrt(Speed)));

        private float _directionAngle;
        public float DirectionAngle
        {
            get => _directionAngle;
            set
            {
                if (State == PlaneState.Destroyed)
                    return;
                if (State == PlaneState.Takeoff)
                {
                    if (value > _directionAngle || _speed < SpeedBoost * 20)
                        return;
                }
                _directionAngle = value;
            }
        }

        public PlaneDirection Direction { get; set; }

        public void Rotate()
        {
            if (Direction == PlaneDirection.Up)
                DirectionAngle += _angleChange;
            else if (Direction == PlaneDirection.Down)
                DirectionAngle -= _angleChange;
        }

        public void Move()
        {
            Speed -= Scene.AirResistance;
            Rotate();
            Position += DisplacementVector + GravityVector;
            CheckBorders();
            if (State == PlaneState.Takeoff && Position.Y < Scene.GroundHeigth - 50)
                State = PlaneState.Flight;
        }

        private void CheckBorders()
        {
            if (Position.X < 0)
                Position = new Vector2(Form1.defaultWidth, Position.Y);
            if (Position.X > Form1.defaultWidth)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 20)
                Speed -= SpeedBoost * 4;


            if (Position.Y > Scene.GroundHeigth - ModelSize.Height / 2)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, Scene.GroundHeigth - ModelSize.Height / 2);
            }

            if (Collide(Scene.House))
                Destroy();

        }

        private void Destroy()
        {
            _gravityValue = 0;
            Speed = 0;
            Health = 0;
            State = PlaneState.Destroyed;
        }
*/

        /*public static void Move(AirPlane airPlane)
        {

        }*/
    }
}
