using ClassLibrary;
using HitThePlane.Entities;
using HitThePlane.Resources;
using System.Numerics;
using System.Windows.Forms;

namespace HitThePlane.Engine;
public class Level
{
    public static readonly int GroundHeight = 62 * Render.ScaleRatio;

    public static readonly Dictionary<int, Vector2> Positions = new Dictionary<int, Vector2>()
        {
            {1, new Vector2(Render.Resolution.Width / 8, Render.Resolution.Height - GroundHeight - AirPlane._modelSize.Height / 2)},
            {2, new Vector2(Render.Resolution.Width / 8, Render.Resolution.Height - GroundHeight - AirPlane._modelSize.Height / 2) * new Vector2(7, 1) }
        };

    public float GravityValue { get; protected set; }
    public float AirResistance { get; protected set; }

    /*public AirPlane PlayerPlane { get; set; }
    public AirPlane EnemyPlane { get; set; }*/
    
    public Collider House { get; protected set; }
    public Collider Ground { get; protected set; }
    public HashSet<Bullet> Bullets { get; protected set; }

    public Level(float gravityValue, float airResistance)
    {
        GravityValue = gravityValue;
        AirResistance = airResistance;

        /*PlayerPlane = InitPlayerPlane();
        EnemyPlane = InitEnemyPlane();*/

        var groundCenter = new Vector2(Render.Resolution.Width / 2, Render.Resolution.Height - GroundHeight / 2);
        Ground = new Collider(groundCenter, new Size(Render.Resolution.Width, GroundHeight));

        var houseSize = new Size(70 * Render.ScaleRatio, 57 * Render.ScaleRatio);
        var houseCenter = new Vector2(groundCenter.X, Render.Resolution.Height - GroundHeight - houseSize.Height / 2);
        House = new Collider(houseCenter, houseSize);

        Bullets = new HashSet<Bullet>();
    }

    private AirPlane InitPlayerPlane()
    {
        var planePos = new Vector2(Render.Resolution.Width / 8, Render.Resolution.Height - GroundHeight - AirPlane._modelSize.Height / 2);

        var greenAnim = new string[] { R.GREEN_PLANE, R.GREEN_PLANE2, R.GREEN_PLANE3, R.GREEN_PLANE2 };

        return new AirPlane(ToLevelStruct(), planePos, 0.2f, 15, 12, 8, 8, greenAnim, R.GREEN_TURN);
    }

    private AirPlane InitEnemyPlane()
    {
        var planePos = new Vector2(Render.Resolution.Width / 8, Render.Resolution.Height - GroundHeight - AirPlane._modelSize.Height / 2);

        var redAnim = new string[] { R.RED_PLANE, R.RED_PLANE2, R.RED_PLANE3, R.RED_PLANE };

        return new AirPlane(ToLevelStruct(), planePos * new Vector2(7, 1), 0.2f, 15, 168, 8, 8, redAnim, R.RED_TURN);
    }

    public LevelStruct ToLevelStruct()
    {
        LevelStruct lvlStr = new LevelStruct();
        lvlStr.GravityValue = GravityValue;
        lvlStr.GroundHeight = GroundHeight;
        lvlStr.Ground = Ground;
        lvlStr.House = House;
        lvlStr.AirResistance = AirResistance;
        return lvlStr;
    }
}
