using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Rover
    {
        public const string incorrectGridSize = "Grid size is incorrect, please choose a grid between 2 and 9.";
        public const string incorrectGridParam = "Grid size is determined using numbers, please choose a grid between 2 and 9.";
        public const string incorrectStartingLoc = "The starting location entered is incorrect.";
        public const string roverOutOfBounds = "The Rover is going off the map! Cancelling remaining commands.";
        public const string greaterThanGridSize = "The last known location is greater than the grid size.";

        private readonly int[] val = { 1, 10, -1, -10 };
        private readonly char[] compass = { 'N', 'E', 'S', 'W' };

        private string rover_Commands;
        private int grid_Size;
        private char rover_Direction;
        private int rover_Location;

        public Rover(string gridsize, string beginningLocation)
        {
            gridsize = String.Concat(gridsize.Where(_ => !Char.IsWhiteSpace(_)));
            beginningLocation = String.Concat(beginningLocation.Where(_ => !Char.IsWhiteSpace(_)));

            if (gridsize.Length != 2)
            {
                throw new ArgumentException(Rover.incorrectGridSize);
            }

            if (beginningLocation.Length != 3)
            {
                throw new ArgumentException(Rover.incorrectStartingLoc);
            }

            try
            {
                grid_Size = Convert.ToInt32(gridsize);
            }
            catch (FormatException)
            {
                Console.WriteLine(Rover.incorrectGridParam);
                throw new FormatException(Rover.incorrectGridParam);
            }

            try
            {
                rover_Location = Convert.ToInt32(beginningLocation.Substring(0, 2));
                rover_Direction = char.ToUpper(Convert.ToChar(beginningLocation[2]));
            }
            catch (FormatException)
            {
                Console.WriteLine(Rover.incorrectStartingLoc);
                throw new FormatException(Rover.incorrectStartingLoc);
            }

            catch (InvalidCastException)
            {
                Console.WriteLine(Rover.incorrectStartingLoc);
                throw new InvalidCastException(Rover.incorrectStartingLoc);
            }

            if (rover_Location > grid_Size)
            {
                Console.WriteLine(Rover.greaterThanGridSize);
                throw new ArgumentOutOfRangeException();
            }
        }


        public int RoverLocation
        {
            get { return rover_Location; }
        }

        public char RoverDirection
        {
            get { return rover_Direction; }
        }

        public string RoverCommands
        {
            get { return rover_Commands; }
        }

        public void Turn_Move(string commands)
        {
            rover_Commands = commands.ToUpper();
            Console.WriteLine("I'm facing {0}", rover_Direction);
            foreach (char c in rover_Commands)
            {
                if (c == 'L')
                {
                    if (rover_Direction == 'N')
                    {
                        rover_Direction = 'W';
                        rover_Direction = compass[Array.IndexOf(val, val[Array.IndexOf(compass, rover_Direction)])];

                    }
                    else
                    {
                        rover_Direction = compass[Array.IndexOf(val, val[Array.IndexOf(compass, rover_Direction) - 1])];
                    }

                    Console.WriteLine("I'm turning to face {0}", rover_Direction);
                }


                if (c == 'R')
                {
                    if (rover_Direction == 'W')
                    {
                        rover_Direction = 'N';
                        rover_Direction = compass[Array.IndexOf(val, val[Array.IndexOf(compass, rover_Direction)])];

                    }
                    else
                    {
                        rover_Direction = compass[Array.IndexOf(val, val[Array.IndexOf(compass, rover_Direction) + 1])];
                    }

                    Console.WriteLine("The current direction is {0}", rover_Direction);
                }


                if (c == 'M')
                {
                    rover_Location = rover_Location + val[Array.IndexOf(compass, rover_Direction)];
                    if ((rover_Location > grid_Size) || (rover_Location < 0))
                    {
                        Console.WriteLine(Rover.roverOutOfBounds);
                        throw new ArgumentOutOfRangeException(Rover.roverOutOfBounds);

                    }
                    else
                    {
                        Console.WriteLine("I'm moving");
                        Console.WriteLine("I'm currently facing {0}\n", rover_Direction);
                    }
                }
            }
            updateLocation();
        }

        public void updateLocation()
        {
            Console.WriteLine("The Rover ended on {0} facing {1}", rover_Location, rover_Direction);
        }

        public static void Main()
        {
            bool done = false;
            while (!done)
            {
                Console.Write("\nRover Program Initiated. Please enter the size of the grid (2 values ranging from 2-9): ");
                string grid = Console.ReadLine();
                Console.Write("What are the last known coordinates of the Rover? ");
                string coords = Console.ReadLine();

                Rover eve = new Rover(grid, coords);

                Console.Write("Please enter the commands: ");
                string commands = Console.ReadLine();
                eve.Turn_Move(commands);
            }
        }
    }
}
