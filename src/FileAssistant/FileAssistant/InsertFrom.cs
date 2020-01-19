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
    public partial class InsertFrom : Form
    {
        public InsertFrom()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "fsdata.xml";
            DataSet ds = XmlHelper.ConvertXMLFileToDataSet(xmlpath);
            if (ds?.Tables.Count > 0)
            {
                if (ds?.Tables[0]?.Rows != null)
                {
                    for (int i = 0, ic = ds.Tables[0].Rows.Count; i < ic; i++)
                    {
                        var row = ds.Tables[0].Rows[i];
                        if (row.ItemArray.Length >= 1)
                        {
                            if (row[0].ToString() == this.textBox1.Text.Trim())
                            {  
                                MessageBox.Show("已经存在这个格式了");
                                return;
                            }
                 
                        }
                    }
                }
            }
            XElement xele = new XElement(
              new XElement("rules",
              new XAttribute("formattype", this.textBox2.Text.Trim()),
              new XAttribute("formatname", this.textBox1.Text.Trim())));
            XmlHelper.InsertIntoXml(xmlpath, xele);
            this.Close();
        }

        private void InsertFrom_FormClosed(object sender, FormClosedEventArgs e)
        {
            Classification c = new Classification();
            c.Show();
        }
    }
}
