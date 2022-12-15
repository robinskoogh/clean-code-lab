namespace Application.Interfaces
{
    public interface IScoreKeeper
    {
        void SetFilename(string filename);
        void WriteToFile(string username, int numberOfGuesses);
        void DisplayTopList();
    }
}
