namespace MarsRover.Domain
{
    /// <summary>
    /// Represents a plateau on Mars
    /// </summary>
    public class Plateau
    {
        public int Width { get; private set; }

        public int Height { get; private set; }
        
        public void InitializePlateau(string plateauSize)
        {
            var inputs = plateauSize.Split(' ');
            int.TryParse(inputs[0], out var width);
            int.TryParse(inputs[1], out var height);

            Width = width;
            Height = height;
        }
        
        public bool ValidateRoverPositionOnThePlateau(Point roverPosition)
        {
            var xCoordinate = roverPosition.X >= 0 && roverPosition.X <= Width;
            var yCoordinate = roverPosition.Y >= 0 && roverPosition.Y <= Height;
            return xCoordinate && yCoordinate;
        }
    }
}