using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c34104032_practice_2_2
{
    class Program
    {
        static void Main(string[] args)
        {         
            //初始棋盤
            string[,] checkerBoard = new string[8, 8];
            for(int i = 0; i<8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    checkerBoard[i, j] = "-";
                }
            }

            
            //簡易黑白棋遊戲開始(不考慮斜向)
            int totalCheckers = 0;
            while (totalCheckers < 64) //棋盤還沒下滿
            {
                string tempPosition; //暫時記錄玩家輸入的下棋位置
                if (totalCheckers == 0 || totalCheckers % 2 == 0) //玩家O下棋
                {
                    printBoard(checkerBoard);
                    printLine("輪到玩家O 請輸入要下的位置:");
                    tempPosition = scan();
                    //判斷是否下在有棋子的地方
                    if (judgeExist(checkerBoard,  transLetter(tempPosition.Substring(0, 1)), int.Parse(tempPosition.Substring(1, 1)) - 1))
                    {
                        Console.ReadKey(); //按任意鍵繼續遊戲
                    }
                    else
                    {
                        checkerBoard[int.Parse(tempPosition.Substring(1, 1)) - 1, transLetter(tempPosition.Substring(0, 1))] = "O";
                        judgeSameRow(checkerBoard, "O", int.Parse(tempPosition.Substring(1, 1)) - 1);
                        judgeSameColumn(checkerBoard,"O", transLetter(tempPosition.Substring(0, 1)));
                        totalCheckers++;
                    }
                    
                }
                else //玩家X下棋
                {
                    printBoard(checkerBoard);
                    printLine("輪到玩家X 請輸入要下的位置:");
                    tempPosition = scan();
                    //判斷是否下在有棋子的地方
                    if (judgeExist(checkerBoard, transLetter(tempPosition.Substring(0, 1)), int.Parse(tempPosition.Substring(1, 1)) - 1))
                    {
                        Console.ReadKey(); //按任意鍵繼續遊戲
                    }
                    else
                    {
                        checkerBoard[int.Parse(tempPosition.Substring(1, 1)) - 1, transLetter(tempPosition.Substring(0, 1))] = "X";
                        judgeSameRow(checkerBoard, "X", int.Parse(tempPosition.Substring(1, 1)) - 1);
                        judgeSameColumn(checkerBoard, "X", transLetter(tempPosition.Substring(0, 1)));
                        totalCheckers++;
                    }
                }              
                    Console.Clear();//把Console Window清乾淨                          
            }
            //棋盤被下滿了，計算雙方棋子數量
            int oPlayerAmount = 0; //O玩家在棋盤上的數量
            int xPlayerAmount = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(checkerBoard[i, j] == "O")
                    {
                        oPlayerAmount++;
                    }
                    else if (checkerBoard[i, j] == "X")
                    {
                        xPlayerAmount++;
                    }

                }
            }
            //最後印出結果的棋盤
            printBoard(checkerBoard);
            //不考慮平手
            if (oPlayerAmount > xPlayerAmount)
            {
                printLine("遊戲結束 玩家O獲勝!");
            }
            else if (oPlayerAmount < xPlayerAmount)
            {
                printLine("遊戲結束 玩家X獲勝!");
            }
            Console.ReadKey();



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


            int transLetter(string letter) //把輸入的英文轉換成棋盤index
            {
                int columnIndex = 0;
                switch (letter)
                {
                    case "A":
                        columnIndex = 0;
                        break;
                    case "B":
                        columnIndex = 1;
                        break;
                    case "C":
                        columnIndex = 2;
                        break;
                    case "D":
                        columnIndex = 3;
                        break;
                    case "E":
                        columnIndex = 4;
                        break;
                    case "F":
                        columnIndex = 5;
                        break;
                    case "G":
                        columnIndex = 6;
                        break;
                    case "H":
                        columnIndex = 7;
                        break;                     

                }
                return columnIndex;
            }

            void printBoard(string[,] board) //印出棋盤
            {
                printLine("  A B C D E F G H");
                for (int i = 0; i < 8; i++)
                {
                    print($"{i + 1} "); //印出每行哪一節: 1,2,3......
                    for (int j = 0; j < 8; j++)
                    {
                        print($"{board[i,j]} ");

                    }
                    printLine("");
                }
            }

            //判斷棋盤是否已有棋子
            bool judgeExist(string[,] board, int letterIndex, int numIndex) //先填字母index再填數字index
            {
                bool judge = false;
                if (board[numIndex,letterIndex] != "-")
                {
                    judge = true;//棋盤位置已經有棋子了
                    printLine("此位置已有棋子! 按任意鍵繼續遊戲");                  
                }
                return judge;
            }
            
            //判斷同行是否有自己的棋子: 把別人夾起來
            void judgeSameRow(string[,] board, string player, int numIndex)
            {
                
                //暫時畫出該行有哪些相同的棋子: -O---OO-
                string[] tempRow = new string[8]; 
                for (int i =0; i <8; i++)
                {
                    if (board[numIndex,i] == player)
                    {
                        tempRow[i] = player;
                    }
                }
                //讀出最右邊和最左邊棋子的位置
                int leftIndex = Array.IndexOf(tempRow, player);
                int rightIndex = Array.LastIndexOf(tempRow, player);
                //把夾住的棋子變自己的
                for (int i = leftIndex+1; i < rightIndex; i++)
                {
                    if (board[numIndex, i] != "-" && board[numIndex, i] != player) //夾住的不是空白也不是自己的
                    {
                        board[numIndex, i] = player; //變自己的棋子
                    }
                }               
            }
            //判斷同列是否有自己的棋子: 把別人夾起來
            void judgeSameColumn(string[,] board, string player, int letterIndex)
            {

                //暫時畫出該列有哪些相同的棋子: -O---OO-
                string[] tempRow = new string[8];
                for (int i = 0; i < 8; i++)
                {
                    if (board[i,letterIndex] == player)
                    {
                        tempRow[i] = player;
                    }
                }
                //讀出最上面和最下面棋子的位置
                int upIndex = Array.IndexOf(tempRow, player);
                int downIndex = Array.LastIndexOf(tempRow, player);
                //把夾住的棋子變自己的
                for (int i = upIndex+1; i < downIndex; i++)
                {
                    if (board[i, letterIndex] != "-" && board[i, letterIndex] != player) //夾住的不是空白也不是自己的
                    {
                        board[i, letterIndex] = player; //變自己的棋子
                    }
                }
            }          
        }
    }
}
