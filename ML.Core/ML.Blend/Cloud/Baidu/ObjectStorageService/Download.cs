using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BaiduBce.Services.Bos;
using ML.Blend.Cloud.Interface.ObjectStorageService;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class Download : ClientBase,IDownload<BosClient>
    {
        public Download(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }

        public void BreakPointContinue(BosClient client, string bucketName, string objectName, string downloadFilename)
        {
            throw new NotImplementedException();
        }

        public void FlowType(BosClient client, string bucketName, string objectName, string downloadFilename)
        {
            throw new NotImplementedException();
        }

        public void GetObjectProgress(BosClient client, string bucketName, string objectName, string downloadFilename)
        {
            throw new NotImplementedException();
        }

        public void Range(BosClient client, string bucketName, string objectName, string downloadFilename)
        {
            throw new NotImplementedException();
        }
    }
}
