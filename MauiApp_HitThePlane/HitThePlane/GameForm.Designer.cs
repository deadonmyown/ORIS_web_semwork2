using HitThePlane.Resources;

namespace HitThePlane
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gameCanvas = new System.Windows.Forms.PictureBox();
            this.joinMenu = new HitThePlane.JoinMenu();
            this.gameMenu = new HitThePlane.GameMenu();
            ((System.ComponentModel.ISupportInitialize)(this.gameCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // gameCanvas
            // 
            this.gameCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameCanvas.Location = new System.Drawing.Point(0, 0);
            this.gameCanvas.Name = "gameCanvas";
            this.gameCanvas.Size = new System.Drawing.Size(282, 253);
            this.gameCanvas.TabIndex = 0;
            this.gameCanvas.TabStop = false;
            // 
            // joinMenu
            // 
            this.joinMenu.Enabled = false;
            this.joinMenu.Name = "joinMenu";
            this.joinMenu.TabIndex = 1;
            this.joinMenu.BackColor = Color.Transparent;
            this.joinMenu.Visible = false;
            // 
            // gameMenu
            // 
            this.gameMenu.Enabled = false;
            this.gameMenu.Name = "gameMenu";
            this.gameMenu.BackColor = Color.Transparent;
            this.gameMenu.TabIndex = 2;
            this.gameMenu.Visible = false;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.gameMenu);
            this.Controls.Add(this.joinMenu);
            this.Controls.Add(this.gameCanvas);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameForm";
            this.Text = "Hit the plane";
            ((System.ComponentModel.ISupportInitialize)(this.gameCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox gameCanvas;
        private JoinMenu joinMenu;
        private GameMenu gameMenu;
    }
}