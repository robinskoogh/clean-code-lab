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

    public override bool Equals(object? obj) => Name.Equals(((ScoreCard)obj).Name);

    public override int GetHashCode() => Name.GetHashCode();
}
