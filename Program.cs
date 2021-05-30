using System;

namespace Struct_and_Enum
{
    class Program
    {
        static void Main(string[] args)
        {
            double score = 0;
            int Numofdifficult = 0;

            PageMainMenu(ref score, ref Numofdifficult);
        }

        static void ShowScoreDifficult(double Score, int DifficultCode)
        {
            Console.WriteLine("Player Score: {0}, Difficulty: {1}", Score, (Difficulty)DifficultCode);
        }

        static void PageMainMenu(ref double score, ref int Numofdifficult)
        {
            double ResetScore = 0;
            ShowScoreDifficult(score, Numofdifficult);

            Console.WriteLine("Please input 0 - 2 to choose page what you want to go: ");
            Console.WriteLine("Input 0 to Game Page");
            Console.WriteLine("Input 1 to Setting Page");
            Console.WriteLine("Input 2 to End Program");

            bool ValidPages = false;
            while (ValidPages == false)
            {
                int PageNum = int.Parse(Console.ReadLine());
                if (PageNum == 0)
                {
                    PagePlayGameAndCalculateScore(ref score, Numofdifficult);
                    PageMainMenu(ref score, ref Numofdifficult);
                    ValidPages = true;
                }
                else if (PageNum == 1)
                {
                    PageSetting(ref Numofdifficult);
                    PageMainMenu(ref ResetScore, ref Numofdifficult);
                    ValidPages = true;
                }
                else if (PageNum == 2)
                {
                    ValidPages = true;
                }
                else
                {
                    Console.WriteLine("Please input 0 - 2");
                }
            }
        }

        static int PageSetting(ref int Numofdifficult)
        {
            Console.WriteLine("Input 0 -2 to choose Difficulty");
            bool ValidNum = false;
            while (ValidNum == false)
            {
                Numofdifficult = int.Parse(Console.ReadLine());
                switch (Numofdifficult)
                {
                    case 0:
                        ValidNum = true;
                        break;
                    case 1:
                        ValidNum = true;
                        break;
                    case 2:
                        ValidNum = true;
                        break;
                    default:
                        Console.WriteLine("Please input 0 - 2");
                        break;
                }
            }
            return Numofdifficult;
        }

        static double PagePlayGameAndCalculateScore(ref double score ,int Numofdifficult)
        {
            int trueAnswer = 0;
            int numQuestion = 0;
            switch (Numofdifficult)
            {
                case 0:
                    numQuestion = 3;
                    break;
                case 1:
                    numQuestion = 5;
                    break;
                case 2:
                    numQuestion = 7;
                    break;
            }

            //CalScore
            double timeBefore = DateTimeOffset.Now.ToUnixTimeSeconds();
            //Console.WriteLine(timeBefore);
            Problem[] Questions = GenerateRandomProblems(numQuestion);
            for (int i = 0; i < numQuestion; i++)
            {
                Console.WriteLine("Question {0}: {1}", i + 1, Questions[i].Message);
                int PlayerAnswer = int.Parse(Console.ReadLine());
                if (PlayerAnswer == Questions[i].Answer)
                {
                    trueAnswer++;
                    //Console.WriteLine("Now. Your trueAnswer is {0}", trueAnswer);
                }
                else
                { /*Console.WriteLine("Daeng! your answer is incorrect."); */}
            }
            double timeAfter = DateTimeOffset.Now.ToUnixTimeSeconds();
            //Console.WriteLine(timeAfter);
            double deltaT = timeAfter - timeBefore;
            double divider = 25 - Math.Pow(Numofdifficult, 2);
            score = ((double)trueAnswer / (double)numQuestion) * (divider / (Math.Max(deltaT, divider))) * (Math.Pow (2 * (Numofdifficult) + 1, 2));
            return score;
        }

        enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }
        struct Problem
        {
            public string Message;
            public int Answer;

            public Problem(string message, int answer)
            {
                Message = message;
                Answer = answer;
            }
        }

        static Problem[] GenerateRandomProblems(int numProblem)
        {
            Problem[] randomProblems = new Problem[numProblem];

            Random rnd = new Random();
            int x, y;

            for (int i = 0; i < numProblem; i++)
            {
                x = rnd.Next(50);
                y = rnd.Next(50);
                if (rnd.NextDouble() >= 0.5)
                    randomProblems[i] = new Problem(String.Format("{0} + {1} = ?", x, y), x + y);
                else
                    randomProblems[i] = new Problem(String.Format("{0} - {1} = ?", x, y), x - y);
            }
            return randomProblems;
        }

    }
}
