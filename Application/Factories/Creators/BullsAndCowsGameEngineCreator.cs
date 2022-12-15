using Application.Factories;
using Application.GameEngines;
using Application.Interfaces;

namespace Application.Creators
{
    public class BullsAndCowsGameEngineCreator : GameEngineFactory
    {
        private readonly IIOHelper _ioHelper;
        private readonly IScoreKeeper _scoreKeeper;

        public BullsAndCowsGameEngineCreator(IIOHelper ioHelper, IScoreKeeper scoreKeeper)
        {
            _ioHelper = ioHelper;
            _scoreKeeper = scoreKeeper;
        }
        public override IGameEngine CreateGameEngine()
        {
            return new BullsAndCowsGameEngine(_ioHelper, _scoreKeeper);
        }
    }
}
