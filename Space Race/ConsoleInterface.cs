using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        /// 

        private static int numbOfRounds = 0;
        
        static void Main(string[] args)
        {
            DisplayIntroductionMessage();

            do
            {
               
                /*                    
                 Set up the board in Board class (Board.SetUpBoard)
                 Determine number of players - initally play with 2 for testing purposes 
                 Create the required players in Game Logic class
                  and initialize players for start of a game             
                 loop  until game is finished           
                    call PlayGame in Game Logic class to play one round
                    Output each player's details at end of round
                 end loop
                 Determine if anyone has won
                 Output each player's details at end of the game
               */

                Board.SetUpBoard();

                DetermineNumberOfPlayers();

                SpaceRaceGame.SetUpPlayers();

                while (SpaceRaceGame.SomeoneHasWon == false && SpaceRaceGame.NoOneHasFuel == false)
                {
                    // Intro message before round of game is played
                    Console.WriteLine("\nPress Enter to play a round ...");
                    Console.ReadLine();

                    DetermineRoundNumber();

                    SpaceRaceGame.PlayOneRound();
                    numbOfRounds++;

                    // displays the results after a round of play
                    for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
                    {
                        Console.WriteLine("\t{0}  is now on square {1}, and has {2} yottawatt of power remaining",
                        SpaceRaceGame.Players[i].Name, SpaceRaceGame.Players[i].Position, SpaceRaceGame.Players[i].RocketFuel);
                    }

                } 


                if (SpaceRaceGame.SomeoneHasWon == true)
                {
                    DisplayResultsOfGame();
                }

                if (SpaceRaceGame.NoOneHasFuel == true)
                {
                    Console.WriteLine("\n\nThe Game is Over, because all players have run out of fuel!");
                    Console.ReadLine();
                }

                PromptForAnotherGame();
                
            } while (SpaceRaceGame.WantsToPlayAgain == true);


            PressEnter();


        }//end Main

        // determines whether it is the first round of the game or not
        // and displays "First Round" or "Next Round" accordingly
        static void DetermineRoundNumber()
        {
            if (numbOfRounds == 0)
            {
                Console.WriteLine("\tFirst Round\n");
            }
            else
            {
                Console.WriteLine("\tNext Round\n");
            }
        }

        // determines the number of players that will be playing in the game
        // filters for non-numeric characters, as well as integers outside of the correct number
        static void DetermineNumberOfPlayers()
        {
            int playersInt;

            int AnInt;

            bool PlayersCorrect = false;

            string inputPlayers;


            do
            {
                //asks user for the number of players
                Console.WriteLine("\tThis game is for 2 to 6 players.");
                Console.Write("\tHow many players (2-6): ");
                inputPlayers = Console.ReadLine();

                if (!int.TryParse(inputPlayers, out AnInt))
                {
                    Console.WriteLine("\nError: Number of Players must be an Integer\n");
                }
                else if (int.TryParse(inputPlayers, out AnInt))
                {
                    playersInt = int.Parse(inputPlayers);
                    if (playersInt < 2 || playersInt > 6)
                    {
                        Console.WriteLine("\nError: Invalid number of Players entered.\n");
                    }
                    else
                    {
                        PlayersCorrect = true;
                        SpaceRaceGame.NumberOfPlayers = playersInt;
                    }
                }


            } while (!PlayersCorrect);
        }

        // displays the results of all the players after anyone has won the game
        // includes all the players who won the game in the same round
        // as well as the name, fuel and board position at the round that someone has won
        static void DisplayResultsOfGame()
        {
            // displays the winner(s) of the game
            Console.WriteLine("\n\nThe following player(s) finished the game\n");

            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                if (SpaceRaceGame.Players[i].Position == 55)
                {
                    Console.Write("\t{0}\n", SpaceRaceGame.Players[i].Name);
                }
            }

            // displays the results of all the players when someone has won
            Console.WriteLine("\nIndividual players finished with the following fuel at the locations specified.\n");

            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                Console.WriteLine("\t{0} with {1} yottawatt of power at square {2}\n"
                    , SpaceRaceGame.Players[i].Name, SpaceRaceGame.Players[i].RocketFuel, SpaceRaceGame.Players[i].Position);
            }

        }

        // prompts the user for whether or not they want to play another game
        // sets up another game if the answer is 'y' or 'Y'
        // ends the program is it is anything else
        static void PromptForAnotherGame()
        {
            // clears all the data from the current players

            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                SpaceRaceGame.Players.Clear();
            }

            // reset game state variable
            SpaceRaceGame.SomeoneHasWon = false;
            SpaceRaceGame.NoOneHasFuel = false;


            // determines if the player wants to play another round
            Console.WriteLine("\nPress Enter key to continue");
            Console.ReadLine();

            string response;

            Console.Write("\n\tPlay again? (Y or N): ");
            response = Console.ReadLine();


            if (response == "Y" || response == "y")
            {
                SpaceRaceGame.WantsToPlayAgain = true;
                numbOfRounds = 0;
            }
            else
            {
                SpaceRaceGame.WantsToPlayAgain = false;
            }
        }

           

   
        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine("\n\tWelcome to Space Race.\n");
        } //end DisplayIntroductionMessage


        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.WriteLine("\n\n\n\tThanks for playing Space Race\n");
            Console.Write("\n\tPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny



    }//end Console class
}
