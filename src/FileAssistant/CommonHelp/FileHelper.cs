using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelp
{
   public class FileHelper
    {
        #region .net封装后的api
        /// <summary>
        ///文件拷贝
        /// </summary>
        /// <param name="oldfilenmme"></param>
        /// <param name="newfilenmme"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool FileCopyTo(string oldfilenmme, string newfilenmme, bool overwrite)
        {
            FileInfo oldfile = new FileInfo(oldfilenmme);
            FileInfo newfile = oldfile.CopyTo(newfilenmme, overwrite);
            return newfile.Exists;
        }
        #endregion

        #region 
         
            private const int FO_COPY = 0x0002;
            private const int FOF_ALLOWUNDO = 0x00044;
            //显示进度条  0x00044 // 不显示一个进度对话框 0x0100 显示进度对话框单不显示进度条  0x0002显示进度条和对话框  
            private const int FOF_SILENT = 0x0002;//0x0100;  
                                                  //  
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 0)]
            public struct SHFILEOPSTRUCT
            {
             public IntPtr hwnd;//指向发送消息的窗口
            [MarshalAs(UnmanagedType.U4)]
                public int wFunc;//执行的操作
            public string pFrom;//源文件名
            public string pTo;//目标文件名
            public short fFlags;//操作与确认标识
            [MarshalAs(UnmanagedType.Bool)]
                public bool fAnyOperationsAborted;//操作是否终止
            public IntPtr hNameMappings;//文件映射
            public string lpszProgressTitle;//进度条标题
        }
            [DllImport("shell32.dll", CharSet = CharSet.Auto)]
            static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
            public static bool DoCopy(string strSource, string strTarget)
            {
                SHFILEOPSTRUCT fileop = new SHFILEOPSTRUCT();
                fileop.wFunc = FO_COPY; //1.FO_COPY：复制2.FO_DELETE：删除3.FO_MOVE：移动4.FO_RENAME：重命名
                fileop.pFrom = strSource;
                fileop.lpszProgressTitle = "复制拷贝中";
                fileop.pTo = strTarget;
                //fileop.fFlags = FOF_ALLOWUNDO;  
                fileop.fFlags = FOF_SILENT;
                return SHFileOperation(ref fileop) == 0;
            }
        
        #endregion
    }
}
