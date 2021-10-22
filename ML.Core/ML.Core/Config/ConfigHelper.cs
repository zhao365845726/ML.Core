//=====================================================================================
// All Rights Reserved , Copyright © MLTechnology 2017-Now
//=====================================================================================
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Core
{
    /// <summary>
    ///  Config配置文件 公共帮助类
    /// 版本：2.0
    /// </summary>
    public class ConfigHelper<T>
    {
        protected string _filePath = string.Empty;

        public ConfigHelper(string filePath)
        {
            _filePath = filePath;
        }

        public T GetEntities(string filePath, string node)
        {
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string text = File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            var model = JsonConvert.DeserializeObject<IDictionary<string, object>>(text);
            var tencent = JsonConvert.DeserializeObject<T>(model[node].ToString());
            return tencent;
        }
    }
}
