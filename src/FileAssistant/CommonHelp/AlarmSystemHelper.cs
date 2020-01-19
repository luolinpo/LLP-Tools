using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CommonHelp
{
    public static class AlarmSystemHelper
    {
        //private static string strPath = System.Environment.CurrentDirectory; //获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。
        private static string strPath = System.AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 获取值
        /// </summary>
        public static string GetValueByIni(string Section, string Key)
        {
            try
            {
                IniFileHelper iniHelper = new IniFileHelper(strPath + "\\AlarmSystem.ini");
                string tempValue = string.Empty;
                if (iniHelper.ExistINIFile())
                {
                    tempValue = iniHelper.IniReadValue(Section, Key);

                }
                return tempValue;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
        /// <summary>
        /// 设置值
        /// </summary>
        public static void SetValueByIni(string Section, string Key, string Value)
        {
            try
            {
                IniFileHelper iniHelper = new IniFileHelper(strPath + "\\AlarmSystem.ini");
                if (iniHelper.ExistINIFile())
                {
                    iniHelper.IniWriteValue(Section, Key, Value);
                }
            }
            catch (Exception ex)
            {

            }


        }
        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static bool TrySetValueByIni(string Section, string Key, string Value)
        {
            try
            {
                IniFileHelper iniHelper = new IniFileHelper(strPath + "\\AlarmSystem.ini");
                if (iniHelper.ExistINIFile())
                {
                    iniHelper.IniWriteValue(Section, Key, Value);
                }
                 return Value == iniHelper.IniReadValue(Section, Key) ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
