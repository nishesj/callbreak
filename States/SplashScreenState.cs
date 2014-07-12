using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cards
{
    class SplashScreenState : IGameState, IDisposable
    {

        Timer timer = new Timer();
        int miliseconds = 3000;
        SplashScreen screen = new SplashScreen();
        public void Render()
        {
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = miliseconds;
            timer.Start();
            screen.ShowDialog();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();           
            screen.Close();
        }

        public void Dispose()
        {
            screen.Dispose();
            timer.Dispose();
        }
    }
}