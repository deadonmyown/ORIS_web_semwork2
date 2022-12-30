/*using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TCPServer.Game
{
    public class Bullet : GameObject
    {
        public static readonly Size _modelSize = new Size(21 * ScaleRatio, 21 * ScaleRatio);
        public static readonly int[] ExplosionAnim = new int[]
        {
            0, 1, 3, 4, 5, 6, 7
        };
        private int frameCounter = 0;
        private bool collided = false;
        public override Size ModelSize => _modelSize;
        public int Sprite => ExplosionAnim[frameCounter];
        public float Speed { get; set; }
        public int Damage { get; set; }
        public override float DirectionAngle { get; set; }

        public static Bullet Create(AirPlane plane)
        {
            var instance = new Bullet()
            { Damage = 20, Speed = 100, DirectionAngle = plane.DirectionAngle, Position = plane.Position };
            Scene.Bullets.Add(instance);
            return instance;
        }

        private Bullet() { }

        private Vector2 DisplacementVector =>
           new Vector2((float)(Speed * Math.Cos(DirectionAngle * Math.PI / 180)),
                   Speed * (float)(Math.Sin(DirectionAngle * Math.PI / 180)));

        public void Move()
        {
            Position = Position + DisplacementVector;
            if (!collided) CheckCollide();
            else if (++frameCounter == ExplosionAnim.Length)
                Destroy();
        }

        private void CheckCollide()
        {
            //TODO Scene.EnemyPlane.BoundingRectangle 
            var colliders = new Rectangle[] { Scene.Ground, Scene.House };
            foreach (var collider in colliders)
                if (Collide(collider))
                {
                    collided = true;
                    Speed = 0;
                }
        }

        public void Destroy()
        {
            Scene.Bullets.Remove(this);
        }
    }
}
*/