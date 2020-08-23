namespace MarsRover.Domain
{
    public class Point
    {
        public int X { get; }

        public int Y { get; }

        public Point(int xPosition, int yPosition)
        {
            X = xPosition;
            Y = yPosition;
        }
    }
}
