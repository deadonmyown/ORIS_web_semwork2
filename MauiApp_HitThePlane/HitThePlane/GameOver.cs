using HitThePlane.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HitThePlane
{
    public partial class GameOver : UserControl
    {
        private Label _winner;
        public GameOver()
        {
            InitializeComponent();
            InitView();
        }

        public void Show(string winner)
        {
            _winner.Text = winner;
            this.Enabled = true;
            this.Visible = true;
        }

        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
            _winner.Text = "";
        }

        public void Restart(object sender, EventArgs e)
        {
            Hide();
            ((GameForm)ParentForm).ShowGameMenu();
        }

        private void InitView()
        {
            this.Width = 256 * 4;
            this.Height = 140 * 4;
            this.ForeColor = Color.FromArgb(210, 201, 165);
            this.BackgroundImage = R.GetImage(R.MENU_BACKGROUND);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            var gameLabel = new Label();
            gameLabel.Width = this.Width;
            gameLabel.Height = 140;
            Location = new Point(0, 20);
            gameLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameLabel.ForeColor = Color.FromArgb(75, 61, 68);
            gameLabel.BackColor = Color.Transparent;
            gameLabel.Text = "Game over";
            gameLabel.Font = new Font(R.Fonts.Families[0], 32);
            gameLabel.UseCompatibleTextRendering = true;

            var winnerLabel = new Label();
            winnerLabel.Width = this.Width;
            winnerLabel.Height = 80;
            winnerLabel.TextAlign = ContentAlignment.MiddleCenter;
            winnerLabel.ForeColor = Color.FromArgb(75, 61, 68);
            winnerLabel.BackColor = Color.Transparent;
            winnerLabel.Location = new Point(0, 140);
            winnerLabel.Text = "Победитель";
            winnerLabel.Font = new Font(R.Fonts.Families[0], 16);
            winnerLabel.UseCompatibleTextRendering = true;

            _winner = new Label();
            _winner.Width = this.Width;
            _winner.Height = 80;
            //winner.Text = "text";
            _winner.TextAlign = ContentAlignment.MiddleCenter;
            _winner.ForeColor = Color.FromArgb(75, 61, 68);
            _winner.BackColor = Color.Transparent;
            _winner.Location = new Point(0, 140 + 80);
            _winner.Font = new Font(R.Fonts.Families[0], 16);
            _winner.UseCompatibleTextRendering = true;


            var restartBtn = new System.Windows.Forms.Button();
            restartBtn.Size = new Size(300, 80);
            restartBtn.Location = new Point(Width / 2 - restartBtn.Width / 2, 440);
            restartBtn.Text = "Начать заново";
            restartBtn.Font = new Font(R.Fonts.Families[0], 16);
            restartBtn.ForeColor = Color.FromArgb(210, 201, 165);
            restartBtn.BackColor = Color.FromArgb(75, 61, 68);
            restartBtn.UseCompatibleTextRendering = true;
            restartBtn.TextAlign = ContentAlignment.MiddleCenter;
            restartBtn.Click += Restart;

            this.Controls.Add(gameLabel);
            this.Controls.Add(winnerLabel);
            this.Controls.Add(_winner);
            this.Controls.Add(restartBtn);
        }
    }
}
