using Aliyun.OSS;
using Aliyun.OSS.Common;
using Aliyun.OSS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Aliyun.ObjectStorageService
{
    public class ManageObject : ClientBase
    {
        public ManageObject(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public bool DoesExist(OssClient client, string bucketName, string objectName)
        {
            try
            {
                // 判断文件是否存在。
                var exist = client.DoesObjectExist(bucketName, objectName);
                Console.WriteLine("Object exist ? " + exist);
                return exist;
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
                throw new Exception();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
                throw new Exception();
            }
        }

        /// <summary>
        /// 管理文件访问权限
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="cannedAccess"></param>
        public void SetObjectAcl(OssClient client, string bucketName, string objectName,CannedAccessControlList cannedAccess)
        {
            // 设置文件权限。
            try
            {
                // 通过SetObjectAcl设置文件权限。
                client.SetObjectAcl(bucketName, objectName, cannedAccess);
                Console.WriteLine("Set Object:{0} ACL succeeded ", objectName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Set Object ACL failed with error info: {0}", ex.Message);
            }
            // 获取文件权限。
            try
            {
                // 通过GetObjectAcl获取文件权限。
                var result = client.GetObjectAcl(bucketName, objectName);
                Console.WriteLine("Get Object ACL succeeded, Id: {0}  ACL: {1}",
                    result.Owner.Id, result.ACL.ToString());
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID: {2}\tHostID: {3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 管理文件元信息
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void ObjectMetaData(OssClient client, string bucketName, string objectName,string localFilename)
        {
            try
            {
                using (var fs = File.Open(localFilename, FileMode.Open))
                {
                    // 创建上传文件的元信息，可以通过文件元信息设置HTTP header。
                    var metadata = new ObjectMetadata()
                    {
                        // 指定文件类型。
                        ContentType = "text/html",
                        // 设置缓存过期时间，格式是格林威治时间（GMT）。
                        ExpirationTime = DateTime.Parse("2025-10-12T00:00:00.000Z"),
                    };
                    // 设置上传文件的长度。如超过此长度，则会被截断为设置的长度。如不足，则为上传文件的实际长度。
                    metadata.ContentLength = fs.Length;
                    // 设置文件被下载时网页的缓存行为。
                    metadata.CacheControl = "No-Cache";
                    // 设置元信息mykey1值为myval1。
                    metadata.UserMetadata.Add("mykey1", "myval1");
                    // 设置元信息mykey2值为myval2。
                    metadata.UserMetadata.Add("mykey2", "myval2");
                    var saveAsFilename = "文件名测试123.txt";
                    var contentDisposition = string.Format("attachment;filename*=utf-8''{0}", HttpUtils.EncodeUri(saveAsFilename, "utf-8"));
                    // 把请求所得的内容存为一个文件的时候提供一个默认的文件名。
                    metadata.ContentDisposition = contentDisposition;
                    // 上传文件并设置文件元信息。
                    client.PutObject(bucketName, objectName, fs, metadata);
                    Console.WriteLine("Put object succeeded");
                    // 获取文件元信息。
                    var oldMeta = client.GetObjectMetadata(bucketName, objectName);
                    // 设置新的文件元信息。
                    var newMeta = new ObjectMetadata()
                    {
                        ContentType = "application/octet-stream",
                        ExpirationTime = DateTime.Parse("2035-11-11T00:00:00.000Z"),
                        // 指定文件被下载时的内容编码格式。
                        ContentEncoding = null,
                        CacheControl = ""
                    };
                    // 增加自定义元信息。
                    newMeta.UserMetadata.Add("author", "oss");
                    newMeta.UserMetadata.Add("flag", "my-flag");
                    newMeta.UserMetadata.Add("mykey2", "myval2-modified-value");
                    // 通过ModifyObjectMeta方法修改文件元信息。
                    client.ModifyObjectMeta(bucketName, objectName, newMeta);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 简单列举文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void List(OssClient client, string bucketName)
        {
            try
            {
                var listObjectsRequest = new ListObjectsRequest(bucketName);
                // 简单列举存储空间下的文件，默认返回100条记录。
                var result = client.ListObjects(listObjectsRequest);
                Console.WriteLine("List objects succeeded");
                foreach (var summary in result.ObjectSummaries)
                {
                    Console.WriteLine("File name:{0}", summary.Key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("List objects failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 列举指定个数的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="number"></param>
        public void SpecifyNumberList(OssClient client, string bucketName, int number = 200)
        {
            try
            {
                var listObjectsRequest = new ListObjectsRequest(bucketName)
                {
                    // 最大返回200条记录。
                    MaxKeys = number,
                };
                var result = client.ListObjects(listObjectsRequest);
                Console.WriteLine("List objects succeeded");
                foreach (var summary in result.ObjectSummaries)
                {
                    Console.WriteLine(summary.Key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("List objects failed, {0}", ex.Message);
            }
        }

        /// <summary>
        /// 列举指定前缀的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        public void SpecifyPrefixList(OssClient client, string bucketName, int number = 100, string prefix = "")
        {
            try
            {
                var keys = new List<string>();
                ObjectListing result = null;
                string nextMarker = string.Empty;
                do
                {
                    var listObjectsRequest = new ListObjectsRequest(bucketName)
                    {
                        Marker = nextMarker,
                        MaxKeys = number,
                        Prefix = prefix,
                    };
                    result = client.ListObjects(listObjectsRequest);
                    foreach (var summary in result.ObjectSummaries)
                    {
                        Console.WriteLine(summary.Key);
                        keys.Add(summary.Key);
                    }
                    nextMarker = result.NextMarker;
                } while (result.IsTruncated);
                Console.WriteLine("List objects of bucket:{0} succeeded ", bucketName);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 列举指定marker之后的文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="marker"></param>
        public void SpecifyMarkerList(OssClient client, string bucketName,string marker)
        {
            try
            {
                var keys = new List<string>();
                ObjectListing result = null;
                string nextMarker = marker;
                do
                {
                    var listObjectsRequest = new ListObjectsRequest(bucketName)
                    // 若想增大返回文件数目，可以修改MaxKeys参数，或者使用Marker参数分次读取。
                    {
                        Marker = nextMarker,
                        MaxKeys = 100,
                    };
                    result = client.ListObjects(listObjectsRequest);
                    foreach (var summary in result.ObjectSummaries)
                    {
                        Console.WriteLine(summary.Key);
                        keys.Add(summary.Key);
                    }
                    nextMarker = result.NextMarker;
                    // 如果IsTruncated为 true， NextMarker将作为下次读取的起点。
                } while (result.IsTruncated);
                Console.WriteLine("List objects of bucket:{0} succeeded ", bucketName);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 列举所有文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void AllList(OssClient client, string bucketName)
        {
            try
            {
                ObjectListing result = null;
                string nextMarker = string.Empty;
                do
                {
                    // 每页列举的文件个数通过maxKeys指定，超过指定数将进行分页显示。
                    var listObjectsRequest = new ListObjectsRequest(bucketName)
                    {
                        Marker = nextMarker,
                        MaxKeys = 100
                    };
                    result = client.ListObjects(listObjectsRequest);
                    Console.WriteLine("File:");
                    foreach (var summary in result.ObjectSummaries)
                    {
                        Console.WriteLine("Name:{0}", summary.Key);
                    }
                    nextMarker = result.NextMarker;
                } while (result.IsTruncated);
            }
            catch (Exception ex)
            {
                Console.WriteLine("List object failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void Delete(OssClient client, string bucketName, string objectName)
        {
            try
            {
                // 删除文件。
                client.DeleteObject(bucketName, objectName);
                Console.WriteLine("Delete object succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete object failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        public void BatchDelete(OssClient client, string bucketName)
        {
            try
            {
                // 设置需要删除的多个文件完整路径。文件完整路径中不能包含Bucket名称。
                var keys = new List<string>();
                keys.Add("exampleobject.txt");
                keys.Add("testdir/sampleobject.txt");
                // 设置为详细模式，返回所有删除的文件列表。
                var quietMode = false;
                var request = new DeleteObjectsRequest(bucketName, keys, quietMode);
                // 删除多个文件。
                var result = client.DeleteObjects(request);
                if ((!quietMode) && (result.Keys != null))
                {
                    foreach (var obj in result.Keys)
                    {
                        Console.WriteLine("Delete successfully : {0} ", obj.Key);
                    }
                }
                Console.WriteLine("Delete objects succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete objects failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 拷贝小文件(不支持跨地域拷贝)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopySmallObject(OssClient client, string sourceBucket,string sourceObject,string targetBucket,string targetObject)
        {
            try
            {
                var metadata = new ObjectMetadata();
                metadata.AddHeader("mk1", "mv1");
                metadata.AddHeader("mk2", "mv2");
                var req = new CopyObjectRequest(sourceBucket, sourceObject, targetBucket, targetObject)
                {
                    // 如果NewObjectMetadata为null则为COPY模式（即拷贝源文件的元信息），非null则为REPLACE模式（覆盖源文件的元信息）。
                    NewObjectMetadata = metadata
                };
                // 拷贝文件。
                client.CopyObject(req);
                Console.WriteLine("Copy object succeeded");
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID: {2} \tHostID: {3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        /// <summary>
        /// 拷贝大文件
        /// 分片拷贝（对于大于1GB的文件，建议使用分片拷贝，步骤如下：1.使用 InitiateMultipartUploadRequest 方法来初始化一个分片上传事件;2.使用 UploadPartCopy 方法进行分片拷贝;3.使用 CompleteMultipartUpload 方法完成文件拷贝。）
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopyBigObject(OssClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
        {
            var uploadId = "";
            var partSize = 50 * 1024 * 1024;
            try
            {
                // 初始化拷贝任务。可以通过InitiateMultipartUploadRequest指定目标文件元信息。
                var request = new InitiateMultipartUploadRequest(targetBucket, targetObject);
                var result = client.InitiateMultipartUpload(request);
                // 打印uploadId。
                uploadId = result.UploadId;
                Console.WriteLine("Init multipart upload succeeded， Upload Id: {0}", result.UploadId);
                // 计算分片数。
                var metadata = client.GetObjectMetadata(sourceBucket, sourceObject);
                var fileSize = metadata.ContentLength;
                var partCount = (int)fileSize / partSize;
                if (fileSize % partSize != 0)
                {
                    partCount++;
                }
                // 开始分片拷贝。
                var partETags = new List<PartETag>();
                for (var i = 0; i < partCount; i++)
                {
                    var skipBytes = (long)partSize * i;
                    var size = (partSize < fileSize - skipBytes) ? partSize : (fileSize - skipBytes);
                    // 创建UploadPartCopyRequest。可以通过UploadPartCopyRequest指定限定条件。
                    var uploadPartCopyRequest = new UploadPartCopyRequest(targetBucket, targetObject, sourceBucket, sourceObject, uploadId)
                    {
                        PartSize = size,
                        PartNumber = i + 1,
                        // BeginIndex用来定位此次上传分片开始所对应的位置。
                        BeginIndex = skipBytes
                    };
                    // 调用uploadPartCopy方法来拷贝每一个分片。
                    var uploadPartCopyResult = client.UploadPartCopy(uploadPartCopyRequest);
                    Console.WriteLine("UploadPartCopy : {0}", i);
                    partETags.Add(uploadPartCopyResult.PartETag);
                }
                // 完成分片拷贝。
                var completeMultipartUploadRequest =
                new CompleteMultipartUploadRequest(targetBucket, targetObject, uploadId);
                // partETags为分片上传中保存的partETag的列表，OSS收到用户提交的此列表后，会逐一验证每个数据分片的有效性。全部验证通过后，OSS会将这些分片合成一个完整的文件。
                foreach (var partETag in partETags)
                {
                    completeMultipartUploadRequest.PartETags.Add(partETag);
                }
                var completeMultipartUploadResult = client.CompleteMultipartUpload(completeMultipartUploadRequest);
                Console.WriteLine("CompleteMultipartUpload succeeded");
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID: {2} \tHostID: {3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }
    
        /// <summary>
        /// 断点续传拷贝
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceObject"></param>
        /// <param name="targetBucket"></param>
        /// <param name="targetObject"></param>
        public void CopyBreakPointContinue(OssClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
        {
            string checkpointDir = $"checkPoint{targetObject}";
            try
            {
                var request = new CopyObjectRequest(sourceBucket, sourceObject, targetBucket, targetObject);
                // checkpointDir目录中会保存断点续传的中间状态，用于失败后，下次继续拷贝。如果checkpointDir为null，断点续传功能不会生效，每次都会重新拷贝。
                client.ResumableCopyObject(request, checkpointDir);
                Console.WriteLine("Resumable copy new object:{0} succeeded", request.DestinationKey);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Resumable copy new object failed, {0}", ex.Message);
            }
        }
    }
}
