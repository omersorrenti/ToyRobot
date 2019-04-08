using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToyRobot.Behavior;
using ToyRobot.Table;

namespace ToyRoboot.Behavior.Test
{
    [TestClass]
    public class RobotCommandsTest
    {


        /// <summary>
        /// Check Movments without Placing
        /// Robot must ignore commands default 0,0
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void test_RobotCommands_Table5X5_CheckMoveWithoutPlace_ReturnZeroZero()
        {

            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.Move();
            robot.Move();
            robot.TurnLeft();
            robot.Move();
            robot.Report();


            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual(String.Empty, robot.CurrentDirection);
        }



        /// <summary>
        /// Check Place (1,2) east,  Move, Move, Left, Move, Report
        /// Result 3,3 north
        /// </summary>
        [TestMethod]
        public void test_RobotCommands_Table5X5_CheckPlaceEastx1y2_2TimesMove_TurnLeft_Move_Report_Resut_3_3_north()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.Place(1, 2, "east");
            robot.Move();
            robot.Move();
            robot.TurnLeft();
            robot.Move();
            robot.Report();

            Assert.AreEqual(3, robot.XCurrent);
            Assert.AreEqual(3, robot.YCurrent);
            Assert.AreEqual("north", robot.CurrentDirection);

        }



        /// <summary>
        /// Check Place (1,2) east,  Move, Move, Right, Move, Report
        /// Result 3,1 south
        /// </summary>
        [TestMethod]
        public void test_RobotCommands_Table5X5_CheckPlaceEastx1y2_2TimesMove_TurnRight_Move_Report_Resut_3_1_south()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.Place(1, 2, "east");
            robot.Move();
            robot.Move();
            robot.TurnRight();
            robot.Move();
            robot.Report();

            Assert.AreEqual(3, robot.XCurrent);
            Assert.AreEqual(1, robot.YCurrent);
            Assert.AreEqual("south", robot.CurrentDirection);

        }

        /// <summary>
        /// Check Place (20,2) north 
        /// Result 0,0 north
        /// because placing is out of Surface range
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void test_RobotCommands_Table5X5_CheckPlaceOutSurface_Result_0_0_north()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.Place(20, 2, "north");
                      

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual(string.Empty, robot.CurrentDirection);
            
        }


        /// <summary>
        /// Check  times Left starting from 0,0,north direction
        /// Result 0,0 east
        /// because placing is out of Surface range
        /// </summary>
        [TestMethod]
        public void test_RobotCommands_Table5X5_3TimesLeftFromNorth_Result_0_0_east()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.Place(0, 0, "north");
            robot.TurnLeft();
            robot.TurnLeft();
            robot.TurnLeft();

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual("east", robot.CurrentDirection);
            
        }

        /// <summary>
        /// Check  send command integrity
        /// Sent Place(0,0,north)
       
        /// </summary>
        [TestMethod]
        public void test_RobotCommands_Table5X5_SendCommand00north_Result_0_0_north()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.SendCommand("place 0,0,north");

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual("north", robot.CurrentDirection);

        }


        /// <summary>
        /// Check  send command integrity
        /// Sent bad command Pla_ce(0,0,north)

        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void test_RobotCommands_Table5X5_SendCommand_Place00north_Result_0_0_empty_warning()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.SendCommand("pla_ce 0,0,north");

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual(string.Empty, robot.CurrentDirection);
            
        }


        /// <summary>
        /// Check  send command integrity
        /// Sent empty command 
        /// return 0,0,'', "Send a valid message"
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void test_RobotCommands_Table5X5_SendCommand_empty_Result_0_0_warning()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.SendCommand("pla_ce 0,0,north");

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual(string.Empty, robot.CurrentDirection);
            
        }

        /// <summary>
        /// Check  send command integrity
        /// Sent null command 
        /// return 0,0,'', "Send a valid message"
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void test_RobotCommands_Table5X5_SendCommand_null_Result_0_0_warning()
        {
            ITableSurface surface = new TableSurface(5, 5);
            IRobotCommands robot = new RobotCommands(false, surface);
            robot.SendCommand(null);

            Assert.AreEqual(0, robot.XCurrent);
            Assert.AreEqual(0, robot.YCurrent);
            Assert.AreEqual(string.Empty, robot.CurrentDirection);
            
        }
    }
}
