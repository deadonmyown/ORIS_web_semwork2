using System.Numerics;
using System.Security.Policy;
using TCPServer;
using ClassLibrary;
using HitThePlane.Engine;
using HitThePlane.Resources;

namespace HitThePlane.Entities
{
    public class Player
    {
        public static Dictionary<int, Player> Players = new Dictionary<int, Player>();

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


        public static void Spawn(int id, LevelStruct levelStr, Vector2 position)
        {
            Player player;
            if (id == NetworkManager.Instance.Client.Id)
            {
                player = new Player(id, true, InitPlayerPlane(levelStr, position));
                NetworkManager.Instance.Player = player;
            }
            else
            {
                player = new Player(id, false, InitEnemyPlane(levelStr, position));
            }

            Players.Add(id, player);
        }

        private static AirPlane InitPlayerPlane(LevelStruct levelStr, Vector2 position)
        {
            var greenAnim = new string[] { R.GREEN_PLANE, R.GREEN_PLANE2, R.GREEN_PLANE3, R.GREEN_PLANE2 };

            return new AirPlane(levelStr, position, 0.2f, 15, 12, 8, 8, greenAnim, R.GREEN_TURN);
        }

        private static AirPlane InitEnemyPlane(LevelStruct levelStr, Vector2 position)
        {
            var redAnim = new string[] { R.RED_PLANE, R.RED_PLANE2, R.RED_PLANE3, R.RED_PLANE };

            return new AirPlane(levelStr, position, 0.2f, 15, 168, 8, 8, redAnim, R.RED_TURN);
        }

        public static void OnDestroy(int id) => Players.Remove(id);
    }
}
