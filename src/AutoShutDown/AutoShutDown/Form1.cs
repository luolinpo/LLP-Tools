using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoShutDown
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;

            //先构造一个dataTable，或者从数据库读取到一个，这里自己构造一个
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Number", typeof(double));
            dataTable.Columns.Add("ShowText", typeof(String));
            dataTable.Rows.Add(new String[] { "10", "10分钟" });
            dataTable.Rows.Add(new String[] { "30", "30分钟" });
            dataTable.Rows.Add(new String[] { "60", "60分钟" });
            dataTable.Rows.Add(new String[] { "120", "2小时" });
            dataTable.Rows.Add(new String[] { "240", "4小时" });
            dataTable.Rows.Add(new String[] { "360", "6小时" });

            comboBox1.DataSource = dataTable;//绑定
            comboBox1.DisplayMember = dataTable.Columns[1].ColumnName;//显示的文本
            comboBox1.ValueMember = dataTable.Columns[0].ColumnName;//对应的值
            button1.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            dateTimePicker1.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan ts = dateTimePicker1.Value.Subtract(DateTime.Now);
            if (ts.TotalSeconds < 0)
            {
                MessageBox.Show("关机时间不能小于当前时间");
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double myTime=0;
            if (radioButton1.Checked == true)
            {
                TimeSpan ts = dateTimePicker1.Value.Subtract(DateTime.Now);
                myTime = ts.TotalSeconds;
            }
            else if (radioButton2.Checked == true)
            {
                if (!double.TryParse(this.comboBox1.SelectedValue.ToString(), out myTime))
                {
                    MessageBox.Show("定时关机设置失败");
                    return;
                }
                else
                {
                    myTime = myTime * 60;
                }
            }
            else
            {
                MessageBox.Show("定时关机设置失败");
                return;
            }
            var startInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true ;
            startInfo.RedirectStandardOutput = true ;
            startInfo.RedirectStandardError = true  ;
            startInfo.CreateNoWindow = true;
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = startInfo;
            myProcess.Start();
            //myProcess.StandardInput.WriteLine("calc");
            DialogResult dr = MessageBox.Show("确认关机吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                //用户选择确认的操作
                //MessageBox.Show("您选择的是【确认】");
                myProcess.StandardInput.WriteLine("shutdown -a ");
                myProcess.StandardInput.WriteLine("shutdown -s -t " + (int)myTime);
                int myTime2 = (int)myTime / 60;
                MessageBox.Show("定时关机设置成功," + myTime2.ToString() + "分钟后将自动关机");
            }
            else if (dr == DialogResult.Cancel)
            {
                //用户选择取消的操作
                //MessageBox.Show("您选择的是【取消】");
            }
           
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            var myProcess = new System.Diagnostics.Process();
            myProcess.StartInfo = startInfo;
            myProcess.Start();
            //myProcess.StandardInput.WriteLine("calc");
            DialogResult dr = MessageBox.Show("确认取消关机吗？", "提示", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                //用户选择确认的操作
                //MessageBox.Show("您选择的是【确认】");
                myProcess.StandardInput.WriteLine("shutdown -a ");
                MessageBox.Show("取消关机成功");
            }
            else if (dr == DialogResult.Cancel)
            {
                //用户选择取消的操作
                //MessageBox.Show("您选择的是【取消】");
            }
        }
    }
}
