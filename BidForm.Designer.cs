namespace Cards
{
    partial class BidForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.BidTrackBar = new System.Windows.Forms.TrackBar();
            this.BidCounter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BidTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(175, 124);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Bid";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = " Please move the scrollbar to make your bid\r\n";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 124);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = " View Score";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // BidTrackBar
            // 
            this.BidTrackBar.Location = new System.Drawing.Point(12, 81);
            this.BidTrackBar.Maximum = 13;
            this.BidTrackBar.Minimum = 1;
            this.BidTrackBar.Name = "BidTrackBar";
            this.BidTrackBar.Size = new System.Drawing.Size(226, 45);
            this.BidTrackBar.TabIndex = 4;
            this.BidTrackBar.Value = 1;
            this.BidTrackBar.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // BidCounter
            // 
            this.BidCounter.AutoSize = true;
            this.BidCounter.BackColor = System.Drawing.Color.Chocolate;
            this.BidCounter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BidCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BidCounter.Location = new System.Drawing.Point(107, 45);
            this.BidCounter.Name = "BidCounter";
            this.BidCounter.Size = new System.Drawing.Size(27, 29);
            this.BidCounter.TabIndex = 5;
            this.BidCounter.Text = "1";
            // 
            // BidForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(244, 159);
            this.ControlBox = false;
            this.Controls.Add(this.BidCounter);
            this.Controls.Add(this.BidTrackBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BidForm";
            this.Text = "Make your bid";
            ((System.ComponentModel.ISupportInitialize)(this.BidTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Label BidCounter;
        public System.Windows.Forms.TrackBar BidTrackBar;

    }
}