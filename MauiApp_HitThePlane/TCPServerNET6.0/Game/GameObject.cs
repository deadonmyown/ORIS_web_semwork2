using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TCPServerNET6._0.Game
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
