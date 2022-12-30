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
        private Game _game;

        public GameForm()
        {
            InitializeComponent();
            SetSizes();
            joinMenu.Show();
        }

        public void StartGame()
        {
            _game = new Game(this, gameCanvas);
            _game.Run();
            KeyPreview = true;
        }

        public void ShowGameMenu()
        {
            gameMenu.Show();
        }

        public void Pause()
        {
            _game.Pause();
            joinMenu.Show();
        }

        public void Resume()
        {
            _game.Resume();
        }

        private void SetSizes()
        {
            var defaultSize = new Size(defaultWidth, defaultHeight);
            this.ClientSize = defaultSize;
            gameCanvas.Size = defaultSize;
            ToCenter(joinMenu);
            ToCenter(gameMenu);
        }

        private void ToCenter(UserControl control)
        {
            control.Location = new Point(defaultWidth / 2 - control.Width / 2, defaultHeight / 2 - control.Height / 2);
        }
    }
}