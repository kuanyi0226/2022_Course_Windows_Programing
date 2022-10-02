using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c34104032_practice_3_2
{
    public partial class Form1 : Form
    {
        static List<string> stackArray1 = new List<string>();
        static List<string> stackArray2 = new List<string>();
        static List<string> stackArray3 = new List<string>();
        static List<string> stackArray4 = new List<string>();

        //控制: 第一次按選擇(pull)會顯示"你選了堆疊X"，第二次(push)不會印
        bool selectPrintStatus = true;
        //建立Game object
        Game stackGame = new Game(stackArray1, stackArray2, stackArray3, stackArray4);

        //紀錄move的時候，從哪裡move，move到哪裡
        int moveFrom = 0;
        int moveTo = 0;


        public Form1()
        {
            InitializeComponent();
        }

        //主畫面
        private void Form1_Load(object sender, EventArgs e)
        {
            //主畫面版面
            //1.視窗大小
            this.Size = new Size(345, 345);
            //2.主畫面東西
            statusLabel1.Text = "請輸入測資";
            statusLabel1.Visible = true;
            stackLabel1.Visible = true;
            stackLabel2.Visible = true;
            stackLabel3.Visible = true;
            stackLabel4.Visible = true;
            stackBox1.Visible = true;
            stackBox2.Visible = true;
            stackBox3.Visible = true;
            stackBox4.Visible = true;
            startButton.Visible = true;

            stackLabel1.Location = new Point(18, 82);
            stackLabel2.Location = new Point(18, 122);
            stackLabel3.Location = new Point(18, 162);
            stackLabel4.Location = new Point(18, 202);
            //3.遊戲畫面東西
            stackCol1.Visible = false;
            stackCol2.Visible = false;
            stackCol3.Visible = false;
            stackCol4.Visible = false;
            selectButton1.Visible = false;
            selectButton2.Visible = false;
            selectButton3.Visible = false;
            selectButton4.Visible = false;
            homeButton.Visible = false;
            statusLabel2.Visible = false;

        }

        private void startButton_Click(object sender, EventArgs e) //開始遊戲
        {           
            //stackArray歸零         
            stackGame.clearList(1);
            stackGame.clearList(2);
            stackGame.clearList(3);
            stackGame.clearList(4);

            //把stackBox的數字分別讀進4個list
            bool startCheck1 = readInData(stackGame.getList(1), stackBox1.Text);
            bool startCheck2 = readInData(stackGame.getList(2), stackBox2.Text);
            bool startCheck3 = readInData(stackGame.getList(3), stackBox3.Text);
            bool startCheck4 = readInData(stackGame.getList(4), stackBox4.Text);         

            if (startCheck1 && startCheck2 && startCheck3 && startCheck4 && stackGame.checkThree())
            {             
                //視窗變大
                this.Size = new System.Drawing.Size(345, 430);
                //隱藏主畫面資訊
                statusLabel1.Visible = false;
                stackBox1.Visible = false;
                stackBox2.Visible = false;
                stackBox3.Visible = false;
                stackBox4.Visible = false;
                startButton.Visible = false;

                stackLabel1.Location = new Point(18, 82);
                stackLabel2.Location = new Point(18, 122);
                stackLabel3.Location = new Point(18, 162);
                stackLabel4.Location = new Point(18, 202);

                //顯示遊戲畫面東西
                stackCol1.Visible = true;
                stackCol2.Visible = true;
                stackCol3.Visible = true;
                stackCol4.Visible = true;
                homeButton.Visible = true;
                statusLabel2.Visible = true;
                statusLabel2.Text = "...";
                selectButton1.Visible = true;
                selectButton2.Visible = true;
                selectButton3.Visible = true;
                selectButton4.Visible = true;
                avoidStupid(); //管理按鈕的顯示

                stackLabel1.Location = new Point(30, 30);
                stackLabel2.Location = new Point(105, 30);
                stackLabel3.Location = new Point(180, 30);
                stackLabel4.Location = new Point(260, 30);


                printAllStacks();

            }
            else
            {
                statusLabel1.Text = "測資錯誤";
            }
        }

        //判斷是否把stackBox的數字分別讀進4個list Method、若可以就印出來
        private bool readInData(List<string> stacksList, string stackBoxText)
        {
            bool readData = false;
            int[] tempDataArray = new int[4]; //暫時存欲開始遊戲的資料(最多存4個數字)

            if(stackBoxText.Length <= 7) //stackBox中最多只有7個字元(4個數字&3個空白)
            {
                //判斷第一個、最後一個字元是否為空白，且只能用一個空白隔開(不能有2~7個空白)
                if (stackBoxText.IndexOf(" ") != 0 && !(stackBoxText.EndsWith(" ")) && !(stackBoxText.Contains("  ")) && !(stackBoxText.Contains("   ")) && !(stackBoxText.Contains("    ")) && !(stackBoxText.Contains("     ")) && !(stackBoxText.Contains("      ")) && !(stackBoxText.Contains("       ")))
                {
                    readData = true;
                }
                else
                {
                    readData = false;
                }
            }
            else
            {
                readData = false;
            }

            //判斷是否沒有非阿拉伯數字、一列堆疊是否超過4個數字
            int tempDataArrayLength = 0; //暫時存tempDataArray的長度
            switch (stackBoxText.Length)
            {
                case 1:
                    tempDataArrayLength = 1;
                    break;
                case 3:
                    tempDataArrayLength = 2;
                    break;

                case 5:
                    tempDataArrayLength = 3;
                    break;

                case 7:
                    tempDataArrayLength = 4;
                    break;
            }
            if (readData)
            {                   
                try
                {
                    for (int i = 0; i < tempDataArrayLength; i++)
                    {
                        //判斷輸入的字沒有大於4
                        if(int.Parse((stackBoxText.Split(' '))[i]) > 4)
                        {
                            readData = false;
                            break;
                        }
                        tempDataArray[i] = int.Parse((stackBoxText.Split(' '))[i]);
                    }
                }
                catch
                {
                    readData = false;
                }
            }     

            //把每個box的值assign給每個stackArray
            if (readData)
            {
                for (int i=0; i< tempDataArrayLength; i++)
                {
                    stacksList.Add((stackBoxText.Split(' '))[i]);
                }
            }

            return readData;
        }

        //印出stackBoxArray到stackCol上
        string printStackBox(List<string> stackBox, string stackCol)
        {

            stackCol = "";
            for(int i = stackBox.Count-1; i>=0; i--)
            {
                stackCol += stackBox[i];
                stackCol += "\n";
            }
            return stackCol;
        }

        //批量重新印出所有stacks 到bars上
        void printAllStacks()
        {
            stackCol1.Text = printStackBox(stackGame.getList(1), stackCol1.Text);
            stackCol2.Text = printStackBox(stackGame.getList(2), stackCol2.Text);
            stackCol3.Text = printStackBox(stackGame.getList(3), stackCol3.Text);
            stackCol4.Text = printStackBox(stackGame.getList(4), stackCol4.Text);
        }


        //遊戲畫面
        private void homeButton_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void selectButton1_Click(object sender, EventArgs e) //選擇1
        {
            if(selectPrintStatus == true)
            {
                statusLabel2.Text = "你選了堆疊1";
                moveFrom = 1;
                selectPrintStatus = false;
                avoidStupid();
            }
            else
            {
                statusLabel2.Text = "...";
                moveTo = 1;
                stackGame.move(moveFrom, moveTo);
                printAllStacks();
                selectPrintStatus = true;
                avoidStupid();
                gameWin();
            }
        }

        private void selectButton2_Click(object sender, EventArgs e) //選擇2
        {
            if (selectPrintStatus == true)
            {
                statusLabel2.Text = "你選了堆疊2";
                moveFrom = 2;
                selectPrintStatus = false;
                avoidStupid();
            }
            else
            {
                statusLabel2.Text = "...";
                moveTo = 2;
                stackGame.move(moveFrom, moveTo);
                printAllStacks();
                selectPrintStatus = true;
                avoidStupid();
                gameWin();

            }
        }

        private void selectButton3_Click(object sender, EventArgs e) //選擇3
        {
            if (selectPrintStatus == true)
            {
                statusLabel2.Text = "你選了堆疊3";
                moveFrom = 3;
                selectPrintStatus = false;
                avoidStupid();

            }
            else
            {
                statusLabel2.Text = "...";
                moveTo = 3;
                stackGame.move(moveFrom, moveTo);
                printAllStacks();
                selectPrintStatus = true;
                avoidStupid();
                gameWin();

            }
        }

        private void selectButton4_Click(object sender, EventArgs e) //選擇4
        {
            if (selectPrintStatus == true)
            {
                statusLabel2.Text = "你選了堆疊4";
                moveFrom = 4;
                selectPrintStatus = false;
                avoidStupid();
            }
            else 
            {
                statusLabel2.Text = "...";
                moveTo = 4;
                stackGame.move(moveFrom, moveTo);
                printAllStacks();
                selectPrintStatus = true;
                avoidStupid();
                gameWin();

            }
        }
        private void avoidStupid() 
        {
            if (selectPrintStatus) //按鈕顯示選取
            {
                selectButton1.Text = "選取";
                selectButton2.Text = "選取";
                selectButton3.Text = "選取";
                selectButton4.Text = "選取";

                if (stackGame.canTake(1) == false)
                {
                    selectButton1.Enabled = false;
                }
                else
                {
                    selectButton1.Enabled = true;
                }
                if (stackGame.canTake(2) == false)
                {
                    selectButton2.Enabled = false;
                }
                else
                {
                    selectButton2.Enabled = true;
                }
                if (stackGame.canTake(3) == false)
                {
                    selectButton3.Enabled = false;
                }
                else
                {
                    selectButton3.Enabled = true;
                }
                if (stackGame.canTake(4) == false)
                {
                    selectButton4.Enabled = false;
                }
                else
                {
                    selectButton4.Enabled = true;
                }
            }
            else {
                selectButton1.Text = "放置";
                selectButton2.Text = "放置";
                selectButton3.Text = "放置";
                selectButton4.Text = "放置";

                if (stackGame.canPlace(1) == false)
                {
                    selectButton1.Enabled = false;
                }
                else
                {
                    selectButton1.Enabled = true;
                }
                if (stackGame.canPlace(2) == false)
                {
                    selectButton2.Enabled = false;
                }
                else
                {
                    selectButton2.Enabled = true;
                }
                if (stackGame.canPlace(3) == false)
                {
                    selectButton3.Enabled = false;
                }
                else
                {
                    selectButton3.Enabled = true;
                }
                if (stackGame.canPlace(4) == false)
                {
                    selectButton4.Enabled = false;
                }
                else
                {
                    selectButton4.Enabled = true;
                }
            }
            
            

        }  
        
        //判斷遊戲勝利
        void gameWin()
        {
            if (stackGame.win())
            {
                statusLabel2.Text = "你贏了";
                selectButton1.Text = "贏";
                selectButton2.Text = "贏";
                selectButton3.Text = "贏";
                selectButton4.Text = "贏";
                selectButton1.Enabled = false;
                selectButton2.Enabled = false;
                selectButton3.Enabled = false;
                selectButton4.Enabled = false;

            }
        }
        
    }
}
