using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Interface.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public interface IUpload<T>
    {

        /// <summary>
        /// 简单上传本地文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFileName"></param>
        public void SimpleFile(T client, string bucketName, string objectName, string localFilename);

        /// <summary>
        /// 简单上传字符串
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="objectContent"></param>
        public void SimpleString(T client, string bucketName, string objectName, string objectContent);

        /// <summary>
        /// 简单上传文件带MD5校验
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void SimpleFileWithValidate(T client, string bucketName, string objectName, string localFilename);

        /// <summary>
        /// 追加上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void Append(T client, string bucketName, string objectName, string localFilename);

        /// <summary>
        /// 断点续传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void BreakPointContinue(T client, string bucketName, string objectName, string localFilename);

        public void Slice(T client, string bucketName, string objectName, string localFilename);

        /// <summary>
        /// 进度条
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void PutObjectProgress(T client, string bucketName, string objectName, string localFilename);
    }
}
