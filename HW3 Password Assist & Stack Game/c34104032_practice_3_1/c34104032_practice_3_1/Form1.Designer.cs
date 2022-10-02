namespace c34104032_practice_3_1
{
    partial class ND_Window
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.searchLabel = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.riskButton = new System.Windows.Forms.Button();
            this.searchResultText = new System.Windows.Forms.Label();
            this.ND_Button = new System.Windows.Forms.Button();
            this.linkLabel = new System.Windows.Forms.Label();
            this.userLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.linkBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.userBox = new System.Windows.Forms.TextBox();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.homeButton = new System.Windows.Forms.Button();
            this.delButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.searchResult = new System.Windows.Forms.Label();
            this.searchBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "密碼管理員";
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(39, 56);
            this.searchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(52, 15);
            this.searchLabel.TabIndex = 1;
            this.searchLabel.Text = "搜尋列";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(101, 52);
            this.searchBox.Margin = new System.Windows.Forms.Padding(4);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(612, 25);
            this.searchBox.TabIndex = 2;
            // 
            // searchButton
            // 
            this.searchButton.BackColor = System.Drawing.SystemColors.Window;
            this.searchButton.Location = new System.Drawing.Point(734, 49);
            this.searchButton.Margin = new System.Windows.Forms.Padding(4);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(73, 29);
            this.searchButton.TabIndex = 3;
            this.searchButton.Text = "搜尋";
            this.searchButton.UseVisualStyleBackColor = false;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // riskButton
            // 
            this.riskButton.BackColor = System.Drawing.SystemColors.Window;
            this.riskButton.Location = new System.Drawing.Point(41, 109);
            this.riskButton.Margin = new System.Windows.Forms.Padding(4);
            this.riskButton.Name = "riskButton";
            this.riskButton.Size = new System.Drawing.Size(766, 29);
            this.riskButton.TabIndex = 4;
            this.riskButton.Text = "風險帳號";
            this.riskButton.UseVisualStyleBackColor = false;
            this.riskButton.Click += new System.EventHandler(this.riskButton_Click);
            // 
            // searchResultText
            // 
            this.searchResultText.AutoSize = true;
            this.searchResultText.Location = new System.Drawing.Point(38, 197);
            this.searchResultText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchResultText.Name = "searchResultText";
            this.searchResultText.Size = new System.Drawing.Size(67, 15);
            this.searchResultText.TabIndex = 6;
            this.searchResultText.Text = "搜尋結果";
            // 
            // ND_Button
            // 
            this.ND_Button.BackColor = System.Drawing.SystemColors.Window;
            this.ND_Button.Location = new System.Drawing.Point(647, 159);
            this.ND_Button.Margin = new System.Windows.Forms.Padding(4);
            this.ND_Button.Name = "ND_Button";
            this.ND_Button.Size = new System.Drawing.Size(160, 29);
            this.ND_Button.TabIndex = 7;
            this.ND_Button.Text = "新增或刪除";
            this.ND_Button.UseVisualStyleBackColor = false;
            this.ND_Button.Click += new System.EventHandler(this.ND_Button_Click);
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Location = new System.Drawing.Point(211, 333);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(37, 15);
            this.linkLabel.TabIndex = 8;
            this.linkLabel.Text = "連結";
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Location = new System.Drawing.Point(211, 368);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(52, 15);
            this.userLabel.TabIndex = 9;
            this.userLabel.Text = "使用者";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(211, 402);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(37, 15);
            this.passwordLabel.TabIndex = 10;
            this.passwordLabel.Text = "密碼";
            // 
            // linkBox
            // 
            this.linkBox.Location = new System.Drawing.Point(271, 330);
            this.linkBox.Margin = new System.Windows.Forms.Padding(4);
            this.linkBox.Name = "linkBox";
            this.linkBox.Size = new System.Drawing.Size(393, 25);
            this.linkBox.TabIndex = 11;
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(271, 399);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(4);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(393, 25);
            this.passwordBox.TabIndex = 12;
            // 
            // userBox
            // 
            this.userBox.Location = new System.Drawing.Point(271, 363);
            this.userBox.Margin = new System.Windows.Forms.Padding(4);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(393, 25);
            this.userBox.TabIndex = 13;
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusBox.Location = new System.Drawing.Point(214, 282);
            this.statusBox.Margin = new System.Windows.Forms.Padding(4);
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(450, 25);
            this.statusBox.TabIndex = 14;
            this.statusBox.Text = "我是狀態列";
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.SystemColors.Window;
            this.homeButton.Location = new System.Drawing.Point(647, 159);
            this.homeButton.Margin = new System.Windows.Forms.Padding(4);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(160, 29);
            this.homeButton.TabIndex = 15;
            this.homeButton.Text = "回主畫面";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // delButton
            // 
            this.delButton.BackColor = System.Drawing.SystemColors.Window;
            this.delButton.Location = new System.Drawing.Point(591, 473);
            this.delButton.Margin = new System.Windows.Forms.Padding(4);
            this.delButton.Name = "delButton";
            this.delButton.Size = new System.Drawing.Size(73, 29);
            this.delButton.TabIndex = 16;
            this.delButton.Text = "刪除";
            this.delButton.UseVisualStyleBackColor = false;
            this.delButton.Click += new System.EventHandler(this.delButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.Window;
            this.addButton.Location = new System.Drawing.Point(491, 473);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(73, 29);
            this.addButton.TabIndex = 17;
            this.addButton.Text = "新增";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // searchResult
            // 
            this.searchResult.BackColor = System.Drawing.SystemColors.Window;
            this.searchResult.Location = new System.Drawing.Point(39, 221);
            this.searchResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.searchResult.Name = "searchResult";
            this.searchResult.Size = new System.Drawing.Size(768, 330);
            this.searchResult.TabIndex = 5;
            // 
            // searchBox2
            // 
            this.searchBox2.Enabled = false;
            this.searchBox2.Location = new System.Drawing.Point(42, 53);
            this.searchBox2.Margin = new System.Windows.Forms.Padding(4);
            this.searchBox2.Name = "searchBox2";
            this.searchBox2.Size = new System.Drawing.Size(671, 25);
            this.searchBox2.TabIndex = 18;
            // 
            // ND_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(854, 554);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.delButton);
            this.Controls.Add(this.homeButton);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.linkBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.userLabel);
            this.Controls.Add(this.linkLabel);
            this.Controls.Add(this.ND_Button);
            this.Controls.Add(this.searchResultText);
            this.Controls.Add(this.searchResult);
            this.Controls.Add(this.riskButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ND_Window";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button riskButton;
        private System.Windows.Forms.Label searchResultText;
        private System.Windows.Forms.Button ND_Button;
        private System.Windows.Forms.Label linkLabel;
        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox linkBox;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button delButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Label searchResult;
        private System.Windows.Forms.TextBox searchBox2;
    }
}

