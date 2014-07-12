using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace Cards.tests
{
    [TestFixture]
    public class PlayerTestClass
    {
        player pTest;
        static int cardIndexAceDiamond, carIndexAceClubs;

        public PlayerTestClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        [SetUp]
        public void Init()
        {
            Hand hand = new Hand();
            Deck deck = new Deck();
         
            cardIndexAceDiamond = Card.ToCardIndex(CardSuit.Diamonds, CardRank.Ace);
            carIndexAceClubs = Card.ToCardIndex(CardSuit.Clubs, CardRank.Ace);
            for (int i = 0; i < 13; i++)
            {
                hand.AddCard((int)deck.CardPile[i]);
            }

            pTest = new player(1, false);
            pTest.Hand = hand;

        }
        [Test]
        public void AddCard()
        {
           
        }
      
      

        [Test]
        public void IsID1()
        {
            Assert.IsTrue(1 == pTest.ID);
        }



        [Test]
        public void handContainsAceofClubs()
        {
            Assert.IsTrue(pTest.Hand.containsCard(carIndexAceClubs));
        }
        [Test]
        public void RemoveAceOfDiamondsFromHand()
        {
            Assert.IsTrue(pTest.Hand.removeCard(cardIndexAceDiamond));

        }
        [Test]
        public void RemoveAceOfclubsFromHand()
        {
            Assert.IsTrue(pTest.Hand.removeCard(carIndexAceClubs));

        }

        [Test]
        public void InitialHandCountisthirteen()
        {
            Assert.AreEqual(13, pTest.Hand.CardPile.Count);
        }
       

    }
}

