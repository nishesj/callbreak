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
    public partial class NameBox : Form
    {
        public string playerName = "";
        public static NameBox nameBox;
        public bool quit = false;
        
        private NameBox()
        {
            InitializeComponent();
            
        }

        public static NameBox getNameBox()
        {
            if (nameBox == null){
                nameBox =  new NameBox();
            }

            return nameBox;
            
         
        }

        public DialogResult ShowDialog(out string outstring)
        {
            DialogResult ret;
            ret = this.ShowDialog();
            outstring = this.playerNameTextBox.Text;
           
            

            return ret;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();          
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NameBox_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  this.Dispose();
           // Application.Exit();
        }

        private void playerNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (playerNameTextBox.Text.Trim().Length > 0) okButton.Enabled = true;
            else okButton.Enabled = false;
        }

        private void playerNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (playerNameTextBox.Text.Trim().Length > 0)
                {
                    this.DialogResult = DialogResult.OK;

                }
            }
        }

        private void NameBox_Load(object sender, EventArgs e)
        {

            //get name from registry if present 
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Games\\CallBreak");
            if (regKey != null)
            {
                string playerName = regKey.GetValue("PlayerName", "").ToString();
                playerNameTextBox.Text = playerName;
                regKey.Close();
            }


        }
    }
}