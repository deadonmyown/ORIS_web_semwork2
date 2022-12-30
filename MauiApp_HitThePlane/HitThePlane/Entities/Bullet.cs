using HitThePlane.Engine;
using HitThePlane.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HitThePlane.Entities
{
    public class Bullet : DrawableObject
    {
        public static readonly Size _modelSize = new Size(21 * ScaleRatio, 21 * ScaleRatio);
        public static readonly string[] ExplosionAnim = new string[]
        {
            R.BULLET, R.EXPLOSION1, R.EXPLOSION2,R.EXPLOSION3, R.EXPLOSION4, R.EXPLOSION5, R.EXPLOSION6, R.EXPLOSION7
        };
        private int frameCounter = 0;
        private bool collided = false;
        public override Size ModelSize => _modelSize;
        public override string Sprite => ExplosionAnim[frameCounter];
        public float Speed { get; set; }
        public int Damage { get; set; }
        public override float DirectionAngle { get; set; }
        private Level _level;

        public static Bullet Create(Level level, AirPlane owner)
        {
            var instance = new Bullet()
            { _level = level, Damage = 20, Speed = 50, DirectionAngle = owner.DirectionAngle, Position = owner.Position };
            level.Bullets.Add(instance);
            return instance;
        }

        private Bullet() { }

        private Vector2 DirectionVector =>
           new Vector2((float)Math.Cos(DirectionAngle * Math.PI / 180),
                   -(float)Math.Sin(DirectionAngle * Math.PI / 180));

        public void Move()
        {
            Position += Speed * DirectionVector;
            if (!collided) CheckCollide();
            else if (++frameCounter == ExplosionAnim.Length)
                Destroy();
        }

        private void CheckCollide()
        {
            if (Position.X < 0 || Position.X > Render.Resolution.Width || Position.Y < 0 || Position.Y > Render.Resolution.Height)
                Destroy();

            //TODO Scene.EnemyPlane.BoundingRectangle 
            //var colliders = new Collider[] { _level.Ground, _level.House };
            //foreach (var collider in colliders)
            //    if (Collide(collider))
            //    {
            //        collided = true;
            //        Speed = 0;
            //    }
        }

        public void Destroy()
        {
            _level.Bullets.Remove(this);
        }
    }
}
