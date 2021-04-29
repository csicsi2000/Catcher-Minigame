﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Catcher
{
    class Program
    {
        public static int yPosition ;
        public static int xPosition ;

        public static int chaserY;
        public static int chaserX;

        public static int difficulty;
        public static int score;

        public static bool gameEnd;
        static void Main(string[] args)
        {
        start:
            xPosition = 0;
            yPosition = 0;
            chaserX = 9;
            chaserY = 9;
            score = 0;
            gameEnd = false;

            Console.WriteLine("Escape the chaser! ");
            Console.WriteLine("Your character is X");
            Console.WriteLine("Enemy character is ¤");
            Console.WriteLine("Use W,A,S,D to move your character");
            Console.Write("Choose you difficulty with number (1 is the easiest, 6 is the hardest): ");
            bool IsValid = false;
            while (!IsValid)
            {
                char diff = Console.ReadKey().KeyChar;
                if (diff == '1')
                {
                    difficulty = 1000;
                    IsValid = true;
                }
                else if (diff == '2')
                {
                    difficulty = 750;
                    IsValid = true;

                }
                else if (diff == '3')
                {
                    difficulty = 500;
                    IsValid = true;

                }
                else if (diff == '4')
                {
                    difficulty = 250;
                    IsValid = true;

                }
                else if (diff == '5')
                {
                    difficulty = 150;
                    IsValid = true;

                }
                else if (diff == '6')
                {
                    difficulty = 100;
                    IsValid = true;

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Not valid difficulty setting");
                    IsValid = false;
                }
            }
            Console.WriteLine();
            Console.Write("Game starting in ");
            Console.WriteLine();
            for(int i = 3; i > 0; i--)
            {
                Thread.Sleep(900);
                Console.Write( i +" ");
            }
            var background = new ThreadStart(loopMove);
            var backgroundThread = new Thread(background);
            backgroundThread.Start();

            var boardT = new ThreadStart(board);
            var boardLoop = new Thread(boardT);
            boardLoop.Start();

            while (!gameEnd)
            {
                char key =Console.ReadKey().KeyChar;
                bool valid = move(key);

                if (!valid)
                {
                    Console.WriteLine("Invalid movement");
                }                
            }
            backgroundThread.Abort();
            boardLoop.Abort();

            Console.WriteLine();
            Console.WriteLine("He catched you!");
            Console.WriteLine("Your score is: " + score);
            Console.WriteLine();
            Console.WriteLine("Try again? y/n");

            bool YN = false;
            while (!YN)
            {
                char again = Console.ReadKey().KeyChar;
                if (again == 'y') 
                {
                    Console.Clear();
                    goto start;
                }
                else if(again == 'n')
                {
                    YN = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid character");
                }
            }
        }

        static bool move (char direction)
        {
            bool success = false;
            if(direction == 'w' || direction == 'W')
            {
                if (check(yPosition - 1))
                {
                    yPosition = yPosition - 1;
                    success = true;
                }
            }
            if (direction == 's' || direction == 'S')
            {
                if (check(yPosition + 1))
                {
                    yPosition = yPosition + 1;
                    success = true;
                }
            }
            if (direction == 'a' || direction == 'A')
            {
                if (check(xPosition - 1))
                {
                    xPosition = xPosition - 1;
                    success = true;
                }
            }
            if (direction == 'd' || direction == 'D')
            {
                if (check(xPosition + 1))
                {
                    xPosition = xPosition + 1;
                    success = true;
                }
            }
            

            return success;
        }
        public static bool check(int position)
        {
            if (position < 0 || position >= 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void board()
        {
            while (!gameEnd)
            {
                Thread.Sleep(difficulty);
                Console.WriteLine();
                Console.WriteLine();
                StringBuilder printBoard = new StringBuilder();
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i == yPosition && j == xPosition)
                        {
                            printBoard.Append("X ");
                        }
                        else if (i == chaserY && j == chaserX)
                        {
                            printBoard.Append("¤ ");
                        }
                        else
                        {
                            printBoard.Append(". ");
                        }
                    }
                    printBoard.Append("\n");
                }
                Console.WriteLine(printBoard);
            }
        }

        public static void loopMove()
        {
            while (!gameEnd)
            {
                if (chaserX == xPosition && chaserY == yPosition)
                {
                    gameEnd = true;
                }
                Thread.Sleep(difficulty);
                chase();

                score++;
            }
        }     

        public static void chase()
        {
            int xDiff = Math.Abs(chaserX - xPosition);
            int yDiff = Math.Abs(chaserY - yPosition);

            if (xDiff > yDiff)
            {
                int chaseD = chaserX - xPosition;
                if(chaseD > 0)
                {
                    chaserX = chaserX - 1;
                }
                if(chaseD < 0)
                {
                    chaserX = chaserX + 1;
                }
            }
            else if(yDiff >= xDiff)
            {
                int chaseD = chaserY - yPosition;
                if (chaseD > 0)
                {
                    chaserY = chaserY - 1;
                }
                if (chaseD < 0)
                {
                    chaserY = chaserY + 1;
                }
            }
        }
    }
}
