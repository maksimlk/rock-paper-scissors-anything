using System.Collections.Immutable;

namespace rock_paper_scissors_anything
{
    internal static class MainMenu
    {

        private static List<List<string>> GenerateMenu(int pages, ImmutableArray<string> moves)
        {
            List<List<string>> options = new();
            for (int i = 0; i < pages; i++)
            {
                options.Add(new List<string>());
                for (int j = 0; j < Math.Min(7, (moves.Length - 7 * i)); j++)
                {
                    options[i].Add((j + 1) + " - " + moves[j + i * 7]);
                }
                if (i > 0) options[i].Add("8 - Go Back");
                if (pages > 1 && i < (pages - 1)) options[i].Add("9 - Next Page");
                options[i].Add("0 - Exit");
                options[i].Add("? - Help (moves table)");
            }
            return options;
        }

        /// <summary>
        /// Prints main menu to the user.
        /// </summary>
        /// <returns>An integer (userinputedint - 1) that indicates the chosen move from the available moves array.
        /// Returns -1 if option exit was chosen. </returns>
        public static int PrintInputMenu(ImmutableArray<string> moves)
        {
            int pages = (moves.Length / 8) + 1;
            int currentPage = 0;

            List<List<string>> options;
            options = GenerateMenu(pages, moves);

            while (true)
            {
                List<string> availableOptions = new();
                foreach (string option in options[currentPage])
                {
                    Console.WriteLine(option);
                    availableOptions.Add(option[..1]);
                }
                Console.Write("Enter your move: ");
                int optionChosen;
                try
                {
                    string input = Console.ReadLine();
                    if (input == "?")
                    {
                        HelpTable.PrintHelp(moves);
                        continue;
                    }
                    optionChosen = Convert.ToInt32(input);
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please input '?' or an integer according to the menu displayed above\n");
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Unhandled input exception. Please input '?' or an integer according to the menu displayed above\n");
                    continue;
                }
                if (!availableOptions.Contains(optionChosen.ToString()))
                {
                    Console.WriteLine("Please input '?' or an integer according to the menu displayed above\n");
                    continue;
                }
                if (optionChosen == 8)
                {
                    currentPage--;
                    continue;
                }
                if (optionChosen == 9)
                {
                    currentPage++;
                    continue;
                }
                Console.WriteLine();
                return optionChosen + 7 * currentPage - 1;
            }
        }
    }
}
