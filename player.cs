using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{

    [Serializable]
    public class player
    {
        private Hand _hand = new Hand();
        private int _id = -1;
        private bool isHuman = false;
        private int _totalBid = 0;
        private string _name = "";
        private int _totalTrick = 0;
        private float _totalScore = 0;

        public float TotalScore
        {
            get { return _totalScore; }
            set { _totalScore = value; }
        }

        public int TotalTrick
        {
            get { return _totalTrick; }
            set { _totalTrick = value; }
        }
        public bool _activePlayer = false;

        private int _positionX= 0;
        private int _positionY = 0;

        /// <summary>
        /// Player's Y coordinate on the screen
        /// </summary>
        public int PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }


        /// <summary>
        /// Player X coordinate on the screen
        /// </summary>
        public int PositionX
        {
            set
            {
                _positionX = value;
            }
            get
            {
                return _positionX;
            }
        }


        /// <summary>
        /// sets player as active player ( its his turn to play)
        /// </summary>
        public bool IsActivePlayer
        {
            set
            {
                _activePlayer = value;
            }
            get
            {
                return _activePlayer;
            }
        }
        /// <summary>
        /// sets or gets Total number of bid the player made in a round
        /// </summary>
        public int TotalBid
        {
            set
            {
                _totalBid = value;
            }
            get
            {
                return _totalBid;
            }

        }
        /// <summary>
        /// sets or gets player Name
        /// </summary>
        public string Name
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name.Substring(0, Math.Min(_name.Length, 10)); // if name is greater than 10 trucncate it
            }
        }

        /// <summary>
        /// sets or gets the current Hand of the player
        /// </summary>
        public Hand Hand
        {

            get { return _hand; }
            set { _hand = value; }
        }

        /// <summary>
        /// sets or gets the ID of the player
        /// </summary>
        public int ID
        {
            get { return _id; }
            set  { _id = value;}
        }

        /// <summary>
        /// gets or sets if player is Human or AI ( not useful )
        /// Because there is class called AIPLAYER AND HUMANPLAYER 
        /// </summary>
        public bool IsHuman
        {
            get
            {
                return isHuman;
            }
            set
            {
                isHuman = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Player ID</param>
        /// <param name="isHuman">isHuman or not</param>
       
        public player(int id,bool isHuman)
        {
          //  Hand = hand;
            ID = id;
            IsHuman = isHuman;
        }

       
        /// <summary>
        /// This method is overloaded by subclass to initialize the Name of the player
        /// </summary>
        public virtual void InitializeName(){}

        /// <summary>
        /// Must be overridden by sub class to implement the bidding function i.e AI and Human 
        /// 
        /// </summary>
        /// <param name="gData">GameData Structure</param>
        /// <returns></returns>
        public virtual int Bid(GameData gData) { return 0; }
        /// <summary>
        /// Must be overridden by subclass
        /// </summary>
        /// <param name="gameData">GameData structure</param>
        /// <returns></returns>
        public virtual SpadesCard makeMove(GameData gameData)
        {
            return null;
        }

        /// <summary>
        /// Reset Score, Bid and Trick of player
        /// </summary>

        public void  ResetPlayerData()
        {
            TotalBid = 0;
            TotalScore = 0;
            TotalTrick = 0;


        }


    }
}
