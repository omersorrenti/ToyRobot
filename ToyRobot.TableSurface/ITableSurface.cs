namespace ToyRobot.Table
{
    public interface ITableSurface
    {

        int GetXDimension();

        int GetYDimension();

        /// <summary>
        /// Check if Table Dimensions are correct
        /// </summary>
        /// <returns></returns>
        bool CheckSurfaceDimension();
    }
}