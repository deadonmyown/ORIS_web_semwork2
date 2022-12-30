using System.Drawing;
using System.Numerics;
using TCPServer.Game;
using XProtocol;
using XProtocol.Serializator;
using ClassLibrary;

namespace TCPServer
{
    public class PlayerController
    {
        public Vector2 Position;

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

        private Vector2 DisplacementVector =>
            new Vector2((float)(Speed * Math.Cos(DirectionAngle * Math.PI / 180)),
                    Speed * (float)(Math.Sin(DirectionAngle * Math.PI / 180)));

        public float GravityValue;

        private Vector2 GravityVector =>
            new Vector2(0, GravityValue * (Speed < 1 ? 1 : 1 / (float)Math.Sqrt(Speed)));

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
                    if (value > _directionAngle || _speed < GameManager.SpeedBoost * 20)
                        return;
                }
                _directionAngle = value;
            }
        }

        public PlaneDirection Direction { get; set; }

        public PlaneState State { get; private set; } = PlaneState.Takeoff;

        public Rectangle BoundingRectangle =>
            new Rectangle((int)Position.X - GameManager.ModelSize.Width / 2, (int)Position.Y - GameManager.ModelSize.Height / 2, GameManager.ModelSize.Width, GameManager.ModelSize.Height);

        protected bool Collide(Rectangle rect) =>
            BoundingRectangle.IntersectsWith(rect);

        private LevelStruct _level;

        public PlayerController(Vector2 position, float speed, float gravityValue, float directionAngle, PlaneState state, PlaneDirection direction, LevelStruct scene)
        {
            Position = position;
            _speed = speed;
            GravityValue = gravityValue;
            _directionAngle = directionAngle;
            State = state;
            Direction = direction;
            _level = scene;
        }

        public void Move(int formX)
        {
            Speed -= _level.AirResistance;
            Rotate();
            Position += DisplacementVector + GravityVector;
            CheckBorders(formX);
            if (State == PlaneState.Takeoff && Position.Y < _level.GroundHeight - 50)
                State = PlaneState.Flight;

            //SendMovement();
        }

        private void Rotate()
        {
            if (Direction == PlaneDirection.Up)
                DirectionAngle += GameManager.AngleChange;
            else if (Direction == PlaneDirection.Down)
                DirectionAngle -= GameManager.AngleChange;
        }

        private void CheckBorders(int formX)
        {
            if (Position.X < 0)
                Position = new Vector2(formX, Position.Y);
            if (Position.X > formX)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 20)
                Speed -= GameManager.SpeedBoost * 4;


            if (Position.Y > _level.GroundHeight - GameManager.ModelSize.Height / 2)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, _level.GroundHeight - GameManager.ModelSize.Height / 2);
            }

            /*if (Collide(_level.House))
                Destroy();*/

        }

        private void Destroy()
        {
            GravityValue = 0;
            Speed = 0;
            State = PlaneState.Destroyed;
        }

        /*private void SendMovement()
        {
            ConnectedClient.SendPacketsToAll(XPacketConverter.Serialize(XPacketType.PlayerMovement, 
                new XPacketPlayerMovement(Position, DirectionAngle, Speed, (int)State, (int)Direction, GravityValue)).ToPacket());
        }*/
    }
}
