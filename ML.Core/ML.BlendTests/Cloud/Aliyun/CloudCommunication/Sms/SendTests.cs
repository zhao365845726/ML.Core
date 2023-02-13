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
    public class SendTests : TestBase<ML.BlendTests.Model.Aliyun>
    {
        [TestMethod()]
        public void SingleSendTest()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.AliyunCloudName);
            Send send = new Send(entity.accessKeyId, entity.accessKeySecret);
            var result = send.SingleSend(Data.Mobile, Data.SignName, Data.TemplateCode, Data.VerificationCode);
            TestOutput<SendSmsResponseBody>.Write(result.Body);
        }

        [TestMethod()]
        public void BatchSendTest()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.AliyunCloudName);
            Send send = new Send(entity.accessKeyId, entity.accessKeySecret);
            var result = send.BatchSend(Data.BatchMobile, Data.BatchSignName, Data.TemplateCode, Data.BatchVerificationCode);
            TestOutput<SendBatchSmsResponseBody>.Write(result.Body);
        }
    }
}