using System;
using System.Collections.Generic;
using ToyRobot.Table;

namespace ToyRobot.Checker
{
    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    /// 
    /// this class implements ICheckMovements interface
    /// takes care to check that the commands sent comply with the requirements for moving
    /// The use of the interface allows us to decouple according 
    /// to the SRP of SOLID PRINCIPLES
    /// </summary>
    public class CheckerMovements : ICheckMovements
    {

        /// <summary>
        /// Interface Table to inject in constructor
        /// </summary>
        private readonly ITableSurface _tableSurface;
        

        /// <summary>
        /// constructor to instantiate a new Table Surface dimensions
        /// in order not to make the class strongly coupled with TableSurface
        /// I use dependency injection with interface
        /// 
        /// </summary>
        /// <param name="tableSurface"></param>
        public CheckerMovements(ITableSurface tableSurface)
        {
            _tableSurface = tableSurface;
   

        }
       
        /// <summary>
        /// List of string Direction 
        /// </summary>
        private readonly List<string> _directions = new List<string> { "north", "south", "west", "east" };

        /// <summary>
        /// checks if the move can be successful without falling off the table
        /// </summary>
        /// <param name="xCurrent"></param>
        /// <param name="yCurrent"></param>
        /// <param name="currentFacing"></param>
        /// <returns>Position</returns>
        public Position Check(int xCurrent, int yCurrent, string currentFacing)
        {
            // initialize with original position
            Position newPosition = new Position(xCurrent, yCurrent);

           
           
            switch (currentFacing.ToLower())
            {
                case "north":
                    yCurrent += 1;
                    break;
                case "south":
                    yCurrent -= 1;
                    break;
                case "west": 
                    xCurrent -= 1;
                    break;
                case "east":  
                    xCurrent += 1;
                    break;
                default:
                    break;
            }


            if (xCurrent >= 0 && xCurrent < _tableSurface.GetXDimension() && yCurrent >= 0 && yCurrent < _tableSurface.GetYDimension())
            {
                newPosition.X = xCurrent;
                newPosition.Y = yCurrent;
                
            }
            

            return newPosition; ;
        }

        /// <summary>
        /// checks if Place Action can be successful without falling off the table
        /// </summary>
        /// <param name="xCurrent"></param>
        /// <param name="yCurrent"></param>
        /// <param name="direction"></param>
        /// <returns>bool</returns>
        public bool CheckPlacing(int xCurrent, int yCurrent,string direction)
        {
            var result = _directions.Find(x => x.Equals(direction));
            if (string.IsNullOrEmpty(result))
                return false;
            return xCurrent <= _tableSurface.GetXDimension() && xCurrent >= 0 && yCurrent <= _tableSurface.GetYDimension() && yCurrent >= 0;
            
        }
    }


}
