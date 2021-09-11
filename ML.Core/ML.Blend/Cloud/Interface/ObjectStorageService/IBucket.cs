using ML.Blend.Unified.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Interface.ObjectStorageService
{
    /// <summary>
    /// 存储空间
    /// </summary>
    public interface IBucket<T>
    {
        /// <summary>
        /// 创建存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void Create(T client, string bucketName);

        /// <summary>
        /// 列举存储空间
        /// </summary>
        /// <param name="client"></param>
        public void List(T client);

        /// <summary>
        /// 判断存储空间是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public bool DoesExist(T client, string bucketName);

        /// <summary>
        /// 获取存储空间的地域
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public object GetBucketLocation(T client, string bucketName);

        /// <summary>
        /// 获取存储空间的信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public object GetBucketInfo(T client, string bucketName);

        /// <summary>
        /// 设置Bucket权限
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="cannedAccess"></param>
        public void SetBucketAcl(T client, string bucketName, AccessControlType cannedAccess);

        /// <summary>
        /// 删除存储空间
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void Delete(T client, string bucketName);
    }
}
