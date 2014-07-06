using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Cards
{
    /// <summary>
    /// states of the game 
    /// </summary>

    class IntroState : IGameState
    {
        string Name;
       public void Render()
        {


            

           // set player name
            NameBox box = NameBox.getNameBox();
            if (box.ShowDialog() == DialogResult.OK)
            {
                
                // write in registry for later refrence :-)
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Games\\CallBreak");
                if (regKey != null)
                {
                    regKey.SetValue("PlayerName", box.playerNameTextBox.Text.Trim());
                    regKey.Close();
                }
                //int playerID = Convert.ToInt32(box.playerID.Text);
                Cards.Main.playerName = box.playerNameTextBox.Text.Trim();
              //  Cards.Main.boardBelongsTo = playerID;
            }
            else
            {
                Environment.Exit(0);
               
            }
        }


       public IntroState(ref string playerName)
        {
            Name = playerName;

        }

    }



   // bidding state for each player 
    public class BidEventArgs : EventArgs
    {
        private int _PlayerID;

        public int PlayerID
        {
            get { return _PlayerID; }
            set { _PlayerID = value; }
        }

        public BidEventArgs(int ID)
        {
            PlayerID = ID;
        }

    }

    // event that occurs after bid to refresh the board or whatever
    public delegate void BidEventHandler(object sender, BidEventArgs e);
    public delegate void BeforeBidEventHandler(object sender, BidEventArgs e);
    class BiddingState : IGameState
    { 
        List<player> playerList;
        GameData gameData;
        public event BidEventHandler Bidded;
        public event BeforeBidEventHandler BeforeBid;

        public virtual void OnBidded(BidEventArgs e)
        {
            if (Bidded != null)
                Bidded(this, e);
        }

        public virtual void OnBeforeBid(BidEventArgs e)
        {
            if (BeforeBid != null)
            {
                BeforeBid(this, e);
            }
        }
        
        public void Render()
        {
           

            // bid in sequence 

            int bidStarter = gameData.ActivePlayerId;
           
                BidEventArgs bidArg = new BidEventArgs(bidStarter);
                if (gameData.CurrentPlayerList[bidStarter].TotalBid > 0) return;
                OnBeforeBid(bidArg);
                gameData.CurrentPlayerList[bidStarter].TotalBid = gameData.CurrentPlayerList[bidStarter].Bid(gameData);
                OnBidded(bidArg);
         
           
        }
        public BiddingState(ref GameData gData)
        {
            gameData = gData;
            playerList = gameData.CurrentPlayerList;
            
        }

    }

   


    



}
