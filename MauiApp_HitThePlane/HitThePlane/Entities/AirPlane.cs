using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading.Tasks;

namespace HitThePlane.Entities
{
    public enum PlaneDirection
    {
        Up, Down
    }

    public enum PlaneState
    {
        Takeoff, Flight, Destroyed
    }

    public class AirPlane : GameObject
    {
        public static readonly Size _modelSize = new Size(52 * 2, 22 * 2);
        public override Size ModelSize => _modelSize;
        public override Image Sprite => _sprite;

        public PlaneState State { get; private set; } = PlaneState.Takeoff;
        public int Health { get; private set; }

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

        public Image _sprite { get; set; }

        private Vector2 DisplacementVector =>
            new Vector2((float)(Speed * Math.Cos(DirectionAngle * Math.PI / 180)),
                    Speed * (float)(Math.Sin(DirectionAngle * Math.PI / 180)));

        private Vector2 GravityVector =>
            new Vector2(0, Scene.GravityValue * (Speed < 1 ? 1 : 1 / Speed));

        private float _directionAngle;
        public override float DirectionAngle
        {
            get => _directionAngle;
            set
            {
                if (State == PlaneState.Destroyed)
                    return;
                if (State == PlaneState.Takeoff)
                {
                    if (value > _directionAngle)
                        return;
                }
                _directionAngle = value;
            }
        }


        public AirPlane(Vector2 position, int health, float speed, float speedBoost, float maxSpeed, float directionAngle, float angleChange, Image sprite)
        {
            Position = position;
            Health = health;
            _speed = speed;
            SpeedBoost = speedBoost;
            _maxSpeed = maxSpeed;
            _angleChange = angleChange;
            _directionAngle = directionAngle;
            _sprite = sprite;
        }

        public void Rotate(PlaneDirection dir)
        {
            if (dir == PlaneDirection.Up)
                DirectionAngle += _angleChange;
            else
                DirectionAngle -= _angleChange;
        }

        public void Move()
        {
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
                Speed -= SpeedBoost * 2;


            if (Position.Y > Scene.GroundHeigth - ModelSize.Height / 2)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, Scene.GroundHeigth - ModelSize.Height / 2);
            }

            if (Collide(Scene.House))
                Destroy();

        }

        public void Shoot()
        {
            Bullet.Create(this);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                State = PlaneState.Destroyed;
        }

        private void Destroy()
        {
            Scene.GravityValue = 0;
            Speed = 0;
            Health = 0;
            State = PlaneState.Destroyed;
        }
    }
}
