using Application.GameEngines;
using Application.Interfaces;
using FluentAssertions;
using Moq;
using System.Reflection;

namespace Test.Application.GameEngines
{
    public class BullsAndCowsGameEngineTests
    {
        private readonly Mock<IIOHelper> _mockIOHelper = new();
        private readonly Mock<IScoreKeeper> _mockScoreKeeper = new();
        private readonly BullsAndCowsGameEngine _gameEngine;

        public BullsAndCowsGameEngineTests()
        {
            _gameEngine = new(_mockIOHelper.Object, _mockScoreKeeper.Object);
        }

        [Theory]
        [InlineData("1234", "1234")]
        [InlineData("4321", "4321")]
        [InlineData("1423", "1423")]
        public void Correct_Guess_Returns_String_For_Correct_Guess(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be("BBBB,");
        }

        [Theory]
        [InlineData("1234", "1111")]
        [InlineData("4321", "4444")]
        [InlineData("1423", "2222")]
        public void One_Digit_Four_Times_Returns_Correct_Digit_And_Three_Digits_In_Wrong_Place(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be("B,CCC");
        }
        
        [Theory]
        [InlineData("1234", "1200")]
        [InlineData("1234", "1030")]
        [InlineData("1234", "1004")]
        [InlineData("1234", "0230")]
        [InlineData("1234", "0034")]
        public void Two_Correct_Digits_Returns_String_Representation_Of_Guess(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be("BB,");
        }

        [Theory]
        [InlineData("1234", "0012")]
        [InlineData("1234", "0103")]
        [InlineData("1234", "4100")]
        [InlineData("1234", "0023")]
        [InlineData("1234", "4003")]
        public void Two_Correct_Digits_In_Wrong_Position_Returns_String_Representation_Of_Guess(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be(",CC");
        }

        [Theory]
        [InlineData("1234", "0014")]
        [InlineData("1234", "0130")]
        [InlineData("1234", "4200")]
        [InlineData("1234", "1003")]
        [InlineData("1234", "0210")]
        public void Two_Correct_Digits_With_One_In_Wrong_Position_Returns_String_Representation_Of_Guess(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be("B,C");
        }
        
        [Theory]
        [InlineData("1234", "5678")]
        [InlineData("1234", "0")]
        [InlineData("1234", "76")]
        [InlineData("1234", "597")]
        [InlineData("1234", "9876")]
        public void Incorrect_Guess_Returns_Only_Separator(string goal, string guess)
        {
            string result = RunPrivateMethod_EvaluatePlayerGuess(goal, guess);

            result.Should().Be(",");
        }

        private string RunPrivateMethod_EvaluatePlayerGuess(string goal, string guess)
        {
            MethodInfo methodInfo = typeof(BullsAndCowsGameEngine).GetMethod("EvaluatePlayerGuess", BindingFlags.NonPublic | BindingFlags.Static)!;
            object[] args = { goal, guess };
            return methodInfo.Invoke(_gameEngine, args)!.ToString()!;
        }
        
        [Theory]
        [InlineData("ROBIN")]
        [InlineData("R123")]
        [InlineData("ROÖ")]
        [InlineData("R0B")]
        [InlineData("ROBINHOOD")]
        public void PromptUsername_Returns_Valid_Username(string username)
        {
            // Arrange
            _mockIOHelper.Setup(x => x.PromptStringInput(It.IsAny<string>(), It.IsAny<string>())).Returns(username);

            // Act
            string result = RunPrivateMethod_PromptUsername();

            // Assert
            result.Should().Be(username);
        }

        private string RunPrivateMethod_PromptUsername()
        {
            MethodInfo methodInfo = typeof(BullsAndCowsGameEngine).GetMethod("PromptUsername", BindingFlags.NonPublic | BindingFlags.Instance)!;
            object[] args = Array.Empty<object>();
            return methodInfo.Invoke(_gameEngine, args)!.ToString()!;
        }

        [Theory]
        [InlineData("RobinHood123")]
        [InlineData("VeryLongUsername")]
        [InlineData("Not Ok")]
        [InlineData("VeryLongUsername With Whitespaces")]
        public void UsernameIsInvalid_Rejects_Invalid_Username(string username)
        {
            bool result = RunPrivateMethod_UsernameIsInvalid(username);

            result.Should().BeTrue();
        }

        private bool RunPrivateMethod_UsernameIsInvalid(string username)
        {
            MethodInfo methodInfo = typeof(BullsAndCowsGameEngine).GetMethod("UsernameIsInvalid", BindingFlags.NonPublic | BindingFlags.Instance)!;
            object[] args = { username };
            return (bool)methodInfo.Invoke(_gameEngine, args)!;
        }

        [Fact]
        public void GenerateStringOfDigits_Returns_Four_Digit_Integer_With_Unique_Numbers()
        {
            int result = RunPrivateMethod_GenerateStringOfDigits();

            int[] resultArray = Array.ConvertAll(result.ToString().ToArray(), x => int.Parse(x.ToString()));

            resultArray.Distinct().Count().Should().Be(resultArray.Length);
        }

        private int RunPrivateMethod_GenerateStringOfDigits()
        {
            MethodInfo methodInfo = typeof(BullsAndCowsGameEngine).GetMethod("GenerateStringOfDigits", BindingFlags.NonPublic | BindingFlags.Static)!;
            object[] args = Array.Empty<object>();
            return int.Parse(methodInfo.Invoke(_gameEngine, args)!.ToString()!);
        }
    }
}
