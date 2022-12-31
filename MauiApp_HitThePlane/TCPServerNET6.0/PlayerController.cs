using System.Drawing;
using System.Numerics;
using TCPServer.Game;
using XProtocol;
using XProtocol.Serializator;
using ClassLibrary;
using System.Reflection;

namespace TCPServer
{
    public class PlayerController
    {
        private bool looksLeft;
        public bool Flip => looksLeft;

        public PlaneState State { get; private set; } = PlaneState.Takeoff;

        public int Health { get; private set; } = 100;

        private float _speed;

        public float Speed
        {
            get => _speed;
            set
            {
                if (value < 0 || State == PlaneState.Destroyed)
                    _speed = 0;
                else if (value > GameManager.MaxSpeed)
                    _speed = GameManager.MaxSpeed;
                else
                    _speed = value;
            }
        }

        public int ReloadCounter { get; private set; } = 0;

        private Vector2 DirectionVector =>
            new Vector2((float)Math.Cos(DirectionAngle * Math.PI / 180),
                    -(float)Math.Sin(DirectionAngle * Math.PI / 180));

        private Vector2 GravityVector =>
            new Vector2(0, _level.GravityValue * (Speed < 1 ? 1 : 1 / (float)Math.Sqrt(Speed)));

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
                    if (_speed < GameManager.SpeedBoost * 20) return;
                    if (looksLeft && value > _directionAngle || !looksLeft && value < _directionAngle)
                        return;
                }
                _directionAngle = value;
            }
        }

        public PlaneDirection Direction { get; set; }

        private LevelStruct _level;

        public Vector2 Position { get; set; }
        public Rectangle BoundingRectangle =>
            new Rectangle((int)Position.X - GameManager.ModelSize.Width / 2, (int)Position.Y - GameManager.ModelSize.Height / 2, GameManager.ModelSize.Width, GameManager.ModelSize.Height);

        public PlayerController(Vector2 position, float speed, float directionAngle, PlaneState state, PlaneDirection direction, LevelStruct scene, int reloadCounter, int health)
        {
            Position = position;
            _speed = speed;
            _directionAngle = directionAngle;
            State = state;
            Direction = direction;
            _level = scene;
            ReloadCounter = reloadCounter;
            Health = health;

            looksLeft = Math.Cos(directionAngle * Math.PI / 180) < 0;
        }

        public PlayerController(Vector2 position, float speed, float directionAngle, PlaneState state, PlaneDirection direction, LevelStruct scene)
        {
            Position = position;
            _speed = speed;
            _directionAngle = directionAngle;
            State = state;
            Direction = direction;
            _level = scene;

            looksLeft = Math.Cos(directionAngle * Math.PI / 180) < 0;
        }

        public void Move(int height, int width)
        {
            if (ReloadCounter > 0) ReloadCounter++;
            if (ReloadCounter >= GameManager.ReloadTime) ReloadCounter = 0;
            Speed -= _level.AirResistance;
            Position += Speed * DirectionVector + GravityVector;
            Rotate();
            CheckBorders(height, width);
            if (State == PlaneState.Takeoff && Position.Y < height - _level.GroundHeight - GameManager.ModelSize.Height)
                State = PlaneState.Flight;
        }

        private void Rotate()
        {
            if (Direction == PlaneDirection.Up)
                DirectionAngle += GameManager.AngleChange;
            else if (Direction == PlaneDirection.Down)
                DirectionAngle -= GameManager.AngleChange;
        }

        private void CheckBorders(int height, int width)
        {
            if (Position.X < 0)
                Position = new Vector2(width, Position.Y);
            if (Position.X > width)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 20)
                Speed -= GameManager.SpeedBoost * 4;


            if (Position.Y > height - _level.GroundHeight - GameManager.ModelSize.Height / 2 + 4)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, height - _level.GroundHeight - GameManager.ModelSize.Height / 2 + 4);
            }

            //if (Collide(_level.House))
            //    Destroy();
        }


        /*public void Shoot()
        {
            if (_reloadCounter != 0) return;
            Bullet.Create(_level, this);
            _reloadCounter++;
        }*/

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
                State = PlaneState.Destroyed;
        }

        private void Destroy()
        {
            Health = 0;
            State = PlaneState.Destroyed;
        }

        private bool Collide(Rectangle rect) =>
            BoundingRectangle.IntersectsWith(rect);
    }
}
