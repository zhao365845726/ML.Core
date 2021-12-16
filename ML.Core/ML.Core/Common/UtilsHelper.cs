//using Senparc.Ncf.Core.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ML.Core
{
    public class UtilsHelper
    {
        public static string RegexFormatContent(string content)
        {
            string result = Regex.Replace(content, "<figure class=\"table\">", "");
            result = Regex.Replace(result, "</figure>", "");
            return result;
        }

        ///编码
        public static string EncodeBase64(string code_type, string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码
        public static string DecodeBase64(string code_type, string code)
        {
            //var val1 = EncodeBase64("utf-8", "<tr><td>你好啊</td></tr>");

            //var val2 = DecodeBase64("utf-8", "PHRyPjx0ZD7kvaDlpb3llYo8L3RkPjwvdHI+");

            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        //public static string EncryptionPassword(string password, string salt, bool isMD5Password)
        //{
        //    string md5 = password.ToUpper().Replace("-", "");
        //    if (!isMD5Password)
        //    {
        //        md5 = MD5.GetMD5Code(password, "").Replace("-", ""); //原始MD5
        //    }
        //    return MD5.GetMD5Code(md5, salt).Replace("-", ""); //再加密
        //}
    }
}
