using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace Cards
{
    [Serializable]
    public class AIPlayer : player
    {
    
        public AIPlayer(int id) : base(id, false) { }

        public bool setName(string name)
        {
            if (name.Length > 1 )
            {
                this.Name = name;
                return true;
            }
            return false;

        }

        public override void InitializeName()
        {

            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Games\\CallBreak");
            if (regKey != null)
            {
                switch (this.ID)
                {
                    case 0:
                        setName(regKey.GetValue("AIPlayer0", "AI - 0").ToString());
                        break;
                    case 1:
                        setName(regKey.GetValue("AIPlayer1", "AI - I").ToString());
                        break;
                    case 2:
                        setName(regKey.GetValue("AIPlayer2", "AI - II").ToString());
                        break;
                    case 3:
                        setName(regKey.GetValue("AIPlayer3", "AI - III").ToString());
                        break;
                }
            
               
                regKey.Close();
            }
            else
            {

                switch (this.ID)
                {
                    case 1:
                        setName("AI-1");
                        break;
                    case 2:
                        setName("AI-2");
                        break;
                    case 3:
                        setName("AI-3");
                        break;
                }
            }
        }

        /// <summary>
        /// Bid Function of AI player 
        /// REFRENCE :: http://members.aol.com/loflyer/spades/#bidint
        /// </summary>
        /// <param name="gData"></param>
        /// <returns></returns>
        public override int Bid(GameData gData)
        {


            float count = 0;
          
            // NON SPADE BID
            
            int spadesCount = NumberOfCardInSuit(CardSuit.Spades, Hand);
            int ShortSuitBid = 0;

            foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
            {
                if (cardSuit != CardSuit.Spades)
                {
                    int numberOfCardInSuit = NumberOfCardInSuit(cardSuit, Hand);
                    if (numberOfCardInSuit > 0)
                    {
                        // count +1 for card if its ace or king
                        if (HasCard(cardSuit, CardRank.Ace, Hand)) count = count + 1;
                        if (HasCard(cardSuit, CardRank.King, Hand)) count = count + 1;
                        if (RankBelow(cardSuit, Hand, CardRank.King) >= 4) count = count - 1;
                        else if (RankBelow(cardSuit, Hand, CardRank.King) == 3) count = count - (1 / 2);
                        if (RankBelow(cardSuit, Hand, CardRank.Ace) >= 6) count = count - 1;
                        else if (RankBelow(cardSuit, Hand, CardRank.Ace) == 5) count = count - (1 / 2);
                        if (HasCard(cardSuit, CardRank.King, Hand) && numberOfCardInSuit == 1) count = count - 1;
                        if (HasCard(cardSuit, CardRank.Ace, Hand) && HasCard(cardSuit, CardRank.Queen, Hand)) count = count + (1 / 2);
                    }
                    if (numberOfCardInSuit <= 2) ShortSuitBid = ShortSuitBid + (3 - numberOfCardInSuit);




                }
            }




            int tempSpadesCount = 0;
            int spadesBid = 0;
            if (spadesCount > 0)
            {

                int spadesCountBaseRank = 15 - spadesCount;

                tempSpadesCount = RankAbove(CardSuit.Spades, Hand, (CardRank)spadesCountBaseRank);


                int R = numberOfMissingSpades(spadesCountBaseRank + 1);

                int tempSubstract = 0;
                if (R > 0) tempSubstract = ShortSuitBid - R;

                spadesBid = (tempSubstract >= ShortSuitBid) ? ShortSuitBid : tempSubstract;

            }

            count = count + tempSpadesCount + spadesBid;
                        
            if (count <= 0) count = 1; // minimum 1 bid must be there             
            return (int)count;
            
        }
        #region BIDDING FUNCTION

        private bool HasCard(CardSuit suit, CardRank rank, Hand hand)
        {

            foreach (SpadesCard card in hand.CardPile)
            {
                if (card.Rank == rank && card.Suit == suit)
                {
                    return true;
                }
            }
            return false;
        }

        private int numberOfMissingSpades(int spadesRankToBid)
        {

            int missingSpades = 0;

            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                if ((int)rank > spadesRankToBid || (int)rank == 0)
                {
                    // search the hand for this rank 
                    bool missing = true;
                    foreach (SpadesCard c in Hand.CardPile)
                    {
                        if (rank == c.Rank && c.Suit == CardSuit.Spades) missing = false;
                    }
                    if (missing) missingSpades++;
                }
            }

            return missingSpades;


        }


        private int NumberOfCardInSuit(CardSuit suit, Hand Hand)
        {
            int count = 0;
            foreach (SpadesCard card in Hand.CardPile)
            {
                if (card.Suit == suit)
                {
                    count++;
                }
            }
            return count;
        }

        private int RankBelow(CardSuit cardSuit, Hand hand, CardRank rank)
        {

            int cardCount = 0;
            foreach (SpadesCard card in hand.CardPile)
            {
                if (card.Suit == cardSuit)
                {
                    if (card.Rank < rank || card.Rank != CardRank.Ace)
                    {
                        cardCount++;
                    }
                }
            }

            return cardCount;

        }

        private int RankAbove(CardSuit cardSuit, Hand hand, CardRank rank)
        {

            int cardCount = 1;

            foreach (SpadesCard card in hand.CardPile)
            {
                if (card.Suit == cardSuit)
                {
                    if (card.Rank >= rank || card.Rank == CardRank.Ace)
                    {
                        cardCount++;
                    }
                }
            }

            return cardCount;
        }

        
        

        
        #endregion

        /// <summary>
        /// AI makeMove function 
        /// It now randomly chooses a valid card.
        /// ToDO :: make this better
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        public override SpadesCard makeMove(GameData gameData)
        {

            RandomAIStrategy strategy = new RandomAIStrategy();
            return strategy.makeMove(gameData);



            //base.makeMove();
        }



    }


}
