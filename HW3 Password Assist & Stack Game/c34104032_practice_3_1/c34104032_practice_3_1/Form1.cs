using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c34104032_practice_3_1
{
    public partial class ND_Window : Form
    {
        List<Account> accountList = new List<Account>();
        public ND_Window()
        {
            InitializeComponent();                  
        }

        //主畫面功能
        private void Form1_Load(object sender, EventArgs e) //初始Load
        {
            //主頁面的東西顯示、Enabled
            searchLabel.Visible = true;
            searchResultText.Visible = true;
            searchResult.Visible = true;
            searchBox.Visible = true;
            riskButton.Enabled = true;
            searchButton.Enabled = true;

            riskButton.Text = "風險帳號";
            //主頁面看不見新增及刪除視窗
            homeButton.Visible = false;
            statusBox.Visible = false;
            linkLabel.Visible = false;
            userLabel.Visible = false;
            passwordLabel.Visible = false;
            linkBox.Visible = false;
            userBox.Visible = false;
            passwordBox.Visible = false;
            addButton.Visible = false;
            delButton.Visible = false;
            searchBox2.Visible = false;

        }
        private void searchButton_Click(object sender, EventArgs e) //搜尋
        {
            if (searchBox.Text == "")
            {
                searchResult.Text = ""; //歸零
                foreach (Account element in accountList)
                {
                    searchResult.Text += $"連結: {element.returnLink()}\n使用者: {element.returnUser()}\n密碼: {element.returnPassword()}\n===========================================\n";
                }
            }
            else
            {
                searchResult.Text = ""; //歸零             
                foreach (Account element in accountList)
                {
                    if (element.returnLink().Contains(searchBox.Text))
                    {
                        searchResult.Text += $"連結: {element.returnLink()}\n使用者: {element.returnUser()}\n密碼: {element.returnPassword()}\n===========================================\n";
                    }
                    else
                    {
                        searchResult.Text += "";
                    }
                    
                }
            }

        }

        private void riskButton_Click(object sender, EventArgs e) //風險帳號
        {
            searchResult.Text = ""; //歸零
            List<Account> tempRiskList = new List<Account>();
            for (int i = 0; i < accountList.Count; i++)
            {
                for (int j = 0 ; j< accountList.Count; j++)
                {
                    if (accountList[i].password == accountList[j].password && i != j)
                    {                    
                        tempRiskList.Add(accountList[i]); //重複1號會當一次i，重覆2號也會當一次i
                    }
                } 
            }
            //大於三組資料同時重複就會出錯，用distinct移除重覆資料
            tempRiskList = tempRiskList.Distinct().ToList();
            foreach (Account element in tempRiskList)
            {
                searchResult.Text += $"連結: {element.returnLink()}\n使用者: {element.returnUser()}\n密碼: {element.returnPassword()}\n===========================================\n";
            }       
        }
        private void ND_Button_Click(object sender, EventArgs e) //新增&刪除畫面切換
        {
            //主頁面的東西消失、disabled
            searchResultText.Visible = false;
            searchResult.Visible = false;
            searchLabel.Visible = false;
            searchBox.Visible = false;

            searchButton.Enabled = false;
            riskButton.Text = "風險密碼";
            riskButton.Enabled = false;

            

            //新增及刪除視窗的東西顯示
            homeButton.Visible = true;
            statusBox.Visible = true;
            linkLabel.Visible = true;
            userLabel.Visible = true;
            passwordLabel.Visible = true;
            linkBox.Visible = true;
            userBox.Visible = true;
            passwordBox.Visible = true;
            addButton.Visible = true;
            delButton.Visible = true;
            searchBox2.Visible = true;
            //清除主畫面所有輸出輸入
            searchBox.Text = "";
            searchResult.Text = "";
        }


        //"新增或刪除"(編輯)畫面功能

        private void homeButton_Click(object sender, EventArgs e) //回主畫面
        {
            //清除編輯畫面所有輸入輸出
            statusBox.Text = "我是狀態列";
            linkBox.Text = "";
            userBox.Text = "";
            passwordBox.Text = "";
          

            Form1_Load(sender, e);
            
            
        }

        private void addButton_Click(object sender, EventArgs e) //新增
        {
            bool existJudge = false; //判斷列表中是否有與輸入連結一樣的element
            for (int i = 0; i < accountList.Count; i++)
            {
                if(accountList[i].link == linkBox.Text)
                {
                    existJudge = true;
                }
            }
            if (existJudge == true) //是否新增新一組帳號
            {
                statusBox.Text = "帳號已存在";
            }
            else
            {
                accountList.Add(new Account(linkBox.Text, userBox.Text, passwordBox.Text));
                statusBox.Text = "新增完成";
                //清空輸入欄位
                linkBox.Text = "";
                userBox.Text = "";
                passwordBox.Text = "";
            }
            
        }

        private void delButton_Click(object sender, EventArgs e) //刪除
        {
            //判斷是否有帳號存在: 判斷列表中是否有與輸入連結一樣的element
            bool existJudge = false;
            Account existAccount = new Account("","","");

            bool safetyCheck = false; 
            for (int i = 0; i < accountList.Count; i++)
            {
                if (accountList[i].link == linkBox.Text)
                {
                    existJudge = true;
                    existAccount = accountList[i]; //存起來需要被刪除的帳號
                }
            }
            if(existJudge == true  ) //判斷使用者、密碼是否輸入正確
            {
                if (existAccount.returnUser() == userBox.Text)  
                {
                    if((existAccount.returnPassword() == passwordBox.Text))
                    {
                        safetyCheck = true;
                    }
                }
            }
            if(safetyCheck == true)
            {
                accountList.Remove(existAccount);
                statusBox.Text = "刪除完成";
                //清空輸入欄位
                linkBox.Text = "";
                userBox.Text = "";
                passwordBox.Text = "";
            }
            else
            {
                statusBox.Text = "帳號不存在或密碼錯誤";
            }
        }
    }
}
