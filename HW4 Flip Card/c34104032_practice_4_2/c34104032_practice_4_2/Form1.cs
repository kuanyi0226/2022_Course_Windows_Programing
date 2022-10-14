using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c34104032_practice_4_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //import圖片檔
        Image backImage = Image.FromFile(@"..\..\..\pic\" + "card.jpg");
        const string address1 = "01.jpg";
        const string address2 = "02.jpg";
        const string address3 = "03.jpg";
        const string address4 = "04.jpg";
        const string address5 = "05.jpg";
        const string address6 = "06.jpg";
        const string address7 = "07.jpg";
        const string address8 = "08.jpg";

        //亂數初始化
        Random rdn = new Random();

        bool flip; //已經點開一張卡了?
        bool canClick; //控制選到不成對時，不能再翻牌
        string name; //記錄每一次的名稱
        int score; //記錄每一次的分數

        List<string> oriPicArray = new List<string>(); //存放尚未亂數排序的所有card的address
        string[] picArray = new string[16]; //存放亂數排列過的所有card的address(後續實際操作使用)

        List<int> chosenCards = new List<int>(); //存放已選擇卡片在picArray中的index
        List<int> knownCards = new List<int>(); //存放已確認成對卡片在picArray中的index


        private void startButton_Click(object sender, EventArgs e) //開始遊戲
        {
            //判斷名字是否OK:不是空白、長度介於3到10
            if (nameBox.Text == "")
            {
                MessageBox.Show("名稱不能為空白", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (nameBox.Text.Length <3 || nameBox.Text.Length>10)
            {
                MessageBox.Show("名稱不合格式 (>=3 && <=10)", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //初始化 (List還沒初始會直接打List[0]會超出索引，記得先使用Add())     
                flip = false;
                canClick = true;

                oriPicArray.Add(address1); oriPicArray.Add(address1); oriPicArray.Add(address2); oriPicArray.Add(address2);
                oriPicArray.Add(address3); oriPicArray.Add(address3); oriPicArray.Add(address4); oriPicArray.Add(address4);
                oriPicArray.Add(address5); oriPicArray.Add(address5); oriPicArray.Add(address6); oriPicArray.Add(address6);
                oriPicArray.Add(address7); oriPicArray.Add(address7); oriPicArray.Add(address8); oriPicArray.Add(address8);

                //隨機排列圖片
                for (int i = 15; i >= 0; i--)
                {
                    int rdnNum = rdn.Next(0, i + 1); //隨機產生亂數
                    picArray[i] = oriPicArray[rdnNum];
                    oriPicArray.RemoveAt(rdnNum); //刪除oriPicArray已選給picArray的圖片
                                                  //再從oriPicArray剩下的隨機抽圖片給picArray
                }

                imageAllVisible(true);
                imageAllBack(); //翻到背面

                name = nameBox.Text;
                score = 100;
                startButton.Enabled = false;
                nameBox.Enabled = false;

                setState();
            }
            

        }

        private void restartButton_Click(object sender, EventArgs e) //重新開始
        {
            imageAllVisible(false);
            startButton.Enabled = true;
            score = 100;
            nameBox.Text = null;
            nameBox.Enabled = true;
            chosenCards.Clear();
            knownCards.Clear();
            imageAllEnable();
            contiButton.Enabled = false;


            setState();
        }

        private void quitButton_Click(object sender, EventArgs e) //離開遊戲
        {
            DialogResult result =  MessageBox.Show("確定要離開遊戲嗎?","離開遊戲", MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
            
        }

        private void contiButton_Click(object sender, EventArgs e) //繼續
        {
            for (int picArrayIndex = 0; picArrayIndex < 16; picArrayIndex++)
            {
                if (!knownCards.Contains(picArrayIndex))
                {
                    indexToButton(picArrayIndex).Image = backImage;
                }
            }
            canClick = true;
            contiButton.Enabled = false;
        }


        //翻牌
        private void pic00_Click(object sender, EventArgs e)
        {
            cardToFront(0);
            setState();
        }

        private void pic01_Click(object sender, EventArgs e)
        {
            cardToFront(1);
            setState();

        }

        private void pic02_Click(object sender, EventArgs e)
        {
            cardToFront(2);
            setState();

        }

        private void pic03_Click(object sender, EventArgs e)
        {
            cardToFront(3);
            setState();

        }

        private void pic10_Click(object sender, EventArgs e)
        {
            cardToFront(4);
            setState();

        }

        private void pic11_Click(object sender, EventArgs e)
        {
            cardToFront(5);
            setState();


        }

        private void pic12_Click(object sender, EventArgs e)
        {
            cardToFront(6);
            setState();


        }

        private void pic13_Click(object sender, EventArgs e)
        {
            cardToFront(7);
            setState();


        }

        private void pic20_Click(object sender, EventArgs e)
        {
            cardToFront(8);
            setState();


        }

        private void pic21_Click(object sender, EventArgs e)
        {
            cardToFront(9);
            setState();


        }

        private void pic22_Click(object sender, EventArgs e)
        {
            cardToFront(10);
            setState();


        }

        private void pic23_Click(object sender, EventArgs e)
        {
            cardToFront(11);
            setState();


        }

        private void pic30_Click(object sender, EventArgs e)
        {
            cardToFront(12);
            setState();


        }

        private void pic31_Click(object sender, EventArgs e)
        {
            cardToFront(13);
            setState();


        }

        private void pic32_Click(object sender, EventArgs e)
        {
            cardToFront(14);
            setState();


        }

        private void pic33_Click(object sender, EventArgs e)
        {
            cardToFront(15);
            setState();


        }


        //點擊卡片後，更新所有按鈕狀態(大整理)
        private void setState()
        {
            scoreLabel.Text = "分數: "+score.ToString();
            foreach (int index in knownCards) //顯示已經翻出成對的
            {
                indexToButton(index).Image = Image.FromFile(@"..\..\..\pic\" + picArray[index]);
                indexToButton(index).Enabled = false;
            }

            winJudge();
        }
        //全部翻到背面
        private void imageAllBack()
        {
            pic00.Image = backImage; pic01.Image = backImage; pic02.Image = backImage; pic03.Image = backImage;
            pic10.Image = backImage; pic11.Image = backImage; pic12.Image = backImage; pic13.Image = backImage;
            pic20.Image = backImage; pic21.Image = backImage; pic22.Image = backImage; pic23.Image = backImage;
            pic30.Image = backImage; pic31.Image = backImage; pic32.Image = backImage; pic33.Image = backImage;

        }

        //全部Visible或Invisible
        private void imageAllVisible(bool yesORno)
        {
            if (yesORno)
            {
                pic00.Visible = true; pic01.Visible = true; pic02.Visible = true; pic03.Visible = true;
                pic10.Visible = true; pic11.Visible = true; pic12.Visible = true; pic13.Visible = true;
                pic20.Visible = true; pic21.Visible = true; pic22.Visible = true; pic23.Visible = true;
                pic30.Visible = true; pic31.Visible = true; pic32.Visible = true; pic33.Visible = true;
            }
            else
            {
                pic00.Visible = false; pic01.Visible = false; pic02.Visible = false; pic03.Visible = false;
                pic10.Visible = false; pic11.Visible = false; pic12.Visible = false; pic13.Visible = false;
                pic20.Visible = false; pic21.Visible = false; pic22.Visible = false; pic23.Visible = false;
                pic30.Visible = false; pic31.Visible = false; pic32.Visible = false; pic33.Visible = false;
            }
            
        }

        //全部Enable
        private void imageAllEnable()
        {   
                pic00.Enabled = true; pic01.Enabled = true; pic02.Enabled = true; pic03.Enabled = true;
                pic10.Enabled = true; pic11.Enabled = true; pic12.Enabled = true; pic13.Enabled = true;
                pic20.Enabled = true; pic21.Enabled = true; pic22.Enabled = true; pic23.Enabled = true;
                pic30.Enabled = true; pic31.Enabled = true; pic32.Enabled = true; pic33.Enabled = true;
        }

        //某張牌翻到正面
        private void cardToFront(int picArrayIndex)
        {
            if (canClick) //先判斷要不要翻開來
            {
                chosenCards.Add(picArrayIndex);
                if (chosenCards.Count == 1)
                {
                    indexToButton(picArrayIndex).Image = Image.FromFile(@"..\..\..\pic\" + picArray[picArrayIndex]);
                    flip = false;
                }
                else if (chosenCards.Count == 2 && chosenCards[0] != chosenCards[1])
                {
                    indexToButton(picArrayIndex).Image = Image.FromFile(@"..\..\..\pic\" + picArray[picArrayIndex]);
                    flip = true;
                }
                else
                {
                    chosenCards.RemoveAt(1);
                }

                if (flip == false)
                {
                    contiButton.Enabled = false;
                }
                else
                {
                    contiButton.Enabled = true;
                }

                if (chosenCards.Count == 2)
                {
                    if (chosenSame()) //選到一樣的
                    {
                        score += 10;
                        knownCards.Add(chosenCards[0]); knownCards.Add(chosenCards[1]);
                        chosenCards.Clear();
                        contiButton.Enabled = false;
                    }
                    else //選到不一樣的
                    {
                        score -= 5;
                        chosenCards.Clear();
                        contiButton.Enabled = true;
                        canClick = false;
                    }
                }
            }


        }
        //判斷選擇的Card在是否相同
        private bool chosenSame()
        {
            if (picArray[chosenCards[0]] == picArray[chosenCards[1]] && chosenCards[0] != chosenCards[1])
                return true;
            else
                return false;
        }

        //輸入在picArray的index回傳對應Button
        private Button indexToButton(int picArrayIndex)
        {
            switch (picArrayIndex)
            {
                case 0:
                    return pic00;
                case 1:
                    return pic01;
                case 2:
                    return pic02;
                case 3:
                    return pic03;
                case 4:
                    return pic10;
                case 5:
                    return pic11;
                case 6:
                    return pic12;
                case 7:
                    return pic13;
                case 8:
                    return pic20;
                case 9:
                    return pic21;
                case 10:
                    return pic22;
                case 11:
                    return pic23;
                case 12:
                    return pic30;
                case 13:
                    return pic31;
                case 14:
                    return pic32;
                case 15:
                    return pic33;
                default:
                    return pic00;
            }
        }

        //判斷遊戲是否勝利
        private void winJudge()
        {
            bool win = false;
            if (knownCards.Count == 16) //全部都翻出來了
                win = true;
            else
                win = false;
            if (win)
            {
                record(); //記錄本次數據到遊戲區
                DialogResult result = MessageBox.Show($"分數: {score}", "遊戲結束", MessageBoxButtons.RetryCancel,MessageBoxIcon.Asterisk); //記得打上標題(就算沒有)
                if(result == DialogResult.Retry) //跟restart button相同
                {
                    imageAllVisible(false);
                    startButton.Enabled = true;
                    score = 100;
                    nameBox.Text = null;
                    nameBox.Enabled = true;
                    chosenCards.Clear();
                    knownCards.Clear();
                    imageAllEnable();
                    contiButton.Enabled = false;

                    setState();
                }
                
            }
        }

        //記錄名稱&分數
        private void record()
        {
            recordLabel.Text += $"{name} 得分為：{score}\n\n";
        }


        
    }
}
