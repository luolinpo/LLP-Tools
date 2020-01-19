using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
   public class JsonHelper
    {
        /// <summary>
        /// Json帮助类 方法一 Newtonsoft.Json
        /// </summary>
        #region 
        //public class JsonHelper
        //{
        //    /// <summary>
        //    /// 将对象序列化为JSON格式
        //    /// </summary>
        //    /// <param name="o">对象</param>
        //    /// <returns>json字符串</returns>
        //    public static string SerializeObject(object o)
        //    {
        //        string json = JsonConvert.SerializeObject(o);
        //        return json;
        //    }

        //    /// <summary>
        //    /// 解析JSON字符串生成对象实体
        //    /// </summary>
        //    /// <typeparam name="T">对象类型</typeparam>
        //    /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        //    /// <returns>对象实体</returns>
        //    public static T DeserializeJsonToObject<T>(string json) where T : class
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        StringReader sr = new StringReader(json);
        //        object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
        //        T t = o as T;
        //        return t;
        //    }

        //    /// <summary>
        //    /// 解析JSON数组生成对象实体集合
        //    /// </summary>
        //    /// <typeparam name="T">对象类型</typeparam>
        //    /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        //    /// <returns>对象实体集合</returns>
        //    public static List<T> DeserializeJsonToList<T>(string json) where T : class
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        StringReader sr = new StringReader(json);
        //        object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
        //        List<T> list = o as List<T>;
        //        return list;
        //    }

        //    /// <summary>
        //    /// 反序列化JSON到给定的匿名对象.
        //    /// </summary>
        //    /// <typeparam name="T">匿名对象类型</typeparam>
        //    /// <param name="json">json字符串</param>
        //    /// <param name="anonymousTypeObject">匿名对象</param>
        //    /// <returns>匿名对象</returns>
        //    public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        //    {
        //        T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        //        return t;
        //    }

        //} 
        #endregion
        /// <summary>
        /// 方法二 c#自带System.Runtime.Serialization.Json
        /// </summary>
        public class JSON
        {
            public static T parse<T>(string jsonString)
            {
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
                }
            }
            public static string stringify(object jsonObject)
            {
                using (var ms = new MemoryStream())
                {
                    new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}

