using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public sealed class MapHelper
    {

        /// <summary>
        /// 获取地图上2个点的距离
        /// </summary>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        private static double Distance(double lon1, double lat1, double lon2, double lat2)
        {
            double R = 6378137; //地球半径
            lat1 = lat1 * Math.PI / 180.0;
            lat2 = lat2 * Math.PI / 180.0;
            double sa2 = Math.Sin((lat1 - lat2) / 2.0);
            double sb2 = Math.Sin(((lon1 - lon2) * Math.PI / 180.0) / 2.0);
            return 2 * R * Math.Asin(Math.Sqrt(sa2 * sa2 + Math.Cos(lat1) * Math.Cos(lat2) * sb2 * sb2));
        }
        #region 点和圆
        /// <summary>
        /// 点是否在圆内(《=在边上也认为在圆内) 
        /// </summary>
        /// <param name="cPoint">圆心坐标</param>
        /// <param name="cRadius">圆半径</param>
        /// <param name="point">当前点</param>
        /// <returns></returns>
        public static bool PointInCircle(Vector2D cPoint, double cRadius, Vector2D point)
        {
            var d1 = Distance(cPoint.Lat, cPoint.Lon, point.Lat, point.Lon);
            //2维平面
            //double distance = Math.Sqrt(Math.Pow(Math.Abs(point.X - cPoint.X), 2) + Math.Pow(Math.Abs(point.Y - cPoint.Y), 2));
            return d1 <= cRadius;
        }
        /// <summary>
        /// 点是否在圆内(在边上也认为在圆内)
        /// </summary>
        /// <param name="cPoint">圆心坐标</param>
        /// <param name="onPoint">圆边上坐标</param>
        /// <param name="point">当前点</param>
        /// <returns></returns>
        public static bool PointInCircle(Vector2D cPoint, Vector2D onPoint, Vector2D point)
        {
            double distance = Distance(cPoint.Lat, cPoint.Lon, point.Lat, point.Lon);
            double cRadius = Distance(cPoint.Lat, cPoint.Lon, onPoint.Lat, onPoint.Lon);
            return distance <= cRadius;
        }
        #endregion

        /// <summary>
        /// 点是否在多边形内(在边上也认为在多边形内)
        /// </summary>
        /// <param name="point">当前点</param>
        /// <param name="polygon">多边形点</param>
        /// <returns></returns>
        public static bool PointInPolygon(Vector2D point, List<Vector2D> polygon)
        {
            var res = MapHelper.CalcPointPolygonRelation(point, polygon);
            return res != PointPolygonRelation.OutOfPolygon;
        }
        /// <summary>
        /// 点是否在矩形内
        /// </summary>
        /// <param name="pointA">对角上点A</param>
        /// <param name="pointB">对角上点B</param>
        /// <param name="point">当前点</param>
        /// <returns></returns>
        public static bool PonitInRectangle(Vector2D pointA, Vector2D pointB, Vector2D point)
        {
            List<Vector2D> polygon = new List<Vector2D>();
            polygon.Add(pointA);
            polygon.Add(new Vector2D(pointA.X, pointB.Y));
            polygon.Add(pointB);
            polygon.Add(new Vector2D(pointB.X, pointA.Y));
            var res = MapHelper.CalcPointPolygonRelation(point, polygon);
            return res != PointPolygonRelation.OutOfPolygon;
        }
        public static PointPolygonRelation CalcPointPolygonRelation(double x, double y, IList polygon)
        {
            if (polygon.Count < 3) return PointPolygonRelation.OutOfPolygon;
            IVector start = (IVector)polygon[polygon.Count - 2];
            if (start == null) return PointPolygonRelation.OutOfPolygon;
            IVector middle = (IVector)polygon[polygon.Count - 1];
            IVector end;
            PointPolygonRelation result = PointPolygonRelation.OutOfPolygon;
            int num = 0;
            double minX, minY, maxX, maxY;
            for (int i = 0; i < polygon.Count; i++, start = middle, middle = end)
            {
                end = (IVector)polygon[i];
                //如果点刚好在顶点上，则直接返回
                if ((x == start.X && y == start.Y) || (x == middle.X && y == middle.Y) || (x == end.X && y == end.Y))
                {
                    result = PointPolygonRelation.OnPolygonVertex;
                    goto Return;
                }
                if (middle.X == end.X && middle.Y == end.Y)
                {
                    continue;
                }
                minX = Math.Min(middle.X, end.X);
                maxX = Math.Max(middle.X, end.X);
                //如果X不落在最大X值和最小X值之间，则跳过
                if (x > maxX || x < minX) continue;

                minY = Math.Min(middle.Y, end.Y);
                maxY = Math.Max(middle.Y, end.Y);
                //如果Y大于最大Y值则跳过，因为向上做射线不可能相交
                if (y > maxY) continue;
                if (minX == maxX)//需要判断相交的线段为Y轴平行线
                {
                    if (y > minY && y < maxY)
                    {
                        result = PointPolygonRelation.OnPolygonBorder;
                        goto Return;
                    }
                    else if (y < minY)//如果在最小的Y之下，因为目标线段为Y轴平行线，所以记为1次
                    {
                        num++;
                    }
                }
                else
                {
                    double k = (middle.Y - end.Y) / (middle.X - end.X);
                    double b = middle.Y - k * middle.X;

                    double crossY = k * x + b;

                    if (crossY < y) continue;//如果交点小于y则跳过，因为我们是向上做射线

                    if (crossY == y)
                    {
                        result = PointPolygonRelation.OnPolygonBorder;
                        goto Return;
                    }

                    if (x == maxX || x == minX)
                    {
                        if ((start.X > middle.X && end.X < middle.X) || (start.X < middle.X && end.X > middle.X))
                            num++;
                    }
                    else
                    {
                        num++;
                    }
                }
            }

            if (num % 2 != 0) result = PointPolygonRelation.InPolygon;
            Return:

            return result;
        }
        /// <summary>
        /// 计算点和面的关系
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns></returns>
        public static PointPolygonRelation CalcPointPolygonRelation(IVector point, IList polygon)
        {
            return CalcPointPolygonRelation(point.X, point.Y, polygon);
        }
        #region 枚举和对象
        public enum PointPolygonRelation
        {
            /// <summary>
            /// 点在面内
            /// </summary>
            InPolygon,
            /// <summary>
            /// 点在边上
            /// </summary>
            OnPolygonBorder,
            /// <summary>
            /// 点在面外
            /// </summary>
            OutOfPolygon,
            /// <summary>
            /// 点在面的拐点上
            /// </summary>
            OnPolygonVertex
        }

        public class Vector2D : IVector
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector2D()
            {

            }

            public Vector2D(double x, double y)
            {
                X = x;
                Y = y;
            }

            public PointF ToPointF()
            {
                return new PointF() { X = (float)this.X, Y = (float)this.Y };
            }

            public bool Equals2D(Vector2D vec)
            {
                return X == vec.X && Y == vec.Y;
            }

            public Vector2D Clone()
            {
                return new Vector2D(X, Y);
            }

            public static Vector2D operator -(Vector2D v1, Vector2D v2)
            {
                return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
            }

            public static Vector2D operator +(Vector2D v1, Vector2D v2)
            {
                return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
            }

            public override string ToString()
            {
                return string.Format("{0},{1}", X, Y);
            }

            public void Offset(double xOffset, double yOffset)
            {
                X += xOffset;
                Y += yOffset;
            }

            public void Normalize()
            {
                var len = Math.Sqrt(X * X + Y * Y);
                X /= len;
                Y /= len;
            }

            #region 属性

            /// <summary>
            /// 无效对象
            /// </summary>
            public static Vector2D Empty
            {
                get
                {
                    return new Vector2D(double.NaN, double.NaN);
                }
            }

            /// <summary>
            /// 返回改点是否为一个无效点
            /// </summary>
            public bool IsEmpty
            {
                get
                {
                    return double.IsNaN(X) || double.IsNaN(Y);
                }
            }

            //[NonSerialized]
            /// <summary>
            /// 经度，即 X 坐标
            /// </summary>
            public double Lon
            {
                get
                {
                    return this.X;
                }
                set
                {
                    this.X = value;
                }
            }

            //[NonSerialized]
            /// <summary>
            /// 纬度，即 Y 坐标
            /// </summary>
            public double Lat
            {
                get
                {
                    return this.Y;
                }
                set
                {
                    this.Y = value;
                }
            }
            #endregion
        }

        /// <summary>
        /// 表示一个二维坐标结构,是所有点类型的基本接口
        /// </summary>
        public interface IVector
        {
            double X
            {
                get;
                set;
            }
            double Y
            {
                get;
                set;
            }

            bool IsEmpty
            {
                get;
            }
        } 
        #endregion
    }
}
