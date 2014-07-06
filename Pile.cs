using System;
using Cards;

using System.Runtime.InteropServices;

namespace Cards
{
	
    
    /// <summary>
    /// Event Delegate when Card is added on the pot
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// </summary>
    public delegate void PotAddEventHandler(object sender, PileEventArgs e);

    /// <summary>
    /// Event argument passed when POT ADD event is raised
    /// Its a SpadeCard
    /// </summary>
    public class PileEventArgs : EventArgs
    {
        public SpadesCard card;
        public PileEventArgs(SpadesCard c)
        {
            card = c;
        }
    }

    /// <summary>
    /// Class to represent a pile of cards
    /// </summary>
    [Serializable]
    public class Pile
	{
		private System.Collections.ArrayList _cardPile;
		protected int MaxNum=0;

        /// </summary>
        [field: NonSerialized] public event PotAddEventHandler PotAdd;
        public virtual void OnPotAdd(PileEventArgs e)
        {
            if (PotAdd != null)
                PotAdd(this, e);
        }

	    public  Pile()
		{
			//
			// TODO: Add constructor logic here
			//
		    CardPile=new System.Collections.ArrayList();
            
		
		}
        public Pile(int maxNumber)
        {
            CardPile = new System.Collections.ArrayList(maxNumber);
        }
		

     

        public void AddCard(SpadesCard c)
        {
            CardPile.Add(c);
            OnPotAdd(new PileEventArgs(c));
        }


        public void AddCard(SpadesCard c, int index)
        {
            CardPile.Insert(index, c);
            OnPotAdd(new PileEventArgs(c));
        }
        


        public bool containsCard(SpadesCard c)
        {
            return CardPile.Contains(c);
        }


	
    
        public int Count()
		{
			return CardPile.Count;
		}

        public System.Collections.ArrayList CardPile
        {
            get
            {
                return _cardPile;
            }
            set
            {
                _cardPile = value;
            }
        }

       
        public bool removeCard(SpadesCard c)
        {
            if (CardPile.Contains(c) == true)
            {
                CardPile.Remove(c);
                return true;
            }
            return false;
        }

	}// class 
}// name space