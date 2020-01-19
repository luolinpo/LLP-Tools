using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{

    public class LLPAccount{
        public string KeyName { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }

    }
    public partial class AccountManage : Form
    {
        public AccountManage()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
        



            DataTable dt = new DataTable();
            dt.Columns.Add("class", typeof(string));//年级  
            dt.Columns.Add("count", typeof(int));   //人数  
            dt.Rows.Add("一年级", 120);
            dt.Rows.Add("二年级", 180);
            dt.Rows.Add("三年级", 890);
            dt.Rows.Add("四年级", 108);
            dt.Rows.Add("五年级", 280);
            dt.Rows.Add("六年级", 320);
            dt.Rows.Add("七年级", 450);
            dt.Rows.Add("八年级", 410);
            dt.Rows.Add("九年级", 230);

            this.dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            MessageBox.Show(e.RowIndex.ToString());
        }
    }
}
