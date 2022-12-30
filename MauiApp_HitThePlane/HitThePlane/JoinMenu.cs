using HitThePlane.Engine;
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
    public partial class JoinMenu : UserControl
    {
        public JoinMenu()
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

        private void button1_Click(object sender, EventArgs e)
        {
            ((GameForm)ParentForm).ShowGameMenu();
            Hide();
        }
    }
}
