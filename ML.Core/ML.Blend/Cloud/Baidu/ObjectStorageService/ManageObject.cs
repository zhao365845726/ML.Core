using BaiduBce.Services.Bos;
using BaiduBce.Services.Bos.Model;
using ML.Blend.Cloud.Interface.ObjectStorageService;
using ML.Blend.Unified.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    public class ManageObject : ClientBase
    {
        public ManageObject(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }

        /// <summary>
        /// 查看Bucket中Object列表
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void AllList(BosClient client, string bucketName)
        {
            // 获取指定Bucket下的所有Object信息
            ListObjectsResponse listObjectsResponse = client.ListObjects(bucketName);

            // 遍历所有Object
            foreach (BosObjectSummary objectSummary in listObjectsResponse.Contents)
            {
                Console.WriteLine("ObjectKey: " + objectSummary.Key);
            }
        }

        /// <summary>
        /// 简单的获取Object
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        public void GetObject(BosClient client, String bucketName, String objectKey)
        {

            // 获取Object，返回结果为BosObject对象
            BosObject bosObject = client.GetObject(bucketName, objectKey);

            // 获取ObjectMeta
            ObjectMetadata meta = bosObject.ObjectMetadata;

            // 获取Object的输入流
            Stream objectContent = bosObject.ObjectContent;

            // 处理Object

            // 关闭流
            objectContent.Close();
        }

        /// <summary>
        /// 通过GetObjectRequest获取Object
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        public void GetObjectWithRequest(BosClient client, String bucketName, String objectKey)
        {

            // 新建GetObjectRequest
            GetObjectRequest getObjectRequest = new GetObjectRequest() { BucketName = bucketName, Key = objectKey };

            // 获取0~100字节范围内的数据
            getObjectRequest.SetRange(0, 100);

            // 获取Object，返回结果为BosObject对象
            BosObject bosObject = client.GetObject(getObjectRequest);
        }


        /// <summary>
        /// 只获取ObjectMetadata
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        public void GetObjectMetadata(BosClient client, String bucketName, String objectKey)
        {

            ObjectMetadata objectMetadata = client.GetObjectMetadata(bucketName, objectKey);
        }

        /// <summary>
        /// 获取Object的URL
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="expirationInSeconds"></param>
        /// <returns></returns>
        public string GeneratePresignedUrl(BosClient client, string bucketName, string objectKey, int expirationInSeconds)
        {
            //指定用户需要获取的Object所在的Bucket名称、该Object名称、时间戳、URL的有效时长
            Uri url = client.GeneratePresignedUrl(bucketName, objectKey, expirationInSeconds);
            return url.AbsoluteUri;
        }

        public void BatchDelete(BosClient client, string bucketName)
        {
            throw new NotImplementedException();
        }

        public void CopyBigObject(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
        {
            throw new NotImplementedException();
        }

        public void CopyBreakPointContinue(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
        {
            throw new NotImplementedException();
        }

        public void CopySmallObject(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void Delete(BosClient client, string bucketName, string objectName)
        {
            // 删除Object
            client.DeleteObject(bucketName, objectName);
        }

        /// <summary>
        /// 简单拷贝Object
        /// </summary>
        /// <param name="client"></param>
        /// <param name="srcBucketName"></param>
        /// <param name="srcKey"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destKey"></param>
        public void CopyObject(BosClient client, String srcBucketName, String srcKey, String destBucketName, String destKey)
        {
            // 拷贝Object
            CopyObjectResponse copyObjectResponse = client.CopyObject(srcBucketName, srcKey, destBucketName, destKey);

            // 打印结果
            Console.WriteLine("ETag: " + copyObjectResponse.ETag + " LastModified: " + copyObjectResponse.LastModified);
        }

        /// <summary>
        /// 通过CopyObjectRequest拷贝Object
        /// </summary>
        /// <param name="client"></param>
        /// <param name="srcBucketName"></param>
        /// <param name="srcKey"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destKey"></param>
        public void CopyObjectWithRequest(BosClient client, String srcBucketName, String srcKey, String destBucketName, String destKey)
        {
            // 创建CopyObjectRequest对象
            CopyObjectRequest copyObjectRequest = new CopyObjectRequest()
            {
                SourceBucketName = srcBucketName,
                SourceKey = srcKey,
                BucketName = destBucketName,
                Key = destKey
            };

            // 设置新的Metadata
            Dictionary<String, String> userMetadata = new Dictionary<String, String>();
            userMetadata["usermetakey"] = "usermetavalue";
            ObjectMetadata objectMetadata = new ObjectMetadata()
            {
                UserMetadata = userMetadata
            };
            copyObjectRequest.NewObjectMetadata = objectMetadata;

            // 复制Object并打印新的ETag
            CopyObjectResponse copyObjectResponse = client.CopyObject(copyObjectRequest);
            Console.WriteLine("ETag: " + copyObjectResponse.ETag + " LastModified: " + copyObjectResponse.LastModified);
        }

        public bool DoesExist(BosClient client, string bucketName, string objectName)
        {
            throw new NotImplementedException();
        }

        public void List(BosClient client, string bucketName)
        {
            throw new NotImplementedException();
        }

        public void ObjectMetaData(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SetObjectAcl(BosClient client, string bucketName, string objectName, AccessControlType cannedAccess)
        {
            throw new NotImplementedException();
        }

        public void SpecifyMarkerList(BosClient client, string bucketName, string marker)
        {
            // 构造ListObjectsRequest请求
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest() { BucketName = bucketName };

            // 设置参数
            listObjectsRequest.Marker = marker;

            ListObjectsResponse listObjectsResponse = client.ListObjects(listObjectsRequest);
        }

        public void SpecifyDelimiterList(BosClient client, string bucketName, string delimiter)
        {
            // 构造ListObjectsRequest请求
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest() { BucketName = bucketName };

            // 设置参数
            listObjectsRequest.Delimiter = delimiter;

            ListObjectsResponse listObjectsResponse = client.ListObjects(listObjectsRequest);
        }

        public void SpecifyNumberList(BosClient client, string bucketName, int number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 递归列出模拟文件夹下所有文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="number"></param>
        /// <param name="prefix"></param>
        public void SpecifyPrefixList(BosClient client, string bucketName, string prefix)
        {
            // 构造ListObjectsRequest请求
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest() { BucketName = bucketName };

            // 递归列出fun目录下的所有文件
            listObjectsRequest.Prefix = $"{prefix}/";

            // List Objects
            ListObjectsResponse listObjectsResponse = client.ListObjects(listObjectsRequest);

            // 遍历所有Object
            Console.WriteLine("Objects:");
            foreach (BosObjectSummary objectSummary in listObjectsResponse.Contents)
            {
                Console.WriteLine("ObjectKey: " + objectSummary.Key);
            }
        }

        /// <summary>
        /// 查看模拟文件夹下的文件和子文件夹
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        public void QueryFileAndSubFolderList(BosClient client, string bucketName, string prefix)
        {
            // 构造ListObjectsRequest请求
            ListObjectsRequest listObjectsRequest = new ListObjectsRequest() { BucketName = bucketName };

            // "/" 为文件夹的分隔符
            listObjectsRequest.Delimiter = "/";

            // 列出fun目录下的所有文件和文件夹
            listObjectsRequest.Prefix = $"{prefix}/";

            // List Objects
            ListObjectsResponse listObjectsResponse = client.ListObjects(listObjectsRequest);

            // 遍历所有Object，相当于获取fun目录下的所有文件
            Console.WriteLine("Objects:");
            foreach (BosObjectSummary objectSummary in listObjectsResponse.Contents)
            {
                Console.WriteLine("ObjectKey: " + objectSummary.Key);
            }

            // 遍历所有CommonPrefix，相当于获取fun目录下的所有文件夹
            Console.WriteLine("\nCommonPrefixs:");
            foreach (ObjectPrefix objectPrefix in listObjectsResponse.CommonPrefixes)
            {
                Console.WriteLine(objectPrefix.Prefix);
            }
        }
    }
}
