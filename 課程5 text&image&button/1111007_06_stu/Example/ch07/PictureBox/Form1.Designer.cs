﻿namespace PictureBox
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCurrent = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPre = new System.Windows.Forms.Button();
            this.lblRate = new System.Windows.Forms.Label();
            this.btnSmall = new System.Windows.Forms.Button();
            this.btnLarge = new System.Windows.Forms.Button();
            this.pic = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(13, 253);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(53, 12);
            this.lblCurrent.TabIndex = 18;
            this.lblCurrent.Text = "lblCurrent";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(237, 249);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 21);
            this.btnNext.TabIndex = 17;
            this.btnNext.Text = "下一張";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPre
            // 
            this.btnPre.Location = new System.Drawing.Point(161, 249);
            this.btnPre.Name = "btnPre";
            this.btnPre.Size = new System.Drawing.Size(70, 21);
            this.btnPre.TabIndex = 16;
            this.btnPre.Text = "上一張";
            this.btnPre.UseVisualStyleBackColor = true;
            this.btnPre.Click += new System.EventHandler(this.btnPre_Click);
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(13, 285);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(38, 12);
            this.lblRate.TabIndex = 15;
            this.lblRate.Text = "lblRate";
            // 
            // btnSmall
            // 
            this.btnSmall.Location = new System.Drawing.Point(237, 281);
            this.btnSmall.Name = "btnSmall";
            this.btnSmall.Size = new System.Drawing.Size(70, 21);
            this.btnSmall.TabIndex = 14;
            this.btnSmall.Text = "縮小";
            this.btnSmall.UseVisualStyleBackColor = true;
            this.btnSmall.Click += new System.EventHandler(this.btnSmall_Click);
            // 
            // btnLarge
            // 
            this.btnLarge.Location = new System.Drawing.Point(161, 281);
            this.btnLarge.Name = "btnLarge";
            this.btnLarge.Size = new System.Drawing.Size(70, 21);
            this.btnLarge.TabIndex = 13;
            this.btnLarge.Text = "放大";
            this.btnLarge.UseVisualStyleBackColor = true;
            this.btnLarge.Click += new System.EventHandler(this.btnLarge_Click);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(9, 12);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(297, 226);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic.TabIndex = 12;
            this.pic.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 309);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPre);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.btnSmall);
            this.Controls.Add(this.btnLarge);
            this.Controls.Add(this.pic);
            this.Name = "Form1";
            this.Text = "圖像瀏覽縮放器";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPre;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.Button btnSmall;
        private System.Windows.Forms.Button btnLarge;
        private System.Windows.Forms.PictureBox pic;

    }
}

