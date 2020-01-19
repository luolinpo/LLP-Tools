using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class MapConvertHelper
    {
        public static readonly double pi = 3.1415926535897932384626;
        public static readonly double a = 6378245.0;
        public static readonly double ee = 0.00669342162296594323;
        /// <summary>
        ///  Wgs84 to 火星坐标系(GCJ-02) 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static Gps Gps84_To_Gcj02(double lat, double lon)
        {
            if (OutOfChina(lat, lon))
            {
                return null;
            }
            double dLat = TransformLat(lon - 105.0, lat - 35.0);
            double dLon = TransformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = lat + dLat;
            double mgLon = lon + dLon;
            return new Gps(mgLat, mgLon);
        }
        /// <summary>
        /// 火星坐标系 (GCJ-02) to Wgs84 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        public static Gps Gcj_To_Gps84(double lat, double lon)
        {
            Gps gps = Transform(lat, lon);
            double lontitude = lon * 2 - gps.getWgLon();
            double latitude = lat * 2 - gps.getWgLat();
            return new Gps(latitude, lontitude);
        }

        /// <summary>
        /// 火星坐标系(GCJ-02) 坐标转换成 BD-09 坐标 
        /// </summary>
        /// <param name="gg_lat"></param>
        /// <param name="gg_lon"></param>
        /// <returns></returns>
        public static Gps Gcj02_To_Bd09(double gg_lat, double gg_lon)
        {
            double x = gg_lon, y = gg_lat;
            double z = Math.Sqrt(x * x + y * y) + 0.00002 * Math.Sin(y * pi);
            double theta = Math.Atan2(y, x) + 0.000003 * Math.Cos(x * pi);
            double bd_lon = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new Gps(bd_lat, bd_lon);
        }

        /// <summary>
        ///  BD-09 坐标转换成火星坐标系(GCJ-02) 坐标
        /// </summary>
        /// <param name="bd_lat"></param>
        /// <param name="bd_lon"></param>
        /// <returns></returns>
        public static Gps BD09_To_Gcj02(double bd_lat, double bd_lon)
        {
            double x = bd_lon - 0.0065, y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * pi);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * pi);
            double gg_lon = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new Gps(gg_lat, gg_lon);
        }
        /// <summary>
        /// BD-09 坐标转换成 Wgs84 坐标
        /// </summary>
        /// <param name="bd_lat"></param>
        /// <param name="bd_lon"></param>
        /// <returns></returns>
        public static Gps BD09_To_Gps84(double bd_lat, double bd_lon)
        {

            Gps gcj02 = BD09_To_Gcj02(bd_lat, bd_lon);
            Gps map84 = Gcj_To_Gps84(gcj02.getWgLat(),
                    gcj02.getWgLon());
            return map84;

        }
        /// <summary>
        /// BD-09 坐标转换成 Wgs84 坐标
        /// </summary>
        /// <param name="bd_lat"></param>
        /// <param name="bd_lon"></param>
        /// <returns></returns>
        public static Gps Gps84_To_BD09(double bd_lat, double bd_lon)
        {

            Gps gcj02 = Gps84_To_Gcj02(bd_lat, bd_lon);
            Gps map84 = Gcj02_To_Bd09(gcj02.getWgLat(),
                    gcj02.getWgLon());
            return map84;

        }
        public static bool OutOfChina(double lat, double lon)
        {
            if (lon < 72.004 || lon > 137.8347)
                return true;
            if (lat < 0.8293 || lat > 55.8271)
                return true;
            return false;
        }
        public static Gps Transform(double lat, double lon)
        {
            if (OutOfChina(lat, lon))
            {
                return new Gps(lat, lon);
            }
            double dLat = TransformLat(lon - 105.0, lat - 35.0);
            double dLon = TransformLon(lon - 105.0, lat - 35.0);
            double radLat = lat / 180.0 * pi;
            double magic = Math.Sin(radLat);
            magic = 1 - ee * magic * magic;
            double sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((a * (1 - ee)) / (magic * sqrtMagic) * pi);
            dLon = (dLon * 180.0) / (a / sqrtMagic * Math.Cos(radLat) * pi);
            double mgLat = lat + dLat;
            double mgLon = lon + dLon;
            return new Gps(mgLat, mgLon);
        }
        public static double TransformLat(double x, double y)
        {
            double ret = -100.0 + 2.0 * x + 3.0 * y + 0.2 * y * y + 0.1 * x * y
                    + 0.2 * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(y * pi) + 40.0 * Math.Sin(y / 3.0 * pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(y / 12.0 * pi) + 320 * Math.Sin(y * pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }
        public static double TransformLon(double x, double y)
        {
            double ret = 300.0 + x + 2.0 * y + 0.1 * x * x + 0.1 * x * y + 0.1
                    * Math.Sqrt(Math.Abs(x));
            ret += (20.0 * Math.Sin(6.0 * x * pi) + 20.0 * Math.Sin(2.0 * x * pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(x * pi) + 40.0 * Math.Sin(x / 3.0 * pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(x / 12.0 * pi) + 300.0 * Math.Sin(x / 30.0
                    * pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
