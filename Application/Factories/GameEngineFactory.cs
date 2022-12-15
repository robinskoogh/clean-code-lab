using Application.Interfaces;

namespace Application.Factories
{
    public abstract class GameEngineFactory
    {
        public abstract IGameEngine CreateGameEngine();

        public IGameEngine FetchGameEngine() => CreateGameEngine();
    }
}
