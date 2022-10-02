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
            string function = "0"; //初始功能選擇

            int tempIncome = 0; //暫時存輸入的收入金額
            int totalIncome = 0;
            double totalOutcome = 0;

            //存放新增的支出項目
            string[] outcomeCase = new string[5];
            int[] outcomeMoney = new int[] { 0,0,0,0,0};

            //計算剩幾個支出項目
            int outcomeCaseNumber = 0;
            

            
            int[] outcome = { 0, 0, 0, 0, 0, 0 };

            while (function != "8")
            {
                Console.Write("(1)輸入收入 (2)輸入支出 (3)新增項目 (4)刪除項目 (5)計算比例 (6)查詢支出 (7)剩餘金額 (8)退出程式\n");
                Console.Write("輸入數字選擇功能: ");
                function = Console.ReadLine();


                if (function == "1") //輸入收入
                {
                    Console.Write("輸入金額: ");

                    try
                    {
                        tempIncome = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException ex) //可能發生型別轉換錯誤
                    {
                        Console.WriteLine("請輸入數字");
                    }
                    if (tempIncome < 0) { Console.WriteLine("收入不可為負數"); }
                    else if (tempIncome >= 0)
                    {
                        totalIncome += tempIncome;
                        tempIncome = 0; //歸零，讓下次再存取收入時，能再次暫存
                    }
                    
                }

                else if (function == "2") //輸入支出
                {
                    //印出項目清單
                    if (outcomeCaseNumber == 0) //要有已存在的支出項目才能寫入支出金額
                    {
                        Console.WriteLine("請新增支出項目");
                    }
                    else
                    {
                        for (int i = 1; i <= outcomeCaseNumber; i++)
                        {
                            Console.Write("({0}){1} ", i, outcomeCase[i-1]);
                        }
                        Console.WriteLine();

                        Console.Write("輸入數字選擇支出項目: ");
                        int tempFunction = int.Parse(Console.ReadLine()); //暫時讀取輸入的支出項目編號
                        if(tempFunction <=0 || tempFunction > outcomeCaseNumber) //項目編號不存在
                        {
                            Console.WriteLine("此數字不在範圍中");
                        }
                        else
                        {
                            Console.Write("輸入支出金額: ");
                            int tempOutcome = int.Parse(Console.ReadLine()); //暫時存輸入的支出金額
                            if ((tempOutcome + totalOutcome) > totalIncome)//若支出大於存款
                            {
                                Console.WriteLine("存款不足");
                            }                     
                            else
                            {
                                outcomeMoney[tempFunction-1] += tempOutcome;
                                totalOutcome += tempOutcome;
                            }
                            
                        }

                        
                    }
                                       
                }
                else if (function == "3") //新增項目
                {                                      
                    
                    if (outcomeCaseNumber >= 5) //最多只能新增5項
                    {
                        Console.WriteLine("已無法再新增支出項目");
                    }
                    else
                    {
                        Console.Write("輸入項目名稱: ");
                        string caseName = Console.ReadLine();
                        if (outcomeCase.Contains(caseName)) //如果新增的項目和項目字典中的重複，不會新增
                        {
                            Console.WriteLine("支出項目已存在");                                                       
                        }
                        else
                        {
                            outcomeCaseNumber++; //項目數量增加                          
                            outcomeCase[outcomeCaseNumber - 1] = caseName;//新增項目編號及項目名稱
                            outcomeMoney[outcomeCaseNumber - 1] = 0; //新增項目編號對應的金額
                        }
                    }                                      
                }

                else if (function == "4") //刪除項目
                {
                    Console.Write("輸入項目名稱: ");
                    string tempDeleteCase = Console.ReadLine();
                    
                    if (!outcomeCase.Contains(tempDeleteCase))
                    {
                        Console.WriteLine("此項目不存在");
                    }
                    else
                    {                       
                        totalOutcome -= outcomeMoney[Array.IndexOf(outcomeCase, tempDeleteCase)]; //total支出扣除刪掉的項目支出
                        
                        if (Array.IndexOf(outcomeCase, tempDeleteCase) == (outcomeCaseNumber-1)) //若要刪除的是最後一個元素 //避免index溢出
                        {
                            outcomeCase[outcomeCaseNumber - 1] = "";
                            outcomeMoney[outcomeCaseNumber - 1] = 0;
                        }
                        else if (Array.IndexOf(outcomeCase, tempDeleteCase) < (outcomeCaseNumber - 1))
                        {
                            for (int i = Array.IndexOf(outcomeCase, tempDeleteCase); i < outcomeCaseNumber - 1; i++)
                            {
                                outcomeCase[i] = outcomeCase[i + 1];
                                outcomeMoney[i] = outcomeMoney[i + 1];
                            }
                        }    
                        
                        outcomeCaseNumber--;                       
                    }
                    
                }

                else if (function == "5") //計算比例
                {
                    if (totalOutcome != 0)
                    {
                        for(int i = 0; i< outcomeCaseNumber; i++)
                        {
                            Console.WriteLine("{0}: {1}%", outcomeCase[i],(outcomeMoney[i] / totalOutcome) * 100);
                        }
                        
                    }
                    else
                    {
                        for (int i = 0; i < outcomeCaseNumber; i++)
                        {
                            Console.WriteLine("{0}: 0%", outcomeCase[i]);
                        }
                        
                    }

                }

                else if (function == "6") //查詢支出
                {
                    Console.WriteLine("目前總支出: {0}", totalOutcome);
                    Console.Write("輸入要查詢的項目: ");
                    string tempFunction = Console.ReadLine();
                    if (!outcomeCase.Contains(tempFunction))
                    {
                        Console.WriteLine("此項目不存在");
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1}", tempFunction, outcomeMoney[Array.IndexOf(outcomeCase,tempFunction)]);
                    }
                }

                else if (function == "7") //剩餘金額
                {
                    Console.WriteLine("剩餘金額為: {0}", totalIncome - totalOutcome);
                }

                Console.WriteLine();
            }

        }

    }
}


