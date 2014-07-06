using System;

namespace Cards
{
	/// <summary>
	/// Pile of card in players Hand for playing 
	/// </summary>
    [Serializable]
	public class Hand : Pile
	{
		public Hand()
		{
			// 
			// TODO: Add constructor logic here
			//
		}
		
	
		public SpadesCard GetCard(int position)
		{
			//return the card at the given position
			
			if (position >= 0 && position < CardPile.Count)
				return (SpadesCard)CardPile[position];
			else
				return null;
		}


       









	}// class
}// namespace
