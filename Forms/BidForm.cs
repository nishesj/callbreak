using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Cards
{
    public partial class BidForm : Form
    {
        public GameData gameData;
        public BidForm(GameData gData)
        {
            InitializeComponent();       
            gameData = gData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScoreDisplay SD = new ScoreDisplay(ref gameData);
            SD.ShowDialog();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar t = (TrackBar)sender;
            BidCounter.Text = t.Value.ToString();
        }
    }
}
