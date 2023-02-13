using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Aliyun.CloudCommunication.Sms;
using ML.BlendTests;
using ML.BlendTests.Model.Monitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Aliyun.CloudCommunication.Sms.Tests
{
    [TestClass()]
    public class RecordTests : TestBase<ML.BlendTests.Model.Aliyun>
    {
        [TestMethod()]
        public void QuerySendDetailsTest()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.AliyunCloudName);
            Record record = new Record(entity.accessKeyId, entity.accessKeySecret);
            var result = record.QuerySendDetails(Data.Mobile, Data.CurDate, Data.PageIndex, Data.PageSize);
            TestOutput<QuerySendDetailsResponseBody>.Write(result.Body);
        }

        
    }
}