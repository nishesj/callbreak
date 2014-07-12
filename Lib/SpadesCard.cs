using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Cards
{
    [Serializable]
    public class SpadesCard
    {

        int _cardPositionX = 0;
        int _cardPositionY = 0;

    
       
        
        /// <summary>
        /// sets or get card Suit (i.e CLUBS,DIAMONDS,HEART,SPADES)
        /// </summary>
        /// 
        public CardSuit Suit;
        
        /// <summary>
        /// sets or gets card Rank ( i.e ACE,TWO,THREE etc..)
        /// </summary>
               
        public CardRank Rank;

        bool _moveUp = false;
        /// <summary>
        ///  For animation sake 
        ///  if set the card is displayed a bit up than normal position
        /// </summary>
        public bool MoveUp
        {
            set
            {
                _moveUp = value;
            }
            get
            {
                return _moveUp;
            }
        }

        /// <summary>
        /// gets or sets X Co-ordinate of the card on the screen
        /// </summary>
        public int CardPositionX
        {
            get
            {
                return _cardPositionX;
            }
            set
            {
                _cardPositionX = value;
            }
        }
        /// <summary>
        /// gets or sets Y coordinate of the card on the screen
        /// </summary>
        public int CardPositionY
        {
            set
            {
                _cardPositionY = value;
            }
            get
            {
                return _cardPositionY;
            }
        }

        private int _cardindex;
      

        /// <summary>
        /// constructor of spades card
        /// </summary>
        /// <param name="i">Card Index</param>
        public SpadesCard(int i)
            : base()
        {
            if (i < 0 || i > 52)
            {
                throw new ApplicationException("Wrong value for card initilization.");
            }
           _cardindex = i;
           Suit = Card.SuitFromCardIndex(i);
           Rank = Card.RankFromCardIndex(i);
        }
       

        /// <summary>
        /// gets or sets the card index 
        /// </summary>
        public int CardIndex
        {
            get
            {
                return _cardindex;
            }
            set
            {
                if (value < -1 || value > 52)
                {
                    throw new ApplicationException("Wrong value for card initilization.");
                }
                _cardindex = value;
            }
        }
            




    }
}
