using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot.Table
{
    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    /// 
    /// this class takes care of creating and controlling the table surface
    /// </summary>
    public class TableSurface : ITableSurface
    {
        private int _xDimension;
        private int _yDimension;
        private void SetXDimension(int value)
        {
            _xDimension = value;
        }
        private void SetYDimension(int value)
        {
            _yDimension = value;
        }
        public int GetXDimension()
        {
            return _xDimension;
        }
        public int GetYDimension()
        {
            return _yDimension;
        }

        


        /// <summary>
        /// Constructor for create Surface Table object
        /// </summary>
        /// <param name="xDimension"></param>
        /// <param name="yDimension"></param>
        public TableSurface(int xDimension, int yDimension)
        {
            SetXDimension(xDimension);
            SetYDimension(yDimension);
        }


     
        /// <summary>
        /// Check if Table Dimensions are correct
        /// </summary>
        /// <returns></returns>
        public bool CheckSurfaceDimension()
        {
            if (GetXDimension() != GetYDimension() || GetXDimension() <= 0 || GetYDimension() <= 0)
                return false;

            return true;
        }

    }
}
