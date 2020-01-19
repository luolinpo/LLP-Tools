using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Common
{
    public class SerializableXml
    {
        public static string XMLSerialize_V1<T>(T entity)
        {
            StringBuilder buffer = new StringBuilder();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            //using (TextWriter writer = new StringWriter(buffer))
            //{
            //    serializer.Serialize(writer, entity,ns);
            //}
            using (XmlWriter writer = XmlWriter.Create(buffer, settings))
            {
                serializer.Serialize(writer, entity, ns);
            }
            return buffer.ToString();

        }

        #region 将对象序列化成一个不带XML文档申明的XML字符串
        /// <summary>
        /// 将对象序列化成一个不带XML文档申明的XML字符串
        /// </summary>
        /// <param name="o">实体对象</param>
        /// <param name="encoding">字符编码</param>
        public static string XmlSerializeInternal(object o, Encoding encoding)
        {
            MemoryStream ms = new MemoryStream();
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            XmlSerializer serializer = new XmlSerializer(o.GetType());
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = "    ";
            // 不生成声明头
            settings.OmitXmlDeclaration = true;
            // 强制指定命名空间，覆盖默认的命名空间。
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (XmlWriter writer = XmlWriter.Create(ms, settings))
            {
                serializer.Serialize(writer, o, namespaces);
                writer.Close();
            }
            return Encoding.UTF8.GetString(ms.ToArray()); ;
        }
        #endregion

        #region XML字符串反序列化为指定类型对象
        /// <summary>
        /// XML字符串反序列化为指定类型对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <param name="outMsg"></param>
        /// <returns></returns>
        public static Object Deserialize(Type type, string xml, out string outMsg)
        {
            Object Obj = null;
            outMsg = string.Empty;
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xs = new XmlSerializer(type);
                    Obj = xs.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                outMsg = string.Format("XML反序列化异常：[Msg]-[{0}]、[信息]-[{1}]。", ex.Message, ex.ToString());
                Obj = null;
            }
            return Obj;
        }
        /// <summary>
        /// XML字符串反序列化为指定类型对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <param name="outMsg"></param>
        /// <returns></returns>
        public static Object DeserializeByStream(Type type, string FilePath, out string outMsg)
        {
            Object Obj = null;
            outMsg = string.Empty;
            FileStream fs = null;
            XmlSerializer xs = null;
            try
            {
                using (fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    xs = new XmlSerializer(type);
                    Obj = xs.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                outMsg = string.Format("XML反序列化异常：[Msg]-[{0}]、[信息]-[{1}]。", ex.Message, ex.ToString());
                Obj = null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
            }
            return Obj;
        }
        #endregion

        #region 
        /// <summary>
        /// 根据指定文件地址、以及指定编码方式序列化文件为实体对象
        /// </summary>
        /// <param name="type">实体对象类型</param>
        /// <param name="FilePath">文件读取地址</param>
        /// <param name="encod">编码类型</param>
        /// <param name="outMsg"></param>
        /// <returns></returns>
        public static Object DeserializeEntityByFilePaht(Type type, string FilePath, Encoding encod, out string outMsg)
        {
            Object Obj = null;
            outMsg = string.Empty;
            FileStream fs = null;
            XmlSerializer xs = null;
            try
            {
                fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fs, encod))
                {
                    xs = new XmlSerializer(type);
                    Obj = xs.Deserialize(sr);
                    fs.Dispose();
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                outMsg = string.Format("XML反序列化异常：[Msg]-[{0}]、[信息]-[{1}]。", ex.Message, ex.ToString());
                Obj = null;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs.Close();
                }
            }
            return Obj;
        }
        #endregion

        #region 将类的对象序列化后保存到本地xml文件
        /// <summary>   
        /// 将类的对象序列化后保存到本地xml文件   
        /// </summary>   
        /// <param name="entityObj">一个对象</param>   
        /// <param name="fileName">本地xml文件路径</param>   
        public static void XmlSerialize(object entityObj, string fileName, out string outMsg)
        {
            outMsg = string.Empty;
            try
            {
                using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(entityObj.GetType());
                    serializer.Serialize(stream, entityObj, ns);
                }
            }
            catch (Exception ex)
            {
                outMsg = string.Format("对象序列化Xml异常：[Msg]-[{0}]、[信息]-[{1}]。", ex.Message, ex.ToString());
            }
        }
        #endregion
    }
}
