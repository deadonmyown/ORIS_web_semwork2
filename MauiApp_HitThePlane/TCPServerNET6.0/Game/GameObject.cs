using System.Drawing;
using System.Numerics;

namespace TCPServer.Game
{
    public abstract class GameObject
    {
        public const int ScaleRatio = 2;
        public Vector2 Position { get; set; }
        public abstract float DirectionAngle { get; set; }
        public abstract Size ModelSize { get; }
        public Rectangle BoundingRectangle =>
            new Rectangle((int)Position.X - ModelSize.Width / 2, (int)Position.Y - ModelSize.Height / 2, ModelSize.Width, ModelSize.Height);

        protected bool Collide(Rectangle rect) =>
            BoundingRectangle.IntersectsWith(rect);
    }
}
