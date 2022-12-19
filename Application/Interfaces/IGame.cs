namespace Application.Interfaces
{
    public interface IGame
    {
        void SetGameEngine(IGameEngine gameEngine);
        bool SelectGame();
        void RunGame();
    }
}
