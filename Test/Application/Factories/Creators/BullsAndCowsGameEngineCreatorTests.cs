using Application.Creators;
using Application.GameEngines;
using Application.Interfaces;
using FluentAssertions;
using Moq;

namespace Test.Application.Factories.Creators
{
    public class BullsAndCowsGameEngineCreatorTests
    {
        private readonly Mock<IIOHelper> _mockIOHelper = new();
        private readonly Mock<IScoreKeeper> _mockScoreKeeper = new();
        private readonly BullsAndCowsGameEngineCreator _creator;

        public BullsAndCowsGameEngineCreatorTests()
        {
            _creator = new(_mockIOHelper.Object, _mockScoreKeeper.Object);
        }

        [Fact]
        public void GameEngineCreator_Instantiates_Requested_Game_Engine()
        {
            var actual = _creator.CreateGameEngine();

            actual.Should()
                .NotBeNull().And
                .BeOfType<BullsAndCowsGameEngine>();
        }
    }
}
