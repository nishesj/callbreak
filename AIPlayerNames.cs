using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cards
{
    public partial class AIPlayerNames : Form
    {
        public AIPlayerNames()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {

            // write in registry for later refrence 
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Games\\CallBreak");
            if (regKey != null)
            {
                regKey.SetValue("AIPlayer1", AI1TextBox.Text.Trim() == "" ? "AI - I" : AI1TextBox.Text.Trim());
                regKey.SetValue("AIPlayer2", AI2TextBox.Text.Trim() == "" ? "AI - II" : AI2TextBox.Text.Trim());
                regKey.SetValue("AIPlayer3", AI3TextBox.Text.Trim() == "" ? "AI - III" : AI3TextBox.Text.Trim());
                
                regKey.Close();
            }

            this.Dispose();



        }

        private void AIPlayerNames_Load(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Games\\CallBreak");
            if (regKey != null)
            {

                AI1TextBox.Text = regKey.GetValue("AIPlayer1", "AI - I").ToString();
                AI2TextBox.Text = regKey.GetValue("AIPlayer2", "AI - II").ToString();
                AI3TextBox.Text = regKey.GetValue("AIPlayer3", "AI - III").ToString();
                regKey.Close();
            }
        }
    }
}
