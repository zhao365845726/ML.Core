using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace ML.Core
{
    /// <summary>
    /// INI文件读写类。 
    /// </summary>
    public class INIFileHelper
    {
        public static string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);


        /// <summary> 
        /// 写INI文件 
        /// </summary> 
        /// <param name="Section"></param> 
        /// <param name="Key"></param> 
        /// <param name="Value"></param> 
        public static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public static string IniRead(string section, string key, string def)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, sb, 1024, path);
            return sb.ToString();
        }

        /// <summary> 
        /// 读取INI文件 
        /// </summary> 
        /// <param name="Section"></param> 
        /// <param name="Key"></param> 
        /// <returns></returns> 
        public static string IniReadValue(string section, string Key)
        {
            //Cache.Insert("Items1", list, new System.Web.Caching.CacheDependency(path)); 
            //HttpSessionState Session = HttpContext.Current.Session;
            //if (path == null) path = Session["path"].ToString();
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(section, Key, "", temp, 255, path);
            return temp.ToString();
        }

        public static byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp;

        }
        public static void ReadSection(string section, StringCollection Idents)
        {
            Byte[] Buffer = new Byte[16384];
            //Idents.Clear();
            int bufLen = GetPrivateProfileString(section, null, null, Buffer, Buffer.GetUpperBound(0), path);
            //对Section进行解析
            GetStringsFromBuffer(Buffer, bufLen, Idents);
        }

        private static void GetStringsFromBuffer(Byte[] Buffer, int bufLen, StringCollection Strings)
        {
            Strings.Clear();
            if (bufLen != 0)
            {
                int start = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    if ((Buffer[i] == 0) && ((i - start) > 0))
                    {
                        String s = Encoding.GetEncoding(0).GetString(Buffer, start, i - start);
                        Strings.Add(s);
                        start = i + 1;
                    }
                }
            }
        }

        public static List<string> StringCollectionToList(StringCollection source)
        {
            List<string> lstDest = new List<string>();
            foreach (string startItem in source)
            {
                lstDest.Add(startItem);
            }
            return lstDest;
        }


        /// <summary> 
        /// 删除ini文件下所有段落 
        /// </summary> 
        public static void ClearAllSection()
        {
            IniWriteValue(null, null, null);
        }
        /// <summary> 
        /// 删除ini文件下personal段落下的所有键 
        /// </summary> 
        /// <param name="Section"></param> 
        public static void ClearSection(string Section)
        {
            IniWriteValue(Section, null, null);
        }
    }
}
