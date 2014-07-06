using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cards
{
    public partial class LooserForm : Form
    {
        public LooserForm()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
