using HitThePlane.Engine;
using HitThePlane.Entities;
using HitThePlane.Resources;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace HitThePlane
{
    public partial class GameForm : Form
    {
        public const int ScaleRatio = 4;
        public const int defaultWidth = 320 * ScaleRatio;
        public const int defaultHeight = 200 * ScaleRatio;

        //private Rectangle _formBounding = new Rectangle(0, 0, defaultWidth, defaultHeight);


        public GameForm()
        {
            InitializeComponent();
            var game = new Game(this, gameCanvas);
            game.Run();
        }

        //private void InitTimer()
        //{
        //    timer1.Interval = 20;
        //    timer1.Tick += new EventHandler(Update);
        //}

        //private void InitScene()
        //{
        //    Console.WriteLine(ClientSize.Height/4);
        //    var groundHeigth = 64;
        //    Scene.GroundHeigth = Height - groundHeigth;
        //    Scene.MyPlane = new AirPlane(new Vector2(100, Height - groundHeigth - AirPlane._modelSize.Height / 2), 100, 0, 0.5f, 20, -15, 10, R.GREEN_PLANE);
        //    Scene.GravityValue = 10;
        //    Scene.Ground = new Rectangle(0, Height - groundHeigth, Width, groundHeigth);
        //    Scene.House = new Rectangle(500, 310, 260, 210);
        //    Scene.Bullets = new HashSet<Bullet>();
        //    Scene.AirResistance = 0.05f;
        //}


        //private void StopGame()
        //{
        //    timer1.Stop();
        //    button1.Visible = true;
        //    button1.Enabled = true;
        //    button1.Focus();
        //}

        //private void Update(object sender, EventArgs e)
        //{
        //    PlayerInputHandler.Apply();
        //    if (Scene.MyPlane.State == PlaneState.Destroyed)
        //        StopGame();
        //    Scene.MyPlane.Move();
        //    foreach (var bullet in Scene.Bullets)
        //        bullet.Move();
        //    Invalidate();
        //}

        //private void OnPaint(object sender, PaintEventArgs e)
        //{
        //    var g = e.Graphics;
        //    DrawEntity(g, Scene.MyPlane);
        //    g.DrawRectangle(Pens.Red, Scene.Ground);
        //    g.DrawRectangle(Pens.Red, Scene.House);
        //    foreach (var bullet in Scene.Bullets)
        //    {
        //        if (!_formBounding.IntersectsWith(
        //            new Rectangle(new Point((int)bullet.Position.X, (int)bullet.Position.Y), bullet.ModelSize)))
        //            bullet.Destroy();
        //        else
        //            DrawEntity(g, bullet);
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    button1.Visible = false;
        //    button1.Enabled = false;
        //    InitScene();
        //    timer1.Start();
        //    this.Focus();
        //}
    }
}