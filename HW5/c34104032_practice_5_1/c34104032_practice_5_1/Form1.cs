using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c34104032_practice_5_1
{
    public partial class Form1 : Form
    {

        Button[] bts = new Button[26];

        string answer;
        int sec;
        int wrongNum;

        public Form1()
        {
            InitializeComponent();
        }

        private void Reset()
        {
            sec = 0; wrongNum = 0;
            timer1.Enabled = false;	     // 暫停計時器
            timer1.Interval = 1000; //(1秒)

            startButton.Visible = true; startButton.Enabled = true;
            textBox.Visible = true; textBox.Enabled = true; textBox.Text = "";
            label1.Visible = true; label2.Visible = true; label3.Visible = true;

            foreach (Button bt in bts)
            {
                bt.BackColor = System.Drawing.SystemColors.Window; //白色
                bt.Visible = false;
            }             
            timeLabel.Visible = false; timeLabel.Text = "時間: 0";
            wrongLabel.Visible = false; wrongLabel.Text = "猜錯次數: 0次";
            letterLabel.Visible = false; letterLabel.Text = "";

        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            bts[0] = button1; bts[1] = button2; bts[2] = button3; bts[3] = button4; bts[4] = button5;
            bts[5] = button6; bts[6] = button7; bts[7] = button8; bts[8] = button9; bts[9] = button10;
            bts[10] = button11; bts[11] = button12; bts[12] = button13; bts[13] = button14; bts[14] = button15;
            bts[15] = button16; bts[16] = button17; bts[17] = button18; bts[18] = button19; bts[19] = button20;
            bts[20] = button21; bts[21] = button22; bts[22] = button23; bts[23] = button24; bts[24] = button25;
            bts[25] = button26;

            Reset();

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            answer = textBox.Text.ToUpper();
            //判斷是否有中文
            bool hasChinese = false;
            for(int i = 0; i< answer.Length; i++)
            {
                if ((int)answer[i] > 127)
                {
                    hasChinese = true;
                    break;
                }
            }

            if (!hasChinese)
            {
                timer1.Enabled = true; //開始計時
                
                textBox.Visible = false; textBox.Enabled = false;
                label1.Visible = false;
                label2.Visible = false;
                label3.Visible = false;
                startButton.Visible = false; startButton.Enabled = false;

                foreach (Button bt in bts)
                    bt.Visible = true;

                for (int i = 0; i < answer.Length; i++)
                {
                    letterLabel.Text += "_ ";
                }

                timeLabel.Visible = true;
                wrongLabel.Visible = true;
                letterLabel.Visible = true;
            }

            

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLabel.Text = $"時間: {sec}";
            sec++;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    Judge("A");
                    break;
                case Keys.B:
                    Judge("B");
                    break;
                case Keys.C:
                    Judge("C");
                    break;
                case Keys.D:
                    Judge("D");
                    break;
                case Keys.E:
                    Judge("E");
                    break;
                case Keys.F:
                    Judge("F");
                    break;
                case Keys.G:
                    Judge("G");
                    break;
                case Keys.H:
                    Judge("H");
                    break;
                case Keys.I:
                    Judge("I");
                    break;
                case Keys.J:
                    Judge("J");
                    break;
                case Keys.K:
                    Judge("K");
                    break;
                case Keys.L:
                    Judge("L");
                    break;
                case Keys.M:
                    Judge("M");
                    break;
                case Keys.N:
                    Judge("N");
                    break;
                case Keys.O:
                    Judge("O");
                    break;
                case Keys.P:
                    Judge("P");
                    break;
                case Keys.Q:
                    Judge("Q");
                    break;
                case Keys.R:
                    Judge("R");
                    break;
                case Keys.S:
                    Judge("S");
                    break;
                case Keys.T:
                    Judge("T");
                    break;
                case Keys.U:
                    Judge("U");
                    break;
                case Keys.V:
                    Judge("V");
                    break;
                case Keys.W:
                    Judge("W");
                    break;
                case Keys.X:
                    Judge("X");
                    break;
                case Keys.Y:
                    Judge("Y");
                    break;
                case Keys.Z:
                    Judge("Z");
                    break;
                    
                    
            }
        }

        private void Judge(string letter)
        {
            //猜錯
            if (!answer.Contains(letter))
            {
                if(bts[Convert.ToInt32(letter[0] - 65)].Visible == true) //避免重複按已知的
                {
                    bts[Convert.ToInt32(letter[0] - 65)].Visible = false;
                    wrongNum++;
                    if (wrongNum > 6)
                    {
                        timer1.Enabled = false;
                        DialogResult Result = MessageBox.Show("You Lose!");
                        if (Result == DialogResult.OK)
                            Reset();

                    }
                    wrongLabel.Text = $"猜錯次數: {wrongNum}次";
                }
                

            }
            //猜對     
            else
            {
                if (bts[Convert.ToInt32(letter[0] - 65)].BackColor != Color.LightGreen) //避免重複按已知的
                {
                    bts[Convert.ToInt32(letter[0] - 65)].BackColor = Color.LightGreen;
                    //把猜到的字母顯示
                    for (int i = 0; i < answer.Length; i++)
                    {
                        if (answer[i] == letter[0])
                        {
                            string tempChar = letter[0].ToString();
                            letterLabel.Text = letterLabel.Text.Remove(2 * i, 1);
                            letterLabel.Text = letterLabel.Text.Insert(2 * i, tempChar);
                            

                        }

                    }

                    //把answer的字元用空格隔開
                    String answerToLabel = "";
                    for (int i = 0; i < answer.Length; i++)
                        answerToLabel += $"{answer[i]} ";
                    //若猜到所有字母，遊戲勝利
                    if (letterLabel.Text == answerToLabel)
                    {
                        timer1.Enabled = false;
                        DialogResult Result = MessageBox.Show($"花費{timeLabel.Text}\n猜錯 {wrongNum}次", "You Win!");
                        if (Result == DialogResult.OK)
                            Reset();
                    }
                }
                
                    
 


            }   
        }
    }
}
