namespace HitThePlaneLibrary
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int Health { get; set; }
        public double Speed { get; set; }
        public double MaxSpeed { get; set; }
        public int PlayerId { get; set; }
        public int Rotate { get; set; }

        public Player(string playerName, int health, float speed, float maxSpeed, int playerId)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
            PlayerId = playerId;
        }

        public Player(string playerName, int health, float speed, float maxSpeed)
        {
            PlayerName = playerName;
            Health = health;
            Speed = speed;
            MaxSpeed = maxSpeed;
        }
    }
}