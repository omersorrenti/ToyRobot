using System;
using System.CodeDom;
using System.Xml;
using ToyRobot.Checker;
using ToyRobot.Table;

namespace ToyRobot.Behavior
{

    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    /// 
    /// this class implements ICommands interface
    /// takes care to manage logic of movements and status
    /// The use of the interface allows us to decouple according 
    /// to the OCP of SOLID PRINCIPLES
    /// 
    /// if you try to get the robot out of the grid, the robot remains at the last position
    /// </summary>
    public class RobotCommands : IRobotCommands
    {

        // indicate if print report automatically after every move
        private readonly bool _autoReportOnCommand;

        // Table Surface
        private readonly ITableSurface _tableSurface;

        // Object for Check Robot Rules
        private readonly Checker.ICheckMovements _robotChecker;




        /// <summary>
        /// Determinate if robot is placed
        /// </summary>
        private bool Placed { get; set; }


        /// <summary>
        /// Current X
        /// </summary>
        public int XCurrent { get; set; }

        /// <summary>
        /// Current Y
        /// </summary>
        public int YCurrent { get; set; }

        /// <summary>
        /// Current direction of Robot
        /// </summary>
        public string CurrentDirection { get; set; }

      

        /// <summary>
        /// Check if Robot is placed
        /// </summary>
        /// <returns></returns>
        private bool CheckPlaced()
        {
            if (Placed) return true;
            throw new InvalidOperationException("Robot not placed!");
            
        }


        /// <summary>
        /// Robot Constructor set auto report property and Table Surface Dimensions
        /// </summary>
        /// <param name="autoReportOnMove"></param>
        /// <param name="tableSurface"></param>
        public RobotCommands(bool autoReportOnMove,ITableSurface tableSurface)
        {
            _tableSurface = tableSurface ?? new TableSurface(0,0);

            if (!_tableSurface.CheckSurfaceDimension())
            {
                throw new ArgumentException( "Please set Surface dimension");

            }

           

            _robotChecker = new Checker.CheckerMovements(tableSurface);

            _autoReportOnCommand = autoReportOnMove;

            CurrentDirection = string.Empty;

            
        }

  


        /// <summary>
        /// Place Robot on dinamic dimension Surfaced instantiated in constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="facing"></param>
        public void Place(int x, int y, string facing)
        {
            if (_robotChecker == null)
            {
                throw new NullReferenceException("Please Instantiate a Robot first with Table Dimensions");
            
            }
            else if (!_robotChecker.CheckPlacing(x, y, facing))
            {
                throw new InvalidOperationException("Placing Failed");
                
            }
            else
            {
                Placed = true;
                CurrentDirection = facing;
                XCurrent = x;
                YCurrent = y;

            }
        }

        /// <summary>
        /// Move Robot (a unit)
        /// </summary>
        public void Move()
        {
            if (!CheckPlaced())
                return;

            Position newPosition = _robotChecker.Check(XCurrent, YCurrent, CurrentDirection);
         
            XCurrent = newPosition.X;
            YCurrent = newPosition.Y;
         
        
        }


        /// <summary>
        /// Print Current Robot Position and Direction
        /// </summary>
        public void Report()
        {
            if (!CheckPlaced())
                return;
            

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("REPORT: Direction {0} position: ({1},{2}) at: {3}" ,CurrentDirection.ToUpper(),XCurrent,YCurrent,DateTime.Now);
            Console.ResetColor();
        }


        /// <summary>
        /// Turn Left robot without move
        /// </summary>
        public void TurnLeft()
        {
            if (!CheckPlaced())
                return;


            switch (CurrentDirection)
            {
                case "north":
                    CurrentDirection = "west";
                    break;
                case "south":
                    CurrentDirection= "east";
                    break;
                case "west": 
                    CurrentDirection = "south";
                    break;
                case "east":
                    CurrentDirection = "north"; 
                    break;
                default:
                    break;
               
            }

        }

        /// <summary>
        /// Turn Right robot without move
        /// </summary>
        public void TurnRight()
        {
            if (!CheckPlaced())
                return;


            switch (CurrentDirection)
            {
                case "north":
                    CurrentDirection = "east";
                    break;
                case "south":
                    CurrentDirection = "west";
                    break;
                case "west": 
                    CurrentDirection = "north";
                    break;
                case "east": 
                    CurrentDirection = "south";
                    break;
                default:
                    break;

            }
        }

       

        /// <summary>
        /// Manage input command string interpretate by robot
        /// </summary>
        /// <param name="line"></param>
        public void SendCommand(string line)
        {
            try
            {
                if (string.IsNullOrEmpty(line))
                {
                    throw new ArgumentNullException ("Please insert a vaild command!");
                    
                }

                var lineCommand = line.Split(',');
                int intResX = 0;
                int intResY = 0;
                string direction = string.Empty;


                if (lineCommand.Length > 1 && lineCommand.Length == 3)
                {
                    if (lineCommand[0].ToLower().StartsWith("place"))
                    {
                        string xTemp = lineCommand[0].ToLower().Replace("place", string.Empty).Trim();
                        if (!int.TryParse(xTemp, out intResX) ||
                            !int.TryParse(lineCommand[1].Trim(), out intResY))
                        {
                            throw new ArgumentException("Insert valid X,Y coordinates");
                            
                        }
                        else
                        {
                            direction = lineCommand[2].Trim();
                            line = "place";
                        }
                    }


                }
                else if (lineCommand.Length != 1)
                {
                    throw new ArgumentException("Insert valid parameters");
                    
                }


                switch (line.ToUpper())
                {
                    case "PLACE":
                        Place(intResX, intResY, direction);
                        break;

                    case "MOVE":
                        Move();
                        break;

                    case "LEFT":
                        TurnLeft();
                        break;

                    case "RIGHT":
                        TurnRight();
                        break;
                    case "REPORT":
                        Report();
                        break;

                    default:
                        throw new ArgumentException("Send a valid message");
                        
                }
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException( ex.Message);
            }

            if (_autoReportOnCommand)
                Report();

        }

     
    }

}
