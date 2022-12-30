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
    public partial class GameMenu : UserControl
    {

        private List<string> players = new List<string>();
        private List<Label> playerLabels = new List<Label>();   
        private Button prepareBtn;

        public GameMenu()
        {
            InitializeComponent();
            InitView();
            players.Add("test1");
            players.Add("test2");
            UpdatePlayers();
        }

        public void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        private async void OnClick(object sender, EventArgs e)
        {
            prepareBtn.BackColor = Color.Green;
            await Task.Delay(3000);
            ((GameForm)ParentForm).Resume();
            Hide();
        }


        private void UpdatePlayers()
        {
            foreach (var item in playerLabels)
            {
                this.Controls.Remove(item);
            }
            Invalidate();
            for (int i = 0; i < players.Count; i++)
            {
                var playerLabel = new Label();
                playerLabel.Width = this.Width;
                playerLabel.Height = 80;
                playerLabel.TextAlign = ContentAlignment.MiddleCenter;
                playerLabel.ForeColor = Color.FromArgb(75, 61, 68);
                playerLabel.BackColor = Color.Transparent;
                playerLabel.Location = new Point(0, 140 + 80 * (i + 1));
                playerLabel.Text = (i + 1) + ". " + players[i];
                playerLabel.Font = new Font(R.Fonts.Families[0], 16);
                playerLabel.UseCompatibleTextRendering = true;

                this.Controls.Add(playerLabel);
                playerLabels.Add(playerLabel);
            }
        }

        private void InitView()
        {
            this.Width = 256 * 4;
            this.Height = 140 * 4;
            this.BackgroundImage = R.GetImage(R.MENU_BACKGROUND);
            this.BackgroundImageLayout = ImageLayout.Stretch;

            var gameLabel = new Label();
            gameLabel.Width = this.Width;
            gameLabel.Height = 140;
            Location = new Point(0, 20);
            gameLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameLabel.ForeColor = Color.FromArgb(75, 61, 68);
            gameLabel.BackColor = Color.Transparent;
            gameLabel.Text = "Hit the plane";
            gameLabel.Font = new Font(R.Fonts.Families[0], 32);
            gameLabel.UseCompatibleTextRendering = true;

            var playersLabel = new Label();
            playersLabel.Width = this.Width;
            playersLabel.Height = 80;
            playersLabel.TextAlign = ContentAlignment.MiddleCenter;
            playersLabel.ForeColor = Color.FromArgb(75, 61, 68);
            playersLabel.BackColor = Color.Transparent;
            playersLabel.Location = new Point(0, 140);
            playersLabel.Text = "Подключенные игроки";
            playersLabel.Font = new Font(R.Fonts.Families[0], 16);
            playersLabel.UseCompatibleTextRendering = true;


            prepareBtn = new Button();
            prepareBtn.Size = new Size(300, 80);
            prepareBtn.Location = new Point(Width / 2 - prepareBtn.Width / 2, 440);
            prepareBtn.Text = "Готов";
            prepareBtn.Font = new Font(R.Fonts.Families[0], 16);
            prepareBtn.ForeColor = Color.FromArgb(210, 201, 165);
            prepareBtn.BackColor = Color.FromArgb(75, 61, 68);
            prepareBtn.UseCompatibleTextRendering = true;
            prepareBtn.TextAlign = ContentAlignment.MiddleCenter;
            prepareBtn.Click += OnClick;

            this.Controls.Add(gameLabel);
            this.Controls.Add(playersLabel);
            this.Controls.Add(prepareBtn);
        }
    }
}
