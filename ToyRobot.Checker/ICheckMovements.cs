namespace ToyRobot.Checker
{

    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    /// 
    /// ICheckMovements interface
    /// </summary>
    public interface ICheckMovements
    {
        Position Check(int xCurrent, int yCurrent, string currentFacing);

        bool CheckPlacing(int xCurrent, int yCurrent,string direction);
    }


}
