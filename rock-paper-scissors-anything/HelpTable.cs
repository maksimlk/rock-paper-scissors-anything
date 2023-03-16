using System.Collections.Immutable;

namespace rock_paper_scissors_anything
{
    internal static class HelpTable
    {
        /// <summary>
        /// Prints a help table that indicates how do all the moves relate to each other (Win, Lose, Draw).
        /// </summary>
        public static void PrintHelp(ImmutableArray<string> moves)
        {
            Console.WriteLine();
            int maxLength = Math.Max(5, moves.OrderByDescending(s => s.Length).First().Length + 1);
            string[] tableRows = new string[moves.Length];
            string tableColumnsStr = "Player:".PadLeft(maxLength + 2);
            foreach(string move in moves)
            {
                tableColumnsStr += move.PadLeft(maxLength);
            }
            Console.WriteLine(tableColumnsStr);
            for (int i = 0; i < moves.Length; i++)
            {
                tableRows[i] += moves[i].PadLeft(maxLength) + " |";
                for (int j = 0; j < moves.Length; j++)
                {
                    tableRows[i] += GameRules.GetGameResult(j, i, moves.Length).ToString().PadLeft(maxLength);
                }
            }
            Console.WriteLine(new String('-', moves.Length * maxLength + 20));
            foreach(string row in tableRows)
            {
                Console.WriteLine(row);
            }
            Console.WriteLine();
        }
    }
}
