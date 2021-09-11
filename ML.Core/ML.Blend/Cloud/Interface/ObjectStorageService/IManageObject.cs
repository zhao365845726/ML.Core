using ML.Blend.Unified.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Interface.ObjectStorageService
{
    public interface IManageObject<T>
    {

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public bool DoesExist(T client, string bucketName, string objectName);

        /// <summary>
        /// 管理文件访问权限
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="cannedAccess"></param>
        public void SetObjectAcl(T client, string bucketName, string objectName, AccessControlType cannedAccess);

        /// <summary>
        /// 管理文件元信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void ObjectMetaData(T client, string bucketName, string objectName, string localFilename);

        /// <summary>
        /// 简单列举文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void List(T client, string bucketName);

        /// <summary>
        /// 列举指定个数的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="number"></param>
        public void SpecifyNumberList(T client, string bucketName, int number);

        /// <summary>
        /// 列举指定前缀的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        public void SpecifyPrefixList(T client, string bucketName, int number, string prefix);

        /// <summary>
        /// 列举指定marker之后的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="marker"></param>
        public void SpecifyMarkerList(T client, string bucketName, string marker);

        /// <summary>
        /// 列举所有文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void AllList(T client, string bucketName);

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void Delete(T client, string bucketName, string objectName);

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void BatchDelete(T client, string bucketName);

        /// <summary>
        /// 拷贝小文件(不支持跨地域拷贝)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopySmallObject(T client, string sourceBucket, string sourceObject, string targetBucket, string targetObject);

        /// <summary>
        /// 拷贝大文件
        /// 分片拷贝（对于大于1GB的文件，建议使用分片拷贝，步骤如下：1.使用 InitiateMultipartUploadRequest 方法来初始化一个分片上传事件;2.使用 UploadPartCopy 方法进行分片拷贝;3.使用 CompleteMultipartUpload 方法完成文件拷贝。）
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopyBigObject(T client, string sourceBucket, string sourceObject, string targetBucket, string targetObject);

        /// <summary>
        /// 断点续传拷贝
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopyBreakPointContinue(T client, string sourceBucket, string sourceObject, string targetBucket, string targetObject);
    }
}
