using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HitThePlane.Game
{
    public class Bullet : GameObject
    {
        public static readonly Size _modelSize = new Size(21 * ScaleRatio, 21 * ScaleRatio);
        public static readonly Image[] ExplosionAnim = new Image[]
        {
            Image.FromFile("Sprites/Explosion/bullet.png"),
            Image.FromFile("Sprites/Explosion/Explosion1.png"),
            Image.FromFile("Sprites/Explosion/Explosion2.png"),
            Image.FromFile("Sprites/Explosion/Explosion3.png"),
            Image.FromFile("Sprites/Explosion/Explosion4.png"),
            Image.FromFile("Sprites/Explosion/Explosion5.png"),
            Image.FromFile("Sprites/Explosion/Explosion6.png"),
            Image.FromFile("Sprites/Explosion/Explosion7.png"),
        };
        private int frameCounter = 0;
        private bool collided = false;
        public override Size ModelSize => _modelSize;
        public override Image Sprite => ExplosionAnim[frameCounter];
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
