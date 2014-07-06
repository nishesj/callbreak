using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Cards
{



    
    /// <summary>
    /// static class to help shuffle, sort card piles etc
    /// </summary>

    public static class CardUtility
    {
        public static byte[] SerializeObject(object pObjectToSerialize)
        {
            BinaryFormatter lFormatter = new BinaryFormatter();
            MemoryStream lStream = new MemoryStream();
            lFormatter.Serialize(lStream, pObjectToSerialize);
            byte[] lRet = new byte[lStream.Length];
            lStream.Position = 0;
            lStream.Read(lRet, 0, (int)lStream.Length);
            lStream.Close();
            return lRet;
        }
        public static T DeserializeObject<T>(byte[] pData)
        {
            if (pData == null)
                return default(T);
            BinaryFormatter lFormatter = new BinaryFormatter();
            MemoryStream lStream = new MemoryStream(pData);
            object lRet = lFormatter.Deserialize(lStream);
            lStream.Close();
            return (T)lRet;
        } 




        #region Static Helpers


        /// <summary>
        /// shuffles the card pile 
        /// </summary>
        /// <param name="pile">Pile of card to shuffle</param>
        /// <returns></returns>

        public static Pile ShufflePile(Pile pile)
        {

            System.Collections.ArrayList CardPile = (System.Collections.ArrayList)pile.CardPile;

            if (CardPile.Count > 0)
            {
                //shuffle the cards
                System.Collections.ArrayList newArray = new System.Collections.ArrayList();
                bool[] used = new bool[CardPile.Count];

                for (int j = 0; j < CardPile.Count; j++)
                {
                    used[j] = false;
                }

                int seed = DateTime.Now.Hour + DateTime.Now.Minute +DateTime.Now.Second +DateTime.Now.Millisecond;

                Random rnd = new Random(seed);
                int iCount = 0;
                int iNum;

                while (iCount < CardPile.Count)
                {
                    iNum = rnd.Next(0, CardPile.Count); // between 0 and 51

                    if (used[iNum] == false)
                    {
                        newArray.Add(CardPile[iNum]);
                        used[iNum] = true;
                        iCount++;
                    }
                }
                // End of Shuffle Routine

                // Load original array with shuffled array
                CardPile = newArray;

            }

            pile.CardPile = CardPile;
            return pile;


        }
        /// <summary>
        /// Sorts the card by their suit
        /// </summary>
        /// <param name="pile"></param>
        /// <returns></returns>
        public static Pile SortBySuit(Pile pile)
        {
            System.Collections.ArrayList newHand = new System.Collections.ArrayList();

            System.Collections.ArrayList CardPile = (System.Collections.ArrayList)pile.CardPile;


            while (CardPile.Count > 0)
            {
                int pos = 0;  // Position of minimal card.
                SpadesCard minSpadescard = (SpadesCard)CardPile[0];  // Minimal card.
                int mincard = (int)minSpadescard.CardIndex;
                for (int i = 1; i < CardPile.Count; i++)
                {
                    SpadesCard nextSpadescard = (SpadesCard)CardPile[i];
                    int nextcard = nextSpadescard.CardIndex;
                    if (Card.SuitFromCardIndex(nextcard) < Card.SuitFromCardIndex(mincard)
                        ||
                        (Card.SuitFromCardIndex(nextcard) == Card.SuitFromCardIndex(mincard) && Card.RankFromCardIndex(nextcard) < Card.RankFromCardIndex(mincard))
                       )
                    {
                        pos = i;
                        mincard = nextcard;
                        minSpadescard = nextSpadescard;
                    }
                }
                CardPile.RemoveAt(pos);
                newHand.Add(minSpadescard);
            }
            CardPile = newHand;
            pile.CardPile = CardPile;

            return pile;

        }

       

        #endregion





    }
}
