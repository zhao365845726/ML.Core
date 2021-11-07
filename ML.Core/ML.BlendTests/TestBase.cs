using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ML.BlendTests.Model;
using ML.BlendTests.Model.Monitor;

namespace ML.BlendTests
{
    public class TestBase<T> : Data
    {
        public string ConfigFile = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
        //public string ConfigFile = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";

        public T GetEntities(string filePath,string node)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string text = File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            var model = JsonConvert.DeserializeObject<IDictionary<string, object>>(text);
            var tencent = JsonConvert.DeserializeObject<T>(model[node].ToString());
            return tencent;
        }
    }
}
