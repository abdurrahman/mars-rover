namespace MarsRover.Domain
{
    public interface IPlateau
    {
        void InitializePlateau(string plateauSize);
        
        /// <summary>
        /// Validation of the Rover's position on the plateau
        /// </summary>
        /// <param name="roverPosition"></param>
        /// <returns></returns>
        bool ValidateRoverPosition(Point roverPosition);
    }
}