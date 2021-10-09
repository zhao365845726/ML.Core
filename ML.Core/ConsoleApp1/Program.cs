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
            //Init(@"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json");
            //Domain_Add();
            //Download_FlowType();
            //Upload_SimpleFile();
            //Bucket_Delete();
            //GetAssemblyDictionaryResult();
            //Bucket_Create();
            //SysHelper_Test();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void SysHelper_Test()
        {
            var aa = SysHelper.GetMacAddressDic(":");
            foreach(KeyValuePair<string, string> kvp in aa)
            {
                Console.WriteLine($"key---{kvp.Key},value----{kvp.Value}");
            }
        }

        static void Domain_Add()
        {
            Analysis a = new Analysis(accessKeyId,accessKeySecret);
            a.AddDomainRecord("milisx.xyz", "ml20210924001", "A", "101.201.66.85");
        }

        static void Download_FlowType()
        {
            Download download = new Download(accessKeyId, accessKeySecret);
            var client = download.CreateOssClient(endpoint);
            string bucketName = $"mltechnology-sandbox";
            string objectName = $"robot/foreend/20210909160455.json";
            string downloadFileName = @"\\MLServer\Robot3\3.json";
            download.FlowType(client, bucketName, objectName, downloadFileName);
        }

        static void Upload_SimpleFile()
        {
            Upload upload = new Upload(accessKeyId, accessKeySecret);
            var client = upload.CreateOssClient(endpoint);
            string bucketName = $"mltechnology-sandbox";
            string objectName = $"robot/foreend/{DateTime.Now.ToString("yyyyMMddHHmmss")}.json";
            //string objectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.json";
            string localFilename = @"\\MLServer\Robot3\1.json";
            upload.SimpleFile(client, bucketName, objectName, localFilename);
        }

        static void Bucket_Delete()
        {
            Bucket bucket = new Bucket(accessKeyId, accessKeySecret);
            var client = bucket.CreateOssClient(endpoint);
            string bucketName = $"test-20210909152435";
            bucket.Delete(client, bucketName);
        }

        static void Bucket_Create()
        {
            Bucket bucket = new Bucket(accessKeyId, accessKeySecret);
            var client = bucket.CreateOssClient(endpoint);
            string bucketName = $"test-{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            bucket.Create(client, bucketName);
        }

        static void Init(string filePath)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            string text = File.ReadAllText(filePath, Encoding.GetEncoding("UTF-8"));
            //var jsonObj = JsonConvert.DeserializeObject<JsonApp>(text);
            //var pageObj = JsonConvert.DeserializeObject<Page>(text);
            var model = JsonConvert.DeserializeObject<IDictionary<string, object>>(text);
            //ConsoleApp1.Model.Aliyun aliyun = JsonConvert.DeserializeObject<ConsoleApp1.Model.Aliyun>(model["aliyun"].ToString());
            ConsoleApp1.Model.Tencent tencent = JsonConvert.DeserializeObject<ConsoleApp1.Model.Tencent>(model["tencent"].ToString());
            accessKeyId = tencent.accessKeyId;
            accessKeySecret = tencent.accessKeySecret;
            endpoint = tencent.region;

        }

        public static void GetAssemblyDictionaryResult()
        {
            //string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"/nuget/";
            ////string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //string path = @"F:\local_dll";
            string path = @"F:\local_dll\P_NetConfChina\netstandard2.1";
            //Console.WriteLine(path);
            AssemblyHandler assemblyHandler = new AssemblyHandler(path);
            var aa = assemblyHandler.GetAssemblyDictionaryResult("P-NetConfChina", "P_NetConfChina.DatabaseModel");
        }
    }
}
