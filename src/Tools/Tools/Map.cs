using Common;
using Model;
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
    public partial class Map : Form
    {
        public Map()
        {
            InitializeComponent();
        }

        private void btn_GCJ02ToBD09_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(this.tbx_lat.Text.Trim());
                double lng = double.Parse(this.tbx_lng.Text.Trim());
                Gps gpsData = MapConvertHelper.Gcj02_To_Bd09(lat, lng);
                this.tbx_return.Text = gpsData.getWgLat().ToString() + "," + gpsData.getWgLon().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void btn_Wgs84ToGcj02_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(this.tbx_lat.Text.Trim());
                double lng = double.Parse(this.tbx_lng.Text.Trim());
                Gps gpsData = MapConvertHelper.Gps84_To_Gcj02(lat, lng);
                this.tbx_return.Text = gpsData.getWgLat().ToString() + "," + gpsData.getWgLon().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btn_WgstoBD09_Click(object sender, EventArgs e)
        {
            try
            {
                double lat = double.Parse(this.tbx_lat.Text.Trim());
                double lng = double.Parse(this.tbx_lng.Text.Trim());
                Gps gpsData = MapConvertHelper.Gps84_To_BD09(lat, lng);
                this.tbx_return.Text = gpsData.getWgLat().ToString() + "," + gpsData.getWgLon().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
