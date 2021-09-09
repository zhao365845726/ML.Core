using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Util;

namespace ML.Blend.Cloud.Aliyun.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload : ClientBase
    {
        public Upload(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 简单上传本地文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFileName"></param>
        public void SimpleFile(OssClient client, string bucketName,string objectName,string localFilename)
        {
            try
            {
                // 上传文件。
                var result = client.PutObject(bucketName, objectName, localFilename);
                Console.WriteLine("Put object succeeded, ETag: {0} ", result.ETag);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 简单上传字符串
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="objectContent"></param>
        public void SimpleString(OssClient client, string bucketName, string objectName, string objectContent)
        {
            try
            {
                byte[] binaryData = Encoding.ASCII.GetBytes(objectContent);
                MemoryStream requestContent = new MemoryStream(binaryData);
                // 上传文件。
                client.PutObject(bucketName, objectName, requestContent);
                Console.WriteLine("Put object succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 简单上传文件带MD5校验
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void SimpleFileWithValidate(OssClient client, string bucketName, string objectName, string localFilename)
        {
            try
            {
                // 计算MD5。
                string md5;
                using (var fs = File.Open(localFilename, FileMode.Open))
                {
                    md5 = OssUtils.ComputeContentMd5(fs, fs.Length);
                }
                var objectMeta = new ObjectMetadata
                {
                    ContentMd5 = md5
                };
                // 上传文件。
                client.PutObject(bucketName, objectName, localFilename, objectMeta);
                Console.WriteLine("Put object succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
            }
        }

        
        public void Append()
        {

        }

        public void BreakPointContinue()
        {

        }

        public void Slice()
        {

        }
    }
}
