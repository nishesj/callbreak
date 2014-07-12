using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Threading;
using Cards.GUI;
namespace Cards
{

    // global enum
    public enum gameState : int
    {
        inputName = 0,
        start = 1,
        Bid = 2,
        end = 3
    }



    class GameEngine
    {
        static protected GameEngine _gameEngine = null;
        private Deck gameDeck = new Deck();
        private Cards.Card card = new Card();
        List<player> SpadesPlayer = new List<player>(4);
        protected gameState _gameState;

        public Form _mainForm;
        protected GameStateManager gameStateManager;
     
        


        GameEngine()
        {
            _gameState = gameState.start;
           

        }

        ~GameEngine()
        {
        }

        public static GameEngine gameEngine
        {
            get
            {
                if (_gameEngine == null)
                    _gameEngine = new GameEngine();
                return _gameEngine;
            }
        }

        public bool Initialize( Form mainForm)
        {
            // initialize card,deck,player,card,rounds,graphics

            _mainForm = mainForm;
            
            
            
            // initialize player

            gameStateManager = new GameStateManager(null);            
            initializePlayers();

            // initialize graphic class
            SpadesGui.Initialize(ref SpadesPlayer);
            return true;
        }



        public bool startGameLoop()
        {

            gameStateManager.switchState(new IntroState(gameStateManager));
            gameStateManager.Process();


            for (int i = 1; i <= 5; i++)
            {
                startRound(1);
                gameStateManager.switchState(new PlayingState(ref SpadesPlayer));
                gameStateManager.Process();

                
                gameStateManager.switchState(new BiddingState( ref SpadesPlayer));
                gameStateManager.Process();
                gameStateManager.switchState(new CardPlayingState( ref SpadesPlayer));
                gameStateManager.Process();
            
            }




            return true;
        }

        public bool initializePlayers()
        {
            bool isHuman = false;
            for (int k = 0; k < 4; k++)
            {
                isHuman = (k == 0) ? true : false;
                if (isHuman)
                    SpadesPlayer.Add(new HumanPlayer(k, isHuman));
                else
                    SpadesPlayer.Add(new AIPlayer(k, isHuman));

            }
            return true;
        }

     

        public bool startRound(int round)
        {

            CardUtility.ShufflePile(gameDeck);
            Hand[] hand = new Hand[4];

            int counter = 0;
            for (int j = 0; j < 4; j++)
            {
                hand[j] = new Hand();

                for (int i = counter; i < counter + 13; i++)
                {
                    hand[j].AddCard((SpadesCard)gameDeck.CardPile[i]);
                }

                counter = counter + 13;
                CardUtility.SortBySuit(hand[j]);

            }

            // initialize 4 players hand           
            for (int k = 0; k < 4; k++)
            {
                SpadesPlayer[k].Hand = hand[k];
            }

            return true;

        }

       

       

     




    }







}
