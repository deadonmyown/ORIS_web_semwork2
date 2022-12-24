namespace HitThePlane.Entities
{
    class Player
    {
        public string PlayerName { get; set; }
        public AirPlane Plane { get; set; }
        public Player(string playerName, AirPlane plane)
        {
            PlayerName = playerName;
            Plane = plane;
        }
    }
}
