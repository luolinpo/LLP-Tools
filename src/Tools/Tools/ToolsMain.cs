using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.ZYT;

namespace Tools
{
    public partial class ToolsMain : Form
    {
        public ToolsMain()
        {
            InitializeComponent();
        }

        private void duff容器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PerformTest performTest = new PerformTest();
            performTest.ShowDialog();
            this.Show();
        }

        private void 坐标转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map();
            map.ShowDialog();
            this.Show();
        }

        private void 汽车之家车型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Crawler crawler = new Crawler();
            crawler.ShowDialog();
            this.Show();
        }

        private void 高德地图地图站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AMapSubwayCrawler aMapSubwayCrawler = new AMapSubwayCrawler();
            aMapSubwayCrawler.ShowDialog();
            this.Show();
        }
        private void api测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ApiTest aMapSubwayCrawler = new ApiTest();
            aMapSubwayCrawler.ShowDialog();
            this.Show();
        }

        private void zyttoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ZytTools aMapSubwayCrawler = new ZytTools();
            aMapSubwayCrawler.ShowDialog();
            this.Show();
            
        }

        private void 接口子路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            IntefaceUrlCreate aMapSubwayCrawler = new IntefaceUrlCreate();
            aMapSubwayCrawler.ShowDialog();
            this.Show();
            
        }

        private void 省市行政区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChinaCityLatLng chinaCityLatLng = new ChinaCityLatLng();
            chinaCityLatLng.ShowDialog();
            this.Show();
             
        }
        private void 点是否在省内ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            PointInProvince remoteDesktop = new PointInProvince();
            remoteDesktop.ShowDialog();
            this.Show();
            
        }
    }
}
