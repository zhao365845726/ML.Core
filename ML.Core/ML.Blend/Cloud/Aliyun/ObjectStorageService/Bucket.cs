using System;
using System.Collections.Generic;
using System.Text;
using Aliyun.OSS;

namespace ML.Blend.Cloud.Aliyun.ObjectStorageService
{
    /// <summary>
    /// 存储空间
    /// </summary>
    public class Bucket : ClientBase
    {
        public Bucket(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 创建存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void Create(OssClient client,string bucketName)
        {
            try
            {
                // 创建存储空间。
                var bucket = client.CreateBucket(bucketName);
                Console.WriteLine("Create bucket succeeded, {0} ", bucket.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create bucket failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void Delete(OssClient client, string bucketName)
        {
            try
            {
                client.DeleteBucket(bucketName);
                Console.WriteLine("Delete bucket succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete bucket failed. {0}", ex.Message);
            }
        }
    }
}
