using ML.Core.Assemblies;
using System;
using ML.Blend.Cloud.Aliyun;
using ML.Blend.Cloud.Aliyun.ObjectStorageService;

namespace ConsoleApp1
{
    class Program
    {
        private static string accessKeyId = "LTAI5tQHVByLs6riFbSG2AYg";
        private static string accessKeySecret = "PxSbw0mphpjMwpN32ct3yZRbc1Tt4U";
        private static string endpoint = "oss-cn-beijing.aliyuncs.com";

        static void Main(string[] args)
        {
            Download_FlowType();
            //Upload_SimpleFile();
            //Bucket_Delete();
            //GetAssemblyDictionaryResult();
            //Bucket_Create();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }

        static void Download_FlowType()
        {
            Download download = new Download(accessKeyId, accessKeySecret);
            var client = download.CreateOssClient(endpoint);
            string bucketName = $"mltechnology-sandbox";
            string objectName = $"robot/foreend/20210909160455.json";
            string downloadFileName = @"\\MLServer\Robot3\2.json";
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
