using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Common
{
    public class SignHelper
    {
       /// <summary>
       /// 创建签名
       /// </summary>
       /// <param name="dicArray">待签名字典</param>
       /// <param name="key">安全码（盐值）</param>
       /// <param name="charset">编码方式</param>
       /// <returns></returns>
        public static string BuildMD5sign(SortedDictionary<string, string> dicArray, string key, string charset)
        {
            Dictionary<string, string> dicPara = FilterPara(dicArray);

            return Sign(dicPara, key, "MD5", charset);
        }
        /// <summary>
        /// 除去数组中的空值并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        private static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Value !=string.Empty &&temp.Value != null)
                {
                    dicArray.Add(temp.Key.ToLower(), temp.Value);
                }
            }
            return dicArray;
        }
        /// <summary>
        /// 生成签名结果
        /// </summary>
        /// <param name="dicArray">要签名的数组</param>
        /// <param name="key">安全校验码</param>
        /// <param name="signtype">签名类型</param>
        /// <param name="charset">编码格式</param>
        /// <returns>签名结果字符串</returns>
        public static string Sign(IDictionary<string, string> dicArray, string key = "", string signtype = "MD5",
            string charset = "utf-8", string separator = "&")
        {
            string prestr = CreateLinkString(dicArray, separator); //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串

            prestr = prestr + key; //把拼接后的字符串再与安全校验码直接连接起来
            string mysign = Sign(prestr, signtype, charset); //把最终的字符串签名，获得签名结果

            return mysign;
        }
        /// <summary>
        /// 签名字符串
        /// </summary>
        /// <param name="prestr">需要签名的字符串</param>
        /// <param name="signtype">签名类型</param>
        /// <param name="charset">编码格式</param>
        /// <returns>签名结果</returns>
        public static string Sign(string prestr, string signtype = "MD5", string charset = "utf-8")
        {
            StringBuilder sb = new StringBuilder(32);
            if (signtype.ToUpper() == "MD5")
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(prestr));
                for (int i = 0; i < t.Length; i++)
                {
                    sb.Append(t[i].ToString("x").PadLeft(2, '0'));
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
        /// </summary>
        /// <param name="dicArray">需要拼接的数组</param>
        /// <param name="separator"></param>
        /// <returns>拼接完成以后的字符串</returns>
        private static string CreateLinkString(IDictionary<string, string> dicArray, string separator = "&")
        {

            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + separator);
            }
            //去掉最後一個&字符
            int nLen = prestr.Length;
            int separatorLen = separator.Length;
            if (prestr.Length >= separatorLen)
            {
                prestr.Remove(nLen - separatorLen, separatorLen);
            }
            return prestr.ToString();
        }

    }
}