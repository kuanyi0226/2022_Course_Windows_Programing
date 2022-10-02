using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int function = 0;
            
            int courseAmount = 0;
            int totalScore = 0; //總學分

            //陣列(課程代號, 星期, 開始節, 結束節) : 最多20門課
            string[] numberList = new string[20];
            string[] dayList = new string[20];
            string[] startList = new string[20];
            string[] endList = new string[20];

            while (function != 5)
            {
                Console.WriteLine("(1)新增課程 (2)刪除課程 (3)列印課表 (4)計算學分 (5)離開程式");               
                Console.Write("請輸入數字選擇功能: ");
                function = int.Parse(Console.ReadLine());
                switch (function)
                {
                    case 1: //新增課程
                        {
                            Console.WriteLine("請輸入要加入的課程，格式為<課程代號 星期 開始節 結束節>");
                            string newCourse = Console.ReadLine(); //暫時紀錄新增輸入的課程

                            //判斷重複
                            bool addClass = true;
                            if (Array.Exists(numberList, x => x == newCourse.Substring(0,5))){
                                Console.WriteLine("課程重複!");
                                addClass = false;
                            }
                            //判斷衝堂(因為judgeTime方法的缺失，判斷第一個加入的課會有runtime error，故需額外判斷Array中是否沒有元素)                       
                            if(judgeTime(dayList, startList, endList, newCourse.Substring(6, 1), int.Parse(newCourse.Substring(8, 1)), int.Parse(newCourse.Substring(10, 1))) )
                            {
                                addClass = false;
                                printLine("課程衝堂!");
                            }
                            
                            //若課程沒問題，將課程加入List
                            if (addClass == true)
                            {
                                courseAmount++;
                                string[] tempCourseArray = newCourse.Split(' ');
                                for (int i = 0; i < 4; i++)
                                {
                                    numberList[courseAmount - 1] = tempCourseArray[0];
                                    dayList[courseAmount - 1] = tempCourseArray[1];
                                    startList[courseAmount - 1] = tempCourseArray[2];
                                    endList[courseAmount - 1] = tempCourseArray[3];
                                }

                                Console.WriteLine("成功加入課程!");
                                totalScore += (int.Parse(newCourse.Substring(10,1))- int.Parse(newCourse.Substring(8, 1))+1);
                            }
                                
                            break;
                        }
                    case 2: //刪除課程
                        {
                            print("請輸入要刪除的課程代號:");
                            string tempNumber = scan();
                            if (!numberList.Contains(tempNumber))
                            {
                                printLine($"課程 {tempNumber} 不在課表中");
                            }
                            else
                            {
                                //紀錄輸入代號對應課程在List中的index
                                int tempindex = Array.IndexOf(numberList, tempNumber);
                                //總學分扣除該課程學分數
                                totalScore -= (int.Parse(endList[tempindex]) - int.Parse(startList[tempindex])+1);
                                //刪除課程資料
                                for (int i = 0; i < 4; i++)
                                {
                                    numberList[courseAmount - 1] = "";
                                    dayList[courseAmount - 1] = "";
                                    startList[courseAmount - 1] = "";
                                    endList[courseAmount - 1] = "";
                                }
                                //後面課程編號往前
                                for (int i = tempindex; i < courseAmount - 1; i++)
                                {
                                    numberList[i] = numberList[i + 1];
                                    dayList[i] = dayList[i + 1];
                                    startList[i] = startList[i + 1];
                                    endList[i] = endList[i + 1];
                                }
                                courseAmount--;
                                printLine($"成功刪除課程: {tempNumber}");
                            }
                            break;
                        }
                    case 3:
                        {
                            //建立7*8空白課表
                            string[,] tempSchedule = new string[7, 8];
                            for(int index = 0; index<courseAmount; index++)
                            {
                                int day = int.Parse(dayList[index]); //紀錄每個index對應到星期幾
                                switch (day)
                                {
                                    case 7:
                                        for(int i = int.Parse(startList[index]); i<= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[0, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 1:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[1, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 2:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[2, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 3:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[3, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 4:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[4, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 5:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[5, i - 1] = numberList[index];
                                        }
                                        break;
                                    case 6:
                                        for (int i = int.Parse(startList[index]); i <= int.Parse(endList[index]); i++)
                                        {
                                            tempSchedule[6, i - 1] = numberList[index];
                                        }
                                        break;
                                }
                            }
                            printLine("      Sun   Mon   Tue   Wed   Thu   Fri   Sat   ");
                            for (int i = 0; i < 8; i++)
                            {
                                print($"{i+1}     "); //印出每行哪一節: 1,2,3......
                                for (int j= 0; j<7; j++)
                                {                                   
                                    print(String.Format("{0,-6}", tempSchedule[j,i]));

                                }
                                printLine("");
                            }

                            break;
                        }
                    case 4:
                        {
                            printLine($"{totalScore}");
                            break;
                        }
                }
                

                Console.WriteLine();


            }
            //判斷是否衝堂
            //已存在的三個列表: dayList, startList, endList; 欲新增的課程: 在星期幾, 起始節, 結束節
            bool judgeTime( string[] dList,string[] S_List, string[] E_List, string day, int S_class, int E_class)
            {
                int[] tempOneSchedule = new int[8];//建立臨時8格(課)的一日課表
                int[] tempOneCourse = new int[E_class-S_class+1]; //建立臨時一堂課的課表
                bool judge = false; //是否衝堂

                //把有佔當日節數的課程時間排入一日課表中(若元素為0即代表有空堂)
                for (int index = 0; index < dayList.Length; index++)
                {
                    if(dayList[index] == day)//讀取dayList中哪些index有輸入的day
                    {                    
                        //把已佔有節數的課程輸入到臨時一日課表中
                        for (int i = 0; i <= (int.Parse(E_List[index]) - int.Parse(S_List[index])); i++)
                        {
                            int courseTime = int.Parse(S_List[index]);
                            tempOneSchedule[i] = courseTime + i;
                        }
                        Array.Sort(tempOneSchedule);//大到小排列一日課表
                    }
                }
                //把欲新增的課程有哪些節數存到tempOneCourse(陣列大小跟節數一樣)
                for(int i = 0; i < tempOneCourse.Length; i++)
                {
                    tempOneCourse[i] = S_class + i;                   
                }

                //判斷欲新增的課程所佔的節，是否與當天現有課表所有的節衝堂      
                for (int i = 0; i < tempOneSchedule.Length; i++)
                {
                    for (int j = 0; j < tempOneCourse.Length; j++)
                    {
                        if (tempOneCourse[j] == tempOneSchedule[i])
                        {
                            judge = true; //衝堂
                            break;
                        }
                        
                    }
                }              
                
                return judge;
            }

            void print(string x)
            {
                Console.Write(x);
            }
            void printLine(string x)
            {
                Console.WriteLine(x);
            }
            string scan()
            {
                return Console.ReadLine();
            }
        }
    }
}
