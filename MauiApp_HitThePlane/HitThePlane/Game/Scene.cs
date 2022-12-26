namespace HitThePlane.Game
{
    public static class Scene
    {
        public static AirPlane MyPlane { get; set; }
        public static AirPlane EnemyPlane { get; set; }
        public static float GravityValue { get; set; }
        public static float AirResistance { get; set; }
        public static HashSet<Bullet> Bullets { get; set; }
        public static Rectangle House { get; set; }
        public static Rectangle Ground { get; set; }
        public static int GroundHeigth { get; set; }

        public static void Initialize(float gravityValue, float airResistance, HashSet<Bullet> bullets, Rectangle house, Rectangle ground, int groundHeigth, AirPlane myPlane, AirPlane enemyPlane = null)
        {
            GravityValue = gravityValue;
            AirResistance = airResistance;
            Bullets = bullets;
            House = house;
            Ground = ground;
            GroundHeigth = groundHeigth;
            MyPlane = myPlane;
            EnemyPlane = enemyPlane;
        }
    }
}
