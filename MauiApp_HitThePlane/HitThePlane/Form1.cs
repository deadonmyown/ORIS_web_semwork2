using System.Drawing.Drawing2D;
using System.Numerics;
using TCPClient;
using HitThePlane.Game;
/*using SceneServer = TCPServer.Game.Scene;
using BulletServer = TCPServer.Game.Bullet;*/
using XProtocol.Serializator;
using XProtocol;
using ClassLibrary;

namespace HitThePlane
{
    public partial class Form1 : Form
    {
        public const int backGroundScaleRatio = 4;
        public const int defaultWidth = 306 * backGroundScaleRatio;
        public const int defaultHeight = 200 * backGroundScaleRatio;

        private Rectangle _bounding = new Rectangle(0, 0, defaultWidth, defaultHeight);

        private int _groundHeigth = 280;
        private int _gravityValue = 8;

        private SceneStruct _scene = new SceneStruct();


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

            InitScene(_groundHeigth, _gravityValue);
            KeyDown += new KeyEventHandler(PlayerInputHandler.KeyPressed);
            KeyUp += new KeyEventHandler(PlayerInputHandler.KeyReleased);
            MouseDown += new MouseEventHandler(PlayerInputHandler.MouseClick);
            KeyPreview = true;
            InitTimer();

            NetworkManager.Instance.Start(4910);

            while(NetworkManager.Instance.Client.Id == 0) { }
            NetworkManager.Instance.Client.QueuePacketSend(XPacketConverter.Serialize(XPacketType.Player,
                new XPacketPlayer(NetworkManager.Instance.Client.Id, new Vector2(100, Height - _groundHeigth - AirPlane._modelSize.Height / 2), _scene)).ToPacket());
            while(NetworkManager.Instance.Player == null) { }

            timer1.Start();
        }

        private void InitTimer()
        {
            timer1.Interval = 50;
            timer1.Tick += new EventHandler(Update);
        }

        private void InitScene(int groundHeigth, int gravityValue)
        {
            _scene.GroundHeigth = Height - groundHeigth;
            _scene.GravityValue = gravityValue;
            _scene.Ground = new Rectangle(0, Height - groundHeigth, Width, groundHeigth);
            _scene.House = new Rectangle(500, 310, 260, 210);
            //scene.Bullets = new HashSet<Bullet>();
            _scene.AirResistance = 0.05f;

            //var sprite = Image.FromFile("Sprites/green_plane.png");
            //_player = new Player("", new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 0.5f, 20, gravityValue, -15, 10, sprite));
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
            NetworkManager.Instance.Player.Plane.SendMove(NetworkManager.Instance.Client, defaultWidth);
            PlayerInputHandler.Apply();
            if (NetworkManager.Instance.Player.Plane.State == PlaneState.Destroyed)
                StopGame();
            /*foreach (var bullet in _scene.Bullets)
                bullet.Move();*/
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            foreach(var player in Player.Players.Values)
            {
                DrawEntity(g, player.Plane);
            }
            g.DrawRectangle(Pens.Red, _scene.Ground);
            g.DrawRectangle(Pens.Red, _scene.House);
            /*foreach (var bullet in _scene.Bullets)
            {
                if (!_bounding.IntersectsWith(
                    new Rectangle(new Point((int)bullet.Position.X, (int)bullet.Position.Y), bullet.ModelSize)))
                    bullet.Destroy();
                else
                    DrawEntity(g, bullet);
            }*/
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
            InitScene(_groundHeigth, _gravityValue);
            timer1.Start();
            this.Focus();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Player.OnDestroy(NetworkManager.Instance.Player.Id);
            NetworkManager.Instance.Client.Dispose();
        }
    }
}