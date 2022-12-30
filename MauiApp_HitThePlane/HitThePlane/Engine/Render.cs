using HitThePlane.Entities;
using HitThePlane.Resources;
using HitThePlane.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HitThePlane.Engine
{
    static class Render
    {
        public static int ScaleRatio { get; set; } = 4;
        public static PictureBox Canvas { get; set; }
        public static Size Resolution => Canvas.Size;

        public static void DrawLevel(Level level)
        {
            if (Canvas.Image == null)
                Clear();
            using var g = Graphics.FromImage(Canvas.Image);
            g.DrawImage(R.GetImage(R.MAIN_LEVEL_BG), 0, 0, Resolution.Width, Resolution.Height);

            DrawCollider(g, level.Ground);
            DrawCollider(g, level.House);

            DrawObject(g, level.PlayerPlane);
            DrawObject(g, level.EnemyPlane);

            foreach (var bullet in level.Bullets)
            {
                DrawObject(g, bullet);
            }

            Canvas.Invalidate();
        }

        private static void DrawCollider(Graphics g, Collider collider)
        {
            g.TranslateTransform(collider.Position.X, collider.Position.Y);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var boundingRect = new Rectangle(-collider.Size.Width / 2, -collider.Size.Height / 2,
                collider.Size.Width, collider.Size.Height);
            g.DrawRectangle(Pens.Red, boundingRect);
            g.ResetTransform();
        }

        private static void DrawObject(Graphics g, DrawableObject obj)
        {
            g.DrawRectangle(Pens.Red, obj.BoundingRectangle);
            g.TranslateTransform(obj.Position.X, obj.Position.Y);
            g.RotateTransform(-obj.DirectionAngle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var sprite = (obj is AirPlane && ((AirPlane)obj).Flip) ?
                R.GetFlip(obj.Sprite) : R.GetImage(obj.Sprite);

            var spriteSize = new Size(sprite.Width / 10 * DrawableObject.ScaleRatio,
                sprite.Height / 10 * DrawableObject.ScaleRatio);

            var boundingRect = new Rectangle(-spriteSize.Width / 2, -spriteSize.Height / 2,
                sprite.Width / 10 * DrawableObject.ScaleRatio, sprite.Height / 10 * DrawableObject.ScaleRatio);

            g.DrawImage(sprite, boundingRect);
            g.DrawRectangle(Pens.Aqua, boundingRect);
            g.FillEllipse(Brushes.Red, new Rectangle(-3, -3, 6, 6));
            g.ResetTransform();
        }

        private static void Clear()
        {
            Bitmap bmp = new Bitmap(Canvas.Width, Canvas.Height);
            using var g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Canvas.Image = bmp;
        }

        public static void DrawBackground()
        {
            if (Canvas.Image == null)
                Clear();
            using var g = Graphics.FromImage(Canvas.Image);
            g.DrawImage(R.GetImage(R.MAIN_LEVEL_BG), 0, 0, Resolution.Width, Resolution.Height);
        }
    }
}
