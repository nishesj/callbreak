namespace Cards
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            card.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gameStatusBar = new System.Windows.Forms.StatusStrip();
            this.gameStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gameRoundStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerNamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.animationSpeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guiUpdatebackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.gameStatusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameStatusBar
            // 
            this.gameStatusBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gameStatusBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gameStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameStatusLabel,
            this.gameRoundStatusLabel});
            this.gameStatusBar.Location = new System.Drawing.Point(0, 455);
            this.gameStatusBar.Name = "gameStatusBar";
            this.gameStatusBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.gameStatusBar.Size = new System.Drawing.Size(581, 22);
            this.gameStatusBar.TabIndex = 0;
            this.gameStatusBar.Text = "statusStrip1";
            // 
            // gameStatusLabel
            // 
            this.gameStatusLabel.Name = "gameStatusLabel";
            this.gameStatusLabel.Size = new System.Drawing.Size(158, 17);
            this.gameStatusLabel.Text = "Select New to start the game";
            // 
            // gameRoundStatusLabel
            // 
            this.gameRoundStatusLabel.Margin = new System.Windows.Forms.Padding(350, 3, 0, 2);
            this.gameRoundStatusLabel.Name = "gameRoundStatusLabel";
            this.gameRoundStatusLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gameRoundStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(581, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.displayScoreToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "&Game";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // displayScoreToolStripMenuItem
            // 
            this.displayScoreToolStripMenuItem.Name = "displayScoreToolStripMenuItem";
            this.displayScoreToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.displayScoreToolStripMenuItem.Text = "Display Score";
            this.displayScoreToolStripMenuItem.Click += new System.EventHandler(this.displayScoreToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerNamesToolStripMenuItem,
            this.deckToolStripMenuItem,
            this.animationSpeedToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "&Option";
            // 
            // playerNamesToolStripMenuItem
            // 
            this.playerNamesToolStripMenuItem.Name = "playerNamesToolStripMenuItem";
            this.playerNamesToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.playerNamesToolStripMenuItem.Text = "&Player Names";
            this.playerNamesToolStripMenuItem.Click += new System.EventHandler(this.playerNamesToolStripMenuItem_Click);
            // 
            // deckToolStripMenuItem
            // 
            this.deckToolStripMenuItem.Name = "deckToolStripMenuItem";
            this.deckToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.deckToolStripMenuItem.Text = "&Deck";
            this.deckToolStripMenuItem.Click += new System.EventHandler(this.deckToolStripMenuItem_Click);
            // 
            // animationSpeedToolStripMenuItem
            // 
            this.animationSpeedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.slowToolStripMenuItem});
            this.animationSpeedToolStripMenuItem.Name = "animationSpeedToolStripMenuItem";
            this.animationSpeedToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.animationSpeedToolStripMenuItem.Text = "Animation Speed";
            // 
            // fastToolStripMenuItem
            // 
            this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
            this.fastToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.fastToolStripMenuItem.Text = "Fast";
            this.fastToolStripMenuItem.Click += new System.EventHandler(this.fastToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Checked = true;
            this.mediumToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click);
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.slowToolStripMenuItem.Text = "Slow";
            this.slowToolStripMenuItem.Click += new System.EventHandler(this.slowToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.Color.Green;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(581, 477);
            this.Controls.Add(this.gameStatusBar);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Call Break";
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Main_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDoubleClick);
            this.gameStatusBar.ResumeLayout(false);
            this.gameStatusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public  System.Windows.Forms.MenuStrip menuStrip1;
        public  System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        public  System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        public  System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.StatusStrip gameStatusBar;
        public System.Windows.Forms.ToolStripStatusLabel gameStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem displayScoreToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel gameRoundStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playerNamesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem animationSpeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker guiUpdatebackgroundWorker;
    }
}