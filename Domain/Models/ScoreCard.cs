namespace Domain.Models
{
    public class ScoreCard
    {
        private int _totalGuesses;

        public string Name { get; private set; }
        public int GamesPlayed { get; private set; }

        public ScoreCard(string name, int guesses)
        {
            Name = name;
            GamesPlayed = 1;
            _totalGuesses = guesses;
        }

        public void Update(int guesses)
        {
            _totalGuesses += guesses;
            GamesPlayed++;
        }

        public double Average() => (double)_totalGuesses / GamesPlayed;
    }
}