namespace Application.Interfaces
{
    public interface IScoreKeeper
    {
        string? Filename { get; }
        void SetFilename(string filename);
        void WriteToFile(string username, int numberOfGuesses);
        void DisplayTopList();
    }
}
