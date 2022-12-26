using System.Drawing.Drawing2D;
using System.Numerics;
using TCPClient;
using HitThePlane.Game;
using SceneServer = TCPServerNET6._0.Game.Scene;
using BulletServer = TCPServerNET6._0.Game.Bullet;

namespace HitThePlane
{
    public partial class Form1 : Form
    {
        private XClient _client;

        public const int backGroundScaleRatio = 4;
        public const int defaultWidth = 306 * backGroundScaleRatio;
        public const int defaultHeight = 200 * backGroundScaleRatio;

        private Rectangle _bounding = new Rectangle(0, 0, defaultWidth, defaultHeight);


        public Form1()
        {
            InitializeComponent();

            _client= new XClient();
            _client.Connect("127.0.0.1", 4910);

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
            var gravityValue = 8;
            var sprite = Image.FromFile("Sprites/green_plane.png");
            Scene.GroundHeigth = Height - groundHeigth;
            Scene.GravityValue = gravityValue;
            Scene.Ground = new Rectangle(0, Height - groundHeigth, Width, groundHeigth);
            Scene.House = new Rectangle(500, 310, 260, 210);
            Scene.Bullets = new HashSet<Bullet>();
            Scene.AirResistance = 0.05f;
            Scene.MyPlane = new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 0.5f, 20, gravityValue, -15, 10, sprite);

            /*Scene.Initialize(8, 0.05f, new HashSet<Bullet>(), new Rectangle(500, 310, 260, 210),
                new Rectangle(0, Height - groundHeigth, Width, groundHeigth), Height - groundHeigth,
                new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 0.5f, 20, gravityValue, -15, 10, sprite));*/
            SceneServer.Initialize(8, 0.05f, new HashSet<BulletServer>(), new Rectangle(500, 310, 260, 210),
                new Rectangle(0, Height - groundHeigth, Width, groundHeigth), Height - groundHeigth);
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
            PlayerInputHandler.Apply(_client);
            if (Scene.MyPlane.State == PlaneState.Destroyed)
                StopGame();
            Scene.MyPlane.Move(defaultWidth);
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