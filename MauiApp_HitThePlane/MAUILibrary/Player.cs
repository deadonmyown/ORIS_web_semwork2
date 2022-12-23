using Microsoft.Maui.Platform;

namespace MAUILibrary
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public double Speed { get; set; }
        public double MaxSpeed { get; set; }
        public int PlayerId { get; set; }
        public int Rotation { get; set; }

        public GameObject GameObject { get; set; }
        public double SpeedBoost { get; set; }
        public double AngleChange { get; set; }

        public bool IsFreezing { get; set; }

        public Player(string playerName, int health, float speed, float maxSpeed, int playerId, GameObject gameObject, double speedBoost, double angleChange)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
            PlayerId = playerId;
            GameObject = gameObject;
            SpeedBoost = speedBoost;
            AngleChange = angleChange;
        }

        public Player(string playerName, int health, float speed, float maxSpeed, GameObject gameObject, double speedBoost, double angleChange)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
            GameObject= gameObject;
            SpeedBoost = speedBoost;
            AngleChange = angleChange;
        }

        public void Shoot(Projectile projectile)
        {
            ContentPage scene = GameObject.Scene;
            Application.Current.Dispatcher.DispatchAsync(new Action(() =>
            {
                projectile.GameObject.Controller.TranslationX = GameObject.Controller.TranslationX;
                projectile.GameObject.Controller.TranslationY = GameObject.Controller.TranslationY;
            }));
        }

        public void Rotate(int rotation) => Rotation = rotation;
    }
}