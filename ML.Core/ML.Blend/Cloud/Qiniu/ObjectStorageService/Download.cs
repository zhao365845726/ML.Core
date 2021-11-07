using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ML.Blend.Cloud.Interface.ObjectStorageService;
using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class Download : ClientBase
    {
        public Download(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 下载可公开访问的文件
        /// </summary>
        public HttpResult DownloadPublicFile(string objectName, string downloadFilename)
        {
            // 可公开访问的url，直接下载
            HttpResult result = DownloadManager.Download(objectName,downloadFilename);
            return result;
        }

        /// <summary>
        /// 下载私有空间中的文件
        /// </summary>
        public HttpResult DownloadPrivateFile(string bucketName, string objectName, string downloadFilename, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            // 设置下载链接有效期3600秒
            int expireInSeconds = 3600;
            string accUrl = DownloadManager.CreateSignedUrl(client, objectName, expireInSeconds);
            // 接下来可以使用accUrl来下载文件
            HttpResult result = DownloadManager.Download(accUrl, downloadFilename);
            //Console.WriteLine(result);
            return result;
        }
    }
}
