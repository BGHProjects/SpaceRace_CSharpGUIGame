using System.Drawing;
using System.ComponentModel;
using Object_Classes;
using System;

namespace Game_Logic_Class
{
    public static class SpaceRaceGame
    {
        // Minimum and maximum number of players.
        public const int MIN_PLAYERS = 2;
        public const int MAX_PLAYERS = 6;

        private static int numberOfPlayers;
        //= 2;  //default value for test purposes only 

        // various game state variables, relating to the end of the game and replaying it
        public static bool SomeoneHasWon = false;
        public static bool WantsToPlayAgain;
        public static bool NoOneHasFuel = false;
        public static int PlayerCounter = 0;

        public static int NumberOfPlayers
        {
            get
            {
                return numberOfPlayers;
            }
            set
            {
                numberOfPlayers = value;
            }
        }

        public static string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };  // default values
        
        // Only used in Part B - GUI Implementation, the colours of each player's token
        private static Brush[] playerTokenColours = new Brush[MAX_PLAYERS] { Brushes.Yellow, Brushes.Red,
                                                                       Brushes.Orange, Brushes.White,
                                                                      Brushes.Green, Brushes.DarkViolet};
        /// <summary>
        /// A BindingList is like an array which grows as elements are added to it.
        /// </summary>
        private static BindingList<Player> players = new BindingList<Player>();
        public static BindingList<Player> Players
        {
            get
            {
                return players;
            }
        }

        // The pair of die
        private static Die die1 = new Die(), die2 = new Die();
       

        /// <summary>
        /// Set up the conditions for this game as well as
        ///   creating the required number of players, adding each player 
        ///   to the Binding List and initialize the player's instance variables
        ///   except for playerTokenColour and playerTokenImage in Console implementation.
        ///   
        ///     
        /// Pre:  none
        /// Post:  required number of players have been initialsed for start of a game.
        /// </summary>
        public static void SetUpPlayers() 
        {
            // for number of players
            //      create a new player object
            //      initialize player's instance variables for start of a game
            //      add player to the binding list

            
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                // creating a new player object
                Player p = new Player(name: names[i]);

                // initialize player's instance variables for start of a game
                p.Position = Board.START_SQUARE_NUMBER;
                p.RocketFuel = Player.INITIAL_FUEL_AMOUNT;
                p.Location = Board.StartSquare;
                p.HasPower = true;
                p.PlayerTokenColour = playerTokenColours[i];

                // add player to the binding list
                players.Add(p);

            }


        }

        public static void CheckIfEveryoneIsOutOfFuel()
        {
            int noFuelCount = 0; // resets after every round


            for (int i = 0; i < numberOfPlayers; i++)
            {
                // for every player in the game currently
                // if they do not have power, count them as a player with no fuel
                if (players[i].HasPower.Equals(false))
                {
                    noFuelCount++;
                }
                // otherwise don't
                else
                {

                }

            }

            // if the count of players with no fuel is the same as the number of players in the game
            // this means that everyone playing the game has no fuel
            if (noFuelCount == numberOfPlayers)
            {
                NoOneHasFuel = true;
            }
        }

        /// <summary>
        ///  Plays one round of a game
        /// </summary>
        public static void PlayOneRound()
        {
            // loops through the play one round of the game
            for (int i = 0; i < numberOfPlayers; i++)
            {
                // calls the play function, taking in the results of the die as parameters
                players[i].Play(die1, die2);

                if (players[i].Position == 55) // checks to see if anyone has won
                {
                    SomeoneHasWon = true;
                }
                
            }

            // checks to see if everyone is out of fuel
            CheckIfEveryoneIsOutOfFuel();
        }

        // single step mode of the game
        // uses the integer of PlayerCount instead of a for loop
        // to ensure that PlayOnePlayer only... plays one player
        public static void PlayOnePlayer()
        {

            // assign the Playercounter to something that isn't annoying to write all the tiem
            int i = PlayerCounter;

            // play one dice roll
            players[i].Play(die1, die2);

            // increment the variable every time this function happens
            PlayerCounter++;

            // if the PlayerCounter equals the number of players in the game
            // then the equivalent of one round has been played
            // and the function needs to check if anyone has won
            if (PlayerCounter == numberOfPlayers)
            {
                for (int j = 0; j < numberOfPlayers; j++)
                {
                    if (players[j].Position == 55) // checks to see if anyone has won
                    {
                        SomeoneHasWon = true;
                    }
                }

                PlayerCounter = 0;
            }

            // checks to see if everyone is out of fuel
            CheckIfEveryoneIsOutOfFuel();

        }

        


    }//end SnakesAndLadders
}