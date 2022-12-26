namespace HitThePlane.Game
{
    class Player
    {
        public static Dictionary<int, Player> list = new Dictionary<int, Player>();

        public int Id { get; set; }
        public string PlayerName { get; set; }
        public AirPlane Plane { get; set; }

        public Player(string playerName, AirPlane plane)
        {
            PlayerName = playerName;
            Plane = plane;
        }

        public Player(int id, string playerName, AirPlane plane)
        {
            Id = id;
            PlayerName = playerName;
            Plane = plane;
        }
    }
}
