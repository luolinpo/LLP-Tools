using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpRequestUtil
    {
        /// <summary>
        /// 获取页面html
        /// </summary>
        public static string GetPageHtml(string url)
        {
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                return content;
            }
            catch (Exception)
            {
                return string.Empty;
            }
           
        }
        public static string GetPageHtmlGB2312(string url)
        {
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding(936));
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                return content;
            }
            catch (Exception)
            {

                return string.Empty;
            }
           
        }
        /// <summary>
        /// Http下载文件 保存
        /// </summary>
        public static void HttpDownloadFile(string url)
        {
            try
            {
                int pos = url.LastIndexOf("/") + 1;
                string fileName = url.Substring(pos);
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\download";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePathName = path + "\\" + fileName;
                if (File.Exists(filePathName)) return;

                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";
                request.Proxy = null;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();

                //创建本地文件写入流
                Stream stream = new FileStream(filePathName, FileMode.Create);

                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
            
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
