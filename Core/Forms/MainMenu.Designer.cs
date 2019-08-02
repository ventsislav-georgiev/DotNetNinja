namespace DotNetNinja.Core.Forms
{
	partial class MainMenu
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}


		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.btn_NewGame = new System.Windows.Forms.Button();
            this.txt_Title = new System.Windows.Forms.Label();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_NewGame
            // 
            this.btn_NewGame.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_NewGame.Location = new System.Drawing.Point(62, 348);
            this.btn_NewGame.Margin = new System.Windows.Forms.Padding(6);
            this.btn_NewGame.Name = "btn_NewGame";
            this.btn_NewGame.Size = new System.Drawing.Size(298, 69);
            this.btn_NewGame.TabIndex = 1;
            this.btn_NewGame.Text = "New Game";
            this.btn_NewGame.UseVisualStyleBackColor = true;
            this.btn_NewGame.Click += new System.EventHandler(this.NewGame);
            // 
            // txt_Title
            // 
            this.txt_Title.BackColor = System.Drawing.Color.Transparent;
            this.txt_Title.Font = new System.Drawing.Font("Times New Roman", 35F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.txt_Title.ForeColor = System.Drawing.Color.Lime;
            this.txt_Title.Location = new System.Drawing.Point(456, 19);
            this.txt_Title.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.txt_Title.Name = "txt_Title";
            this.txt_Title.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txt_Title.Size = new System.Drawing.Size(552, 125);
            this.txt_Title.TabIndex = 7;
            this.txt_Title.Text = "DotNetNinja";
            this.txt_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Restart
            // 
            this.btn_Restart.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btn_Restart.Location = new System.Drawing.Point(1096, 348);
            this.btn_Restart.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(298, 69);
            this.btn_Restart.TabIndex = 8;
            this.btn_Restart.Text = "Restart Game";
            this.btn_Restart.UseVisualStyleBackColor = true;
            this.btn_Restart.Visible = false;
            this.btn_Restart.Click += new System.EventHandler(this.btn_Restart_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DotNetNinja.Properties.Resources.MainMenuBackground;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1456, 794);
            this.Controls.Add(this.btn_Restart);
            this.Controls.Add(this.txt_Title);
            this.Controls.Add(this.btn_NewGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotNetNinja";
            this.ResumeLayout(false);

		}


		private System.Windows.Forms.Button btn_NewGame;
		private System.Windows.Forms.Label txt_Title;
		private System.Windows.Forms.Button btn_Restart;
	}
}