using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.SDK.Alidns20150109.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Aliyun.Domain
{
    /// <summary>
    /// 域名
    /// </summary>
    public class DomainName : ClientBase
    {
        public DomainName(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 查询域名列表
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="groupId"></param>
        /// <param name="searchMode"></param>
        /// <param name="resourceGroupId"></param>
        /// <returns></returns>
        public DescribeDomainsResponse DescribeDomains(string keyword = "", string groupId = "", string searchMode = "", string resourceGroupId = "")
        {
            Client client = CreateDomainClient("");
            DescribeDomainsRequest request = new DescribeDomainsRequest
            {
                Lang = lang,
                KeyWord = keyword,
                GroupId = groupId,
                SearchMode = searchMode,
                ResourceGroupId = resourceGroupId
            };
            // 复制代码运行请自行打印 API 的返回值
            return client.DescribeDomains(request);
        }
    }
}
