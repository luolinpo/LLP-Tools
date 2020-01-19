using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonHelp;

namespace FileAssistant
{
    public partial class uc_Rule : UserControl
    {
        public uc_Rule()
        {
            InitializeComponent();
        }

        //定义委托
        public delegate void BtnUpdateHandle(object sender, EventArgs e);
        //定义事件
        public event BtnUpdateHandle BtnUpdateEvent;

        public delegate void BtnDeleteHandle(object sender, EventArgs e);
        public event BtnDeleteHandle BtnDeleteEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.button1.Text == "修改")
            {
                this.textBox1.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
                this.textBox2.BackColor = Color.FromKnownColor(KnownColor.ActiveCaption);
                this.button1.Text = "保存";
                SetReadOnly(false);
            }
            else if (this.button1.Text == "保存")
            {
                string xmlpath = System.AppDomain.CurrentDomain.BaseDirectory + "fsdata.xml";
                //XmlHelper.UpdateIntoXml(xmlpath,"rules", "formattype", "formatname",);
                this.textBox1.BackColor = Color.FromKnownColor(KnownColor.Control);
                this.textBox2.BackColor = Color.FromKnownColor(KnownColor.Control);
                this.button1.Text = "修改";
                SetReadOnly(true);
            }
           
            BtnUpdateEvent?.Invoke(this,e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            BtnDeleteEvent?.Invoke(this,e);
        }

        public void SetValue(string filetype,string filename)
        {
            this.textBox1.Text = filetype;
            this.textBox2.Text = filename;
        }

        public void SetReadOnly(bool bo)
        {
            this.textBox1.ReadOnly = bo;
            this.textBox2.ReadOnly = bo;
        }
        public Tuple<string,string> GetValue()
        {
            return new Tuple<string, string>(this.textBox1.Text.ToString(), this.textBox2.Text.ToString());
        }
    }

}
