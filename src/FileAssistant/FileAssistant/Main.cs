using CommonHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAssistant
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        #region 窗体移动
        private Point mouseOffset;        //记录鼠标指针的坐标
        private bool isMouseDown = false; //记录鼠标按键是否按下
        private void Main_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight -
                SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }
        private void Main_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isMouseDown)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                Location = mousePos;
            }
        }

        private void Main_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // 修改鼠标状态isMouseDown的值
            // 确保只有鼠标左键按下并移动时，才移动窗体
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }
        #endregion
        /// <summary>
        /// 松开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //获取主目录位置
                string mainpath = AlarmSystemHelper.GetValueByIni("Main", "MainCatalog");
                mainpath = System.Web.HttpUtility.UrlDecode(mainpath, System.Text.Encoding.Unicode); //编码
                                                                                                     //GetValue(0) 为第1个文件全路径
                                                                                                     //DataFormats 数据的格式，下有多个静态属性都为string型，除FileDrop格式外还有Bitmap,Text,WaveAudio等格式  
                List<string> pathList = new List<string>();
                for (int i = 0, len = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).Length; i < len; i++)
                {
                    string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(i).ToString();
                    pathList.Add(path);
                    Console.WriteLine(i + path);
                }

                foreach (var item in pathList)
                {
                    if (System.IO.Directory.Exists(item))
                    {
                        MessageBox.Show("暂不支持文件夹分类");
                        //文件夹
                        // FileHelper.DoCopy(item, "F:\\c\\llp.iso");
                    }
                    else if (System.IO.File.Exists(item))
                    {
                        //获取扩展名 “.aspx”
                        string extension = System.IO.Path.GetExtension(item).TrimStart('.');

                        //根据扩展名生成文件夹
                        var dicnmae = XmlHelper.SelectFromXml(System.AppDomain.CurrentDomain.BaseDirectory + "fsdata.xml", "rules", "formattype", "formatname", extension).Item2;
                        if (string.IsNullOrWhiteSpace(dicnmae))
                        {
                            dicnmae = "其他";
                        }
                        string newPath = mainpath + $"\\{dicnmae}";//合成新文件夹路径
                                                                   //判断是否存在新文件路径 不存在创建
                        if (!System.IO.Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);//创建该文件
                        }
                        // 获取文件名
                        string filename = System.IO.Path.GetFileName(item);//文件名  “Default.aspx”
                        newPath = newPath + $"\\{filename}";
                        label1.Text = "复制中···";
                        FileHelper.FileCopyTo(item, newPath,true);
                        label1.Text = string.Empty;
                        newPath = string.Empty;
                    }
                }
                this.label1.Cursor = System.Windows.Forms.Cursors.IBeam; //还原鼠标形状
                button1.Visible = false;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
           
        }
        /// <summary>
        /// 进入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
                this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;  //指定鼠标形状（更好看）  
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
            button1.Visible = true;
        }

        private void Main_DragOver(object sender, DragEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(Properties.Resources.mouse);
            BitmapRegion.CreateControlRegion(button1, bmp, 307, 146);
            //Rectangle r = new Rectangle(0, 0, 307, 146);//width和height均为控件的宽和高
            //button1.DrawToBitmap(bmp, r);
        }

        private void 设置主目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;
                FolderBrowserDialog dilog = new FolderBrowserDialog();
                dilog.Description = "请选择文件夹";
                DialogResult = dilog.ShowDialog();
                if (DialogResult == DialogResult.OK || DialogResult == DialogResult.Yes)
                {
                    path = dilog.SelectedPath;
                    var tempPath = path;
                    path = System.Web.HttpUtility.UrlEncode(path, System.Text.Encoding.Unicode); //编码
                    if (AlarmSystemHelper.TrySetValueByIni("Main", "MainCatalog", path))
                    {
                        MessageBox.Show($"主目录：{tempPath}设置成功");
                    }
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开主目录位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string strPath = System.AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Process.Start(strPath);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Classification classificationRule = new Classification();
            classificationRule.ShowDialog();
            this.Show();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 按时ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mainpath = AlarmSystemHelper.GetValueByIni("Main", "MainCatalog");
            mainpath = System.Web.HttpUtility.UrlDecode(mainpath, System.Text.Encoding.Unicode);
            System.Diagnostics.Process.Start(mainpath);
        }

        private void 开机自启动ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
 }
