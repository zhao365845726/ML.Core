using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Qiniu
{
    /// <summary>
    /// 七牛云基类
    /// 参考地址：https://developer.qiniu.com/kodo/4056/c-sdk-v7-2-15
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
    }
}
