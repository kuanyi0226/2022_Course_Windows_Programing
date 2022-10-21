using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace c34104032_practice_5_2
{
    public partial class Form1 : Form
    {   
        //Initialize button arrays
        Button[] bts = new Button[42];
        Button[] p1Control = new Button[5];
        Button[] p2Control = new Button[5];

        int prepareTime = 1;
        string turn = "p1"; //p1's turn
        string gameStatus = "Prepare"; //讓點擊棋盤時，判斷要做啥 "Prepare", "Game"
        int gameTurn = 1; //1~3分別是p1戰士、法師、遊俠操作時間；4~6
        int preWarNum = 1; //準備階段，某一方還沒擺放warior的數量->協助判斷時間到是否自動擺放
        int preWizNum = 1;
        int preRanNum = 1;
        Button nowChooseButton = new Button(); // 目前點擊到的角色按鈕
        Button nowTurnButton = new Button(); //目前輪到哪個棋子

        //Initialize Chess Objects
        Warior p1War = new Warior("p1"); Warior p2War = new Warior("p2");
        Wizard p1Wiz = new Wizard("p1"); Wizard p2Wiz = new Wizard("p2");
        Ranger p1Ran = new Ranger("p1"); Ranger p2Ran = new Ranger("p2");

        //判斷chess掛了嗎
        bool war1Dead = false, wiz1Dead = false, ran1Dead = false, war2Dead = false, wiz2Dead = false, ran2Dead = false;
        public Form1()
        {
            InitializeComponent();
        }
        //判斷誰還活著，協助turn的更迭
        List<int> p1Alive = new List<int>();
        List<int> p2Alive = new List<int>();
      
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Interval = 1000; //1 sec per tick
            //btns[0~20]: p1; bts[21~41]: p2
            bts[0] = button1; bts[1] = button2; bts[2] = button3; bts[3] = button4; bts[4] = button5; bts[5] = button6;
            bts[6] = button7; bts[7] = button8; bts[8] = button9; bts[9] = button10; bts[10] = button11; bts[11] = button12;
            bts[12] = button13; bts[13] = button14; bts[14] = button15; bts[15] = button16; bts[16] = button17; bts[17] = button18;
            bts[18] = button19; bts[19] = button20; bts[20] = button21; bts[21] = button22; bts[22] = button23; bts[23] = button24;
            bts[24] = button25; bts[25] = button26; bts[26] = button27; bts[27] = button28; bts[28] = button29; bts[29] = button30;
            bts[30] = button31; bts[31] = button32; bts[32] = button33; bts[33] = button34; bts[34] = button35; bts[35] = button36;
            bts[36] = button37; bts[37] = button38; bts[38] = button39; bts[39] = button40; bts[40] = button41; bts[41] = button42;

            p1Control[0] = p1Atk; p1Control[1] = p1Move; p1Control[2] = p1Skill; p1Control[3] = p1Skip; p1Control[4] = p1Over;
            p2Control[0] = p2Atk; p2Control[1] = p2Move; p2Control[2] = p2Skill; p2Control[3] = p2Skip; p2Control[4] = p2Over;

            //放置角色chess的6個按鈕共享Click with p1ChooseWar
            p1ChooseWiz.Click += p1ChooseWar_Click; p1ChooseRan.Click += p1ChooseWar_Click;
            p2ChooseWar.Click += p1ChooseWar_Click; p2ChooseWiz.Click += p1ChooseWar_Click; p2ChooseRan.Click += p1ChooseWar_Click;

            //棋盤42格共享click
            foreach (Button bt in bts)
                bt.Click += button1_Click;
            //控制項10個按鈕共享click
            p1Move.Click += p1Atk_Click; p1Skill.Click += p1Atk_Click; p1Skip.Click += p1Atk_Click; p1Over.Click += p1Atk_Click;
            p2Atk.Click += p1Atk_Click; p2Move.Click += p1Atk_Click; p2Skill.Click += p1Atk_Click; p2Skip.Click += p1Atk_Click; p2Over.Click += p1Atk_Click;

            //初始化Alive Lists
            p1Alive.Add(1); p1Alive.Add(2); p1Alive.Add(3);
            p2Alive.Add(4); p2Alive.Add(5); p2Alive.Add(6);
        }

        

        private void startButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            turnLabel.Visible = true;
            startButton.Visible = false;
            p1Label.Visible = true; p2Label.Visible = true;
            p1CharLabel.Visible = true;
            nowChooseButton = null;

            foreach (Button bt in bts) bt.Visible = true;
            for (int i = 0; i < 7; i++)//p1 prepare
            {
                bts[3+6*i].Enabled = false; bts[4 + 6 * i].Enabled = false; bts[5 + 6 * i].Enabled = false;
            }

            p1ChooseWar.Visible = true; p1ChooseWiz.Visible = true; p1ChooseRan.Visible = true;
            p2ChooseWar.Visible = true; p2ChooseWiz.Visible = true; p2ChooseRan.Visible = true;


        }

        //一、遊戲準備期
        private void timer1_Tick(object sender, EventArgs e)
        {
            turnLabel.Text = $"準備階段\n{prepareTime}";
            switch (turn)
            {
                case "p1":                    
                    if (prepareTime == 0) //p1準備時間結束
                    {
                        prepareTime = 1; //restart timing
                        //時間到時，自動幫你放chess
                        if (preWarNum == 1 || preWizNum == 1 || preRanNum == 1)
                            autocreateChess();

                        for (int i = 0; i < 7; i++)//p2 prepare
                        {
                            bts[0 + 6 * i].Enabled = false; bts[1 + 6 * i].Enabled = false; bts[2 + 6 * i].Enabled = false;
                            bts[41 - (0 + 6 * i)].Enabled = true; bts[41 - (1 + 6 * i)].Enabled = true; bts[41 - (2 + 6 * i)].Enabled = true;
                        }                      
                        turn = "p2";
                        p2CharLabel.Visible = true;
                        p2ChooseWar.Enabled = true; p2ChooseWiz.Enabled = true; p2ChooseRan.Enabled = true;
                        p1ChooseWar.Enabled = false; p1ChooseWiz.Enabled = false; p1ChooseRan.Enabled = false;

                        preWarNum = 1; preWizNum = 1; preRanNum = 1;
                        break;
                    }
                    prepareTime--;                   
                    break;
                case "p2":
                    if (prepareTime == 0)
                    {
                        //時間到時，自動幫你放chess
                        if (preWarNum == 1 || preWizNum == 1 || preRanNum == 1)
                            autocreateChess();

                        timer1.Enabled = false; turnLabel.Text = "P1 turn";                      
                        foreach (Button bt in bts) bt.Enabled = true; //正式開始遊戲
                        foreach (Button bt in p1Control) bt.Visible = true;
                        foreach (Button bt in p2Control) bt.Visible = true;
                        turn = "p1";
                        p2ChooseWar.Visible = false; p2ChooseWiz.Visible = false; p2ChooseRan.Visible = false;
                        p1ChooseWar.Visible = false; p1ChooseWiz.Visible = false; p1ChooseRan.Visible = false;                     
                        nowChooseButton = null; gameStatus = "Game";
                        setState();
                        p2CharLabel.Visible = false; p2InfoLabel.Visible = false;
                        break;
                    }
                    prepareTime--;
                    break;
            }
            
        }

        //選擇要放哪個chess
        private void p1ChooseWar_Click(object sender, EventArgs e)
        {
            Button clickbt = (Button)sender;
            nowChooseButton = clickbt;
            if (turn == "p1")
            {
                if (clickbt == p1ChooseWar)
                    p1CharLabel.Text = "戰士";
                if (clickbt == p1ChooseWiz)
                    p1CharLabel.Text = "法師";
                if (clickbt == p1ChooseRan)
                    p1CharLabel.Text = "遊俠";
            }
            else
            {
                if (clickbt == p2ChooseWar)
                    p2CharLabel.Text = "戰士";
                if (clickbt == p2ChooseWiz)
                    p2CharLabel.Text = "法師";
                if (clickbt == p2ChooseRan)
                    p2CharLabel.Text = "遊俠";
            }
                

            
        }

        //偵測點擊棋盤
        private void button1_Click(object sender, EventArgs e) //有共享，46個按鈕都可以執行
        {
            Button clickbt = (Button)sender; //點擊的按鈕
            //準備階段
            if(gameStatus == "Prepare" && nowChooseButton != null) 
            {
                if(turn == "p1")
                {
                    switch (nowChooseButton.Tag)
                    {
                        case "War":
                            createChess("War", clickbt);
                            break;
                        case "Wiz":
                            createChess("Wiz", clickbt);
                            break;
                        case "Ran":
                            createChess("Ran", clickbt);
                            break;
                    }
                }
                else
                {
                    switch (nowChooseButton.Tag)
                    {
                        case "War":
                            createChess("War", clickbt);
                            break;
                        case "Wiz":
                            createChess("Wiz", clickbt);
                            break;
                        case "Ran":
                            createChess("Ran", clickbt);
                            break;
                    }
                }
                
            }
            else if (gameStatus == "Prepare" && nowChooseButton == null && preWarNum ==1) //預設先擺放戰士
            {
                createChess("War", clickbt);
                preWarNum = 0;
            }

            //遊戲階段，需要點棋盤的功能
            if(gameStatus == "Game" && nowChooseButton!=null)
            {
                switch (nowChooseButton.Text)
                {
                    case "攻擊":
                        Attack(clickbt);
                        setState();
                        break;
                    case "移動":
                        Move(clickbt);
                        setState();
                        break;
                    case "技能":
                        Skill(clickbt);
                        setState();
                        break;
                }
                setState();
            }
        }

        //Create a Chess Object
        private void createChess(string chessType, Button positionButton)
        {
            if(turn == "p1")
            {
                switch (chessType)
                {
                    case "War":
                        p1War.position = Array.IndexOf(bts, positionButton);
                        positionButton.BackColor = Color.LightBlue; positionButton.Text = "戰士"; positionButton.Enabled =false;
                        preWarNum = 0;
                        p1ChooseWar.Enabled = false; p1ChooseWar.Text = "戰士: 0顆";
                        break;
                    case "Wiz":
                        p1Wiz.position = Array.IndexOf(bts, positionButton); 
                        positionButton.BackColor = Color.LightBlue; positionButton.Text = "法師"; positionButton.Enabled = false;
                        preWizNum = 0;
                        p1ChooseWiz.Enabled = false; p1ChooseWiz.Text = "法師: 0顆";
                        break;
                    case "Ran":
                        p1Ran.position = Array.IndexOf(bts, positionButton); 
                        positionButton.BackColor = Color.LightBlue; positionButton.Text = "遊俠"; positionButton.Enabled = false;
                        preRanNum = 0;
                        p1ChooseRan.Enabled = false; p1ChooseRan.Text = "遊俠: 0顆";
                        break;
                }
            }
            else
            {
                switch (chessType)
                {
                    case "War":
                        p2War.position = Array.IndexOf(bts, positionButton); positionButton.Text = "戰士";
                        positionButton.BackColor = Color.LightPink; positionButton.Enabled = false;
                        preWarNum = 0;
                        p2ChooseWar.Enabled = false; p2ChooseWar.Text = "戰士: 0顆";
                        break;
                    case "Wiz":
                        p2Wiz.position = Array.IndexOf(bts, positionButton); positionButton.Text = "法師";
                        positionButton.BackColor = Color.LightPink; positionButton.Enabled = false;
                        preWizNum = 0;
                        p2ChooseWiz.Enabled = false; p2ChooseWiz.Text = "法師: 0顆";
                        break;
                    case "Ran":
                        p2Ran.position = Array.IndexOf(bts, positionButton); positionButton.Text = "遊俠";
                        positionButton.BackColor = Color.LightPink; positionButton.Enabled = false;
                        preRanNum = 0;
                        p2ChooseRan.Enabled = false; p2ChooseRan.Text = "遊俠: 0顆";
                        break;
                }
            }
            nowChooseButton = null; //清空temp Button
        }

        //準備時間到了，自動放Chess
        private void autocreateChess()
        {
            if(turn == "p1")
            {
                List<Button> p1Empty = new List<Button>();
                int index = 0;
                //在第一行找三個空位
                for (int i = 0; i <7; i++)
                {
                    if (bts[i * 6].Text == "" && index < 4)
                        p1Empty.Add(bts[i * 6]);
                }
                p1Empty.Reverse(); //讓第一個擺放的棋子，能放到List的尾巴
                //自動擺放還沒下的棋
                if (preWarNum == 1)
                {
                    p1War.position = Array.IndexOf(bts, p1Empty.Last());
                    p1Empty.Last().BackColor = Color.LightBlue; p1Empty.Last().Text = "戰士";
                    p1Empty.Last().Enabled = false;
                    p1Empty.RemoveAt(p1Empty.Count - 1); //刪除list最後一個(即第一個空格)
                }
                if (preWizNum == 1)
                {
                    p1Wiz.position = Array.IndexOf(bts, p1Empty.Last());
                    p1Empty.Last().BackColor = Color.LightBlue; p1Empty.Last().Text = "法師";
                    p1Empty.Last().Enabled = false;
                    p1Empty.RemoveAt(p1Empty.Count - 1); 
                }
                if (preRanNum == 1)
                {
                    p1Ran.position = Array.IndexOf(bts, p1Empty.Last());
                    p1Empty.Last().BackColor = Color.LightBlue; p1Empty.Last().Text = "遊俠";
                    p1Empty.Last().Enabled = false;
                    p1Empty.RemoveAt(p1Empty.Count - 1); 
                }

            }
            else
            {
                List<Button> p2Empty = new List<Button>();
                int index = 0;
                //在第一行找三個空位
                for (int i = 0; i < 7; i++)
                {
                    if (bts[5 + i * 6].Text == "" && index < 4)
                        p2Empty.Add(bts[5 + i * 6]);
                }
                p2Empty.Reverse(); //讓第一個擺放的棋子，能放到List的尾巴
                //自動擺放還沒下的棋
                if (preWarNum == 1)
                {
                    p2War.position = Array.IndexOf(bts, p2Empty.Last());
                    p2Empty.Last().BackColor = Color.LightPink; p2Empty.Last().Text = "戰士";
                    p2Empty.Last().Enabled = false;
                    p2Empty.RemoveAt(p2Empty.Count - 1); //刪除list最後一個(即第一個空格)
                }
                if (preWizNum == 1)
                {
                    p2Wiz.position = Array.IndexOf(bts, p2Empty.Last());
                    p2Empty.Last().BackColor = Color.LightPink; p2Empty.Last().Text = "法師";
                    p2Empty.Last().Enabled = false;
                    p2Empty.RemoveAt(p2Empty.Count - 1);
                }
                if (preRanNum == 1)
                {
                    p2Ran.position = Array.IndexOf(bts, p2Empty.Last());
                    p2Empty.Last().BackColor = Color.LightPink; p2Empty.Last().Text = "遊俠";
                    p2Empty.Last().Enabled = false;
                    p2Empty.RemoveAt(p2Empty.Count - 1); 
                }

            }
        }



        //二、遊戲中
        private void p1Skip_Click(object sender, EventArgs e)
        {
            Skip();
            setState();
        }

        private void p2Skip_Click(object sender, EventArgs e)
        {
            Skip();
            setState();
        }
        private void p1Atk_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            nowChooseButton = bt;
        }
        private void p1Over_Click(object sender, EventArgs e)
        {
            gameTurn = 4;
            if (war2Dead)
                gameTurn = 5;
            if (war2Dead && wiz2Dead)
                gameTurn = 6;

            turn = "p2";
            setState();
            p1Over.Enabled = false; 
            p2Atk.Enabled = true; p2Move.Enabled = true; p2Skill.Enabled = true; p2Skip.Enabled = true;
            p1CharLabel.Visible = false; p1InfoLabel.Visible = false;
            p2CharLabel.Visible = true; p2InfoLabel.Visible = true;

            switch (gameTurn)
            {
                case 1:
                    nowTurnButton = bts[p1War.position];
                    break;
                case 2:
                    nowTurnButton = bts[p1Wiz.position];
                    break;
                case 3:
                    nowTurnButton = bts[p1Ran.position];
                    break;
                case 4:
                    nowTurnButton = bts[p2War.position];
                    break;
                case 5:
                    nowTurnButton = bts[p2Wiz.position];
                    break;
                case 6:
                    nowTurnButton = bts[p2Ran.position];
                    break;
            }
        }

        private void p2Over_Click(object sender, EventArgs e)
        {
            gameTurn = 1;
            if (war1Dead)
                gameTurn = 2;
            if (war1Dead && wiz1Dead)
                gameTurn = 3;

            turn = "p1";
            setState();
            p2Over.Enabled = false;
            p1Atk.Enabled = true; p1Move.Enabled = true; p1Skill.Enabled = true; p1Skip.Enabled = true;
            p1CharLabel.Visible = true; p1InfoLabel.Visible = true;
            p2CharLabel.Visible = false; p2InfoLabel.Visible = false;
            switch (gameTurn)
            {
                case 1:
                    nowTurnButton = bts[p1War.position];
                    break;
                case 2:
                    nowTurnButton = bts[p1Wiz.position];
                    break;
                case 3:
                    nowTurnButton = bts[p1Ran.position];
                    break;
                case 4:
                    nowTurnButton = bts[p2War.position];
                    break;
                case 5:
                    nowTurnButton = bts[p2Wiz.position];
                    break;
                case 6:
                    nowTurnButton = bts[p2Ran.position];
                    break;
            }
        }

        private void setState() //按下按鈕後，隨時更新遊戲顯示
        {
            //顯示狀態欄
            int hpInfo = 0, mpInfo = 0, atkInfo = 0, atkRInfo = 0, moveRInfo = 0, posiInfo=0;
            string charInfo = "";
            switch (gameTurn)
            {
                case 1:
                    hpInfo = p1War.hp; mpInfo = p1War.mp; atkInfo = p1War.atk; posiInfo = p1War.position;
                    atkRInfo = p1War.atkRange; moveRInfo = p1War.moveRange; charInfo = "戰士";
                    break;
                case 2:
                    hpInfo = p1Wiz.hp; mpInfo = p1Wiz.mp; atkInfo = p1Wiz.atk; posiInfo = p1Wiz.position;
                    atkRInfo = p1Wiz.atkRange; moveRInfo = p1Wiz.moveRange; charInfo = "法師";
                    break;
                case 3:
                    hpInfo = p1Ran.hp; mpInfo = p1Ran.mp; atkInfo = p1Ran.atk; posiInfo = p1Ran.position;
                    atkRInfo = p1Ran.atkRange; moveRInfo = p1Ran.moveRange; charInfo = "遊俠";
                    break;
                case 4:
                    hpInfo = p2War.hp; mpInfo = p2War.mp; atkInfo = p2War.atk; posiInfo = p2War.position;
                    atkRInfo = p2War.atkRange; moveRInfo = p2War.moveRange; charInfo = "戰士";
                    break;
                case 5:
                    hpInfo = p2Wiz.hp; mpInfo = p2Wiz.mp; atkInfo = p2Wiz.atk; posiInfo = p2Wiz.position;
                    atkRInfo = p2Wiz.atkRange; moveRInfo = p2Wiz.moveRange; charInfo = "法師";
                    break;
                case 6:
                    hpInfo = p2Ran.hp; mpInfo = p2Ran.mp; atkInfo = p2Ran.atk; posiInfo = p2Ran.position;
                    atkRInfo = p2Ran.atkRange; moveRInfo = p2Ran.moveRange; charInfo = "遊俠";
                    break;
            }
            //其他顯示
            if(turn == "p1")
            {
                turnLabel.Text = "P1 Turn";
                p1InfoLabel.Text = $"HP: {hpInfo}\nMP: {mpInfo}\nATK: {atkInfo}\nATK Range: {atkRInfo}\nMOVE Range: {moveRInfo}\nPosition: {posiInfo}";
                p1InfoLabel.Visible = true; 
                p1CharLabel.Text = charInfo;
                
           
            }
            else
            {
                turnLabel.Text = "P2 Turn";
                p2InfoLabel.Text = $"HP: {hpInfo}\nMP: {mpInfo}\nATK: {atkInfo}\nATK Range: {atkRInfo}\nMOVE Range: {moveRInfo}\nPosition: {posiInfo}";
                p2InfoLabel.Visible = true; 
                p2CharLabel.Text = charInfo;
                
              
            }
            //判斷棋子掛了
            deadJudge();
            //判斷勝利
            winJudge();

        }

        //gameTurn換下一個(在不是下一個換turn的情況下)
        private void nextTurn()
        {
            if (gameTurn != 3 && gameTurn != 6)
                gameTurn++;

                                  
            if (gameTurn < 4) //1,2,3
                turn = "p1";
            else //4,5,6
                turn = "p2";
            //nowTurnButton更換
            switch (gameTurn)
            {
                case 1:
                    nowTurnButton = bts[p1War.position];
                    break;
                case 2:
                    nowTurnButton = bts[p1Wiz.position];
                    break;
                case 3:
                    nowTurnButton = bts[p1Ran.position];
                    break;
                case 4:
                    nowTurnButton = bts[p2War.position];
                    break;
                case 5:
                    nowTurnButton = bts[p2Wiz.position];
                    break;
                case 6:
                    nowTurnButton = bts[p2Ran.position];
                    break;
            }
        }

        private void Attack( Button Target)
        {
            int locationX = 0, locationY = 0, atkRange = 0;
            double deltaXSqr = 0, deltaYSqr = 0, lengthSqr = 0;
            bool successAtk = false;
            bool tooFar = false; //超出攻擊範圍
            int targetTurn = 0; //紀錄是誰被攻擊了

            //讀取自身位置
            switch (gameTurn)
            {
                case 1:
                    locationX = bts[p1War.position].Location.X; locationY = bts[p1War.position].Location.Y;
                    atkRange = p1War.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1War.position];
                    break;
                case 2:
                    locationX = bts[p1Wiz.position].Location.X; locationY = bts[p1Wiz.position].Location.Y;
                    atkRange = p1Wiz.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1Wiz.position];
                    break;
                case 3:
                    locationX = bts[p1Ran.position].Location.X; locationY = bts[p1Ran.position].Location.Y;
                    atkRange = p1Ran.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1Ran.position];
                    break;
                case 4:
                    locationX = bts[p2War.position].Location.X; locationY = bts[p2War.position].Location.Y;
                    atkRange = p2War.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2War.position];
                    break;
                case 5:
                    locationX = bts[p2Wiz.position].Location.X; locationY = bts[p2Wiz.position].Location.Y;
                    atkRange = p2Wiz.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2Wiz.position];
                    break;
                case 6:
                    locationX = bts[p2Ran.position].Location.X; locationY = bts[p2Ran.position].Location.Y;
                    atkRange = p2Ran.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2Ran.position];
                    break;
            }
            deltaXSqr = Math.Pow(Math.Abs(Target.Location.X - locationX), 2.0); //deltaX 平方
            deltaYSqr = Math.Pow(Math.Abs(Target.Location.Y - locationY), 2.0); //deltaY 平方   
            lengthSqr = deltaXSqr + deltaYSqr;


            if (turn == "p1" && Target.BackColor == Color.LightPink)
            {
                switch (gameTurn)
                {
                    case 1:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p1War.attack(p2War);
                                    successAtk = true;
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    p1War.attack(p2Wiz);
                                    successAtk = true;
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    p1War.attack(p2Ran);
                                    successAtk = true;
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 2:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p1Wiz.attack(p2War);
                                    successAtk = true;
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    p1Wiz.attack(p2Wiz);
                                    successAtk = true;
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    p1Wiz.attack(p2Ran);
                                    successAtk = true;
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 3:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p1Ran.attack(p2War);
                                    successAtk = true;
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    p1Ran.attack(p2Wiz);
                                    successAtk = true;
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    p1Ran.attack(p2Ran);
                                    successAtk = true;
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                }
            }
            else if (turn == "p2" && Target.BackColor == Color.LightBlue)
            {
                switch (gameTurn)
                {
                    case 4:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p2War.attack(p1War);
                                    successAtk = true;
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    p2War.attack(p1Wiz);
                                    successAtk = true;
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    p2War.attack(p1Ran);
                                    successAtk = true;
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 5:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p2Wiz.attack(p1War);
                                    successAtk = true;
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    p2Wiz.attack(p1Wiz);
                                    successAtk = true;
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    p2Wiz.attack(p1Ran);
                                    successAtk = true;
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 6:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    p2Ran.attack(p1War);
                                    successAtk = true;
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    p2Ran.attack(p1Wiz);
                                    successAtk = true;
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    p2Ran.attack(p1Ran);
                                    successAtk = true;
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                }
            }
            if (successAtk) //顯示被攻擊目標的資訊
            {
                showTargetInfo(targetTurn);
                if (gameTurn == p1Alive[p1Alive.Count-1] || gameTurn == p2Alive[p2Alive.Count - 1])
                    EnableOver();
                else
                    nextTurn();
            }
            else 
            {
                if(tooFar)
                    MessageBox.Show("超出攻擊範圍");
            }
            nowChooseButton = null;
        }

        

        private void Move(Button Target)
        {
            if(Target.BackColor == SystemColors.Window) //只能走空白
            {
                int locationX = 0, locationY = 0, moveRange = 0;
                double deltaXSqr = 0, deltaYSqr = 0, lengthSqr = 0;
                
                //讀取自身位置
                switch (gameTurn)
                {
                    case 1:
                        locationX = bts[p1War.position].Location.X; locationY = bts[p1War.position].Location.Y;
                        moveRange = p1War.moveRange*50; //一格按鈕長50
                        nowTurnButton = bts[p1War.position];
                        break;
                    case 2:
                        locationX = bts[p1Wiz.position].Location.X; locationY = bts[p1Wiz.position].Location.Y;
                        moveRange = p1Wiz.moveRange * 50; //一格按鈕長50
                        nowTurnButton = bts[p1Wiz.position];
                        break;
                    case 3:
                        locationX = bts[p1Ran.position].Location.X; locationY = bts[p1Ran.position].Location.Y;
                        moveRange = p1Ran.moveRange * 50; //一格按鈕長50
                        nowTurnButton = bts[p1Ran.position];
                        break;
                    case 4:
                        locationX = bts[p2War.position].Location.X; locationY = bts[p2War.position].Location.Y;
                        moveRange = p2War.moveRange * 50; //一格按鈕長50
                        nowTurnButton = bts[p2War.position];
                        break;
                    case 5:
                        locationX = bts[p2Wiz.position].Location.X; locationY = bts[p2Wiz.position].Location.Y;
                        moveRange = p2Wiz.moveRange * 50; //一格按鈕長50
                        nowTurnButton = bts[p2Wiz.position];
                        break;
                    case 6:
                        locationX = bts[p2Ran.position].Location.X; locationY = bts[p2Ran.position].Location.Y;
                        moveRange = p2Ran.moveRange * 50; //一格按鈕長50
                        nowTurnButton = bts[p2Ran.position];
                        break;
                }
                deltaXSqr = Math.Pow(Math.Abs(Target.Location.X - locationX), 2.0); //deltaX 平方
                deltaYSqr = Math.Pow(Math.Abs(Target.Location.Y - locationY), 2.0); //deltaY 平方   
                lengthSqr = deltaXSqr + deltaYSqr; 
                
                switch (gameTurn)
                {
                    case 1:
                        if(lengthSqr <= Math.Pow(moveRange,2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightBlue; Target.Text = "戰士"; //move
                            p1War.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if (p1Alive[p1Alive.Count - 1] == 1)
                                EnableOver();
                            else
                                nextTurn();
                        }                         
                        break;
                    case 2:
                        if (lengthSqr <= Math.Pow(moveRange, 2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightBlue; Target.Text = "法師"; //move
                            p1Wiz.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if (p1Alive[p1Alive.Count - 1] == 2)
                                EnableOver();
                            else
                                nextTurn();
                        }
                        break;
                    case 3:
                        if (lengthSqr <= Math.Pow(moveRange, 2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightBlue; Target.Text = "遊俠"; //move
                            p1Ran.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if(p1Alive[p1Alive.Count-1] == 3)
                                EnableOver();
                        }
                        break;
                    case 4:
                        if (lengthSqr <= Math.Pow(moveRange, 2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightPink; Target.Text = "戰士"; //move
                            p2War.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if (p2Alive[p2Alive.Count - 1] == 4)
                                EnableOver();
                            else
                                nextTurn();
                        }
                        break;
                    case 5:
                        if (lengthSqr <= Math.Pow(moveRange, 2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightPink; Target.Text = "法師"; //move
                            p2Wiz.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if (p2Alive[p2Alive.Count - 1] == 5)
                                EnableOver();
                            else
                                nextTurn();
                        }
                        break;
                    case 6:
                        if (lengthSqr <= Math.Pow(moveRange, 2))
                        {
                            nowTurnButton.BackColor = SystemColors.Window; nowTurnButton.Text = ""; //刪除原本的棋子
                            Target.BackColor = Color.LightPink; Target.Text = "遊俠"; //move
                            p2Ran.position = Array.IndexOf(bts, Target); //更改物件position資訊
                            if (p2Alive[p2Alive.Count - 1] == 6)
                                EnableOver();                          
                        }
                        break;

                }
            }
            nowChooseButton = null;
        }

        private void Skip()
        {
            
            if (gameTurn == p1Alive[p1Alive.Count-1] || gameTurn == p2Alive[p2Alive.Count - 1])
                EnableOver();
            else
                nextTurn();
            nowChooseButton = null;
        }

        private void Skill(Button Target)
        {
            int locationX = 0, locationY = 0, atkRange = 0;
            double deltaXSqr = 0, deltaYSqr = 0, lengthSqr = 0;
            bool successSkill = false;
            bool tooFar = false; //超出攻擊範圍
            int targetTurn = 0; //紀錄是誰被攻擊了

            //讀取自身位置
            switch (gameTurn)
            {
                case 1:
                    locationX = bts[p1War.position].Location.X; locationY = bts[p1War.position].Location.Y;
                    atkRange = p1War.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1War.position];
                    break;
                case 2:
                    locationX = bts[p1Wiz.position].Location.X; locationY = bts[p1Wiz.position].Location.Y;
                    atkRange = p1Wiz.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1Wiz.position];
                    break;
                case 3:
                    locationX = bts[p1Ran.position].Location.X; locationY = bts[p1Ran.position].Location.Y;
                    atkRange = p1Ran.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p1Ran.position];
                    break;
                case 4:
                    locationX = bts[p2War.position].Location.X; locationY = bts[p2War.position].Location.Y;
                    atkRange = p2War.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2War.position];
                    break;
                case 5:
                    locationX = bts[p2Wiz.position].Location.X; locationY = bts[p2Wiz.position].Location.Y;
                    atkRange = p2Wiz.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2Wiz.position];
                    break;
                case 6:
                    locationX = bts[p2Ran.position].Location.X; locationY = bts[p2Ran.position].Location.Y;
                    atkRange = p2Ran.atkRange * 50; //一格按鈕長50
                    nowTurnButton = bts[p2Ran.position];
                    break;
            }
            deltaXSqr = Math.Pow(Math.Abs(Target.Location.X - locationX), 2.0); //deltaX 平方
            deltaYSqr = Math.Pow(Math.Abs(Target.Location.Y - locationY), 2.0); //deltaY 平方   
            lengthSqr = deltaXSqr + deltaYSqr;

            int hpInfo = 0, mpInfo = 0, atkInfo = 0, atkRInfo = 0, moveRInfo = 0;
            if(turn == "p1" && Target.BackColor == Color.LightPink)
            {
                switch(gameTurn)
                {
                    case 1:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p1War.Skill(p2War);
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    successSkill = p1War.Skill(p2Wiz);
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    successSkill = p1War.Skill(p2Ran);
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 2:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p1Wiz.Skill(p2War);
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    successSkill = p1Wiz.Skill(p2Wiz);
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    successSkill = p1Wiz.Skill(p2Ran);
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 3:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p1Ran.Skill(p2War);
                                    targetTurn = 4;
                                    break;
                                case "法師":
                                    successSkill = p1Ran.Skill(p2Wiz);
                                    targetTurn = 5;
                                    break;
                                case "遊俠":
                                    successSkill = p1Ran.Skill(p2Ran);
                                    targetTurn = 6;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                }
            }
            else if (turn == "p2" && Target.BackColor == Color.LightBlue)
            {
                switch (gameTurn)
                {
                    case 4:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p2War.Skill(p1War);
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    successSkill = p2War.Skill(p1Wiz);
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    successSkill = p2War.Skill(p1Ran);
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 5:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p2Wiz.Skill(p1War);
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    successSkill = p2Wiz.Skill(p1Wiz);
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    successSkill = p2Wiz.Skill(p1Ran);
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                    case 6:
                        if (lengthSqr <= Math.Pow(atkRange, 2))
                        {
                            switch (Target.Text)
                            {
                                case "戰士":
                                    successSkill = p2Ran.Skill(p1War);
                                    targetTurn = 1;
                                    break;
                                case "法師":
                                    successSkill = p2Ran.Skill(p1Wiz);
                                    targetTurn = 2;
                                    break;
                                case "遊俠":
                                    successSkill = p2Ran.Skill(p1Ran);
                                    targetTurn = 3;
                                    break;
                            }
                        }
                        else
                            tooFar = true;
                        break;
                }
            }
            if (successSkill) //顯示被攻擊目標的資訊
            {
                showTargetInfo(targetTurn);
                if (gameTurn == p1Alive[p1Alive.Count-1] || gameTurn == p2Alive[p2Alive.Count - 1])
                    EnableOver();
                else
                    nextTurn();
            }
            else
            {
                if(tooFar)
                    MessageBox.Show("超出攻擊範圍");
            }
            nowChooseButton = null;

        }

        private void showTargetInfo(int targetTurnNum) //印出目標資訊
        {
            int hpInfo = 0, mpInfo = 0, atkInfo = 0, atkRInfo = 0, moveRInfo = 0;
            string CharInfo = "";
            if (turn == "p1")
            {                     
                switch (targetTurnNum)
                {
                    case 4:
                        hpInfo = p2War.hp; mpInfo = p2War.mp; atkInfo = p2War.atk; atkRInfo = p2War.atkRange;
                        moveRInfo = p2War.moveRange; CharInfo = "戰士";
                        break;
                    case 5:
                        hpInfo = p2Wiz.hp; mpInfo = p2Wiz.mp; atkInfo = p2Wiz.atk; atkRInfo = p2Wiz.atkRange;
                        moveRInfo = p2Wiz.moveRange; CharInfo = "法師";
                        break;
                    case 6:
                        hpInfo = p2Ran.hp; mpInfo = p2Ran.mp; atkInfo = p2Ran.atk; atkRInfo = p2Ran.atkRange;
                        moveRInfo = p2Ran.moveRange; CharInfo = "遊俠";
                        break;

                }
                p2InfoLabel.Text = $"HP: {hpInfo}\nMP: {mpInfo}\nATK: {atkInfo}\nATK Range: {atkRInfo}\nMOVE Range: {moveRInfo}";
                p2CharLabel.Text = CharInfo;
                p2InfoLabel.Visible = true; p2CharLabel.Visible = true;
            }
            else
            {
                switch (targetTurnNum)
                {
                    case 1:
                        hpInfo = p1War.hp; mpInfo = p1War.mp; atkInfo = p1War.atk; atkRInfo = p1War.atkRange;
                        moveRInfo = p1War.moveRange; CharInfo = "戰士";
                        break;
                    case 2:
                        hpInfo = p1Wiz.hp; mpInfo = p1Wiz.mp; atkInfo = p1Wiz.atk; atkRInfo = p1Wiz.atkRange;
                        moveRInfo = p1Wiz.moveRange; CharInfo = "法師";
                        break;
                    case 3:
                        hpInfo = p1Ran.hp; mpInfo = p1Ran.mp; atkInfo = p1Ran.atk; atkRInfo = p1Ran.atkRange;
                        moveRInfo = p1Ran.moveRange; CharInfo = "遊俠";
                        break;


                }

                p1InfoLabel.Text = $"HP: {hpInfo}\nMP: {mpInfo}\nATK: {atkInfo}\nATK Range: {atkRInfo}\nMOVE Range: {moveRInfo}";
                p1CharLabel.Text = CharInfo;
                p1InfoLabel.Visible = true; p1CharLabel.Visible = true;
            }
        }
   

        //Enable p1 or p2's Over button(輪到gameturn 3 or 6時，下一步是點over button)
        private void EnableOver()
        {
            if (turn == "p1")
            {
                p1Atk.Enabled = false; p1Move.Enabled = false; p1Skill.Enabled = false; p1Skip.Enabled = false;
                p1Over.Enabled = true;
            }
            else {
                p2Atk.Enabled = false; p2Move.Enabled = false; p2Skill.Enabled = false; p2Skip.Enabled = false;
                p2Over.Enabled = true;
            }
        }


        private void deadJudge()
        {
            if(war1Dead == false && p1War.hp <= 0)
            {
                bts[p1War.position].BackColor = SystemColors.Window; bts[p1War.position].Text = "";
                war1Dead = true;
                p1Alive.Remove(1);
            }
            if (war2Dead == false && p2War.hp <= 0)
            {
                bts[p2War.position].BackColor = SystemColors.Window; bts[p2War.position].Text = "";
                war2Dead = true;
                p2Alive.Remove(4);
            }
            if (wiz1Dead == false && p1Wiz.hp <= 0)
            {
                bts[p1Wiz.position].BackColor = SystemColors.Window; bts[p1Wiz.position].Text = "";
                wiz1Dead = true;
                p1Alive.Remove(2);
            }
            if (wiz2Dead == false && p2Wiz.hp <= 0)
            {
                bts[p2Wiz.position].BackColor = SystemColors.Window; bts[p2Wiz.position].Text = "";
                wiz2Dead = true;
                p2Alive.Remove(5);
            }
            if (ran1Dead == false && p1Ran.hp <= 0)
            {
                bts[p1Ran.position].BackColor = SystemColors.Window; bts[p1Ran.position].Text = "";
                ran1Dead = true;
                p1Alive.Remove(3);
            }
            if (ran2Dead == false && p2Ran.hp <= 0)
            {
                bts[p2Ran.position].BackColor = SystemColors.Window; bts[p2Ran.position].Text = "";
                ran2Dead = true;
                p2Alive.Remove(6);
            }

        }

        
        private void winJudge()
        {
            if (p1War.hp<=0 && p1Wiz.hp<=0 && p1Ran.hp <= 0)
            {
                DialogResult result =  MessageBox.Show("P2贏了", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    Application.Exit();

            }
            if (p2War.hp <= 0 && p2Wiz.hp <= 0 && p2Ran.hp <= 0)
            {
                DialogResult result = MessageBox.Show("P1贏了", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    Application.Exit();

            }

        }

        
    }
}
