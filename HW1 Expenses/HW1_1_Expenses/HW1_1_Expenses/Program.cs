using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string function = "0";

            int totalIncome = 0;
            double totalOutcome = 0;
            string outcomeCase = "0"; //食衣住行育樂選擇功能

            //存取食衣住行育樂分別的支出金額
            //food = 0, clothes = 0, living = 0, trans = 0, entertain = 0
            int[] outcome = { 0, 0, 0, 0, 0, 0 };

            while (function != "5")
            {
                Console.Write("(1)輸入收入 (2)輸入支出 (3)計算比例 (4)剩餘金額 (5)退出程式\n");
                Console.Write("輸入數字選擇功能: ");
                function = Console.ReadLine();


                if (function == "1")
                {
                    Console.Write("輸入金額: ");
                    totalIncome += int.Parse(Console.ReadLine());
                }

                else if (function == "2")
                {
                    Console.WriteLine("(1)食 (2)衣 (3)住 (4)行 (5)育樂");
                    Console.Write("輸入數字選擇支出項目: ");
                    outcomeCase = Console.ReadLine();
                    if (outcomeCase == "1")
                    {
                        Console.Write("輸入支出金額: ");
                        outcome[1] += int.Parse(Console.ReadLine());
                    }
                    else if (outcomeCase == "2")
                    {
                        Console.Write("輸入支出金額: ");
                        outcome[2] += int.Parse(Console.ReadLine());
                    }
                    else if (outcomeCase == "3")
                    {
                        Console.Write("輸入支出金額: ");
                        outcome[3] += int.Parse(Console.ReadLine());
                    }
                    else if (outcomeCase == "4")
                    {
                        Console.Write("輸入支出金額: ");
                        outcome[4] += int.Parse(Console.ReadLine());
                    }
                    else if (outcomeCase == "5")
                    {
                        Console.Write("輸入支出金額: ");
                        outcome[5] += int.Parse(Console.ReadLine());
                    }
                    //每筆支出算入totalOutcome
                    totalOutcome = outcome[1] + outcome[2] + outcome[3] + outcome[4] + outcome[5];
                }

                else if (function == "3")
                {                  
                    if (totalOutcome != 0)
                    {
                        Console.WriteLine("食: {0}%", (outcome[1] / totalOutcome) * 100);
                        Console.WriteLine("衣: {0}%", (outcome[2] / totalOutcome) * 100);
                        Console.WriteLine("住: {0}%", (outcome[3] / totalOutcome) * 100);
                        Console.WriteLine("行: {0}%", (outcome[4] / totalOutcome) * 100);
                        Console.WriteLine("育樂: {0}%", (outcome[5] / totalOutcome) * 100);
                    }
                    else
                    {
                        Console.WriteLine("食: 0%");
                        Console.WriteLine("衣: 0%");
                        Console.WriteLine("住: 0%");
                        Console.WriteLine("行: 0%");
                        Console.WriteLine("育樂: 0%");
                    }




                }

                else if (function == "4")
                {                  
                    Console.WriteLine("剩餘金額為: {0}", totalIncome - totalOutcome);
                }

                Console.WriteLine();
            }

        }

    }
}

