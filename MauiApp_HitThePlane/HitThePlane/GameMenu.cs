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
        public GameMenu()
        {
            InitializeComponent();
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
            btnReady.BackColor = Color.Green;
        }
    }
}
