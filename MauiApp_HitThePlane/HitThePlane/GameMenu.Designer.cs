namespace HitThePlane
{
    partial class GameMenu
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
            this.Players = new System.Windows.Forms.Label();
            this.btnReady = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Players
            // 
            this.Players.AutoSize = true;
            this.Players.Location = new System.Drawing.Point(16, 12);
            this.Players.Name = "Players";
            this.Players.Size = new System.Drawing.Size(170, 20);
            this.Players.TabIndex = 0;
            this.Players.Text = "Подключенные игроки";
            // 
            // btnReady
            // 
            this.btnReady.Location = new System.Drawing.Point(204, 190);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(94, 29);
            this.btnReady.TabIndex = 1;
            this.btnReady.Text = "Готов";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.OnClick);
            // 
            // GameMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.Players);
            this.Name = "GameMenu";
            this.Size = new System.Drawing.Size(485, 251);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Players;
        private Button btnReady;
    }
}
