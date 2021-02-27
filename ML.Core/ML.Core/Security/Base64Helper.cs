//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML.Core.Base.Security
{
    public class Base64Helper
    {
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

        //var val1 = EncodeBase64("utf-8", "<tr><td>你好啊</td></tr>");

        //var val2 = DecodeBase64("utf-8", "PHRyPjx0ZD7kvaDlpb3llYo8L3RkPjwvdHI+");
    }
}
