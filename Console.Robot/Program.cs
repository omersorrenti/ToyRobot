

using System;
using System.Runtime.Remoting.Channels;
using System.Xml;
using ToyRobot.Table;
using ToyRobot.Behavior;

namespace Console.Robot
{
    /// <summary>
    /// OMER SORRENTI 
    /// 05/04/2019
    /// 
    /// This application console is a user interface of robot
    /// It implements interaction with final users
    /// </summary>
    class Program
    {
        //Message Types
        enum MessageType { Error,Info};

        // Robot instance
        static IRobotCommands _robot = null;

        // Table dimensions
        private const int XTable = 5;
        private const int YTable = 5;

        static void Main(string[] args)
        {
            Start();
        }

        /// <summary>
        /// initializes the robot and starts the communication
        /// </summary>
        private static void Start()
        {
            try
            {

                _robot = null;
                PrintHelpInfo();
                InitializeRobot();
                StartCommunication();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        /// <summary>
        /// initializes Robot and pass table dimension surface
        /// Ask to enable Autoreport
        /// </summary>
        private static void InitializeRobot()
        {
            System.Console.WriteLine("Do you want enabled auto report after every command? Y/N");

            string line = System.Console.ReadLine(); 

            if (line != null && line.ToLower() == "y")
            {
                var surface = new TableSurface(XTable, YTable);
                _robot = new ToyRobot.Behavior.RobotCommands(true, surface);
            }
            else
            {
                PrintConsoleMessage("Invalid Command",MessageType.Error);
                InitializeRobot();
            }
        }

        /// <summary>
        ///  Print Help info to user
        /// </summary>
        private static void PrintHelpInfo()
        {
            System.Console.WriteLine("------------------------------------------------");
            System.Console.WriteLine("WELCOME!");
            System.Console.WriteLine("Commands available:");
            System.Console.WriteLine("PLACE X,Y,F  (f=north,south,east,west)");
            System.Console.WriteLine("MOVE");
            System.Console.WriteLine("LEFT");
            System.Console.WriteLine("RIGHT");
            System.Console.WriteLine("REPORT");
            System.Console.WriteLine("");

            System.Console.WriteLine("EXIT");
            System.Console.WriteLine("Note:");
            System.Console.WriteLine("You must send place as first commands!");
            System.Console.WriteLine("------------------------------------------------");
        }

        /// <summary>
        /// Manage iteration with user
        /// </summary>
        private static void StartCommunication()
        {

            
            while (true)
            {
                try
                {
                    string line = String.Empty;
                    System.Console.WriteLine("Send a command to robot...");
                    line = System.Console.ReadLine();
                    if (line != null && line.ToLower().Equals("exit"))
                    {
                        System.Console.WriteLine("Are you shure? Y/N");
                        string exit = System.Console.ReadLine();
                        if (exit != null && exit.ToLower().Equals("y"))
                            Environment.Exit(0);

                    }
                    else if (line != null && line.ToLower().Equals("start"))
                    {
                        break;
                    }

                    if(_robot==null)
                        InitializeRobot();

                    _robot.SendCommand(line);

                }
                catch (Exception ex)
                {
                    PrintConsoleMessage(ex.Message, MessageType.Error);
                }

            }

            Start();


        }

        /// <summary>
        /// Print Robot message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="mType"></param>
        private static void PrintConsoleMessage(string message, MessageType mType)
        {
            switch (mType)
            {
                case MessageType.Error:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case MessageType.Info:
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    System.Console.ForegroundColor = ConsoleColor.White;
                    break;
            }


            System.Console.WriteLine(message);
            System.Console.ResetColor();



        }


    }




}
