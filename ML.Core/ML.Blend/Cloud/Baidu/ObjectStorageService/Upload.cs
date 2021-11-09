using BaiduBce.Services.Bos;
using BaiduBce.Services.Bos.Model;
using ML.Blend.Cloud.Interface.ObjectStorageService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload : ClientBase, IUpload<BosClient>
    {
        public Upload(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
        {
        }

        #region 分块上传   Block
        /// <summary>
        /// 分块上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void BlockFileObject(BosClient client, String bucketName, String objectKey, string localFileName)
        {

            try
            {
                // 初始化：创建示例Bucket
                client.CreateBucket(bucketName);

                // 开始Multipart Upload
                InitiateMultipartUploadRequest initiateMultipartUploadRequest =
                    new InitiateMultipartUploadRequest() { BucketName = bucketName, Key = objectKey };
                InitiateMultipartUploadResponse initiateMultipartUploadResponse =
                    client.InitiateMultipartUpload(initiateMultipartUploadRequest);

                // 获取Bucket内的Multipart Upload
                ListMultipartUploadsRequest listMultipartUploadsRequest =
                    new ListMultipartUploadsRequest() { BucketName = bucketName };
                ListMultipartUploadsResponse listMultipartUploadsResponse =
                    client.ListMultipartUploads(listMultipartUploadsRequest);
                foreach (MultipartUploadSummary multipartUpload in listMultipartUploadsResponse.Uploads)
                {
                    Console.WriteLine("Key: " + multipartUpload.Key + " UploadId: " + multipartUpload.UploadId);
                }

                // 分块上传，首先设置每块为 5Mb
                long partSize = 1024 * 1024 * 5L;
                FileInfo partFile = new FileInfo("d:\\lzb\\sample");

                // 计算分块数目
                int partCount = (int)(partFile.Length / partSize);
                if (partFile.Length % partSize != 0)
                {
                    partCount++;
                }

                // 新建一个List保存每个分块上传后的ETag和PartNumber
                List<PartETag> partETags = new List<PartETag>();
                for (int i = 0; i < partCount; i++)
                {
                    // 获取文件流
                    Stream stream = partFile.OpenRead();

                    // 跳到每个分块的开头
                    long skipBytes = partSize * i;
                    stream.Seek(skipBytes, SeekOrigin.Begin);

                    // 计算每个分块的大小
                    long size = Math.Min(partSize, partFile.Length - skipBytes);

                    // 创建UploadPartRequest，上传分块
                    UploadPartRequest uploadPartRequest = new UploadPartRequest();
                    uploadPartRequest.BucketName = bucketName;
                    uploadPartRequest.Key = objectKey;
                    uploadPartRequest.UploadId = initiateMultipartUploadResponse.UploadId;
                    uploadPartRequest.InputStream = stream;
                    uploadPartRequest.PartSize = size;
                    uploadPartRequest.PartNumber = i + 1;
                    UploadPartResponse uploadPartResponse = client.UploadPart(uploadPartRequest);

                    // 将返回的PartETag保存到List中。
                    partETags.Add(new PartETag()
                    {
                        ETag = uploadPartResponse.ETag,
                        PartNumber = uploadPartResponse.PartNumber
                    });

                    // 关闭文件
                    stream.Close();
                }

                // 获取UploadId的所有Upload Part
                ListPartsRequest listPartsRequest = new ListPartsRequest()
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    UploadId = initiateMultipartUploadResponse.UploadId,
                };

                // 获取上传的所有Part信息
                ListPartsResponse listPartsResponse = client.ListParts(listPartsRequest);

                // 遍历所有Part
                foreach (PartSummary part in listPartsResponse.Parts)
                {
                    Console.WriteLine("PartNumber: " + part.PartNumber + " ETag: " + part.ETag);
                }

                // 完成分块上传
                CompleteMultipartUploadRequest completeMultipartUploadRequest =
                    new CompleteMultipartUploadRequest()
                    {
                        BucketName = bucketName,
                        Key = objectKey,
                        UploadId = initiateMultipartUploadResponse.UploadId,
                        PartETags = partETags
                    };
                CompleteMultipartUploadResponse completeMultipartUploadResponse =
                    client.CompleteMultipartUpload(completeMultipartUploadRequest);
                Console.WriteLine(completeMultipartUploadResponse.ETag);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        #endregion

        #region 简单上传   Simple
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void SimpleFileObject(BosClient client, String bucketName, String objectKey, string localFileName)
        {

            try
            {
                // 新建一个Bucket
                client.CreateBucket(bucketName); //指定Bucket名称

                // 以文件形式上传Object
                PutObjectResponse putObjectFromFileResponse = client.PutObject(bucketName, objectKey, new FileInfo(localFileName));

                // 打印四种方式的ETag。示例中，文件方式和stream方式的ETag相等，string方式和byte方式的ETag相等
                Console.WriteLine(putObjectFromFileResponse.ETAG);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// 二进制上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void SimpleByteData(BosClient client, String bucketName, String objectKey, string localFileName)
        {

            try
            {
                // 新建一个Bucket
                client.CreateBucket(bucketName); //指定Bucket名称

                // 以二进制串上传Object
                PutObjectResponse putObjectResponseFromByte = client.PutObject(bucketName, objectKey, Encoding.Default.GetBytes("sampledata"));

                Console.WriteLine(putObjectResponseFromByte.ETAG);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// 流数据上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void SimpleStreamData(BosClient client, String bucketName, String objectKey, string localFileName)
        {

            try
            {
                // 新建一个Bucket
                client.CreateBucket(bucketName); //指定Bucket名称

                // 以数据流形式上传Object
                PutObjectResponse putObjectResponseFromInputStream = client.PutObject(bucketName, objectKey, new FileInfo(localFileName).OpenRead());

                Console.WriteLine(putObjectResponseFromInputStream.ETAG);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// 字符串上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void SimpleString(BosClient client, String bucketName, String objectKey,string localFileName)
        {

            try
            {
                // 新建一个Bucket
                client.CreateBucket(bucketName); //指定Bucket名称

                // 以字符串上传Object
                PutObjectResponse putObjectResponseFromString = client.PutObject(bucketName, objectKey, "sampledata");

                Console.WriteLine(putObjectResponseFromString.ETAG);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// 字符串上传
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectKey"></param>
        /// <param name="localFileName"></param>
        public void SimpleStringWithCustom(BosClient client, String bucketName, String objectKey, string localFileName)
        {

            try
            {
                // 新建一个Bucket
                client.CreateBucket(bucketName); //指定Bucket名称
                // 以字符串上传Object
                PutObjectResponse putObjectResponseFromString = client.PutObject(bucketName, objectKey, "sampledata");

                // 上传Object并设置自定义参数
                ObjectMetadata meta = new ObjectMetadata();
                // 设置ContentLength大小
                meta.ContentLength = 10;
                // 设置ContentType
                meta.ContentType = "application/json";
                // 设置自定义元数据name的值为my-data
                meta.UserMetadata["name"] = "my-data";
                // 上传Object并打印ETag
                putObjectResponseFromString = client.PutObject(bucketName, objectKey, "sampledata", meta);
                Console.WriteLine(putObjectResponseFromString.ETAG);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        #endregion

        public void Append(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void BreakPointContinue(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        

        public void PutObjectProgress(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleFile(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleFileWithValidate(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void SimpleString(BosClient client, string bucketName, string objectName, string objectContent)
        {
            throw new NotImplementedException();
        }

        public void Slice(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }
    }
}
