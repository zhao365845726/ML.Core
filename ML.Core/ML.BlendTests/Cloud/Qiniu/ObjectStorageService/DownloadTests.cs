using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Qiniu.ObjectStorageService;
using ML.BlendTests;
using ML.BlendTests.Model.Monitor;
using Qiniu.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService.Tests
{
    [TestClass()]
    public class DownloadTests : TestBase<ML.BlendTests.Model.Qiniu>
    {
        [TestMethod()]
        public void DownloadPublicFileTest()
        {
            var entity = GetEntities($"{Data.FilePath}{Data.FileName}", "qiniu");
            Download download = new Download(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = download.DownloadPublicFile($"http://{entity.defaultEndPoint}/20211107163848", @"F:\source\download\qiniu\20211107163848.jpg");
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello DownloadPublicFileTest");
        }

        [TestMethod()]
        public void DownloadPrivateFileTest()
        {
            var entity = GetEntities($"{Data.FilePath}{Data.FileName}", "qiniu");
            Download download = new Download(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = download.DownloadPrivateFile(entity.bucketName,$"http://{entity.defaultEndPoint}/20211107163848", @"F:\source\download\qiniu\20211107163848.jpg", ZoneID.CN_North);
            Console.WriteLine($"{result}");
            Console.WriteLine($"Hello DownloadPrivateFileTest");
        }
    }
}