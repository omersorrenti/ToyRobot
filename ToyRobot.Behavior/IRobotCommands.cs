namespace ToyRobot.Behavior
{
    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    ///
    /// ICommands interface
    /// </summary>
    public interface IRobotCommands
    {
        int XCurrent { get;  set; }

     
        int YCurrent { get;  set; }

      
        string CurrentDirection { get;  set; }


        

        void Place(int x, int y, string direction);

        void TurnLeft();

        void TurnRight();

        void Move();

        void Report();

        void SendCommand(string line);

    }
}
