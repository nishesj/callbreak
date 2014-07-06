using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    /// <summary>
    /// static class for validating the game rule of Call Break
    /// </summary>
    static class GameRule
    {

        /// <summary>
        /// checks whether the card played by the player is valid or not in current scenario
        /// </summary>
        /// <param name="currentPlayer">The card player</param>
        /// <param name="playedCard">what card has the player chooosen to play </param>
        /// <param name="currentPot">pot of current running hand</param>
        /// <returns></returns>
        public static bool isValidCard(player currentPlayer,  SpadesCard playedCard, Pot currentPot)
        {
            // if pot is empty its the first card and is always valid
            if (currentPot.CardPile.Count == 0)
            {
                return true;
            }
            else
            {
                // get 1st played card
              
               
                
                
                SpadesCard first = (SpadesCard)currentPot.CardPile[0];
               
                // check if player has the same suit to play
                if (hasSuit(first.Suit, currentPlayer.Hand))
                {
                    if (first.Suit == playedCard.Suit)
                    {
                       
                        
                        if (first.Suit != CardSuit.Spades && currentPot.highestCard.Suit == CardSuit.Spades)
                        {
                            return true;
                        }
                        else
                        {
                            // check its higher than the higest card in the pile
                            if (playedCard.Rank == CardRank.Ace) return true;
                            else
                            {
                                if (playedCard.Rank > currentPot.highestCard.Rank) return true;
                                else
                                {
                                    // check if he has higher card in this hand 
                                    if (checkHandForHigherCard(currentPot.highestCard, currentPlayer.Hand))
                                    {
                                        // player has other higher card
                                        return false;
                                    }
                                    else
                                    {
                                        return true;
                                    }

                                }
                            }
                        }

                        

                    }
                    else return false;
                }
                // check if he has spades to play
                else if (hasSuit(CardSuit.Spades, currentPlayer.Hand))
                {

                    
                    if (checkHandForHigherCard(currentPot.highestCard, currentPlayer.Hand))
                    {
                        
                        if (currentPot.highestCard.Suit != CardSuit.Spades && playedCard.Suit == CardSuit.Spades) return true;
                        if (playedCard.Rank == CardRank.Ace && playedCard.Suit == CardSuit.Spades) return true;
                        if ((playedCard.Rank > currentPot.highestCard.Rank) && ((currentPot.highestCard.Suit == CardSuit.Spades) && (playedCard.Suit == CardSuit.Spades))) return true;
                        


                        return false;
                    }

                 
                   
                  // if no higher card play any thing     
                  return true;                                      
                    
                    
                }
                // if nothing play anyhitng
                else return true;



                
            }









        }


        /// <summary>
        /// checks tha current Hand for the higher card
        /// </summary>
        /// <param name="card">card to check againts </param>
        /// <param name="h">Current hand of the player</param>
        /// <returns></returns>
        public static bool checkHandForHigherCard(SpadesCard card,Hand h)
        {

            if (card.Suit == CardSuit.Spades && card.Rank == CardRank.Ace) return false;
         
            foreach (SpadesCard c in h.CardPile)
            {
               
                if (hasSuit(card.Suit, h))
                {
                    if (c.Suit == card.Suit)
                    {
                        // same suit found
                        if (c.Rank == CardRank.Ace) return true;
                        else if (c.Rank > card.Rank) return true;

                    }
                    // check for spades also 
                }
                else
                {
                    if (card.Suit != CardSuit.Spades)
                    {
                        if (c.Suit == CardSuit.Spades) return true; // if spades he has high card
                    }

                }



            }




            return false;
        }

        /// <summary>
        /// checks the hand if it has the passed suit or not
        /// </summary>
        /// <param name="suit">Suit of the card to check.</param>
        /// <param name="hand">Hand of the player to check in.</param>
        /// <returns></returns>
        public static bool hasSuit(CardSuit suit, Hand hand)
        {

            foreach (SpadesCard card in hand.CardPile)
            {
                if (Card.SuitFromCardIndex(card.CardIndex) == suit) return true;
            }
            return false;

        }



    }
}
