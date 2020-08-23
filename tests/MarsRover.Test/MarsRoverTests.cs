using System.Collections.Generic;
using MarsRover.Domain;
using Moq;
using NUnit.Framework;

namespace MarsRover.Test
{
    public class Tests
    {
        private Mock<IPlateau> _mockPlateau;
        private Mock<IMarsRover> _mockMarsRover;
        private IDictionary<char, Direction> _directionDict;

        [SetUp]
        public void Setup()
        {
            _mockPlateau = new Mock<IPlateau>();
            _mockMarsRover = new Mock<IMarsRover>();
        }

        [TestCase("5 5", "1 2 N", "LMLMLMLMM", 1, 3, Direction.North)]
        [TestCase("5 5", "3 3 E", "MMRMMRMRRM", 5, 1, Direction.East)]
        public void ShouldRoverFinalCoordinatesBeValidAsExpected(string plateauSize, string roverCoordinate,
            string roverMovementCommands, int expectedXResult, int expectedYResult, Direction directionResult)
        {
            // arrange
            _mockPlateau.Setup(x => x.ValidateRoverPosition(It.IsAny<Point>())).Returns(true);
            var rover = new MarsRover.Domain.MarsRover();
            
            // act
            rover.Launch(_mockPlateau.Object, roverCoordinate);
            rover.Move(roverMovementCommands);
            
            // assert
            Assert.NotNull(rover);
            Assert.NotNull(rover.RoverPosition);
            Assert.AreEqual(expectedXResult, rover.RoverPosition.X);
            Assert.AreEqual(expectedYResult, rover.RoverPosition.Y);
            Assert.AreEqual(directionResult, rover.RoverDirection);
        }

        [TestCase("0 0")]
        [TestCase("1 5")]
        [TestCase("2 2")]
        [TestCase("6 3")]
        public void ShouldPlateauInitializeAsExpected(string plateauSize)
        {
            // arrange
            _mockPlateau.Setup(x => x.ValidateRoverPosition(It.IsAny<Point>())).Returns(true);
            var plateau = new MarsRover.Domain.Plateau();
            
            // act
            plateau.InitializePlateau(plateauSize);

            var inputs = plateauSize.Split(' ');
            int.TryParse(inputs[0], out var width);
            int.TryParse(inputs[1], out var height);
            
            // assert
            Assert.AreEqual(plateau.Width, width);
            Assert.AreEqual(plateau.Height, height);
        }

        [TestCase("1 2 N")]
        [TestCase("5 3 W")]
        [TestCase("3 1 S")]
        [TestCase("2 2 E")]
        public void ShouldRoverLaunchAsExpected(string roverCoordinate)
        {
            // arrange
            _mockPlateau.Setup(x => x.ValidateRoverPosition(It.IsAny<Point>())).Returns(true);
            var plateau = new MarsRover.Domain.Plateau();
            var rover = new MarsRover.Domain.MarsRover();
            
            // act
            var plateauSize = "0 0";
            plateau.InitializePlateau(plateauSize);

            rover.Launch(_mockPlateau.Object, roverCoordinate);

            var roverPosition = roverCoordinate.Split(' ');
            int.TryParse(roverPosition[0], out int xPosition);
            int.TryParse(roverPosition[1], out int yPosition);
            var roverDirection = roverPosition[2][0];
            
            _directionDict = new Dictionary<char, Direction>
            {
                {'N', Direction.North},
                {'E', Direction.East},
                {'S', Direction.South},
                {'W', Direction.West}
            };
            
            // assert
            Assert.AreEqual(rover.RoverPosition.X, xPosition);
            Assert.AreEqual(rover.RoverPosition.Y, yPosition);
            Assert.AreEqual(rover.RoverDirection, _directionDict[roverDirection]);
        }
    }
}