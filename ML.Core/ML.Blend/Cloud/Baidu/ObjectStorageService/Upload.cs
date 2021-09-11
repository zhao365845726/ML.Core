using BaiduBce.Services.Bos;
using ML.Blend.Cloud.Interface.ObjectStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload : ClientBase, IUpload<BosClient>
    {
        public Upload(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }

        public void Append(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void BreakPointContinue(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void PutObjectProgress(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleFile(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleFileWithValidate(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleString(BosClient client, string bucketName, string objectName, string objectContent)
        {
            throw new NotImplementedException();
        }

        public void Slice(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }
    }
}
