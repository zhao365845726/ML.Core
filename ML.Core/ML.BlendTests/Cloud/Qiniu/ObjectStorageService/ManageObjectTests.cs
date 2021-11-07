using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Qiniu.ObjectStorageService;
using ML.BlendTests;
using System;
using System.Collections.Generic;
using System.Text;
using Qiniu.Common;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService.Tests
{
    [TestClass()]
    public class ManageObjectTests : TestBase<ML.BlendTests.Model.Qiniu>
    {
        [TestMethod()]
        public void ListFilesTest()
        {
            var entity = GetEntities(ConfigFile, "qiniu");
            ManageObject manageObject = new ManageObject(entity.accessKeyId, entity.accessKeySecret);
            string strObjectName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            manageObject.ListFiles(entity.bucketName, "", "", "/", ZoneID.CN_North);
            Console.WriteLine($"Hello ListFilesTest");
        }
    }
}