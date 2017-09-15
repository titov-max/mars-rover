namespace MarsRover
{
    public interface IDispatcher
    {
        void AddRover(int x, int y, char bearingSignal);
        string ExecuteQueue(int roverIndex, string commandQueue);
        string SendCommand(int roverIndex, char commandSignal);
        string GetRoverState(int roverIndex);
    }
}
