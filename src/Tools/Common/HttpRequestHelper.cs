using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Common
{   
    /// <summary>
    /// http请求
    /// </summary>
    public class HttpRequestHelper
    {
        #region Post请求
        /// <summary>
        /// Post请求 utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="dic">参数字典</param>
        /// <returns></returns>
        public static string HttpPost(string url, Dictionary<string, string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POst";
            req.ContentType = "application/x-www-form-urlencoded";
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }

            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// Post请求 utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="headersdic">head头字典</param>
        /// <param name="jsonstring">参数字符串</param>
        /// <returns></returns>
        public static string HttpPost(string url, SortedDictionary<string, string> headersdic, string jsonstring)
        {
            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Post";
                foreach (var item in headersdic)
                {
                    req.Headers.Add(item.Key, item.Value);
                }
                #region 添加Post 参数

                byte[] data = Encoding.UTF8.GetBytes(jsonstring);
                req.ContentLength = data.Length;
                req.ContentType = "application/json"; //用dynamic 接收
                                                      //req.ContentType = "application/x-www-form-urlencoded";
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }

        }
        /// <summary>
        /// Post请求 utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="headersdic">head头字典</param>
        /// <param name="paramsdic">参数字符串</param>
        /// <returns></returns>
        public static string HttpPost(string url, SortedDictionary<string, string> headersdic, SortedDictionary<string, string> paramsdic)
        {
            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "Post";
                foreach (var item in headersdic)
                {
                    req.Headers.Add(item.Key, item.Value);
                }
                //req.ContentType = "application/x-www-form-urlencoded";
                req.ContentType = "application/json";
                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                if (paramsdic.Count >= 0)
                {
                    foreach (var item in paramsdic)
                    {
                        if (i > 0)
                            builder.Append("&");
                        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                        i++;
                    }

                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception)
            {

                return null;
            }

        }
        #endregion
        #region Get请求
        /// <summary>
        /// Get请求  utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="dic">参数字典</param>
        /// <returns></returns>
        public static string HttpGet(string url, Dictionary<string, string> dic)
        {
            #region 添加Get 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            string postDataStr = builder.ToString();
            #endregion
            url = url + (postDataStr == "" ? "" : "?") + postDataStr;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        /// <summary>
        /// Get请求  utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="dic">参数字典</param>
        /// <returns></returns>
        public static string HttpGet(string url, SortedDictionary<string, string> dic)
        {
            try
            {
                #region 添加Get 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                string postDataStr = builder.ToString();
                #endregion
                url = url + (postDataStr == "" ? "" : "?") + postDataStr;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception)
            {

                return "";
            }

        }
        /// <summary>
        /// Get请求  utf-8
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        } 
        #endregion
    }
}