using HitThePlane.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HitThePlane
{
    public partial class JoinMenu : UserControl
    {
        public JoinMenu()
        {
            InitializeComponent();
            InitView();
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

        private void OnClick(object sender, EventArgs e)
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

            var gameLabel = new System.Windows.Forms.Label();
            gameLabel.Width = this.Width;
            gameLabel.Height = 140;
            Location = new Point(0, 20);
            gameLabel.TextAlign = ContentAlignment.MiddleCenter;
            gameLabel.ForeColor = Color.FromArgb(75, 61, 68);
            gameLabel.BackColor = Color.Transparent;
            gameLabel.Text = "Hit the plane";
            gameLabel.Font = new Font(R.Fonts.Families[0], 32);
            gameLabel.UseCompatibleTextRendering = true;

            var inputLabel = new System.Windows.Forms.Label();
            inputLabel.Width = this.Width;
            inputLabel.Height = 80;
            inputLabel.TextAlign = ContentAlignment.MiddleCenter;
            inputLabel.ForeColor = Color.FromArgb(75, 61, 68);
            inputLabel.BackColor = Color.Transparent;
            inputLabel.Location = new Point(0, 140);
            inputLabel.Text = "Введите имя";
            inputLabel.Font = new Font(R.Fonts.Families[0], 16);
            inputLabel.UseCompatibleTextRendering = true;

            var nameInput = new System.Windows.Forms.TextBox();
            nameInput.Font = new Font(R.Fonts.Families[0], 16);
            nameInput.ForeColor = Color.FromArgb(75, 61, 68);
            nameInput.BackColor = Color.FromArgb(210, 201, 165);
            nameInput.BorderStyle = BorderStyle.FixedSingle;
            nameInput.Size = new Size(300, 100);
            nameInput.Location = new Point(Width / 2 - nameInput.Width / 2, 240);

            var joinBtn = new System.Windows.Forms.Button();
            joinBtn.Size = new Size(300, 80);
            joinBtn.Location = new Point(Width / 2 - joinBtn.Width / 2, 440);
            joinBtn.Text = "Подключиться";
            joinBtn.Font = new Font(R.Fonts.Families[0], 16);
            joinBtn.ForeColor = Color.FromArgb(210, 201, 165);
            joinBtn.BackColor = Color.FromArgb(75, 61, 68);
            joinBtn.UseCompatibleTextRendering = true;
            joinBtn.TextAlign = ContentAlignment.MiddleCenter;
            joinBtn.Click += OnClick;

            this.Controls.Add(gameLabel);
            this.Controls.Add(inputLabel);
            this.Controls.Add(nameInput);
            this.Controls.Add(joinBtn);
        }
    }
}
