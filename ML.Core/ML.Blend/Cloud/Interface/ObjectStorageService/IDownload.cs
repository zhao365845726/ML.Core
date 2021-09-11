using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Interface.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public interface IDownload<T>
    {

        /// <summary>
        /// 流式下载
        /// </summary>
        /// <param name="client">Oss客户端对象</param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void FlowType(T client, string bucketName, string objectName, string downloadFilename);

        /// <summary>
        /// 范围下载
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void Range(T client, string bucketName, string objectName, string downloadFilename);

        /// <summary>
        /// 断点续传下载
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void BreakPointContinue(T client, string bucketName, string objectName, string downloadFilename);

        /// <summary>
        /// 获取进度条
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void GetObjectProgress(T client, string bucketName, string objectName, string downloadFilename);
    }
}
