namespace Laboration_Smells
{
    public static class ScoreKeeper
    {
        private const string Divider = "#&#";

        public static void WriteToFile(string name, int numberOfGuesses)
        {
            StreamWriter output = new("result.txt", append: true);
            output.WriteLine(string.Concat(name, Divider, numberOfGuesses));
            output.Close();
        }

        public static void DisplayTopList()
        {
            StreamReader resultsReader = new("result.txt");
            List<ScoreCard> scoreCards = new();
            string resultEntry;

            while ((resultEntry = resultsReader.ReadLine()) != null)
            {
                CompileScoreCardsFromResultEntries(resultEntry, out string name, out int numberOfGuesses);

                ScoreCard scoreCard = new(name, numberOfGuesses);

                if (!scoreCards.Contains(scoreCard))
                    scoreCards.Add(scoreCard);
                else
                    scoreCards.FirstOrDefault(pd => pd.Name == name).Update(numberOfGuesses);
            }
            resultsReader.Close();

            PrintHighScore(scoreCards, 5);
        }

        private static void PrintHighScore(List<ScoreCard> topList, int sizeOfHighScoreList)
        {
            IOHelper _ioHelper = ConsoleHelper.GetInstance();

            topList.Sort((playerOne, playerTwo) => playerOne.Average().CompareTo(playerTwo.Average()));

            _ioHelper.OutputMessage("\n----------- High Score -----------");
            _ioHelper.OutputMessage("Player\t\t| Games\t| Average");
            _ioHelper.OutputMessage("----------------------------------");
            foreach (ScoreCard player in topList.Take(sizeOfHighScoreList))
            {
                _ioHelper.OutputMessage($"{player.Name}\t\t| {player.GamesPlayed}\t| {player.Average():0.##}");
            }
            _ioHelper.OutputMessage("----------------------------------");
        }

        private static void CompileScoreCardsFromResultEntries(string line, out string name, out int guesses)
        {
            string[] nameAndScore = line.Split(new string[] { Divider }, StringSplitOptions.None);
            name = nameAndScore[0];
            guesses = int.Parse(nameAndScore[1]);
        }
    }
}
