using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Cards.GUI;
using Microsoft.Win32;





namespace Cards
{

    public enum gameMessages
    {
        gameStateUpdate,
        chatMessage,
        playerInfo

    }

    public struct playerPosition
    {
        public string seat; // E,W,N,S
        public int x;
        public int y;
    }

    /// <summary>
    /// Struct that contains all the game information 
    /// </summary>
    /// 
    [Serializable]
    public struct GameData
    {
        public gameMessages gameMessageType;
        public int CurrentRound; // current round being played MAX is 5
        public int HandCounter; // keeps track of played card MAX is 13
        public int ActivePlayerId; // ID of the current player

        /// <summary>
        /// score[RoundIndex,PlayerID] = score;
        /// RoundIndex starts from 0 and goes upto 4;
        /// </summary>
        public float[,] score;

        public int HandWinnerID; // Player ID of the Hand Winner for animation 
        public int RoundStarter; // (not used but may be useful )
        public GameState GameState; // state of the game 
        public Pile WastePile; // waste cards or Already played cards
        public List<player> CurrentPlayerList; // list of current player currentPlayerList[0] is player with ID 0
        public Pot CurrentPot; // when a valid card is played then it is added in the pot ( displayed in middle of screen)        
    }
    [Serializable]
    public struct NetwrokGameData
    {
        public int CurrentRound; // current round being played MAX is 5
        public int HandCounter; // keeps track of played card MAX is 13
        public int ActivePlayerId; // ID of the current player

        /// <summary>
        /// score[RoundIndex,PlayerID] = score;
        /// RoundIndex starts from 0 and goes upto 4;
        /// </summary>
        public float[,] score;

        public int HandWinnerID; // Player ID of the Hand Winner for animation 
        public int RoundStarter; // (not used but may be useful )
        public GameState GameState; // state of the game 
        public Pile WastePile; // waste cards or Already played cards
        public Pot CurrentPot; // when a valid card is played then it is added in the pot ( displayed in middle of screen)
    }
    [Serializable]
    public enum GameState
    {
        INTRO,
        BIDDING,
        PLAYING,
        ROUNDEND,
        SCOREDISPLAY,
        END
    }

    public enum AnimationSpeed
    {
        FAST,
        NORMAL,
        SLOW
    }


    [Serializable]
    public partial class Main : Form
    {
        private Deck gameDeck;
        private Cards.Card card = new Card();

        public static int boardBelongsTo = 0; // variable to store playerID which clients board is this so that we can arranage the position 
        public static string playerName;
        delegate void refreshBoard();

        private GameStateManager gameStateManager;
        static public bool RefreshWholeBoard = true;

        public static List<playerPosition> playerPositions = new List<playerPosition>(); // player position with respect to client 

        AnimationSpeed animationSpeed = AnimationSpeed.NORMAL; // controls the animation speed of the game ....


        // Timer animate pot 
        private System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        private static int animationCounter = 40;
        GameData gameData = new GameData();

        private static bool _isAnimating = false;

        /// <summary>
        /// set to true is some animation is taking place
        /// </summary>
        public static bool IsAnimating
        {
            get { return Main._isAnimating; }
            set { Main._isAnimating = value; }
        }


        public Main()
        {
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();

        }



        /// <summary>
        /// this method is called when ever painting of the board is needed
        /// can be called by Invalidate,Refresh or Update method;
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;


            if (RefreshWholeBoard)
            {
                SpadesGui.refreshBoard(graphics, ref gameData);
            }

            // base.OnPaint(e);
        }

        /// <summary>
        /// occurs when user clicks the mouse
        /// just does some little animation to lift the card up 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_MouseClick(object sender, MouseEventArgs e)
        {

            int activePlayerId = gameData.ActivePlayerId;

            foreach (SpadesCard card in gameData.CurrentPlayerList[activePlayerId].Hand.CardPile)
            {
                card.MoveUp = false;
            }


            // look for the card index from the mouse click 
            int mouseX = e.X;
            int mouseY = e.Y;

            // now calculate the card position form player initial position of refrence 
            // count how many cards are there in the pile 
            int cardCount = gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count;

            // check for valid click 
            // 71 is card width 97 is height
            if (gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count > 0)
            {
                if (mouseX >= playerPositions[activePlayerId].x && mouseX <= ((playerPositions[activePlayerId].x + 56) + (15 * cardCount)))
                {
                    // check for y coordinate 
                    if (mouseY >= playerPositions[activePlayerId].y && mouseY <= (playerPositions[activePlayerId].y + 97))
                    {

                        //valid coordinate
                        // calculate card index clicked
                        // 15 is the width of the card overlapping each other 
                        int index = (int)(mouseX - playerPositions[activePlayerId].x) / 15;

                        // if index is more than last index then make it last index
                        if (index > (gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count - 1))
                        {
                            index = gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count - 1;
                        }


                        SpadesCard c = (SpadesCard)gameData.CurrentPlayerList[activePlayerId].Hand.CardPile[index];
                        c.MoveUp = true;
                        gameData.CurrentPlayerList[activePlayerId].Hand.CardPile[index] = c;
                        //this.Invalidate(new Rectangle(gameData.CurrentPlayerList[0].PositionX - 100, gameData.CurrentPlayerList[0].PositionY - 50, (gameData.CurrentPlayerList[0].PositionX + 75) + (15 * cardCount), 150), true);
                        this.Invalidate();
                    }

                }
            }
        }

        /// <summary>
        /// Event that occurs when the user double clicks the mouse on the form
        /// checks if it is valid card or not first
        /// If valid add to Pot
        /// updates card position
        /// updates turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int activePlayerId = gameData.ActivePlayerId;

            if (gameData.CurrentPlayerList[boardBelongsTo].IsActivePlayer == false) return;

            // look for the card index from the mouse click 
            int mouseX = e.X;
            int mouseY = e.Y;

            // now calculate the card position form player initial position of refrence 
            // count how many cards are there in the pile 
            int cardCount = gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count;

            // check for valid click 
            // 71 is card width 97 is height
            if (cardCount > 0 && IsAnimating == false)
            {
                if (mouseX >= playerPositions[activePlayerId].x && mouseX <= ((playerPositions[activePlayerId].x + 56) + (15 * cardCount)))
                {
                    // check for y coordinate 
                    if (mouseY >= playerPositions[activePlayerId].y && mouseY <= (playerPositions[activePlayerId].y + 97))
                    {

                        //valid coordinate
                        // calculate card index clicked 
                        int index = (int)(mouseX - playerPositions[activePlayerId].x) / 15;

                        // if index is more than last index then make it last index
                        if (index > (gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count - 1))
                        {
                            index = gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.Count - 1;
                        }



                        if (GameRule.isValidCard(gameData.CurrentPlayerList[activePlayerId], (SpadesCard)gameData.CurrentPlayerList[activePlayerId].Hand.CardPile[index], gameData.CurrentPot))
                        {
                            // check if its a  valid card
                            // place the card in pot card
                            SpadesCard potCard = (SpadesCard)gameData.CurrentPlayerList[activePlayerId].Hand.CardPile[index];
                            // remove from hand
                            gameData.CurrentPlayerList[activePlayerId].Hand.CardPile.RemoveAt(index);

                            potCard.CardPositionX = playerPositions[activePlayerId].x + 80;
                            potCard.CardPositionY = playerPositions[activePlayerId].y - 120;

                            gameData.CurrentPot.AddPot(potCard, gameData.CurrentPlayerList[activePlayerId]);



                            this.updateTurn(activePlayerId);

                        }
                    }

                }

            }

        }


        /// <summary>
        /// This function updates the player turn by incrementing the id by 1
        /// 
        /// E.g if given ID is 1 then this function set the ActivePlayerID to 2.
        /// </summary>
        /// <param name="playerID">ID of the last current player</param>
        public void updateTurn(int playerID)
        {

            int nextTurn = playerID + 1;
            if (nextTurn >= 4)
            {
                nextTurn = 0;
            }

            gameData.CurrentPlayerList[gameData.ActivePlayerId].IsActivePlayer = false;
            gameData.ActivePlayerId = nextTurn;


            updateGameState(gameData);

        }



        /// <summary>
        /// initialize game and other thigs such as player,graphics animation timer etc
        /// </summary>
        /// <returns></returns>
        public bool Initialize()
        {
            gameDeck = new Deck();

            this.gameStateManager = new GameStateManager(null);
            this.animationTimer.Tick += new EventHandler(animationTimer_Tick);
            this.animationTimer.Interval = 5;

            // initialize graphic class
            //initialize score array to null 
            // score array

            // for 5 rounds and 4 player
            float[,] score = new float[5, 4];


            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    score[i, j] = 0; // initialize score to null
                }

            }

            gameData.CurrentRound = 1;
            gameData.ActivePlayerId = 0;
            gameData.HandCounter = 0;
            gameData.score = score;
            gameData.HandWinnerID = 0;
            gameData.RoundStarter = 0;
            gameData.GameState = GameState.INTRO;
            gameData.WastePile = new Pile();
            gameData.CurrentPlayerList = new List<player>();
            gameData.CurrentPot = new Pot();
            initializePlayers();
            SpadesGui.Initialize(ref gameData);
            gameData.CurrentPot.PotAdd += new PotAddEventHandler(Pot_PotAdd);
            initializePlayerBoardsPosition();
            return true;

        }

        /// <summary>
        /// Event handler of animationTimer
        /// Lots of thing is going on here such as animating pot end , calculating socre , updating player tricks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void animationTimer_Tick(object sender, EventArgs e)
        {


            if (animationCounter == 40)
            {
                if (animationSpeed == AnimationSpeed.FAST) Thread.Sleep(1000);
                else if (animationSpeed == AnimationSpeed.NORMAL) Thread.Sleep(1500);
                else if (animationSpeed == AnimationSpeed.SLOW) Thread.Sleep(2000);

            }

            gameData.HandWinnerID = gameData.CurrentPot.highestPlayer.ID;

            foreach (SpadesCard card in gameData.CurrentPot.CardPile)
            {
                // make all cards go to winner 
                switch (playerPositions[gameData.HandWinnerID].seat)
                {
                    case "S":
                        card.CardPositionY = card.CardPositionY + 80;
                        break;
                    case "W":
                        card.CardPositionX = card.CardPositionX - 80;
                        break;
                    case "N":
                        card.CardPositionY = card.CardPositionY - 80;
                        break;
                    case "E":
                        card.CardPositionX = card.CardPositionX + 80;
                        break;
                }

            }

            this.Invalidate();
            this.Update();

            //redrawForm();
            animationCounter--;


            if (animationCounter == 0)
            {
                animationTimer.Stop();
                // calculate Score

                gameData.CurrentPlayerList[gameData.HandWinnerID].TotalTrick++; // update the trick of the player that has won the hand

                // do hand counter increment here
                gameData.HandCounter++;

                // end of the round 
                if (gameData.HandCounter == 13)
                {
                    // store score 
                    //  int roundIndex = CurrentRound - 1;
                    int roundIndex = gameData.CurrentRound - 1;
                    CalculateScore(roundIndex);




                    updateRoundStarter(gameData.RoundStarter);

                    gameData.CurrentRound++; // increase the round number


                    if (gameData.CurrentRound > 5)
                    {
                        // this is the finsh of the game 
                        GameFinished();
                        return;

                    }

                    gameData.GameState = GameState.ROUNDEND;
                    updateGameState(gameData);

                    return;
                }


                IsAnimating = false;
                gameData.CurrentPot.ClearPot(); // clear the pot for next round
                updateTurn(gameData.HandWinnerID - 1);
            }





        }
        /// <summary>
        /// Updates the Id of round starter. (who is going to bid first)
        /// </summary>
        /// <param name="p"></param>
        private void updateRoundStarter(int p)
        {
            gameData.RoundStarter = p + 1;

            if (gameData.RoundStarter > 3) gameData.RoundStarter = 0;

            updateGameState(gameData);
            return;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// End of Round 5
        /// compares score and shows respective dialog
        /// 
        /// </summary>

        private void GameFinished()
        {
            gameData.GameState = GameState.END;
            bool isWinner = true;
            float playerOneScore = gameData.CurrentPlayerList[0].TotalScore; // SpadesPlayer[0].TotalScore;
            foreach (player p in gameData.CurrentPlayerList)  //SpadesPlayer)
            {
                if (p.ID != 0 && p.TotalScore > playerOneScore) isWinner = false;
            }


            if (isWinner == false)
            {
                LooserForm LF = new LooserForm();
                //LF.MdiParent = this;
                LF.StartPosition = FormStartPosition.CenterParent;
                LF.ShowDialog();
            }
            else
            {
                WinnerForm WF = new WinnerForm();
                WF.StartPosition = FormStartPosition.CenterParent;
                WF.ShowDialog();
            }


            ResetGame();
            startGame();


            // throw new NotImplementedException();
        }
        /// <summary>
        /// Resets game variables 
        /// </summary>
        private void ResetGame()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    gameData.score[i, j] = 0; // initialize score to null
                }

            }

            gameData.CurrentRound = 1;
            gameData.ActivePlayerId = 0;
            gameData.HandCounter = 0;
            gameData.HandWinnerID = 0;
            gameData.RoundStarter = 0;
            gameData.GameState = GameState.PLAYING;
            gameData.CurrentPot.ClearPot();
            IsAnimating = false;

        }

        /// <summary>
        /// stores score in gameData.score
        /// gameData.score is a multiDimension array 
        /// score[RoundIndex,PlayerID] = score;
        /// RoundIndex starts from 0 and goes upto 4;
        /// 
        /// </summary>
        /// <param name="roundIndex">round-1 to calculate the score (because index starts from 0) eg . if round 1 then pass 0 </param>
        private void CalculateScore(int roundIndex)
        {
            foreach (player p in gameData.CurrentPlayerList)
            {

                if (p.TotalBid > p.TotalTrick)
                {
                    // player has negative hand 
                    gameData.score[roundIndex, p.ID] = 0 - p.TotalBid; // negative

                }
                else if (p.TotalBid == p.TotalTrick)
                {
                    gameData.score[roundIndex, p.ID] = p.TotalBid;
                }
                else
                {
                    // has greater number of tricks than the bid add as float 
                    float difference = p.TotalTrick - p.TotalBid;
                    float point = difference / 10;
                    gameData.score[roundIndex, p.ID] = (float)Math.Round((p.TotalBid + point), 1);
                }
            }
        }


        /// <summary>
        /// Events that occurs when a card is added to pot
        /// when there are 4 cards in the pot it starts the animation 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Pot_PotAdd(object sender, PileEventArgs e)
        {
            gameData.WastePile.AddCard(e.card); // add the played card to waste pile too

            if (gameData.CurrentPot.CardPile.Count == 4)
            {
                lock (this)
                {
                    IsAnimating = true;
                    animationCounter = 40;
                    this.animationTimer.Start();

                }

            }

        }








        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gameDeck = new Deck();
            playerName = "";
            gameStateManager.switchState(new IntroState(ref playerName));//SpadesPlayer));
            gameStateManager.Process();
            initializePlayerBoardsPosition(); // re position 
            int i = 0;
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("Software\\Games\\CallBreak");
            foreach (player p in gameData.CurrentPlayerList)
            {
                string key = "AIPlayer" + i.ToString();
                if (i == 0) p.Name = playerName;
                else p.Name = regKey.GetValue(key, "AI - I").ToString();
                i++;
            }

            startGame();

        }


        /// <summary>
        ///  Iniialize Round, state of game and Starts the Turn
        /// </summary>
        /// <returns></returns>
        public bool startGame()
        {


            startRound(gameData.CurrentRound);
            this.Refresh();


            gameData.GameState = GameState.PLAYING;

            IsAnimating = false;
            this.startTurn();
            return true;
        }

        void bidState_BeforeBid(object sender, BidEventArgs e)
        {
            gameStatusLabel.Text = " waiting for " + gameData.CurrentPlayerList[e.PlayerID].Name + " to bid .....";
            this.Update();
            this.Invalidate();

        }

        void bidState_Bidded(object sender, BidEventArgs e)
        {
            updateTurn(gameData.ActivePlayerId);
        }


        /// <summary>
        /// Starts the turn of the player [ ActivePlayerID ]
        /// if its user turn , does nothing and waits for users input mouse clicks
        /// </summary>
        public void startTurn()
        {

            if (IsAnimating) newToolStripMenuItem.Enabled = false;
            else newToolStripMenuItem.Enabled = true;

            foreach (player p in gameData.CurrentPlayerList) //SpadesPlayer
            {
                p.IsActivePlayer = false;
            }



            // this is to prevent computer moves overlap
            if (IsAnimating == true) return;




            gameData.CurrentPlayerList[gameData.ActivePlayerId].IsActivePlayer = true;
            gameStatusLabel.Text = " Waiting for " + gameData.CurrentPlayerList[gameData.ActivePlayerId].Name + " ....";

            this.Invalidate();
            this.Update();


            if (gameData.CurrentPlayerList[gameData.ActivePlayerId] is AIPlayer)
            {
                // make a computer move

                // can control speed of the movement here // fast or slow computer moves
                // Thread.Sleep(10);

                Thread.Sleep(Convert.ToInt32(animationSpeed) * 50);


                SpadesCard card = gameData.CurrentPlayerList[gameData.ActivePlayerId].makeMove(gameData);

                if (playerPositions[gameData.ActivePlayerId].seat == "N")
                {
                    card.CardPositionX = playerPositions[gameData.ActivePlayerId].x + 80;
                    card.CardPositionY = playerPositions[gameData.ActivePlayerId].y + 120;
                }

                if (playerPositions[gameData.ActivePlayerId].seat == "W")
                {
                    card.CardPositionX = playerPositions[gameData.ActivePlayerId].x + 180;
                    card.CardPositionY = playerPositions[gameData.ActivePlayerId].y + 80;
                }
                if (playerPositions[gameData.ActivePlayerId].seat == "S")
                {
                    card.CardPositionX = playerPositions[gameData.ActivePlayerId].x + 80;
                    card.CardPositionY = playerPositions[gameData.ActivePlayerId].y - 120;
                }
                if (playerPositions[gameData.ActivePlayerId].seat == "E")
                {
                    card.CardPositionX = playerPositions[gameData.ActivePlayerId].x - 180;
                    card.CardPositionY = playerPositions[gameData.ActivePlayerId].y + 90;
                }

                gameData.CurrentPot.AddPot(card, gameData.CurrentPlayerList[gameData.ActivePlayerId]);
                gameData.CurrentPlayerList[gameData.ActivePlayerId].Hand.CardPile.Remove(card);
                string msg = (gameData.ActivePlayerId + " Played " + card.Rank + " of " + card.Suit + "\r\n");
                this.Invalidate();
                this.Update();
                updateTurn(gameData.ActivePlayerId);

            }
            else
            {
                return; // wait for human move 
            }
        }






        /// <summary>
        /// Initialize players and there respective positions
        /// </summary>
        /// <returns></returns>
        public bool initializePlayers()
        {
            bool isHuman = false;
            for (int id = 0; id < 4; id++)
            {
                isHuman = (id == 0) ? true : false; // if id = 0 make it a human player else AI player
                if (isHuman)
                {
                    HumanPlayer p = new HumanPlayer(id);
                    gameData.CurrentPlayerList.Add(p);
                }

                else
                    gameData.CurrentPlayerList.Add(new AIPlayer(id));

            }

            return true;
        }


        public void initializePlayerBoardsPosition()
        {
            //add dummy positions
            playerPositions.Clear();
            for (int j = 0; j < 4; j++)
            {
                playerPositions.Add(new playerPosition());
            }

            int StartPosition = boardBelongsTo;


            for (int i = 0; i < 4; i++)
            {
                playerPosition Position = new playerPosition();

                if (StartPosition > 3) StartPosition = 0;
                if (i == 0)
                {
                    Position.seat = "S";
                    Position.x = 175;
                    Position.y = 350;

                }
                if (i == 1)
                {
                    Position.seat = "W";
                    Position.x = 20;
                    Position.y = 100;
                }
                if (i == 2)
                {
                    Position.seat = "N";
                    Position.x = 175;
                    Position.y = 30;
                }
                if (i == 3)
                {
                    Position.seat = "E";
                    Position.x = 480;
                    Position.y = 100;
                }

                playerPositions[StartPosition] = Position;
                StartPosition++;
            }
        }


        /// <summary>
        /// This method initializes PlayerHands  and initializes round
        /// TO DO :: SET ACTIVE PLAYER ID TO START THE ROUND
        /// </summary>
        /// <param name="round">Round Index</param>
        /// <returns></returns>
        public bool startRound(int round)
        {

            gameData.ActivePlayerId = gameData.RoundStarter;
            this.gameRoundStatusLabel.Text = "Round : " + gameData.CurrentRound;
            gameData.HandCounter = 0;
            gameData.CurrentPot.ClearPot();

            gameDeck = new Deck();


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
            int k = 0;
            foreach (player p in gameData.CurrentPlayerList)
            {
                p.Hand = hand[k];
                k++;
                p.TotalBid = 0;
                p.TotalTrick = 0;

            }

            updateCardPosition();
            gameData.GameState = GameState.BIDDING;
            updateGameState(gameData);


            return true;

        }

        /// <summary>
        /// updates each cards position in player hand
        /// </summary>
        private void updateCardPosition()
        {/*
            foreach (player p in gameData.CurrentPlayerList)
            {
                int xinc = 0;
                int yinc = 0;
                foreach (SpadesCard icard in p.Hand.CardPile)
                {
                    //int x = p.PositionX + xinc;
                    //int y = p.PositionY + yinc;

                    int x = playerPositions[p.ID].x + xinc;
                    int y = playerPositions[p.ID].y + yinc;

                    icard.CardPositionX = x;
                    icard.CardPositionY = y;

                    // if 0 0r 2 
                    if (playerPositions[p.ID].seat == "N" || playerPositions[p.ID].seat == "S") xinc = xinc + 15;
                    else yinc = yinc + 15;
                }
            }
          */
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void displayScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScoreDisplay SD = new ScoreDisplay(ref gameData);
            SD.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Call Break 0.0.0 \nNishes Joshi 2009");
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.C)
            {
                SpadesGui.CardFaceCheat = !SpadesGui.CardFaceCheat;
                this.Refresh();

            }
        }

        private void playerNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AIPlayerNames PlayerNameBox = new AIPlayerNames();
            PlayerNameBox.StartPosition = FormStartPosition.CenterParent;
            PlayerNameBox.ShowDialog(this);

        }

        private void deckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseDeck cd = new ChooseDeck();
            cd.StartPosition = FormStartPosition.CenterParent;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                SpadesGui.CardBack = cd.choosenCardBack;
                Refresh();
            }
        }

        private void fastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.animationSpeed = AnimationSpeed.FAST;
            this.fastToolStripMenuItem.Checked = true;
            this.mediumToolStripMenuItem.Checked = false;
            this.slowToolStripMenuItem.Checked = false;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.animationSpeed = AnimationSpeed.NORMAL;
            this.mediumToolStripMenuItem.Checked = true;
            this.fastToolStripMenuItem.Checked = false;
            this.slowToolStripMenuItem.Checked = false;
        }

        private void slowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.animationSpeed = AnimationSpeed.SLOW;
            this.slowToolStripMenuItem.Checked = true;
            this.fastToolStripMenuItem.Checked = false;
            this.mediumToolStripMenuItem.Checked = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            gameStateManager.switchState(new SplashScreenState());
            gameStateManager.Process();

            gameStatusLabel.TextChanged += new EventHandler(gameStatusLabel_TextChanged);

        }

        void gameStatusLabel_TextChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        public void updateGameState(GameData gdata)
        {
            this.gameData = gdata;
            gameData.CurrentPot.PotAdd += new PotAddEventHandler(Pot_PotAdd);
            RefreshWholeBoard = true;
            if (gameData.GameState == GameState.BIDDING)
            {
                enterBiddingState();
            }
            else if (gameData.GameState == GameState.PLAYING)
            {
                enterPlayingState();
            }
            else if (gameData.GameState == GameState.ROUNDEND)
            {
                showScoreBoard();
            }
            else
            {
                redrawForm();
            }

        }


        public void enterBiddingState()
        {
            initializePlayerBoardsPosition();
            redrawForm();
            makeBid();
        }

        public void showScoreBoard()
        {
            ScoreDisplay sd = new ScoreDisplay(ref gameData);
            sd.ShowDialog();
            startGame();
        }

        private void makeBid()
        {
            if (this.InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                this.Invoke(new refreshBoard(makeBid));
                return;
            }

            BiddingState bidState = new BiddingState(ref gameData);
            bidState.Bidded += new BidEventHandler(bidState_Bidded);
            bidState.BeforeBid += new BeforeBidEventHandler(bidState_BeforeBid);
            gameStateManager.switchState(bidState);
            gameStateManager.Process();
        }
        public void enterPlayingState()
        {
            redrawForm();
            this.startTurn();

        }


        public void redrawForm()
        {
            if (this.InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                this.Invoke(new refreshBoard(redrawForm));
                return;
            }
            SpadesGui.Initialize(ref gameData);
            this.Update();
            this.Refresh();
            //this.startTurn();

        }

    }

}