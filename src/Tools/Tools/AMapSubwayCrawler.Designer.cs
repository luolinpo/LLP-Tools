namespace Tools
{
    partial class AMapSubwayCrawler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Start = new System.Windows.Forms.Button();
            this.rbox_info = new System.Windows.Forms.RichTextBox();
            this.rbox_errInfo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(433, 91);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 0;
            this.btn_Start.Text = "获取数据";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // rbox_info
            // 
            this.rbox_info.Location = new System.Drawing.Point(25, 38);
            this.rbox_info.Name = "rbox_info";
            this.rbox_info.Size = new System.Drawing.Size(379, 517);
            this.rbox_info.TabIndex = 1;
            this.rbox_info.Text = "";
            // 
            // rbox_errInfo
            // 
            this.rbox_errInfo.Location = new System.Drawing.Point(565, 38);
            this.rbox_errInfo.Name = "rbox_errInfo";
            this.rbox_errInfo.Size = new System.Drawing.Size(379, 522);
            this.rbox_errInfo.TabIndex = 1;
            this.rbox_errInfo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "抓取信息：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(562, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "错误信息：";
            // 
            // AMapSubwayCrawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 567);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbox_errInfo);
            this.Controls.Add(this.rbox_info);
            this.Controls.Add(this.btn_Start);
            this.Name = "AMapSubwayCrawler";
            this.Text = "AMapSubway";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.RichTextBox rbox_info;
        private System.Windows.Forms.RichTextBox rbox_errInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}