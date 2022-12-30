using System.Numerics;
using System.Security.Policy;
using TCPServer;
using ClassLibrary;

namespace HitThePlane.Game
{
    public class Player
    {
        public static Dictionary<int, Player> Players = new Dictionary<int, Player>();

        public static readonly Dictionary<int, Image> Sprites = new Dictionary<int, Image>() 
        {
            {0, Image.FromFile("Sprites/green_plane.png") },
            {1, Image.FromFile("Sprites/green_plane.png") } //red_plane
        };

        public int Id { get; set; }
        public bool IsLocal { get; set; }
        public AirPlane Plane { get; set; }

        public Player()
        {
        }

        public Player(int id, bool isLocal, AirPlane plane)
        {
            Id = id;
            IsLocal = isLocal;
            Plane = plane;
        }


        public static void Spawn(int id, Vector2 position, SceneStruct _scene)
        {
            Player player;
            if (id == NetworkManager.Instance.Client.Id)
            {
                player = new Player(id, true, new AirPlane(position, 100, 0, 0.5f, 20, _scene.GravityValue, -15, 10, Sprites[id - 1], _scene));
                NetworkManager.Instance.Player = player;
            }
            else
            {
                player = new Player(id, false, new AirPlane(position, 100, 0, 0.5f, 20, _scene.GravityValue, -15, 10, Sprites[id - 1], _scene));
            }

            Players.Add(id, player);
        }

        public static void OnDestroy(int id) => Players.Remove(id);
    }
}
