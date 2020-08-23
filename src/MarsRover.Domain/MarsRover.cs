using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MarsRover.Domain
{
    public class MarsRover : IMarsRover
    {
        public Point RoverPosition { get; set; }
        public Direction RoverDirection { get; set; }

        private readonly IDictionary<char, Direction> _directionDict;
        private readonly IDictionary<char, Movement> _movementDict;    

        public MarsRover()
        {
            _directionDict = new Dictionary<char, Direction>
            {
                {'N', Direction.North},
                {'E', Direction.East},
                {'S', Direction.South},
                {'W', Direction.West}
            };
            
            _movementDict = new Dictionary<char, Movement>
            {
                {'L', Movement.Left},
                {'R', Movement.Right},
                {'M', Movement.Forward}
            };
        }

        public void Launch(IPlateau plateau, string roverPlace)
        {
            var roverPosition = roverPlace.Split(' ');
            
            int.TryParse(roverPosition[0], out int xPosition);
            int.TryParse(roverPosition[1], out int yPosition);
            RoverPosition = new Point(xPosition, yPosition);

            if (!plateau.ValidateRoverPosition(RoverPosition))
            {
                throw new InvalidDataException(
                    $"Rover position {xPosition}:{yPosition} seems out of the dimension of current plateau");
            }
            
            var roverDirection = roverPosition[2][0];
            RoverDirection = _directionDict[roverDirection];
        }

        public void Move(string movementCommands)
        {
            var input = movementCommands.ToCharArray();
            var commands = input.Select(x => _movementDict[x]).ToList();
            
            foreach (var movement in commands)
            {
                switch (movement)
                {
                    case Movement.Left:
                        LeftMove(RoverDirection);
                        break;
                    case Movement.Right:
                        RightMove(RoverDirection);
                        break;
                    case Movement.Forward:
                        MoveForward(RoverDirection);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        #region Utilities

        private Direction LeftMove(Direction direction) =>
            direction switch
            {
                Direction.North => RoverDirection = Direction.West,
                Direction.East => RoverDirection = Direction.North,
                Direction.South => RoverDirection = Direction.East,
                Direction.West => RoverDirection = Direction.South
            };

        private Direction RightMove(Direction direction) =>
            direction switch
            {
                Direction.North => RoverDirection = Direction.East,
                Direction.East => RoverDirection = Direction.South,
                Direction.South => RoverDirection = Direction.West,
                Direction.West => RoverDirection = Direction.North
            };

        private Point MoveForward(Direction direction) =>
            direction switch
            {
                Direction.North => RoverPosition = new Point(RoverPosition.X, RoverPosition.Y + 1),
                Direction.East => RoverPosition = new Point(RoverPosition.X + 1, RoverPosition.Y),
                Direction.South => RoverPosition = new Point(RoverPosition.X, RoverPosition.Y - 1),
                Direction.West => RoverPosition = new Point(RoverPosition.X - 1, RoverPosition.Y)
            };

        #endregion
    }
}