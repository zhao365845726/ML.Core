using ML.Core.Assemblies;
using System;
using ML.Blend.Cloud.Aliyun;
using ML.Blend.Cloud.Aliyun.ObjectStorageService;
using ML.Blend.Cloud.Aliyun.Domain;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using ML.Core;
using ML.Core.Enum;
//using ML.Blend.Cloud.Tencent;
//using ML.Blend.Cloud.Tencent.ObjectStorageService;

namespace ConsoleApp1
{
    class Program
    {
        private static string accessKeyId = string.Empty;
        private static string accessKeySecret = string.Empty;
        private static string endpoint = string.Empty;
        private static string region = string.Empty;

        static void Main(string[] args)
        {
            Console.WriteLine($"时间差：{(DateTime.Now - Convert.ToDateTime($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} 00:00:00")).TotalSeconds}");
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
