using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Aliyun.CloudCommunication.Sms;
using ML.BlendTests;
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
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "aliyun");
            Send send= new Send(entity.accessKeyId,entity.accessKeySecret);
            var result = send.SingleSend("17803565206", "米立科技", "SMS_122230050", "888888");
            Console.WriteLine(result.Body);
        }
    }
}