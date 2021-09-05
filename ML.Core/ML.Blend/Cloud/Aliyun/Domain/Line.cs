using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.SDK.Alidns20150109.Models;

namespace ML.Blend.Cloud.Aliyun.Domain
{
    /// <summary>
    /// 线路
    /// </summary>
    public class Line : Base
    {
        public Line(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 查询云解析支持的所有线路列表
        /// </summary>
        /// <param name="clientIp"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public DescribeSupportLinesResponse DescribeSupportLines(string clientIp = "",string domainName = "")
        {
            Client client = CreateClient();
            DescribeSupportLinesRequest describeSupportLinesRequest = new DescribeSupportLinesRequest
            {
                Lang = lang,
                UserClientIp = clientIp,
                DomainName = domainName,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeSupportLines(describeSupportLinesRequest);
        }

        /// <summary>
        /// 查询自定义线路列表
        /// </summary>
        /// <param name="domainName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DescribeCustomLinesResponse DescribeCustomLines(string domainName,int pageIndex,int pageSize)
        {
            Client client = CreateClient();
            DescribeCustomLinesRequest describeCustomLinesRequest = new DescribeCustomLinesRequest
            {
                Lang = lang,
                DomainName = domainName,
                PageNumber = pageIndex,
                PageSize = pageSize,
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeCustomLines(describeCustomLinesRequest);
        }
    }
}
