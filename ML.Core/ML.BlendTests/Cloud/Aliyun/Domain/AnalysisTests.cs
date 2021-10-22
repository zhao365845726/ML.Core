using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Aliyun.Domain;
using ML.BlendTests;
using Senparc.CO2NET.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Aliyun.Domain.Tests
{
    [TestClass()]
    public class AnalysisTests : TestBase<ML.BlendTests.Model.Aliyun>
    {
        [TestMethod()]
        public void DescribeDomainRecordsTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "aliyun");
            Analysis a = new Analysis(entity.accessKeyId, entity.accessKeySecret);
            //var result = a.DescribeDomainRecords("milisx.xyz", 1, 10);

            for(int i = 1; i <= 10; i++)
            {
                var result = a.DescribeDomainRecords("milisx.xyz", i, 10);
                if(result != null)
                {
                    var record = result.Body.DomainRecords.Record;
                    foreach(var recordItem in record)
                    {
                        Console.WriteLine($"recordItem {recordItem.RR}.{recordItem.DomainName} , Remark : {recordItem.Remark}");
                    }
                }
            }
        }
    }
}