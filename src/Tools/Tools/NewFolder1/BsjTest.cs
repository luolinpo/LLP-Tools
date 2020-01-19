using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tools
{
    public static class common
    {
        #region 方法

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalLati_Longi">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        static public string ConvertDigitalToDegrees(string digitalLati_Longi)
        {
            double digitalDegree = Convert.ToDouble(digitalLati_Longi);
            return ConvertDigitalToDegrees(digitalDegree);
        }

        /// <summary>
        /// 数字经纬度和度分秒经纬度转换 (Digital degree of latitude and longitude and vehicle to latitude and longitude conversion)
        /// </summary>
        /// <param name="digitalDegree">数字经纬度</param>
        /// <return>度分秒经纬度</return>
        static public string ConvertDigitalToDegrees(double digitalDegree)
        {
            const double num = 60;
            int degree = (int)digitalDegree;
            double tmp = (digitalDegree - degree) * num;
            int minute = (int)tmp;
            double second = (tmp - minute) * num;
            string degrees = "" + degree + "°" + minute + "′" + second + "″";
            return degrees;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'°')和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital(string degrees)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf('°');           //度的符号对应的 Unicode 代码为：00B0[1]（六十进制），显示为°。
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf('′');           //分的符号对应的 Unicode 代码为：2032[1]（六十进制），显示为′。
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.IndexOf('″');           //秒的符号对应的 Unicode 代码为：2033[1]（六十进制），显示为″。
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }


        /// <summary>
        /// 度分秒经纬度(必须含有'/')和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital_default(string degrees)
        {
            char ch = '/';
            return ConvertDegreesToDigital(degrees, ch);
        }

        /// <summary>
        /// 度分秒经纬度和数字经纬度转换
        /// </summary>
        /// <param name="digitalDegree">度分秒经纬度</param>
        /// <param name="cflag">分隔符</param>
        /// <return>数字经纬度</return>
        static public double ConvertDegreesToDigital(string degrees, char cflag)
        {
            const double num = 60;
            double digitalDegree = 0.0;
            int d = degrees.IndexOf(cflag);
            if (d < 0)
            {
                return digitalDegree;
            }
            string degree = degrees.Substring(0, d);
            digitalDegree += Convert.ToDouble(degree);

            int m = degrees.IndexOf(cflag, d + 1);
            if (m < 0)
            {
                return digitalDegree;
            }
            string minute = degrees.Substring(d + 1, m - d - 1);
            digitalDegree += ((Convert.ToDouble(minute)) / num);

            int s = degrees.Length;
            if (s < 0)
            {
                return digitalDegree;
            }
            string second = degrees.Substring(m + 1, s - m - 1);
            digitalDegree += (Convert.ToDouble(second) / (num * num));

            return digitalDegree;
        }

        #endregion
        /// <summary>
        /// 将16进制字符串转换为byte数组，有分隔符（如 A3 78 5E 3F）
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByte(string hexString)
        {
            string[] strArray = hexString.Split(' ');
            byte[] byteArray = new byte[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                byteArray[i] = byte.Parse(strArray[i], System.Globalization.NumberStyles.HexNumber);
            }
            return byteArray;
        }
        private static Byte[] ConvertFrom(string strTemp)
        {
            try
            {
                if (Convert.ToBoolean(strTemp.Length & 1))//数字的二进制码最后1位是1则为奇数  
                {
                    strTemp = "0" + strTemp;//数位为奇数时前面补0  
                }
                Byte[] aryTemp = new Byte[strTemp.Length / 2];
                for (int i = 0; i < (strTemp.Length / 2); i++)
                {
                    aryTemp[i] = (Byte)(((strTemp[i * 2] - '0') << 4) | (strTemp[i * 2 + 1] - '0'));
                }
                return aryTemp;//高位在前  
            }
            catch
            { return null; }
        }
        /// <summary>  
        /// BCD码转换16进制(压缩BCD)  
        /// </summary>  
        /// <param name="strTemp"></param>  
        /// <returns></returns>  
        public static Byte[] ConvertFrom(string strTemp, int IntLen)
        {
            try
            {
                Byte[] Temp = common.ConvertFrom(strTemp.Trim());
                Byte[] return_Byte = new Byte[IntLen];
                if (IntLen != 0)
                {
                    if (Temp.Length < IntLen)
                    {
                        for (int i = 0; i < IntLen - Temp.Length; i++)
                        {
                            return_Byte[i] = 0x00;
                        }
                    }
                    Array.Copy(Temp, 0, return_Byte, IntLen - Temp.Length, Temp.Length);
                    return return_Byte;
                }
                else
                {
                    return Temp;
                }
            }
            catch
            { return null; }
        }
        /// <summary>  
        /// 16进制转换BCD（解压BCD）  
        /// </summary>  
        /// <param name="AData"></param>  
        /// <returns></returns>  
        public static string ConvertTo(Byte[] AData)
        {
            try
            {
                StringBuilder sb = new StringBuilder(AData.Length * 2);
                foreach (Byte b in AData)
                {
                    sb.Append(b >> 4);
                    sb.Append(b & 0x0f);
                }
                return sb.ToString();
            }
            catch { return null; }
        }
        ///// <summary>
        ///// 16进制数组转无间隔10进制字符串
        ///// </summary>
        ///// <param name="AData"></param>
        ///// <returns></returns>
        //public static string HexByteConvertDecString(Byte[] AData)
        //{
        //    StringBuilder retSb = new StringBuilder();
        //    foreach (var item in AData)
        //    {
        //        retSb.Append();
        //    }
        //}
    }
    public partial class BsjTest : Form
    {
        public BsjTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           byte[] testByte= common.HexStringToByte("29 29 80 00 28 0C 3A 3B 2A 08 06 05 14 26 34 02 23 25 56 11 40 52 81 01 20 01 54 F8 00 4E 20 7F FC 5F 00 00 1E 00 00 00 00 00 00 70 0D");
           if (testByte[0]==0x29&& testByte[1]==0x29)
            {
                if (testByte[2]==0x80)
                {
                    if (testByte[3]==0x00)
                    {
                        string temp ="";
                        byte[] posByte = new byte[34];
                        Array.Copy(testByte,9,posByte,0,34);
                        byte[] gpsTimeByte = new byte[6];
                        Array.Copy(posByte, 0, gpsTimeByte, 0, 6);
                        string a = "20"+common.ConvertTo(gpsTimeByte);
                        DateTime gpsTime2 = DateTime.ParseExact("20141010", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        DateTime gpsTime =DateTime.ParseExact("20080605112521", "yyyyMMddHHmmss", CultureInfo.InvariantCulture,DateTimeStyles.AssumeLocal);
                        byte[] latByte = new byte[4];
                        Array.Copy(posByte, 6, latByte, 0, 4);
                        string lat= common.ConvertTo(latByte);
                        double latvalue = 0;
                        double latDouble = double.Parse(lat) / 100000;
                        double latDoubleFloor = Math.Floor(latDouble);
                        double latDoubleDecimal = ((latDouble - latDoubleFloor) * 10000) / 6000;
                        double latDoubleLast = latDoubleFloor + latDoubleDecimal;
                        if ((((int)lat[0] >> 3) & 1)<0)
                        {
                            latvalue = latDoubleLast*(-1);
                        }
                        Array.Copy(posByte, 10, latByte, 0, 4);
                        string lng = common.ConvertTo(latByte);
                        double lngvalue = 0;
                        double lngDouble = double.Parse(lng) / 100000;
                        double lngDoubleFloor = Math.Floor(lngDouble);
                        double lngDoubleDecimal = ((lngDouble - lngDoubleFloor) * 10000) / 6000;
                        double lngDoubleLast = lngDoubleFloor + lngDoubleDecimal;
                        if ((((int)lng[0] >> 3) & 1) < 0)
                        {
                            lngvalue = lngDoubleLast * (-1);
                        }

                        byte[] veoByte = new byte[2];
                        Array.Copy(posByte, 14, veoByte, 0,2);
                        temp= common.ConvertTo(veoByte);
                        double veo = double.Parse(temp);

                        byte[] dirByte = new byte[2];
                        Array.Copy(posByte, 16, dirByte, 0, 2);
                        temp = common.ConvertTo(dirByte);
                        double dir = double.Parse(temp);

                        byte location = posByte[18];
                        var T1 = ((location >> 7) & 1) == 1 ? true : false;
                        var T2 = ((location&64) >> 6 == 1 )? true : false;
                        var T3 = ((location & 32) >> 5 == 1) ? true : false;
                        var T4 = ((location & 16) >> 4 == 1) ? true : false;
                        var T5 = ((location & 8) >> 3 == 1) ? true : false;

                        byte[] mileageByte = new byte[3];
                        Array.Copy(posByte, 19, mileageByte, 0, 3);
                        if (mileageByte.Length>3)
                        {
                            double mileage = ((mileageByte[0] << 16) + (mileageByte[1] << 8) + mileageByte[2]) / 1000f;
                        }

                    }
                }
            }
        }
    }
}
