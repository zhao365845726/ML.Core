using System;
using System.Collections.Generic;
using System.Text;
using BaiduBce;
using BaiduBce.Auth;
using BaiduBce.Services.Bos;
using ML.Blend.Cloud.Interface.ObjectStorageService;
using ML.Blend.Unified.Enum;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 存储空间
    /// 参考地址：https://cloud.baidu.com/product/bos.html?track=navigation20200904
    /// </summary>
    public class Bucket : ClientBase,IBucket<BosClient>
    {
        public Bucket(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }


        /// <summary>
        /// 创建存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public object Create(BosClient client,string bucketName)
        {
            try
            {
                if (!DoesExist(client, bucketName))
                {
                    // 新建一个Bucket
                    var bucket = client.CreateBucket(bucketName);
                    Console.WriteLine("Create bucket succeeded, {0} ", bucket.BceRequestId);
                    return bucket;
                }
                else
                {
                    Console.WriteLine("Create bucket failed, {0} ", "bucket is Exist.");
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Create bucket failed, {0}", ex.Message);
                throw new Exception();
            }
        }

        /// <summary>
        /// 列举存储空间
        /// </summary>
        /// <param name="client"></param>
        public object List(BosClient client)
        {
            try
            {
                // 获取用户的Bucket列表
                var buckets = client.ListBuckets();

                Console.WriteLine("List bucket succeeded");

                // 遍历Bucket
                foreach (var bucket in buckets.Buckets)
                {
                    Console.WriteLine("Bucket name：{0}，Location：{1}，Owner：{2}", bucket.Name, bucket.Location,bucket.CreationDate);
                }
                return buckets;
            }
            catch (Exception ex)
            {
                Console.WriteLine("List bucket failed. {0}", ex.Message);
                throw new Exception();
            }
        }

        /// <summary>
        /// 判断存储空间是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public bool DoesExist(BosClient client, string bucketName)
        {
            try
            {
                //获取Bucket的存在信息
                var exist = client.DoesBucketExist(bucketName); //指定Bucket名称

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
        /// 删除存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void Delete(BosClient client, string bucketName)
        {
            try
            {
                // 删除Bucket
                client.DeleteBucket(bucketName);
                Console.WriteLine("Delete bucket succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete bucket failed. {0}", ex.Message);
            }
        }

        public object GetBucketLocation(BosClient client, string bucketName)
        {
            throw new NotImplementedException();
        }

        public object GetBucketInfo(BosClient client, string bucketName)
        {
            throw new NotImplementedException();
        }

        public void SetBucketAcl(BosClient client, string bucketName, AccessControlType cannedAccess)
        {
            try
            {
                // 设置存储空间的访问权限为公共读。
                switch (cannedAccess)
                {
                    case AccessControlType.Default:
                        {
                            client.SetBucketAcl(bucketName, BosConstants.CannedAcl.Private);
                            break;
                        }
                    case AccessControlType.Private:
                        {
                            client.SetBucketAcl(bucketName, BosConstants.CannedAcl.Private);
                            break;
                        }
                    case AccessControlType.PublicRead:
                        {
                            client.SetBucketAcl(bucketName, BosConstants.CannedAcl.PublicRead);
                            break;
                        }
                    case AccessControlType.PublicReadWrite:
                        {
                            client.SetBucketAcl(bucketName, BosConstants.CannedAcl.PublicReadWrite);
                            break;
                        }
                }
                
                Console.WriteLine("Set bucket ACL succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set bucket ACL failed. {0}", ex.Message);
            }
        }
    }
}
