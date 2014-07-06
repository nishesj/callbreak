using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;

namespace Cards
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

           
           
            using (Main mainForm = new Main())
            {
                mainForm.Initialize();
                mainForm.Show();
                Application.Run(mainForm);
            }
           
        }
    }
}
