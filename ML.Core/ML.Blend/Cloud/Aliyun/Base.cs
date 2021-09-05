using System;
using System.Collections.Generic;
using System.Text;
using AlibabaCloud.SDK.Alidns20150109;
using AlibabaCloud.OpenApiClient.Models;

namespace ML.Blend.Cloud.Aliyun
{
    /// <summary>
    /// 阿里云基类
    /// 参考地址：https://next.api.aliyun.com/api/Alidns/2015-01-09/AddCustomLine?params={}&sdkStyle=dara&lang=CSHARP
    /// </summary>
    public class Base
    {
        /// <summary>
        /// AK
        /// </summary>
        public string accessKeyId = string.Empty;
        /// <summary>
        /// AKS
        /// </summary>
        public string accessKeySecret = string.Empty;
        /// <summary>
        /// 语言
        /// </summary>
        public string lang = "en";

        public Base(string akId,string akSecret)
        {
            accessKeyId = akId;
            accessKeySecret = akSecret;
        }

        /**
         * 使用AK&SK初始化账号Client
         * string accessKeyId, string accessKeySecret
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public Client CreateClient()
        {
            Config config = new Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "dns.aliyuncs.com";
            return new Client(config);
        }

        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public Client CreateClient(string akId, string akSecret)
        {
            Config config = new Config
            {
                // 您的AccessKey ID
                AccessKeyId = akId,
                // 您的AccessKey Secret
                AccessKeySecret = akSecret,
            };
            // 访问的域名
            config.Endpoint = "dns.aliyuncs.com";
            return new Client(config);
        }
    }
}
