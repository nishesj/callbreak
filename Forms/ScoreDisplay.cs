using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cards
{
    public partial class ScoreDisplay : Form
    {

        List<player> playerList;
        float[,] scoreArray;
        int currentRound = 0;
        public ScoreDisplay()
        {
            InitializeComponent();
        }


        public ScoreDisplay(ref GameData gameData)
        {
            this.currentRound = gameData.CurrentRound;
            playerList = gameData.CurrentPlayerList;
            scoreArray = gameData.score;
            InitializeComponent();
        }

        private void ScoreDisplay_Paint(object sender, PaintEventArgs e)
        {


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.ResetClip();
            e.Graphics.Clear(Color.DarkGray);
            drawPlayerNames(e.Graphics);
            drawPlayerScores(e.Graphics);
            e.Graphics.Dispose();
        }

        private void drawTotal(Graphics graphics)
        {
            // draw a line 



            // throw new NotImplementedException();
        }

        private void drawPlayerScores(Graphics graphics)
        {

            String drawString = "";
            PointF drawPoint;
            StringFormat sf;
            // Create font and brush.
            Font drawFont = new Font("Arial", 10f, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            drawPoint = new PointF(0, 0);
            sf = new StringFormat();


            int y = 45;


            for (int i = 0; i < this.currentRound; i++)
            {
                int inc = 20;

                foreach (player p in playerList)
                {
                    drawPoint = new PointF(inc, y);

                    if (scoreArray[i, p.ID] < 0) drawBrush = new SolidBrush(Color.Red);
                    else drawBrush = new SolidBrush(Color.Black);
                    drawString = scoreArray[i, p.ID].ToString();
                    graphics.DrawString(drawString, drawFont, drawBrush, drawPoint, sf);
                    inc = inc + 100;
                }
                y = y + 40;

            }

            // display total score 

            graphics.DrawLine(new Pen(drawBrush), new Point(20, y + 10), new Point(350, y + 10));

            int incy = y + 20;
            int incx = 20;
            foreach (player p in playerList)
            {

                double total = 0;
                for (int k = 0; k < this.currentRound; k++)
                {



                    drawPoint = new PointF(incx, incy);
                    total = Math.Round((double)(total + scoreArray[k, p.ID]), 1);
                    if (total < 0) drawBrush = new SolidBrush(Color.Red);
                    else drawBrush = new SolidBrush(Color.Black);
                    p.TotalScore = (float)total;
                    drawString = total.ToString();


                }
                graphics.DrawString(drawString, drawFont, drawBrush, drawPoint, sf);

                incx = incx + 100;

            }



        }

        private void drawPlayerNames(Graphics graphics)
        {

            String drawString = "";
            PointF drawPoint;
            StringFormat sf;
            // Create font and brush.
            Font drawFont = new Font("Arial", 12f, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            drawPoint = new PointF(0, 0);
            sf = new StringFormat();

            int inc = 10;
            foreach (player p in playerList)
            {
                drawPoint = new PointF(inc, 15);
                drawString = p.Name;
                graphics.DrawString(drawString, drawFont, drawBrush, drawPoint, sf);
                inc = inc + 100;
            }

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
