namespace Tools
{
    partial class ZytTools
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
            this.btn_openflp = new System.Windows.Forms.Button();
            this.ofd_red = new System.Windows.Forms.OpenFileDialog();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_openflp
            // 
            this.btn_openflp.Location = new System.Drawing.Point(37, 24);
            this.btn_openflp.Name = "btn_openflp";
            this.btn_openflp.Size = new System.Drawing.Size(475, 33);
            this.btn_openflp.TabIndex = 0;
            this.btn_openflp.Text = "选择execl(根据车牌批量设置报警和通知人)";
            this.btn_openflp.UseVisualStyleBackColor = true;
            this.btn_openflp.Click += new System.EventHandler(this.btn_openflp_Click);
            // 
            // ofd_red
            // 
            this.ofd_red.FileName = "openFileDialog1";
            this.ofd_red.Filter = "(*.xls)|*.xls|(*.xlsx)|*.xlsx";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(600, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(675, 601);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // ZytTools
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 625);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_openflp);
            this.Name = "ZytTools";
            this.Text = "ZytTools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_openflp;
        private System.Windows.Forms.OpenFileDialog ofd_red;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}