using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Qiniu.ObjectStorageService;
using ML.BlendTests;
using Qiniu.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ML.Core;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService.Tests
{
    [TestClass()]
    public class UploadTests : TestBase<ML.BlendTests.Model.Qiniu>
    {
        [TestMethod()]
        public void SimpleFileTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "qiniu");
            Upload upload = new Upload(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = upload.SimpleFile(entity.bucketName, strObjectName, @"F:\source\image\003.jpg", ZoneID.CN_North);
            //var dtResult = JsonHelper.JsonToDataTable(result.Text);
            Console.WriteLine($"http://{entity.defaultEndPoint}/{strObjectName}");
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello SimpleFileTest {entity.accessKeyId}");
        }

        [TestMethod()]
        public void UploadBytesDataTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "qiniu");
            Upload upload = new Upload(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = upload.UploadBytesData(entity.bucketName, strObjectName, @"F:\source\image\004.jpg", ZoneID.CN_North);
            //var dtResult = JsonHelper.JsonToDataTable(result.Text);
            Console.WriteLine($"http://{entity.defaultEndPoint}/{strObjectName}");
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello UploadBytesDataTest");
        }

        [TestMethod()]
        public void UploadStreamTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "qiniu");
            Upload upload = new Upload(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = upload.UploadBytesData(entity.bucketName, strObjectName, @"F:\source\image\005.jpg", ZoneID.CN_North);
            //var dtResult = JsonHelper.JsonToDataTable(result.Text);
            Console.WriteLine($"http://{entity.defaultEndPoint}/{strObjectName}");
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello UploadStreamTest");
        }

        [TestMethod()]
        public void UploadBigFileTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "qiniu");
            Upload upload = new Upload(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = upload.UploadBigFile(entity.bucketName, strObjectName, @"F:\source\video\screencast-chinaevent.microsoft.com-2020.07.15-19_01_49.webm", ZoneID.CN_North);
            //var dtResult = JsonHelper.JsonToDataTable(result.Text);
            Console.WriteLine($"http://{entity.defaultEndPoint}/{strObjectName}");
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello UploadStreamTest");
        }
    }
}