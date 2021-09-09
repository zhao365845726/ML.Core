using System;
using System.Collections.Generic;
using System.Text;
using COSXML;
using COSXML.Auth;
using COSXML.Model.Object;
using COSXML.Model.Bucket;
using COSXML.CosException;

namespace ML.Blend.Cloud.Tencent
{
    /// <summary>
    /// 腾讯云基类
    /// 参考地址：https://cloud.tencent.com/document/product
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
        /// 应用Id
        /// </summary>
        public string appid = string.Empty;
        /// <summary>
        /// 地域
        /// </summary>
        public string region = string.Empty;
        /// <summary>
        /// 语言
        /// </summary>
        public string lang = "en";

        public Base(string akId, string akSecret,string region)
        {
            accessKeyId = akId;
            accessKeySecret = akSecret;
            this.region = region;
        }

        /**
         * 使用AK&SK初始化账号Client
         * string accessKeyId, string accessKeySecret
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public CosXml CreateClient()
        {
            //初始化 CosXmlConfig , 提供配置 SDK 接口
            CosXmlConfig config = new CosXmlConfig.Builder()
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetRegion(region)  //设置一个默认的存储桶地域
              .SetDebugLog(true)  //显示日志
              .Build();  //创建 CosXmlConfig 对象
            //提供设置密钥信息接口
            string secretId = accessKeyId; //"云 API 密钥 SecretId";
            string secretKey = accessKeySecret; //"云 API 密钥 SecretKey";
            long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
              secretId, secretKey, durationSecond);
            //提供各种 COS API 服务接口
            CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
            return cosXml;
        }

        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public CosXml CreateClient(string akId, string akSecret, string region)
        {
            //初始化 CosXmlConfig , 提供配置 SDK 接口
            CosXmlConfig config = new CosXmlConfig.Builder()
              .IsHttps(true)  //设置默认 HTTPS 请求
              .SetRegion(region)  //设置一个默认的存储桶地域
              .SetDebugLog(true)  //显示日志
              .Build();  //创建 CosXmlConfig 对象
            //提供设置密钥信息接口
            string secretId = akId; //"云 API 密钥 SecretId";
            string secretKey = akSecret; //"云 API 密钥 SecretKey";
            long durationSecond = 600;  //每次请求签名有效时长，单位为秒
            QCloudCredentialProvider cosCredentialProvider = new DefaultQCloudCredentialProvider(
              secretId, secretKey, durationSecond);
            //提供各种 COS API 服务接口
            CosXml cosXml = new CosXmlServer(config, cosCredentialProvider);
            return cosXml;
        }
    }
}
