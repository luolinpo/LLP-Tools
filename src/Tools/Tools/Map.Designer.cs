﻿namespace Tools
{
    partial class Map
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_GCJ02ToBD09 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_lng = new System.Windows.Forms.TextBox();
            this.tbx_lat = new System.Windows.Forms.TextBox();
            this.tbx_return = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Wgs84ToGcj02 = new System.Windows.Forms.Button();
            this.btn_WgstoBD09 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_GCJ02ToBD09
            // 
            this.btn_GCJ02ToBD09.Location = new System.Drawing.Point(150, 162);
            this.btn_GCJ02ToBD09.Name = "btn_GCJ02ToBD09";
            this.btn_GCJ02ToBD09.Size = new System.Drawing.Size(198, 23);
            this.btn_GCJ02ToBD09.TabIndex = 0;
            this.btn_GCJ02ToBD09.Text = "GCJ02=>BD09";
            this.btn_GCJ02ToBD09.UseVisualStyleBackColor = true;
            this.btn_GCJ02ToBD09.Click += new System.EventHandler(this.btn_GCJ02ToBD09_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "经度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "纬度";
            // 
            // tbx_lng
            // 
            this.tbx_lng.Location = new System.Drawing.Point(150, 23);
            this.tbx_lng.Name = "tbx_lng";
            this.tbx_lng.Size = new System.Drawing.Size(198, 25);
            this.tbx_lng.TabIndex = 3;
            // 
            // tbx_lat
            // 
            this.tbx_lat.Location = new System.Drawing.Point(150, 64);
            this.tbx_lat.Name = "tbx_lat";
            this.tbx_lat.Size = new System.Drawing.Size(198, 25);
            this.tbx_lat.TabIndex = 4;
            // 
            // tbx_return
            // 
            this.tbx_return.Location = new System.Drawing.Point(150, 109);
            this.tbx_return.Name = "tbx_return";
            this.tbx_return.Size = new System.Drawing.Size(198, 25);
            this.tbx_return.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(77, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "转换结果";
            // 
            // btn_Wgs84ToGcj02
            // 
            this.btn_Wgs84ToGcj02.Location = new System.Drawing.Point(150, 208);
            this.btn_Wgs84ToGcj02.Name = "btn_Wgs84ToGcj02";
            this.btn_Wgs84ToGcj02.Size = new System.Drawing.Size(198, 23);
            this.btn_Wgs84ToGcj02.TabIndex = 7;
            this.btn_Wgs84ToGcj02.Text = "Wgs84=>Gcj02";
            this.btn_Wgs84ToGcj02.UseVisualStyleBackColor = true;
            this.btn_Wgs84ToGcj02.Click += new System.EventHandler(this.btn_Wgs84ToGcj02_Click);
            // 
            // btn_WgstoBD09
            // 
            this.btn_WgstoBD09.Location = new System.Drawing.Point(150, 253);
            this.btn_WgstoBD09.Name = "btn_WgstoBD09";
            this.btn_WgstoBD09.Size = new System.Drawing.Size(198, 23);
            this.btn_WgstoBD09.TabIndex = 8;
            this.btn_WgstoBD09.Text = "Wgs84=>BD09";
            this.btn_WgstoBD09.UseVisualStyleBackColor = true;
            this.btn_WgstoBD09.Click += new System.EventHandler(this.btn_WgstoBD09_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(622, 253);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(185, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "获取距离";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(622, 163);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 25);
            this.textBox1.TabIndex = 10;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(622, 86);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(185, 25);
            this.textBox2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(622, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "纬度，经度格式";
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 382);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_WgstoBD09);
            this.Controls.Add(this.btn_Wgs84ToGcj02);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_return);
            this.Controls.Add(this.tbx_lat);
            this.Controls.Add(this.tbx_lng);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_GCJ02ToBD09);
            this.Name = "Map";
            this.Text = "工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_GCJ02ToBD09;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_lng;
        private System.Windows.Forms.TextBox tbx_lat;
        private System.Windows.Forms.TextBox tbx_return;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Wgs84ToGcj02;
        private System.Windows.Forms.Button btn_WgstoBD09;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
    }
}

