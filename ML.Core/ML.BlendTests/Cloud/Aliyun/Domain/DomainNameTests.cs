using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Aliyun.Domain;
using ML.BlendTests;
using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.SDK.Alidns20150109.Models;

namespace ML.Blend.Cloud.Aliyun.Domain.Tests
{
    [TestClass()]
    public class DomainNameTests : TestBase<ML.BlendTests.Model.Aliyun>
    {
        [TestMethod()]
        public void DescribeDomainsTest()
        {
            string filePath = @"E:\_GitHub\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "aliyun");
            DomainName domainName = new DomainName(entity.accessKeyId, entity.accessKeySecret);
            var response = domainName.DescribeDomains();
            var domainList = response.Body.Domains.Domain;
            foreach(var item in domainList)
            {
                Console.WriteLine($"域名  {item.DomainName}");
            }
        }
    }
}