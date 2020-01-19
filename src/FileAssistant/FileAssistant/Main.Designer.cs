namespace FileAssistant
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.按时ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开主目录位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置主目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.开机自启动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(58, 171);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(301, 126);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按时ToolStripMenuItem,
            this.打开主目录位置ToolStripMenuItem,
            this.设置主目录ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.开机自启动ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 162);
            // 
            // 按时ToolStripMenuItem
            // 
            this.按时ToolStripMenuItem.Name = "按时ToolStripMenuItem";
            this.按时ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.按时ToolStripMenuItem.Text = "打开主目录位置";
            this.按时ToolStripMenuItem.Click += new System.EventHandler(this.按时ToolStripMenuItem_Click);
            // 
            // 打开主目录位置ToolStripMenuItem
            // 
            this.打开主目录位置ToolStripMenuItem.Name = "打开主目录位置ToolStripMenuItem";
            this.打开主目录位置ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.打开主目录位置ToolStripMenuItem.Text = "打开文件运行位置";
            this.打开主目录位置ToolStripMenuItem.Click += new System.EventHandler(this.打开主目录位置ToolStripMenuItem_Click);
            // 
            // 设置主目录ToolStripMenuItem
            // 
            this.设置主目录ToolStripMenuItem.Name = "设置主目录ToolStripMenuItem";
            this.设置主目录ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.设置主目录ToolStripMenuItem.Text = "设置主目录";
            this.设置主目录ToolStripMenuItem.Click += new System.EventHandler(this.设置主目录ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(204, 26);
            this.toolStripMenuItem1.Text = "文件整理规则";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 开机自启动ToolStripMenuItem
            // 
            this.开机自启动ToolStripMenuItem.Name = "开机自启动ToolStripMenuItem";
            this.开机自启动ToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
            this.开机自启动ToolStripMenuItem.Text = "开机自启动";
            this.开机自启动ToolStripMenuItem.Click += new System.EventHandler(this.开机自启动ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(159, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 2;
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::FileAssistant.Properties.Resources.init;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(450, 663);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Text = "Main";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.Main_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.Main_DragOver);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开主目录位置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置主目录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem 按时ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 开机自启动ToolStripMenuItem;
    }
}

