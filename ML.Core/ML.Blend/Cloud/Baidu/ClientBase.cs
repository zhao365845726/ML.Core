using System;
using System.Collections.Generic;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace ML.Blend.Cloud.Baidu
{
    public class ClientBase : Base
    {
        public ClientBase(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 使用OSS域名新建OSSClient
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public OssClient CreateOssClient(string endpoint)
        {
            // 由用户指定的OSS访问地址、阿里云颁发的AccessKeyId/AccessKeySecret构造一个新的OssClient实例。
            var ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret);
            return ossClient;
        }

        /// <summary>
        /// 使用自定义域名新建OssClient
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public OssClient CreateOssClientByCustomeDomain(string endpoint)
        {
            // 创建ClientConfiguration实例，按照您的需要修改默认参数。
            var conf = new ClientConfiguration();

            // 开启支持CNAME。CNAME是指将自定义域名绑定到存储空间上。
            conf.IsCname = true;

            // 创建OssClient实例。
            var ossClient = new OssClient(endpoint, accessKeyId, accessKeySecret,conf);
            return ossClient;
        }
    }
}
