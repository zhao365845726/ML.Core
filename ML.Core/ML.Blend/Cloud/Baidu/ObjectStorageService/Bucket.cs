using System;
using System.Collections.Generic;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using ML.Blend.Cloud.Interface.ObjectStorageService;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
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
                if (!DoesExist(client, bucketName))
                {
                    // 创建存储空间。
                    var bucket = client.CreateBucket(bucketName);
                    Console.WriteLine("Create bucket succeeded, {0} ", bucket.Name);
                }
                else
                {
                    Console.WriteLine("Create bucket failed, {0} ", "bucket is Exist.");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create bucket failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 列举存储空间
        /// </summary>
        /// <param name="client"></param>
        public void List(OssClient client)
        {
            try
            {
                var buckets = client.ListBuckets();

                Console.WriteLine("List bucket succeeded");
                foreach (var bucket in buckets)
                {
                    Console.WriteLine("Bucket name：{0}，Location：{1}，Owner：{2}", bucket.Name, bucket.Location, bucket.Owner);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("List bucket failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 判断存储空间是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public bool DoesExist(OssClient client, string bucketName)
        {
            try
            {
                var exist = client.DoesBucketExist(bucketName);

                Console.WriteLine("Check object Exist succeeded");
                Console.WriteLine("exist ? {0}", exist);

                return exist;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Check object Exist failed. {0}", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取存储空间的地域
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public BucketLocationResult GetBucketLocation(OssClient client, string bucketName)
        {
            try
            {
                // 获取存储空间所在的地域。
                var result = client.GetBucketLocation(bucketName);
                Console.WriteLine("Get bucket:{0} Info succeeded ", bucketName);
                Console.WriteLine("bucket Location: {0}", result.Location);
                return result;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                throw new Exception();
            }
        }

        /// <summary>
        /// 获取存储空间的信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public BucketInfo GetBucketInfo(OssClient client, string bucketName)
        {
            try
            {
                //  存储空间的信息包括地域（Region或Location）、创建日期（CreationDate）、拥有者（Owner）、权限（Grants）等。
                var bucketInfo = client.GetBucketInfo(bucketName);
                Console.WriteLine("Get bucket:{0} Info succeeded ", bucketName);
                //  获取存储空间所在的地域。
                Console.WriteLine("bucketInfo Location: {0}", bucketInfo.Bucket.Location);
                // 获取存储空间的创建日期。
                Console.WriteLine("bucketInfo CreationDate: {0}", bucketInfo.Bucket.CreationDate);
                // 获取存储空间的数据容灾类型。
                Console.WriteLine("bucketInfo DataRedundancyType: {0}", bucketInfo.Bucket.DataRedundancyType);
                // 获取存储空间的权限信息。
                Console.WriteLine("bucketInfo Grant: {0}", bucketInfo.Bucket.AccessControlList.Grant);
                return bucketInfo;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                throw new Exception();
            }
        }

        public void SetBucketAcl(OssClient client, string bucketName, CannedAccessControlList cannedAccess)
        {
            try
            {
                // 设置存储空间的访问权限为公共读。
                client.SetBucketAcl(bucketName, cannedAccess);
                Console.WriteLine("Set bucket ACL succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set bucket ACL failed. {0}", ex.Message);
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
