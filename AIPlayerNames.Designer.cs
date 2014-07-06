namespace Cards
{
    partial class AIPlayerNames
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
            this.AI1TextBox = new System.Windows.Forms.TextBox();
            this.AI2TextBox = new System.Windows.Forms.TextBox();
            this.AI3TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AI1TextBox
            // 
            this.AI1TextBox.Location = new System.Drawing.Point(133, 12);
            this.AI1TextBox.MaxLength = 10;
            this.AI1TextBox.Name = "AI1TextBox";
            this.AI1TextBox.Size = new System.Drawing.Size(100, 20);
            this.AI1TextBox.TabIndex = 0;
            this.AI1TextBox.Text = "AI -  I";
            // 
            // AI2TextBox
            // 
            this.AI2TextBox.Location = new System.Drawing.Point(132, 38);
            this.AI2TextBox.MaxLength = 10;
            this.AI2TextBox.Name = "AI2TextBox";
            this.AI2TextBox.Size = new System.Drawing.Size(100, 20);
            this.AI2TextBox.TabIndex = 1;
            this.AI2TextBox.Text = "AI - II";
            // 
            // AI3TextBox
            // 
            this.AI3TextBox.Location = new System.Drawing.Point(132, 64);
            this.AI3TextBox.MaxLength = 10;
            this.AI3TextBox.Name = "AI3TextBox";
            this.AI3TextBox.Size = new System.Drawing.Size(100, 20);
            this.AI3TextBox.TabIndex = 2;
            this.AI3TextBox.Text = "AI -III";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "First Opponent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Second Opponent";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Third Opponent";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(132, 90);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(100, 23);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "Ok";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // AIPlayerNames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 124);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AI3TextBox);
            this.Controls.Add(this.AI2TextBox);
            this.Controls.Add(this.AI1TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AIPlayerNames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AIPlayerNames";
            this.Load += new System.EventHandler(this.AIPlayerNames_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AI1TextBox;
        private System.Windows.Forms.TextBox AI2TextBox;
        private System.Windows.Forms.TextBox AI3TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OKButton;
    }
}