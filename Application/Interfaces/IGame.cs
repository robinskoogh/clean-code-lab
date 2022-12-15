namespace Application.Interfaces
{
    public interface IGame
    {
        void SetGameEngine(IGameEngine gameEngine);
        void SelectGame();
        void RunGame();
    }
}
