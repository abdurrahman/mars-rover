namespace MarsRover.Domain
{
    public interface IMarsRover
    {
        Point RoverPosition { get; set; }

        Direction RoverDirection { get; set; }
        
        /// <summary>
        /// Launching the Mars rover with given position
        /// </summary>
        /// <param name="plateau">Plateau instance</param>
        /// <param name="roverPlace">The Rover's x,y coordinates</param>
        void Launch(IPlateau plateau, string roverPlace);

        
        /// <summary>
        /// Runs of Mars rover's movement with a list of commands 
        /// </summary>
        /// <param name="movementCommands">A list of movement commands</param>
        void Move(string movementCommands);
    }
}