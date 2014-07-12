using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Cards.GUI
{
    static class SpadesGui
    {

        static Card card = new Card();
        public static CardBack CardBack = CardBack.Heart;
        static Graphics G = null;
        static List<player> SpadesPlayer;
        static Pot Pot;
        static GameData gameData;
        static bool backFace = true;
        static bool cardFaceCheat = false;

        public static bool CardFaceCheat
        {
            get { return SpadesGui.cardFaceCheat; }
            set { SpadesGui.cardFaceCheat = value; }
        }

        public static bool BackFace
        {
            get { return SpadesGui.backFace; }
            set { SpadesGui.backFace = value; }
        }
     
        public static bool Initialize(ref GameData gData){
            try
            {
              //  targetForm = _targetForm;
                gameData = gData;
                SpadesPlayer = gData.CurrentPlayerList;//spadesPlayer;
                Pot = gData.CurrentPot;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            return true;

        }

        /// <summary>
        /// Refresh the whole GUI with current information about the card and player
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="gData"></param>
        public static void refreshBoard(Graphics graphics, ref GameData gData)
        {
            G = graphics;
            gameData = gData;
            foreach (player p in SpadesPlayer)
            {                
                 drawPlayerHand(p);                
                 drawPlayerName(p);
                if (gameData.GameState != GameState.INTRO) drawPlayerBidsAndTricks(p);
            
            }

            drawPot();       

        }

        /// <summary>
        /// Draws the card in the pot 
        /// card position are defined within the card class itself 
        /// so the card position must be updated when adding to pot for display 
        /// </summary>
        public static void drawPot()
        {
            int potCount = Pot.CardPile.Count;
            if ( potCount > 0)
            {
                foreach (Cards.SpadesCard pot in Pot.CardPile)
                {

                    card.Begin(G);
                    int xnew = pot.CardPositionX;
                    int ynew = pot.CardPositionY;
                    card.DrawCard(new Point(xnew, ynew), pot.CardIndex);
                    card.End();

                }
                
            }
        }

        /// <summary>
        /// Draws player Bid and tricks on the screen 
        /// </summary>
        /// <param name="p">Player to draw </param>
        
        public static void drawPlayerBidsAndTricks(player p)
        {

               // Create font and brush.
               
            
                Font drawFont = new Font("verdana", 8.5f, FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                StringFormat sf;

                // Create point for upper-left corner of drawing.
                PointF drawPoint = new PointF(0,0);
                sf = new StringFormat();
                // draw with respective to client 
                
                switch (Cards.Main.playerPositions[p.ID].seat)
                {
                    case "S":
                        //drawPoint = new PointF(p.PositionX + 50, p.PositionY - 20);
                        drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x + 50, Cards.Main.playerPositions[p.ID].y - 20);
                        
                        break;
                    case "W":
                        sf = new StringFormat(StringFormatFlags.DirectionVertical);
                        drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x + 70, Cards.Main.playerPositions[p.ID].y + 60);
                        break;    
                    case "N":
                        drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x + 50, Cards.Main.playerPositions[p.ID].y + 100);
                        break;
                    case "E":
                        sf = new StringFormat(StringFormatFlags.DirectionVertical);
                        drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x - 25, Cards.Main.playerPositions[p.ID].y + 60);
                        break;
                }
               
                G.DrawString("Bid : " + p.TotalBid.ToString() +"        " + "Tricks : " + p.TotalTrick.ToString(), drawFont, drawBrush, drawPoint,sf);
            
        }


       

        /// <summary>
        /// draws the card in player hands
        /// cards position must be updated before calling this function 
        /// </summary>
        /// <param name="p">player of which to  draw the hand </param>
        public static void drawPlayerHand(player p)
        {

            cardFaceCheat = false;
            if (CardFaceCheat == false)
            {
                if (p.ID == Cards.Main.boardBelongsTo) BackFace = false; else BackFace = true;
            }
            else
            {
                // if cheat enabled 
                BackFace = false;
            }

            
            int xinc = 0;
            int yinc = 0;
  
            foreach (SpadesCard c in p.Hand.CardPile)
            {
                card.Begin(G);
                int xnew = Main.playerPositions[p.ID].x + xinc;
                int ynew = Main.playerPositions[p.ID].y + yinc;

                if (c.MoveUp == true) ynew = ynew - 5;
                if (!BackFace)   card.DrawCard(new Point(xnew, ynew), c.CardIndex);
                else card.DrawCardBack(new Point(xnew, ynew), CardBack);                   
                
                card.End();

                if (Main.playerPositions[p.ID].seat == "N" || Main.playerPositions[p.ID].seat == "S") xinc = xinc + 15;
                else yinc = yinc + 15;
                
            }
        
          

        }

        /// <summary>
        /// draws player name on the screen
        /// </summary>
        /// <param name="p">Player to draw the name of.</param>
        public static void drawPlayerName(player p){

            //draw name also 
           
            String drawString = p.Name;
            PointF drawPoint;
            StringFormat sf;

             //  if (p.IsActivePlayer) 
            // Create font and brush.
             Font drawFont = new Font("Arial", 8.5f,FontStyle.Bold);
             SolidBrush drawBrush = new SolidBrush(Color.Black);
             drawPoint = new PointF(0, 0);
             sf = new StringFormat();
             switch (Cards.Main.playerPositions[p.ID].seat)
             {
                 case "S":
                     {
                         sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                         drawPoint = new PointF((Cards.Main.playerPositions[p.ID].x), Cards.Main.playerPositions[p.ID].y + 85);
                         break;
                     }
                 case "W":
                     {
                         drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x, Cards.Main.playerPositions[p.ID].y - 15);
                         break;
                     }
                 case "N":
                     {
                         drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x + ((13 * 15) + 60), Cards.Main.playerPositions[p.ID].y);
                         break;
                     }
                 case "E":
                     {
                         drawPoint = new PointF(Cards.Main.playerPositions[p.ID].x, Cards.Main.playerPositions[p.ID].y + (13 * 15) + 85);
                         break;
                     }
             }


         
             G.DrawString(drawString, drawFont, drawBrush, drawPoint, sf);
        }


        public static void  Refresh()
        {
            refreshBoard(G, ref gameData);

        }
   



    }
}
