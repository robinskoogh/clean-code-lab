using Application.Exceptions;
using Application.Interfaces;
using FluentAssertions;
using Infrastructure;
using Moq;

namespace Test.Infrastructure
{
    public class BullsAndCowsScoreKeeperTests
    {
        private const string ResultsFilename = "test-result.txt";

        private readonly Mock<IIOHelper> _mockIIOHelper;
        private readonly BullsAndCowsScoreKeeper _cabScoreKeeper;

        public BullsAndCowsScoreKeeperTests()
        {
            _mockIIOHelper = new Mock<IIOHelper>();
            _cabScoreKeeper = new(_mockIIOHelper.Object);
        }

        [Fact]
        public void Filename_Is_Not_Set_Throws_Exception()
        {
            // Arrange
            RemoveTxtFileIfItExists();
            Action act = () => _cabScoreKeeper.WriteToFile("", 1);

            // Act
            act.Should().Throw<FilenameNotSetException>()
                .WithMessage("You need to set the filename of the scorekeeper before using it");
        }

        [Fact]
        public void Filename_Has_Been_Set_Data_Is_Written_To_File()
        {
            // Arrange
            RemoveTxtFileIfItExists();
            _cabScoreKeeper.SetFilename(ResultsFilename);

            Action act = () => _cabScoreKeeper.WriteToFile("test", 1);

            // Act
            act.Should().NotThrow();

            // Assert
            StreamReader reader = new(ResultsFilename);
            var fileContent = reader.ReadLine();

            fileContent.Should().Be("test#&#1");
        }

        [Fact]
        public void Filename_Is_Invalid_Throws_InvalidFilenameException()
        {
            // Arrange
            Action act = () => _cabScoreKeeper.SetFilename("not^ allowed.txt");

            // Act
            act.Should().Throw<InvalidFilenameException>()
                .WithMessage("Filename may only contain alphanumerical characters, including ._- without white-spaces.");
        }

        private static void RemoveTxtFileIfItExists()
        {
            if (File.Exists(ResultsFilename))
                File.Delete(ResultsFilename);
        }
    }
}
