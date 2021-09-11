using System;
using System.Collections.Generic;
using System.Text;
using BaiduBce;
using BaiduBce.Auth;
using BaiduBce.Services.Bos;

namespace ML.Blend.Cloud.Baidu
{
    /// <summary>
    /// 百度云基类
    /// 参考地址：https://cloud.baidu.com/doc/BOS/s/Bjwvys0hp
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
        /// endpoint
        /// </summary>
        public string endpoint = string.Empty;
        /// <summary>
        /// 语言
        /// </summary>
        public string lang = "en";

        public Base(string akId,string akSecret)
        {
            accessKeyId = akId;
            accessKeySecret = akSecret;
        }

        public Base(string akId, string akSecret,string endpoint)
        {
            accessKeyId = akId;
            accessKeySecret = akSecret;
            this.endpoint = endpoint;
        }

        /**
         * 使用AK&SK初始化账号Client
         * string accessKeyId, string accessKeySecret
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public BosClient CreateClient()
        {
            // 初始化一个BosClient
            BceClientConfiguration config = new BceClientConfiguration();
            config.Credentials = new DefaultBceCredentials(accessKeyId, accessKeySecret);
            config.Endpoint = endpoint;
            BosClient client = new BosClient(config);
            return client;
        }

        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public BosClient CreateClient(string akId, string akSecret, string endpoint)
        {
            // 初始化一个BosClient
            BceClientConfiguration config = new BceClientConfiguration();
            config.Credentials = new DefaultBceCredentials(akId, akSecret);
            config.Endpoint = endpoint;
            BosClient client = new BosClient(config);
            return client;
        }
    }
}
