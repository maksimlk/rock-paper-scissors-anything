namespace rock_paper_scissors_anything
{
    public enum GameResults
    {
        Win, Lose, Draw
    }

    internal static class GameRules
    {
        /// <summary>
        /// Computes Game Result with respect to the move chosen by the player
        /// </summary>
        /// <param name="playerObjectID">Move chosen by the player</param>
        /// <param name="computerObjectID">Move chosen by the computer</param>
        /// <param name="totalSize">Total number of possible moves in the game</param>
        /// <returns>enum GameResults: Win, Lose, Draw</returns>
        /// 

        public static GameResults GetGameResult(int playerMoveID, int computerMoveID, int totalSize)
        {
            int halfDist = totalSize / 2;

            if (playerMoveID == computerMoveID)
                return GameResults.Draw;

            if (Math.Abs(computerMoveID - playerMoveID) <= halfDist && playerMoveID > computerMoveID)
            {
                return GameResults.Lose;
            }
            else if (Math.Abs(computerMoveID - playerMoveID) <= halfDist && playerMoveID < computerMoveID)
            {
                return GameResults.Win;
            }
            else if (Math.Abs(computerMoveID - playerMoveID) > halfDist && playerMoveID < computerMoveID)
            {
                return GameResults.Lose;
            }
            else if (Math.Abs(computerMoveID - playerMoveID) > halfDist && playerMoveID > computerMoveID)
            {
                return GameResults.Win;
            }

            throw new Exception("Game Result not defined");
        }
    }
}
