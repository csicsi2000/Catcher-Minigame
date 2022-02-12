using System;
using System.Text;
using System.Threading;

namespace Catcher
{
    class Program
    {
        static int yPosition;
        static int xPosition;

        static int chaserY;
        static int chaserX;

        static int difficulty;
        static char diff;
        static int score;

        static bool gameEnd;
        static bool restart = true;

        static int[] speeds = { 1000, 750, 500, 250, 150, 100 };
        static void Main(string[] args)
        {
            while (restart)
            {
                xPosition = 0;
                yPosition = 0;
                chaserX = 9;
                chaserY = 9;
                score = 0;
                gameEnd = false;

                Console.WriteLine("Run form the chaser! ");
                Console.WriteLine("X is your character");
                Console.WriteLine("The enemy is ¤");
                Console.WriteLine("Use W,A,S,D to move");
                Console.Write("Choose difficulty (1 is the easiest, 6 is the hardest): ");

                SetDifficulty();

                CountDown();

                StartGame();

                PrintBoard();
                Console.WriteLine();
                Console.WriteLine("He catched you!");
                Console.WriteLine("\nYour Score: " + score * Convert.ToInt32(diff));
                Console.WriteLine();

                restart = IsPlayAgain();
            }
        }
        static void CountDown()
        {
            Console.WriteLine();
            Console.Write("The game begins in ");
            Console.WriteLine();
            for (int i = 3; i > 0; i--)
            {
                Thread.Sleep(900);
                Console.Write(i + " ");
            }
            Thread.Sleep(900);
        }
        static bool IsPlayAgain()
        {
            Console.WriteLine("Try again? y/n");

            while (true)
            {
                char again = Console.ReadKey().KeyChar;
                if (again == 'y')
                {
                    Console.Clear();
                    return true;
                }
                else if (again == 'n')
                {
                    return false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Non valid character");
                }
            }
        }

        static void StartGame()
        {
            Console.Clear();
            Console.CursorVisible = false;

            var backgroundThread = new Thread(LoopMove);
            backgroundThread.Start();

            int count = 0;

            while (!gameEnd)
            {
                char key = Console.ReadKey().KeyChar;
                bool valid = Move(key);

                if (!valid)
                {
                    Console.WriteLine("Non valid movement");
                }

                if(count == 20)
                {
                    difficulty = difficulty - 50;
                    count = 0;
                }
                count++;
            }
            backgroundThread.Abort();

            Console.Clear();
            Console.CursorVisible = true;
        }

        static void SetDifficulty()
        {
            bool IsValid = false;


            while (!IsValid)
            {
                diff = Console.ReadKey().KeyChar;
                if (diff == '1')
                {
                    difficulty = speeds[0];
                    IsValid = true;
                }
                else if (diff == '2')
                {
                    difficulty = speeds[1];
                    IsValid = true;

                }
                else if (diff == '3')
                {
                    difficulty = speeds[2];
                    IsValid = true;

                }
                else if (diff == '4')
                {
                    difficulty = speeds[3];
                    IsValid = true;

                }
                else if (diff == '5')
                {
                    difficulty = speeds[4];
                    IsValid = true;

                }
                else if (diff == '6')
                {
                    difficulty = speeds[5];
                    IsValid = true;

                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Non valid number for difficulty");
                    IsValid = false;
                }
            }
        }
        static bool Move(char direction)
        {
            Console.SetCursorPosition(0, 10);
            Console.Write("                             ");
            Console.SetCursorPosition(0, 10);
            Console.SetCursorPosition(0, 11);
            Console.Write("  ");


            if (gameEnd)
            {
                return true;
            }
            bool success = false;
            if (direction == 'w' || direction == 'W')
            {
                if (Check(yPosition - 1))
                {
                    yPosition = yPosition - 1;
                    success = true;
                }
            }
            if (direction == 's' || direction == 'S')
            {
                if (Check(yPosition + 1))
                {
                    yPosition = yPosition + 1;
                    success = true;
                }
            }
            if (direction == 'a' || direction == 'A')
            {
                if (Check(xPosition - 1))
                {
                    xPosition = xPosition - 1;
                    success = true;
                }
            }
            if (direction == 'd' || direction == 'D')
            {
                if (Check(xPosition + 1))
                {
                    xPosition = xPosition + 1;
                    success = true;
                }
            }

            PrintBoard();

            IsGameEnd();

            return success;
        }
        public static bool Check(int position)
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

        public static void PrintBoard()
        {
            Console.SetCursorPosition(0, 0);

            StringBuilder printBoard = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == chaserY && j == chaserX)
                    {
                        printBoard.Append("¤ ");
                    }
                    else if (i == yPosition && j == xPosition)
                    {
                        printBoard.Append("X ");
                    }
                    else
                    {
                        printBoard.Append(". ");
                    }
                }
                printBoard.Append("\n");
            }

            Console.Write(printBoard);
        }

        public static void LoopMove()
        {
            while (!gameEnd)
            {
                IsGameEnd();

                Thread.Sleep(difficulty);
                Chase();

                PrintBoard();

                score++;
            }
        }

        public static void IsGameEnd()
        {
            if (chaserX == xPosition && chaserY == yPosition)
            {
                gameEnd = true;
            }
        }

        public static void Chase()
        {
            int xDiff = Math.Abs(chaserX - xPosition);
            int yDiff = Math.Abs(chaserY - yPosition);

            if (xDiff > yDiff)
            {
                int chaseD = chaserX - xPosition;
                if (chaseD > 0)
                {
                    chaserX = chaserX - 1;
                }
                if (chaseD < 0)
                {
                    chaserX = chaserX + 1;
                }
            }
            else if (yDiff >= xDiff)
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

