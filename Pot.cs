using System;
using System.Collections.Generic;
using System.Text;

namespace Cards
{
    [Serializable]
    public struct SpadesPot
    {
        public SpadesCard card;
        public player potter;
    }

   [Serializable]
   public  class Pot : Pile
    {


       public player highestPlayer = null;
       public SpadesCard highestCard = null;


       public List<SpadesPot> potList = new List<SpadesPot>();
       
       public void AddPot(SpadesCard c, player p)
       {

           this.AddCard(c);
           SpadesPot newPot = new SpadesPot();
           newPot.card = c;
           newPot.potter = p;
           setHighest(c,p);
           potList.Add(newPot);
           
       }


       public void ClearPot()
       {
           this.CardPile.Clear();
           potList.Clear();
           highestCard = null;
           highestPlayer = null;
       }

       public void setHighest(SpadesCard c,player p)
       {
           // just set this as highest
           if (highestCard == null)
           {
               highestPlayer = p;
               highestCard = c;
           }
           else
           {
               // if already set highest check with it 

               // if same suit then check value 
               if (c.Suit == highestCard.Suit)
               {
                   // compare the values
                   if (c.Rank == CardRank.Ace)
                   {
                       highestCard = c;
                       highestPlayer = p;
                       return;
                   }
                   else if (c.Rank > highestCard.Rank && highestCard.Rank != CardRank.Ace)
                   {
                       highestCard = c;
                       highestPlayer = p;
                       return;
                   }
               }
               else
               {
                   // if not same suit check if spades has been played
                   if (c.Suit == CardSuit.Spades)
                   {
                       highestPlayer = p;
                       highestCard = c;
                       return;
                   }
               }    


           }

       }

    }
}