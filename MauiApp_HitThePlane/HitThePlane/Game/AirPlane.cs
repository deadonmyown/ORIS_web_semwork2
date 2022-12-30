using System.Numerics;
using TCPClient;
using XProtocol;
using XProtocol.Serializator;
using ClassLibrary;

namespace HitThePlane.Game
{
    public enum PlaneDirection
    {
        Up, Forward, Down
    }

    public enum PlaneState
    {
        Takeoff, Flight, Destroyed
    }

    public class AirPlane : GameObject
    {
        public static readonly Size _modelSize = new Size(52 * ScaleRatio, 22 * ScaleRatio);
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

        private Image _sprite;

        private Vector2 DisplacementVector =>
            new Vector2((float)(Speed * Math.Cos(DirectionAngle * Math.PI / 180)),
                    Speed * (float)(Math.Sin(DirectionAngle * Math.PI / 180)));

        private float _gravityValue;

        private Vector2 GravityVector =>
            new Vector2(0, _gravityValue * (Speed < 1 ? 1 : 1 / (float)Math.Sqrt(Speed)));

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
                    if (value > _directionAngle || _speed < SpeedBoost * 20)
                        return;
                }
                _directionAngle = value;
            }
        }

        private SceneStruct _scene;

        public PlaneDirection Direction { get; set; }


        public AirPlane(Vector2 position, int health, float speed, float speedBoost, float maxSpeed, float gravityValue, float directionAngle, float angleChange, Image sprite, SceneStruct scene)
        {
            Position = position;
            Health = health;
            _speed = speed;
            SpeedBoost = speedBoost;
            _maxSpeed = maxSpeed;
            _angleChange = angleChange;
            _gravityValue = gravityValue;
            _directionAngle = directionAngle;
            _sprite = sprite;
            _scene = scene;
        }

        public void SendMove(XClient client, int formX) 
        {
            client.QueuePacketSendUpdate(XPacketConverter.Serialize(XPacketType.PlayerMovement, 
                new XPacketPlayerMovement(Position, DirectionAngle, Speed, (int)State, (int)Direction, _gravityValue, client.Id, formX, _scene)).ToPacket());
        }

        public void Move(XPacketPlayerMovement movement)
        {
            Position = movement.Position;
            DirectionAngle = movement.DirectionAngle;
            Speed = movement.Speed;
            State = (PlaneState)movement.State;
            Direction = (PlaneDirection)movement.Direction;
            _gravityValue = movement.GravityValue;
        }

        public void GetInputResult(XPacketPlayerInputResult res)
        {
            Speed = res.Speed;
            Direction = (PlaneDirection)res.Direction;
        }

        private void Rotate()
        {
            if (Direction == PlaneDirection.Up)
                DirectionAngle += _angleChange;
            else if (Direction == PlaneDirection.Down)
                DirectionAngle -= _angleChange;
        }

        public void Move(int formX)
        {
            Speed -= _scene.AirResistance;
            Rotate();
            Position += DisplacementVector + GravityVector;
            CheckBorders(formX);
            if (State == PlaneState.Takeoff && Position.Y < _scene.GroundHeigth - 50)
                State = PlaneState.Flight;
        }

        private void CheckBorders(int formX)
        {
            if (Position.X < 0)
                Position = new Vector2(formX, Position.Y);
            if (Position.X > formX)
                Position = new Vector2(0, Position.Y);
            if (Position.Y < 20)
                Speed -= SpeedBoost * 4;


            if (Position.Y > _scene.GroundHeigth - ModelSize.Height / 2)
            {
                if (State == PlaneState.Flight)
                    Destroy();
                Position = new Vector2(Position.X, _scene.GroundHeigth - ModelSize.Height / 2);
            }

            if (Collide(_scene.House))
                Destroy();

        }


        /*public void Shoot()
        {
            Bullet.Create(this);
        }*/

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
                State = PlaneState.Destroyed;
        }

        private void Destroy()
        {
            _gravityValue = 0;
            Speed = 0;
            Health = 0;
            State = PlaneState.Destroyed;
        }
    }
}
