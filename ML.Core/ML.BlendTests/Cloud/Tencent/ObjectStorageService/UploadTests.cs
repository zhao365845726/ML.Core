using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Tencent.ObjectStorageService;
using ML.BlendTests;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Tencent.ObjectStorageService.Tests
{
    [TestClass()]
    public class UploadTests : TestBase<ML.BlendTests.Model.Tencent>
    {
        [TestMethod()]
        public void SimpleFileTest()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "tencent");
            Console.WriteLine($"Hello SimpleFileTest {entity.accessKeyId}");
        }
    }
}