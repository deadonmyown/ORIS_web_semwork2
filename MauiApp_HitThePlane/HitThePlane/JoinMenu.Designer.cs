namespace HitThePlane
{
    partial class JoinMenu
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Join = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Join
            // 
            this.Join.Location = new System.Drawing.Point(121, 148);
            this.Join.Name = "Join";
            this.Join.Size = new System.Drawing.Size(153, 29);
            this.Join.TabIndex = 2;
            this.Join.Text = "Подключиться";
            this.Join.UseVisualStyleBackColor = true;
            this.Join.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(135, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 3;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(148, 50);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(97, 20);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Введите имя";
            // 
            // JoinMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Join);
            this.Name = "JoinMenu";
            this.Size = new System.Drawing.Size(397, 213);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button Join;
        private TextBox textBox1;
        private Label nameLabel;
    }
}
