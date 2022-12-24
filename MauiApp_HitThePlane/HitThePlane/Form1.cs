using HitThePlane.Entities;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HitThePlane
{
    public partial class Form1 : Form
    {
        public const int scaleRatio = 4;
        public const int defaultWidth = 320 * scaleRatio;
        public const int defaultHeight = 200 * scaleRatio;

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
            KeyDown += new KeyEventHandler(PlayerInputHandler.KeyboardPressed);
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
            Scene.MyPlane = new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 2f, 20, -15, 8, sprite);
            Scene.GravityValue = 5;
            Scene.Ground = new Rectangle(0, Height - groundHeigth, Width, groundHeigth);
            Scene.House = new Rectangle(500, 310, 260, 210);
            Scene.Bullets = new HashSet<Bullet>();
        }


        private void StopGame()
        {
            timer1.Stop();
            button1.Visible = true;
        }

        private void Update(object sender, EventArgs e)
        {
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
            g.TranslateTransform(obj.Position.X, obj.Position.Y);
            g.RotateTransform(obj.DirectionAngle);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(Scene.MyPlane.Sprite, -obj.ModelSize.Width / 2,
                -obj.ModelSize.Height / 2,
                obj.ModelSize.Width, obj.ModelSize.Height);
            g.ResetTransform();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitScene();
            timer1.Start();
            button1.Visible = false;
        }
    }
}