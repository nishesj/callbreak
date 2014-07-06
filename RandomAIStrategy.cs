using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    class RandomAIStrategy : IAIStrategy
    {
        /// <summary>
        /// AI makeMove function 
        /// It now randomly chooses a valid card.
        /// ToDO :: make this better
        /// </summary>
        /// <param name="gameData"></param>
        /// <returns></returns>
        /// 

        Random rand;
        public SpadesCard makeMove(GameData gameData)
        {
            Pot pot = gameData.CurrentPot;
            List<SpadesCard> validCardList = new List<SpadesCard>();
            player currentPlayer = gameData.CurrentPlayerList[gameData.ActivePlayerId];
            // make some move
            // get all valid card move to narrow down the search scope first 

            if (pot.Count() == 0)
            {
                foreach (SpadesCard card in currentPlayer.Hand.CardPile)
                {
                    validCardList.Add(card);
                }// any hand card is valid to play ..
            }
            else
            {
                foreach (SpadesCard card in currentPlayer.Hand.CardPile)
                {
                    if (GameRule.isValidCard(currentPlayer, card, pot))
                    {
                        validCardList.Add(card);
                    }
                }
            }

            int count = validCardList.Count;


            // throw some random card now 
            if (count > 0)
            {
                DateTime x = DateTime.Now;
                float mins = x.Minute;
                float secs = x.Second;
                float hour = x.Hour;
                if (gameData.ActivePlayerId != 0)
                    rand = new Random(Convert.ToInt32(((secs * mins) + hour) / gameData.ActivePlayerId));
                else
                    rand = new Random(Convert.ToInt32(((secs * mins) + hour)));
              
                SpadesCard c;
               
                    int index = rand.Next(0, validCardList.Count-1);
                    c = (SpadesCard)validCardList[index];
                    return c;

            }
            else return null;

        }


    }
}
