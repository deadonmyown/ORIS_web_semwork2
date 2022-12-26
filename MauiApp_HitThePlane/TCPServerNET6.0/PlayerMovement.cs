using System.Drawing;
using System.Numerics;
using TCPServerNET6._0.Game;
using XProtocol;
using XProtocol.Serializator;

namespace TCPServer
{
    public class PlayerMovement
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

        public void Move(int formX)
        {
            Speed -= Scene.AirResistance;
            Rotate();
            Position += DisplacementVector + GravityVector;
            CheckBorders(formX);
            if (State == PlaneState.Takeoff && Position.Y < Scene.GroundHeigth - 50)
                State = PlaneState.Flight;

            SendMovement();
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


            if (Position.Y > Scene.GroundHeigth - GameManager.ModelSize.Height / 2)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, Scene.GroundHeigth - GameManager.ModelSize.Height / 2);
            }

            if (Collide(Scene.House))
                Destroy();

        }

        private void Destroy()
        {
            GravityValue = 0;
            Speed = 0;
            State = PlaneState.Destroyed;
        }

        private void SendMovement()
        {
            ConnectedClient.SendPacketsToAll(XPacketConverter.Serialize(XPacketType.PlayerMovement, 
                new XPacketPlayerMovement(Position, DirectionAngle, Speed, (int)State, (int)Direction, GravityValue)).ToPacket());
        }
    }
}
