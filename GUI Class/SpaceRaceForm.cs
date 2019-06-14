using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();

             Board.SetUpBoard();
             ResizeGUIGameBoard();
             SetUpGUIGameBoard();
             SetupPlayersDataGridView();
             DetermineNumberOfPlayers();
             SpaceRaceGame.SetUpPlayers();
             PrepareToPlay();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        //  ******************* Uncomment - Remove Block Comment below
        //                         once you've added the TableLayoutPanel to your form.
        //
        //       You will have to replace "tableLayoutPanel" by whatever (Name) you used.
        //
        //        Likewise with "playerDataGridView" by your DataGridView (Name)
        //  ******************************************************************************************


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            // Code needs to be added here to do the mapping

            // first row
            if (squareNum == 0 || squareNum == 1 || squareNum == 2 || squareNum == 3 || squareNum == 4
                || squareNum == 5 || squareNum == 6 || squareNum == 7)
            {
                // the first row is the bottom row of the screen
                // and there are 6 rows
                screenRow = 6;

                // the columns of the map fortunately equal the square numbers
                screenCol = squareNum;
            }
            // second row
            else if (squareNum == 8 || squareNum == 9 || squareNum == 10 || squareNum == 11 || squareNum == 12
                || squareNum == 13 || squareNum == 14 || squareNum == 15)
            {
                // the second row is the second from the bottom
                screenRow = 5;

                // taking the square number away from 15 gives the negative form of the row
                // 15 is chosen because the squares range from 8 to 15
                // and are going from left to right
                screenCol = (squareNum - 15) * -1;
            }
            // third row
            else if (squareNum == 16 || squareNum == 17 || squareNum == 18 || squareNum == 19 || squareNum == 20
                || squareNum == 21 || squareNum == 22 || squareNum == 23)
            {
                // the third row is the third from the bottom
                screenRow = 4;

                // taking the square number away from 16 gives the row
                // 16 is chosen because the squares range from 16 to 23
                // and are going from right to left
                screenCol = squareNum - 16;

            }
            // fourth row
            else if (squareNum == 24 || squareNum == 25 || squareNum == 26 || squareNum == 27 || squareNum == 28
                || squareNum == 29 || squareNum == 30 || squareNum == 31)
            {
                // the fourth row is the fourth from the top
                screenRow = 3;

                // taking the square number away from 31 gives the negative form of the row
                // 31 is chosen because the squares range from 24 to 31
                // and are going from left to right
                screenCol = (squareNum - 31) * -1;
            }
            // fifth row
            else if (squareNum == 32 || squareNum == 33 || squareNum == 34 || squareNum == 35 || squareNum == 36
                || squareNum == 37 || squareNum == 38 || squareNum == 39)
            {
                // the fifth row is the third from the top
                screenRow = 2;

                // taking the square number away from 32 gives the row
                // 32 is chosen because the squares range from 32 to 39
                // and are going from right to left
                screenCol = squareNum - 32;
            }
            // sixth row
            else if (squareNum == 40 || squareNum == 41 || squareNum == 42 || squareNum == 43 || squareNum == 44
                || squareNum == 45 || squareNum == 46 || squareNum == 47)
            {
                // the sixth row is the second from the top
                screenRow = 1;

                // taking the square number away from 47 gives the negative form of the row
                // 47 is chosen because the squares range from 40 to 47
                // and are going from left to right
                screenCol = (squareNum - 47) * -1;
            }

            // seventh and final row
            // 'else' is used because there are no other squares
            else
            {
                // the final row is the top row
                screenRow = 0;

                // taking the square number away from 48 gives the row
                // 48 is chosen because the squares range from 48 to 55
                // and are going from right to left
                screenCol = squareNum - 48;
            }



                // Makes the compiler happy - these two lines below need to deleted 
                //    once mapping code is written above
                //screenRow = 0;
            //screenCol = 0;

        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            // Store the SelectedItem property of the ComboBox in a string
            string numbOfPlayers = comboBox1.SelectedItem.ToString();

            // Parse string to a number
            int theNumbOfPlayers = int.Parse(numbOfPlayers);

            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = theNumbOfPlayers;
            
        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlay()
        {
            // More code will be needed here to deal with restarting 
            // a game after the Reset button has been clicked. 
            //
            // Leave this method with the single statement 
            // until you can play a game through to the finish square
            // and you want to implement the Reset button event handler.
            //

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            GameResetButton.Enabled = false;
            playersDataGridView.Enabled = true;

            groupBox1.Enabled = true;
            
            // "At the Start of Game - Right Panel has Other (Roll Dice) Buttons not enabled
            RollDiceButton.Enabled = false;

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
             MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
             return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);

            //return null; //added so code compiles
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.
            int playerSquareNumber;

            playerSquareNumber = SpaceRaceGame.Players[playerNumber].Position;

            //     delete the "return -1;" once body of method has been written 

            return playerSquareNumber;

        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
                  tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
           
            //   for each player
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                //       determine the square number of the player
                int newPosition = GetSquareNumberOfPlayer(i);

                //       retrieve the SquareControl object with that square number
                SquareControl PlayerSquareControl = SquareControlAt(newPosition);

                //       using the typeOfGuiUpdate, update the appropriate element of 
                //          the ContainsPlayers array of the SquareControl object.
                if (typeOfGuiUpdate == TypeOfGuiUpdate.RemovePlayer )
                {
                    PlayerSquareControl.ContainsPlayers[i] = false;
                }
                else if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
                {
                    PlayerSquareControl.ContainsPlayers[i] = true;
                }

            }        

            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void RollDiceButton_Click(object sender, EventArgs e)
        {
            // disables all the buttons/features except for the reset button
            exitButton.Enabled = false;
            GameResetButton.Enabled = true;
            playersDataGridView.Enabled = false;
            comboBox1.Enabled = false;
            RollDiceButton.Enabled = false;

            // removes the players from the board
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);

            // finds the new positions of the players, according to which mode is being played
            if (singleStepYesButton.Checked == true)
            {
                SpaceRaceGame.PlayOnePlayer();
            }

            else if (singleStepNoButton.Checked == true)
            {
                SpaceRaceGame.PlayOneRound();
            }

            // adds the players back to the board at their new positions an updates the GUI
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();

            // re-enables necessary features after this process
            RollDiceButton.Enabled = true;
            exitButton.Enabled = true;

            // determines if no one has any fuel
            if (SpaceRaceGame.NoOneHasFuel == true)
            {
                RollDiceButton.Enabled = false;
                MessageBox.Show("The Game is Over, because everyone has run out of fuel!");
            }

            // determines if someone has won the game
            if (SpaceRaceGame.SomeoneHasWon == true)
            {
                string text = "";

                RollDiceButton.Enabled = false;

                for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                {
                    if (SpaceRaceGame.Players[i].Position == 55)
                    {
                        text = text + SpaceRaceGame.Players[i].Name + "\n\t";
                    }
                }

                MessageBox.Show("The following player(s) have finished the game\n\n\t" + text);
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // changes the number of players according to the new number in the combobox
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            }

            // disables the combobox so the player can't change the number of players again
            comboBox1.Enabled = false;

            // re-initiates the process of counting the number of players and updates the GUI accordingly
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            
        }

        private void GameResetButton_Click_1(object sender, EventArgs e)
        {
            // resets all the game state variables
            SpaceRaceGame.SomeoneHasWon = false;
            SpaceRaceGame.NoOneHasFuel = false;
            SpaceRaceGame.PlayerCounter = 0;

            // removes the players from the board, and resets all of their respective variables
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
                SpaceRaceGame.Players[i].Position = Board.START_SQUARE_NUMBER;
                SpaceRaceGame.Players[i].RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                SpaceRaceGame.Players[i].Location = Board.StartSquare;
                SpaceRaceGame.Players[i].HasPower = true;
            }

            // resets the Single Step mode selection
            singleStepYesButton.Checked = false;
            singleStepNoButton.Checked = false;

            // resets the selection for the number of players to match the "Starting" condition
            comboBox1.Text = "6";
            comboBox1.Enabled = true;

            // primes the game for gameplay
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            PrepareToPlay();
            
        }

        private void singleStepYesButton_Click(object sender, EventArgs e)
        {
            // "Click either radio button - GroupBox not enabled until a new game commences"
            groupBox1.Enabled = false;

            // disables the combobox, locks in the number of players and primes the game
            comboBox1.Enabled = false;
            RollDiceButton.Enabled = true;
        }

        private void singleStepNoButton_Click(object sender, EventArgs e)
        {
            // "Click either radio button - GroupBox not enabled until a new game commences"
            groupBox1.Enabled = false;

            // disables the combobox, locks in the number of players and primes the game
            comboBox1.Enabled = false;
            RollDiceButton.Enabled = true;
        }

    }// end class
}
