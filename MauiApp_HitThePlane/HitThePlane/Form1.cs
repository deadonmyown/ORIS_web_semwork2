using HitThePlane.Entities;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HitThePlane
{
    public partial class Form1 : Form
    {
        public const int backGroundScaleRatio = 4;
        public const int defaultWidth = 306 * backGroundScaleRatio;
        public const int defaultHeight = 200 * backGroundScaleRatio;

        private Rectangle _bounding = new Rectangle(0, 0, defaultWidth, defaultHeight);


        public Form1()
        {
            InitializeComponent();

            Width = defaultWidth;
            Height = defaultHeight;

            this.BackgroundImage = Image.FromFile("Sprites/scene.png");
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            button1.Visible = false;

            InitScene();
            KeyDown += new KeyEventHandler(PlayerInputHandler.KeyPressed);
            KeyUp += new KeyEventHandler(PlayerInputHandler.KeyReleased);
            MouseDown += new MouseEventHandler(PlayerInputHandler.MouseClick);
            KeyPreview = true;
            InitTimer();
            timer1.Start();
        }

        private void InitTimer()
        {
            timer1.Interval = 20;
            timer1.Tick += new EventHandler(Update);
        }

        private void InitScene()
        {
            var groundHeigth = 280;
            Scene.GroundHeigth = Height - groundHeigth;
            var sprite = Image.FromFile("Sprites/green_plane.png");
            Scene.MyPlane = new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 0.5f, 20, -15, 10, sprite);
            Scene.GravityValue = 8;
            Scene.Ground = new Rectangle(0, Height - groundHeigth, Width, groundHeigth);
            Scene.House = new Rectangle(500, 310, 260, 210);
            Scene.Bullets = new HashSet<Bullet>();
            Scene.AirResistance = 0.05f;
        }


        private void StopGame()
        {
            timer1.Stop();
            button1.Visible = true;
            button1.Enabled = true;
            button1.Focus();
        }

        private void Update(object sender, EventArgs e)
        {
            PlayerInputHandler.Apply();
            if (Scene.MyPlane.State == PlaneState.Destroyed)
                StopGame();
            Scene.MyPlane.Move();
            foreach (var bullet in Scene.Bullets)
                bullet.Move();
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            DrawEntity(g, Scene.MyPlane);
            g.DrawRectangle(Pens.Red, Scene.Ground);
            g.DrawRectangle(Pens.Red, Scene.House);
            foreach (var bullet in Scene.Bullets)
            {
                if (!_bounding.IntersectsWith(
                    new Rectangle(new Point((int)bullet.Position.X, (int)bullet.Position.Y), bullet.ModelSize)))
                    bullet.Destroy();
                else
                    DrawEntity(g, bullet);
            }
        }

        private void DrawEntity(Graphics g, GameObject obj)
        {
            g.DrawRectangle(Pens.Red, obj.BoundingRectangle);
            g.TranslateTransform(obj.Position.X, obj.Position.Y);
            g.RotateTransform(obj.DirectionAngle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var boundingRect = new Rectangle(-obj.ModelSize.Width / 2, -obj.ModelSize.Height / 2,
                obj.ModelSize.Width, obj.ModelSize.Height);
            g.DrawImage(obj.Sprite, boundingRect);
            g.DrawRectangle(Pens.Aqua, boundingRect);
            g.ResetTransform();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button1.Enabled = false;
            InitScene();
            timer1.Start();
            this.Focus();
        }
    }
}