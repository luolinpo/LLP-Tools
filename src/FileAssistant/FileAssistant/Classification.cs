using CommonHelp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FileAssistant
{
    public partial class Classification : Form
    {
        private string xmlpath=string.Empty;
        public Classification()
        {
            xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "fsdata.xml";
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        public int AddNum = 1;
        public void AddRuleTextAndButton(string filetype, string filename)
        {
            if (AddNum > 10)
            {
                return;
            }
            uc_Rule ucRule = new uc_Rule();
            ucRule.SetValue( filetype, filename);
            ucRule.BtnUpdateEvent += UcRule_BtnUpdateEvent;
            ucRule.BtnDeleteEvent += UcRule_BtnDeleteEvent;
            if (AddNum == 1)
            {
                ucRule.Location = new Point(12, 12);
            }
            else
            {
                ucRule.Location = new Point(12, 12 + 29 * (AddNum - 1));
            }
            this.Controls.Add(ucRule);
            AddNum++;
        }

        private void UcRule_BtnDeleteEvent(object sender, EventArgs e)
        {
            uc_Rule ucRule =(uc_Rule)sender;
            Tuple<string,string> temp= ucRule.GetValue();
            XmlHelper.DeleteFromXml(xmlpath, "rules","formattype", temp.Item1);
            GetAllRule();
        }

        private void Classification_Load(object sender, EventArgs e)
        {
            GetAllRule();
        }

        public void GetAllRule()
        {
            AddNum = 1;
            foreach (Control ctl in this.Controls)
            {
                if (ctl is uc_Rule)
                {
                    this.Controls.Remove(ctl);
                }
            }
            DataSet ds = XmlHelper.ConvertXMLFileToDataSet(System.AppDomain.CurrentDomain.BaseDirectory + "fsdata.xml");
            if ( ds?.Tables.Count>0)
            {
                if (ds?.Tables[0]?.Rows != null)
                {
                    for (int i = 0, ic = ds.Tables[0].Rows.Count; i < ic; i++)
                    {
                        var row = ds.Tables[0].Rows[i];
                        if (row.ItemArray.Length >= 1)
                        {
                            AddRuleTextAndButton(row[0].ToString(), row[1].ToString());
                        }
                    }
                }      
            }
            else
            {
                MessageBox.Show("没有规则,请添加");
            }
          
        }

        private void UcRule_BtnUpdateEvent(object sender, EventArgs e)
        {
          uc_Rule ucRule = (uc_Rule)sender;
          Tuple<string, string> temp = ucRule.GetValue();
          XmlHelper.DeleteFromXml(xmlpath, "rules", "formattype", temp.Item1);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            InsertFrom insertFrom = new InsertFrom();
            insertFrom.Show();
        }
    }
}
