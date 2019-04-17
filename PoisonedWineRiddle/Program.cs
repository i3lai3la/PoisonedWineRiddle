using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoisonedWineRiddle
{
    class Program
    {
        const string SAPARATOR = " | ";
        const string GUINEAPIG_STATUS_ALIVE = "ALIVE";
        const string GUINEAPIG_STATUS_DEAD = "DEAD";

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            Write($"**************************************************");
            Write($"");
            Write($"WELCOME TO POISONED WINE RIDDLE");
            Write($"");
            Write($"Please select :-");
            Write($"1 : Find Poisoned bottle");
            Write($"2 : Find Total Guinea pig needed based on wine bottle");
            Write($"");
            Write($"**************************************************");

            int selected = 0;
            if(!int.TryParse(Console.ReadLine(), out selected))
            {
                Write($"Incorrect entry!!");
                Menu();
            }

            switch (selected)
            {
                case 1:
                    FindPoisonedBottle();
                    break;
                case 2:
                    FindTotalGuineaPig();
                    break;
            };

            ContinuePlaying();
        }

        static void ContinuePlaying()
        {
            Write($"");
            Write($"**************************************************");
            Write($"");
            Write($"Would you like to redo the riddle?");
            Write($"Please select :-");
            Write($"1 : YES");
            Write($"0 : NO");
            Write($"");
            Write($"**************************************************");
           

            int selected = 0;
            if (!int.TryParse(Console.ReadLine(), out selected))
            {
                Write($"Incorrect entry!!");
                ContinuePlaying();
            }

            switch (selected)
            {
                case 1:
                    Console.Clear();
                    Menu();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Write($"Incorrect entry!!");
                    ContinuePlaying();
                    break;
            };
        }

        static void FindPoisonedBottle()
        {
            long tBottle = SetTotalWineBottle();
            long tSlave = GetTotalGuineaPigNeeded(tBottle);

            Write($"Total Wine bottle(s)  : {tBottle}");
            Write($"Total of guinea pig(s) : {tSlave}");

            var deadAliveResults = SetDeadGuineaPig(tSlave);
            string joinValue = string.Join("", deadAliveResults.Reverse());

            long result = Convert.ToInt64(joinValue, 2);

            Write($"###################### RESULT #####################");
            Write($"Wine bottle number {result} is poisoneous.");
            Write($"###################### RESULT #####################");
        }

        static void FindTotalGuineaPig()
        {
            long tBottle = SetTotalWineBottle();
            long tSlave = GetTotalGuineaPigNeeded(tBottle);
          
            Write($"###################### RESULT #####################");
            Write($"Total of guinea pig(s) needed : {tSlave}");
            Write($"###################### RESULT #####################");
        }

        static long[] SetDeadGuineaPig(long tSlave)
        {
            Write($"**************************************************");
            Write($"");
            Write($"We have {tSlave} guinea pig(s) line up");
            Write($"Please key in either 1 or 0 to represent either guinea pig is alive of dead.");
            Write($"0 : represent Alive");
            Write($"1 : represent Dead");
            Write($"");
            Write($"**************************************************");

            long[] deadAliveResults = new long[tSlave];
            string gpCount = string.Empty;
            string gpStatus = string.Empty;
            for (int a = 1; a <= tSlave; a++)
            {
                var deadAliveResult = SetDeadOrAlive(a);
                deadAliveResults[a-1] = deadAliveResult;
                gpCount += $"GP-{a}{SAPARATOR}";
                gpStatus += $"{GetDeadOrAliveStataus(deadAliveResult)}{SAPARATOR}";
            }
            gpCount = gpCount.TrimEnd(SAPARATOR.ToCharArray());
            gpStatus = gpStatus.TrimEnd(SAPARATOR.ToCharArray());

            Write($"");
            Write($"Your guinea pig(s) status :-");
            Write(gpCount);
            Write(gpStatus);

            //if user not satified, user can re-enter the result of the guinea pig
            deadAliveResults = IsStatusSatisfied(tSlave, deadAliveResults);

            return deadAliveResults;
        }

        static long[] IsStatusSatisfied(long tSlave, long[] tResult)
        {
            Write($"**************************************************");
            Write($"");
            Write($"Is the result correct?");
            Write($"1 : YES");
            Write($"0 : REDO");
            Write($"");
            Write($"**************************************************");
            
            int userSelection = -1;
            if (!int.TryParse(Console.ReadLine(), out userSelection))
            {
                Write($"Invalid selection!!");
                IsStatusSatisfied(tSlave, tResult);
            }

            switch (userSelection)
            {
                case 0:
                    tResult = SetDeadGuineaPig(tSlave);
                    break;
                case 1:
                default:
                    break;
            };

            return tResult;
        }

        static int SetDeadOrAlive(int slave)
        {
            Write($"Is Guinea Pig {slave} is dead or alive?");

            int result = -1;
            if (!int.TryParse(Console.ReadLine(), out result) || !(result == 1 || result == 0))
            {
                Write($"Please key in only 1 or 0.");
                result = SetDeadOrAlive(slave);
            }

            return result;
        }

        static long SetTotalWineBottle()
        {
            long bottleOfWine = 0;
            Write("Total wine bottle(s) in number : ");
            string inputStr = Console.ReadLine();

            if (!long.TryParse(inputStr, out bottleOfWine))
            {
                Write("");
                Write("Please key in valid value. ");
                Write("");
                bottleOfWine = SetTotalWineBottle();
            }

            return bottleOfWine;
        }

        static long GetTotalGuineaPigNeeded(long tBottle)
        {
            // round it up to ensure we have enough guinea pig for test
            return (long)Math.Ceiling(CalculatLog(tBottle));
        }
        
        static double CalculatLog(long n)
        {
            return Math.Log(n, 2);
        }

        static void Write(string value)
        {
            Console.WriteLine(value);
        }

        static string GetDeadOrAliveStataus(int status)
        {
            string result = string.Empty;
            switch (status)
            {
                case 0:
                    result = GUINEAPIG_STATUS_DEAD;
                    break;
                case 1:
                    result = GUINEAPIG_STATUS_ALIVE;
                    break;
                default:
                    break;
            };

            return result;
        }

    }
}
