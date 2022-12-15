using Application.Factories;
using Application.GameEngines;
using Application.Interfaces;

namespace Application.Creators
{
    internal class RockPaperScissorsGameEngineCreator : GameEngineFactory
    {
        private readonly IIOHelper _ioHelper;
        private readonly IScoreKeeper _scoreKeeper;

        public RockPaperScissorsGameEngineCreator(IIOHelper ioHelper, IScoreKeeper scoreKeeper)
        {
            _ioHelper = ioHelper;
            _scoreKeeper = scoreKeeper;
        }

        public override IGameEngine CreateGameEngine()
        {
            return new RockPaperScissorsGameEngine(_ioHelper, _scoreKeeper);
        }
    }
}
