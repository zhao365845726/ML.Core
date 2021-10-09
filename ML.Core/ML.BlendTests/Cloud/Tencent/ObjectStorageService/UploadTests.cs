using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Tencent.ObjectStorageService;
using ML.BlendTests;
using ML.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Tencent.ObjectStorageService.Tests
{
    [TestClass()]
    public class UploadTests : TestBase<ML.BlendTests.Model.Tencent>
    {
        [TestMethod("简单上传文件")]
        public void SimpleFileTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "tencent-test");
            Upload upload = new Upload(entity.accessKeyId,entity.accessKeySecret,entity.region);
            var cosxml = upload.CreateClient();
            string strObjectName = $"industry/coal/shanxi_kexing_group/nanyang/{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            var result = upload.SimpleFile(cosxml, entity.bucketName, strObjectName, @"F:\source\image\003.jpg");
            Console.WriteLine($"https://{entity.defaultEndPoint}/{strObjectName}");
            Console.WriteLine($"Hello SimpleFileTest {entity.accessKeyId}");
        }
    }
}