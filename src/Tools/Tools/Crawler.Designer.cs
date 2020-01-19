namespace Tools
{
    partial class Crawler
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
            this.btn_maopoo = new System.Windows.Forms.Button();
            this.btn_attack = new System.Windows.Forms.Button();
            this.btn_vehicleType = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_maopoo
            // 
            this.btn_maopoo.Location = new System.Drawing.Point(44, 45);
            this.btn_maopoo.Name = "btn_maopoo";
            this.btn_maopoo.Size = new System.Drawing.Size(135, 23);
            this.btn_maopoo.TabIndex = 0;
            this.btn_maopoo.Text = "猫扑图片";
            this.btn_maopoo.UseVisualStyleBackColor = true;
            this.btn_maopoo.Click += new System.EventHandler(this.btn_maopoo_Click);
            // 
            // btn_attack
            // 
            this.btn_attack.Location = new System.Drawing.Point(44, 93);
            this.btn_attack.Name = "btn_attack";
            this.btn_attack.Size = new System.Drawing.Size(135, 23);
            this.btn_attack.TabIndex = 1;
            this.btn_attack.Text = "打人图片";
            this.btn_attack.UseVisualStyleBackColor = true;
            this.btn_attack.Click += new System.EventHandler(this.btn_attack_Click);
            // 
            // btn_vehicleType
            // 
            this.btn_vehicleType.Location = new System.Drawing.Point(44, 138);
            this.btn_vehicleType.Name = "btn_vehicleType";
            this.btn_vehicleType.Size = new System.Drawing.Size(135, 23);
            this.btn_vehicleType.TabIndex = 2;
            this.btn_vehicleType.Text = "车型（汽车之家）";
            this.btn_vehicleType.UseVisualStyleBackColor = true;
            this.btn_vehicleType.Click += new System.EventHandler(this.btn_vehicleType_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "存金宝测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Crawler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 420);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_vehicleType);
            this.Controls.Add(this.btn_attack);
            this.Controls.Add(this.btn_maopoo);
            this.Name = "Crawler";
            this.Text = "Crawler";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_maopoo;
        private System.Windows.Forms.Button btn_attack;
        private System.Windows.Forms.Button btn_vehicleType;
        private System.Windows.Forms.Button button1;
    }
}