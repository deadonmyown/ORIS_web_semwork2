using HitThePlane.Engine;
using System.Numerics;
using TCPClient;
using XProtocol.Serializator;
using XProtocol;
using ClassLibrary;

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

    public class AirPlane : DrawableObject
    {
        public static readonly Size _modelSize = new Size(52 * ScaleRatio, 22 * ScaleRatio);
        public override Size ModelSize => _modelSize;

        private bool looksLeft;
        public bool Flip => looksLeft;

        private int frameCounter = 0;
        private int turnCounter = 0;
        private string[] _screwsAnim;
        private string _turnFrame;
        public override string Sprite
        {
            get
            {
                if (turnCounter > 0)
                {
                    if (++turnCounter > 5) turnCounter = 0;
                    return _turnFrame;
                }
                var cos = Math.Cos(DirectionAngle * Math.PI / 180);
                if (looksLeft && cos > 0.9 || !looksLeft && cos < -0.9)
                {
                    looksLeft = !looksLeft;
                    turnCounter++;
                    return _turnFrame;
                }
                if (frameCounter >= _screwsAnim.Length) frameCounter = 0;
                return _screwsAnim[frameCounter++];
            }
        }

        public PlaneState State { get; private set; } = PlaneState.Takeoff;
        public int Health { get; private set; } = 100;

        public float SpeedBoost;
        private float _maxSpeed;
        private float _speed = 0;

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

        public float AngleChange { get; set; }
        private int ReloadTime { get; set; }
        private int _reloadCounter = 0;

        private Vector2 DirectionVector =>
            new Vector2((float)Math.Cos(DirectionAngle * Math.PI / 180),
                    -(float)Math.Sin(DirectionAngle * Math.PI / 180));

        private Vector2 GravityVector =>
            new Vector2(0, _level.GravityValue * (Speed < 1 ? 1 : 1 / (float)Math.Sqrt(Speed)));

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
                    if (_speed < SpeedBoost * 20) return;
                    if (looksLeft && value > _directionAngle || !looksLeft && value < _directionAngle)
                        return;
                }
                _directionAngle = value;
            }
        }

        public PlaneDirection Direction { get; set; }

        private LevelStruct _level;


        public AirPlane(LevelStruct level, Vector2 position, float speedBoost, float maxSpeed, float directionAngle, float angleChange, int reloadTime, string[] spriteSheet, string turnFrame)
        {
            _level = level;
            Position = position;
            SpeedBoost = speedBoost;
            _maxSpeed = maxSpeed;
            AngleChange = angleChange;
            _directionAngle = directionAngle;
            _screwsAnim = spriteSheet;
            _turnFrame = turnFrame;
            ReloadTime = reloadTime;

            looksLeft = Math.Cos(directionAngle * Math.PI / 180) < 0;
        }

        public void SendMove(XClient client, int formX)
        {
            client.QueuePacketSendUpdate(XPacketConverter.Serialize(XPacketType.PlayerController,
                new XPacketPlayerController(Position, DirectionAngle, Speed, (int)State, (int)Direction, _level.GravityValue, client.Id, formX, _level)).ToPacket()); ;
        }

        public void Move(XPacketPlayerController movement)
        {
            Position = movement.Position;
            DirectionAngle = movement.DirectionAngle;
            Speed = movement.Speed;
            State = (PlaneState)movement.State;
            Direction = (PlaneDirection)movement.Direction;
        }

        public void GetInputResult(XPacketPlayerInputResult res)
        {
            Speed = res.Speed;
            Direction = (PlaneDirection)res.Direction;
        }

        private void Rotate()
        {
            if (Direction == PlaneDirection.Up)
                DirectionAngle += AngleChange;
            else if (Direction == PlaneDirection.Down)
                DirectionAngle -= AngleChange;
        }


        //+
        public void Rotate(PlaneDirection dir)
        {
            if (dir == PlaneDirection.Up)
                DirectionAngle += AngleChange;
            else
                DirectionAngle -= AngleChange;
        }

        public void Move()
        {
            if (_reloadCounter > 0) _reloadCounter++;
            if (_reloadCounter >= ReloadTime) _reloadCounter = 0;
            Speed -= _level.AirResistance;
            Position += Speed * DirectionVector + GravityVector;
            CheckBorders();
            if (State == PlaneState.Takeoff && Position.Y < Render.Resolution.Height - _level.GroundHeight - ModelSize.Height)
                State = PlaneState.Flight;
        }

        private void CheckBorders()
        {
            if (Position.X < 0)
                Position = new Vector2(Render.Resolution.Width, Position.Y);
            if (Position.X > Render.Resolution.Width)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 20)
                Speed -= SpeedBoost * 4;


            if (Position.Y > Render.Resolution.Height - _level.GroundHeight - ModelSize.Height / 2 + 4)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, Render.Resolution.Height - _level.GroundHeight - ModelSize.Height / 2 + 4);
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
    }
}
