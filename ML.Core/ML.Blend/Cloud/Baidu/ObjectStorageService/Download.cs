using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BaiduBce.Services.Bos;
using BaiduBce.Services.Bos.Model;
using ML.Blend.Cloud.Interface.ObjectStorageService;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class Download : ClientBase
    {
        public Download(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }

        #region 简单接口  Simple
        public void SimpleDownloadObject(BosClient client, string bucketName, string objectName, string savelocalFilename)
        {
            try
            {
                // 新建GetObjectRequest
                GetObjectRequest getObjectRequest = new GetObjectRequest() { BucketName = bucketName, Key = objectName };

                // 下载Object到文件
                ObjectMetadata objectMetadata = client.GetObject(getObjectRequest, new FileInfo(savelocalFilename));
            }
            catch (Exception ex)
            {

            }
        }

        //public void SimpleDownloadObject(BosClient client, string bucketName, string objectName, string savelocalFilename)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public void SimpleDownloadObject(BosClient client, string bucketName, string objectName, string savelocalFilename)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

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
