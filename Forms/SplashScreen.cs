using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Resources;

namespace Cards
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
          //  SetStyle(ControlStyles.SupportsTransparentBackColor, true);
           // this.BackColor = Color.Transparent;
            this.StartPosition = FormStartPosition.CenterScreen;
           
            InitializeComponent();
          
            
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
          
        }

     
    }
}
