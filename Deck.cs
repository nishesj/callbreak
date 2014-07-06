using System;
namespace Cards
{
	/// <summary>
	/// Nothing more than a pile of 52 standard cards
	/// </summary>
	public class Deck : Pile
	{
        const int cardPerDeck = 52;
        
        
        public Deck()
		{                    
            foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardRank cardRank in Enum.GetValues(typeof(CardRank)))
                {
                    CardPile.Add(new SpadesCard(Card.ToCardIndex(cardSuit,cardRank)));
                }

            }


		}
		
	}
}