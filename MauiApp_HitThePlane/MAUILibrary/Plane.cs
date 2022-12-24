using Microsoft.Maui.Platform;

namespace MAUILibrary
{
    public class Plane
    {
        public static Size modelSize = new Size(100, 40);

        public Point Position { get; set; }
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public double Speed { get; set; }
        public double Direction { get; set; }
        public double MaxSpeed { get; set; }

        public double AngleChange { get; set; }

        public Plane(Point position, string playerName, int health, double speed, double direction, double maxSpeed, int playerId, double angleChange)
        {
            Position = position;
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            Direction = direction;
            MaxSpeed = maxSpeed;
            AngleChange = angleChange;
        }

        public void Rotate(int direction)
        {
            if (direction >= 0)
                Direction += AngleChange;
            else
                Direction -= AngleChange;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public void ChangeSpeed()
        {
            throw new NotImplementedException();
        }


        //public int Rotation { get; set; }

        //public GameObject GameObject { get; set; }
        //public double SpeedBoost { get; set; }
        //public double AngleChange { get; set; }

        //public bool IsFreezing { get; set; }

        //public Player(Point position, string playerName, int health, float speed, float maxSpeed, int playerId, GameObject gameObject, double speedBoost, double angleChange)
        //{
        //    PlayerName = playerName;
        //    Health = health;
        //    Speed = speed;
        //    MaxSpeed = maxSpeed;
        //    PlayerId = playerId;
        //    GameObject = gameObject;
        //    SpeedBoost = speedBoost;
        //    AngleChange = angleChange;
        //}

        //public Player(string playerName, int health, float speed, float maxSpeed, GameObject gameObject, double speedBoost, double angleChange)
        //{
        //    PlayerName = playerName;
        //    Health = health;
        //    Speed = speed;
        //    MaxSpeed = maxSpeed;
        //    GameObject = gameObject;
        //    SpeedBoost = speedBoost;
        //    AngleChange = angleChange;
        //}

        //public void Shoot(Projectile projectile)
        //{
        //    ContentPage scene = GameObject.Scene;
        //    Application.Current.Dispatcher.DispatchAsync(new Action(() =>
        //    {
        //        projectile.GameObject.Controller.TranslationX = GameObject.Controller.TranslationX;
        //        projectile.GameObject.Controller.TranslationY = GameObject.Controller.TranslationY;
        //    }));
        //}

        //public void Rotate(int rotation) => Rotation = rotation;
    }
}

