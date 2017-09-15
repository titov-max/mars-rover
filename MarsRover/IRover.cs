namespace MarsRover
{
    public interface IRover
    {
        Location CurrentLocation { get; }
        Location PredictedLocation { get; }
        string State { get; }

        void TurnLeft();
        void TurnRight();
        void Move();
    }
}
