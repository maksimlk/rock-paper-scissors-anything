using rock_paper_scissors_anything;
using System.Collections.Immutable;
using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 3 || args.Length % 2 == 0)
        {
            Console.WriteLine("Error: wrong input! Number of the arguments (moves) should be odd and >= 3!");
            Console.WriteLine("Usage example: ./rock-paper-scissors-anything.exe rock paper scissors lizard spock");
            return;
        }

        if (args.Length > 7)
        {
            Console.WriteLine("Warning: you play with too many moves. The help table could not be displayed correctly.");
        }

        ImmutableArray<string> movesArray = args.ToImmutableArray();

        if (movesArray.Length != movesArray.Distinct().Count())
        {
            Console.WriteLine("Error: wrong input! Arguments contain duplicate moves");
            Console.WriteLine("Usage example: ./rock-paper-scissors-anything.exe rock paper scissors lizard spoke");
            return;
        }

        while (true)
        {
            string randomString = KeyGenerator.CreateSecureRandomString();
            HmacSHA3 hmac = new(randomString);

            int computerMoveID = RandomNumberGenerator.GetInt32(movesArray.Length - 1);
            string moveHMAC = hmac.GetHMAC(movesArray[computerMoveID]);
            Console.WriteLine("HMAC:\n" + moveHMAC);
            Console.WriteLine();

            int playerMoveID = MainMenu.PrintInputMenu(movesArray);
            if (playerMoveID == -1) { Console.WriteLine("Thank you for the game!"); break; } // Player wants to exit the game.

            GameResults result = GameRules.GetGameResult(playerMoveID, computerMoveID, movesArray.Length);
            Console.WriteLine("You played " + movesArray[playerMoveID]);
            Console.WriteLine("Computer played " + movesArray[computerMoveID] + "\n");
            switch (result)
            {
                case GameResults.Win:
                    {
                        Console.WriteLine("You won the game! Congratulations\n");
                        break;
                    }
                case GameResults.Lose:
                    {
                        Console.WriteLine("You lost the game! Good luck next time\n");
                        break;
                    }
                case GameResults.Draw:
                    {
                        Console.WriteLine("The result of this game is draw. Try again\n");
                        break;
                    }
            }
            Console.WriteLine("The key used to generate HMAC is:");
            Console.WriteLine(randomString);
            Console.WriteLine();
            Console.Write("Do you want to play again with the same moves? 1 - yes, 0 - no: ");
            int playAgain;
            while (!(int.TryParse(Console.ReadLine(), out playAgain)) || !(playAgain == 0 || playAgain == 1))
            {
                Console.Write("Please input 1 to play again or 0 to exit: ");
            }
            Console.WriteLine();
            if (playAgain == 0) break;
        }
    }
}