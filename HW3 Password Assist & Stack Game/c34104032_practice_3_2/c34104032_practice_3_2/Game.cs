using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_3_2
{
    class Game
    {
        //game instnace
        private List<string>[] stackLists = new List<string>[4]; //Array裡面放四個list

        //public method
        //Constructor
        public Game(List<string> input1, List<string> input2, List<string> input3, List<string> input4)
        {
            stackLists[0] = input1;
            stackLists[1] = input2;
            stackLists[2] = input3;
            stackLists[3] = input4;

        }

        //遊戲開始前，判斷是否4個堆疊含有剛好1,2,3各三個
        public bool checkThree()
        {
            int times1 = 0, times2 = 0, times3 = 0;
            for(int i = 0; i<4; i++)
            {
                foreach(string j in stackLists[i])
                {
                    switch (j)
                    {
                        case "1":
                            times1++;
                            break;
                        case "2":
                            times2++;
                            break;
                        case "3":
                            times3++;
                            break;
                    }
                }
            }
            if (times1==3 && times2==3 && times3 == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //get
        public List<string> getList(int i) //讀取第幾個堆疊list
        {
            return stackLists[i-1];
        }

        //set
        public void setList(int i, List<string> _stackList)//修改第幾個堆疊list
        {
            stackLists[i-1].Clear();
            for (int j = 0; j< _stackList.Count; j++)
            {
                stackLists[i - 1].Add(_stackList[j]);
            }
        }

        //清空某堆疊
        public void clearList(int i)
        {
            stackLists[i - 1].Clear();
        }

        //可以放置
        public bool canPlace(int i) //點擊第幾個stackCol(1~4)
        {
            if (stackLists[i - 1].Count < 4)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //可以選取
        public bool canTake(int i)
        {
            if (stackLists[i - 1].Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //移動
        public void move(int i, int j) //i, j 是堆疊1~4
        {
            if(canTake(i) && canPlace(j))
            {
                stackLists[j - 1].Add(stackLists[i-1][stackLists[i-1].Count-1]); //堆疊i的最後一個給j
                stackLists[i - 1].RemoveAt(stackLists[i - 1].Count - 1); //堆疊最後一個去掉
            }
        }
      
        //某一堆疊是否數字都一樣
        private bool allIsSame(int i) //i=1~4
        {
            if(stackLists[i-1].Distinct().ToList().Count == 1 || stackLists[i-1].Count ==0) //重複的元素只有一個或為空
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //判斷某堆疊最後只剩三個數字或無數字
        private bool numThree(int i) 
        {
            if(stackLists[i-1].Count == 3 || stackLists[i - 1].Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            
        //判斷是否勝利
        public bool win()
        {
            int col1=1, col2=2, col3=3, col4=4;
            if(allIsSame(col1) && allIsSame(col2) && allIsSame(col3) && allIsSame(col4))
            {
                if(numThree(col1) && numThree(col2) && numThree(col3) && numThree(col4))
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }     
        }
    }
}
