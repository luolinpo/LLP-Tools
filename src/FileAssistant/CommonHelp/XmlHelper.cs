using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CommonHelp
{    
     /// <summary>
     /// xml文件助手 20170625 罗林坡
     /// </summary>
     public class XmlHelper
    {
        #region Linq to XML
        /// <summary>
        /// 读取所有信息
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> ReadAllXmlFromFile(string xmlpath, string firstnodename)
          {
              XElement xe = XElement.Load(xmlpath);
              IEnumerable<XElement> elements = from ele in xe.Elements(firstnodename)
                                               select ele;
            return elements;
          }
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public static Tuple<bool,string> InsertIntoXml(string xmlpath, XElement xelement)
        {
            try
            {
                XElement xe = XElement.Load(xmlpath);
                xe.Add(xelement);
                xe.Save(xmlpath);
                return new Tuple<bool, string>(true, "插入成功");
            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(true, ex.ToString());
            }

        }
        /// <summary>
        /// 删除一条数据 删除指定节点指定属性（一级节点）
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public static Tuple<bool, string> DeleteFromXml(string xmlpath,string nodename,string attributename,string attributevalue)
        {
            try
            {
                 XElement xe = XElement.Load(xmlpath);
                 IEnumerable<XElement> elements = from ele in xe.Elements(nodename)
                                                  where(string)ele.Attribute(attributename) == attributevalue
                                                  select ele;
                 {
                 if (elements.Count() > 0)
                     elements.First().Remove();
                 }
                 xe.Save(xmlpath);
                return new Tuple<bool, string>(true, "删除成功");
            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(true, ex.ToString());
            }

        }
        /// <summary>
        /// 获取指定数据
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public static Tuple<bool, string> SelectFromXml(string xmlpath, string nodename, string attributename, string attributename2, string attributevalue)
        {
            try
            {
                XElement xe = XElement.Load(xmlpath);
                IEnumerable<XElement> elements = from ele in xe.Elements(nodename)
                                               
                                                 select ele;

                var query = (from ele in xe.Elements(nodename)
                             where (string)ele.Attribute(attributename) == attributevalue
                             select (string)ele.Attribute(attributename2)).FirstOrDefault();
                              
                             //{
                             //    Name = item.Value,
                             //    Visible = item.Attribute("visible") == null ? (string)null : item.Attribute("visible").Value,
                             //    Order = item.Attribute("order") == null ? (string)null : item.Attribute("order").Value,
                             //    Width = item.Attribute("width") == null ? (string)null : item.Attribute("width").Value
                             //}).ToList();
                return new Tuple<bool, string>(true, query);
            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(false, ex.ToString());
            }

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <returns></returns>
        public static Tuple<bool, string> UpdateIntoXml(string xmlpath, string nodename, string attributename, string attributename2, string attributevalue,
                      string newtype,string newname)
        {
            try
            {
                XElement xe = XElement.Load(xmlpath);
                    IEnumerable<XElement> element = from ele in xe.Elements(nodename)
                                                    where ele.Attribute(attributename).Value == attributevalue
                                                    select ele;
                    if (element.Count() > 0)
                    {
                        XElement first = element.First();
                        ///设置新的属性
                        first.SetAttributeValue(attributename, newtype);
                        first.SetAttributeValue(attributename2, newname);
                   }
                    xe.Save(xmlpath);
                return new Tuple<bool, string>(true, "修改成功");
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(true, ex.ToString());
            }

        }

        #endregion
        #region
        /// <summary>
        /// 将xml文件转换为DataSet
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static DataSet ConvertXMLFileToDataSet(string xmlFile)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                XmlDocument xmld = new XmlDocument();
                xmld.Load(xmlFile);

                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmld.InnerXml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                //xmlDS.ReadXml(xmlFile);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        #endregion
        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var dt = new DataTable();
            try
            {
                var props = typeof(T).GetProperties();
              
                dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
                if (collection.Count() > 0)
                {
                    for (int i = 0; i < collection.Count(); i++)
                    {
                        ArrayList tempList = new ArrayList();
                        foreach (PropertyInfo pi in props)
                        {
                            object obj = pi.GetValue(collection.ElementAt(i), null);
                            tempList.Add(obj);
                        }
                        object[] array = tempList.ToArray();
                        dt.LoadDataRow(array, true);
                    }
                }
                
            }
            catch (Exception)
            {
            }
            return dt;
        }
    }
}
