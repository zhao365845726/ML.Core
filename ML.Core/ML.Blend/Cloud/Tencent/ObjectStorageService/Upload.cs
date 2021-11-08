using COSXML;
using COSXML.Model.Bucket;
using COSXML.Model.Object;
using COSXML.Model.Tag;
using COSXML.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ML.Blend.Cloud.Tencent.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload : ClientBase
    {
        private string uploadId = string.Empty;
        private string eTag = string.Empty;

        public Upload(string akId, string akSecret, string region) : base(akId, akSecret, region)
        {
        }

        #region 高级接口
        /// <summary>
        /// 上传本地文件
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public async Task UploadLocalFile(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            //手动设置分块上传阈值，小于阈值的对象使用简单上传，大于阈值的对象使用分块上传，不设定则默认为5MB
            transferConfig.DivisionForUpload = 5242880;
            //手动设置高级接口的自动分块大小，不设定则默认为1MB
            transferConfig.SliceSizeForUpload = 2097152;

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            // 上传对象
            COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucketName, objectName);
            uploadTask.SetSrcPath(localFilename);

            uploadTask.progressCallback = delegate (long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            try
            {
                COSXML.Transfer.COSXMLUploadTask.UploadTaskResult result = await
                  transferManager.UploadAsync(uploadTask);
                Console.WriteLine(result.GetResultInfo());
                string eTag = result.eTag;
            }
            catch (Exception e)
            {
                Console.WriteLine("CosException: " + e);
            }
        }

        /// <summary>
        /// 简单上传字符串
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="objectContent"></param>
        public void SimpleString(CosXml cosXml, string bucketName, string objectName, string objectContent)
        {
            try
            {
                byte[] binaryData = Encoding.ASCII.GetBytes(objectContent);
                PutObjectRequest putObjectRequest = new PutObjectRequest(bucketName, objectName, binaryData);

                cosXml.PutObject(putObjectRequest);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 高级上传字节数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="objectContent"></param>
        public void SeniorUploadByteData(CosXml cosXml, string bucketName, string objectName, string objectContent)
        {
            try
            {
                byte[] data = new byte[1024]; // 二进制数据
                PutObjectRequest putObjectRequest = new PutObjectRequest(bucketName, objectName, data);
                // 发起请求
                PutObjectResult result = cosXml.PutObject(putObjectRequest);
                // 对象的 eTag
                string eTag = result.eTag;
                // 对象的 crc64ecma 校验值
                //string crc64ecma = result.crc64ecma;
                // 打印请求结果
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 高级上传文件流数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void SeniorUploadFileStreamData(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            try
            {
                FileStream fileStream = new FileStream(localFilename, FileMode.Open, FileAccess.Read);
                // 组装上传请求，其中 offset sendLength 为可选参数
                long offset = 0L;
                long sendLength = fileStream.Length;
                PutObjectRequest request = new PutObjectRequest(bucketName, objectName, fileStream.ToString(), offset, sendLength);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //关闭文件流
                fileStream.Close();
                //对象的 eTag
                string eTag = result.eTag;
                //对象的 crc64ecma 校验值
                //string crc64ecma = result.crc64ecma;
                //打印请求结果
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 高级批量上传数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename">本地文件绝对路径</param>
        public async Task SeniorBatchUploadDataAsync(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            for (int i = 0; i < 5; i++)
            {
                // 上传对象
                string cosPath = objectName + i; //对象在存储桶中的位置标识符，即称对象键
                COSXMLUploadTask uploadTask = new COSXMLUploadTask(bucketName, cosPath);
                uploadTask.SetSrcPath(localFilename);
                await transferManager.UploadAsync(uploadTask);
            }
        }

        /// <summary>
        /// 高级创建目录
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void SeniorCreateFolder(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                string cosPath = $"{objectName}/"; // 对象键
                PutObjectRequest putObjectRequest = new PutObjectRequest(bucketName, cosPath, new byte[0]);

                cosXml.PutObject(putObjectRequest);
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 高级复制对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid"></param>
        /// <param name="sourceBucketName"></param>
        /// <param name="sourceRegion"></param>
        /// <param name="sourceObject"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <returns></returns>
        public async Task SeniorCopyObjectAsync(CosXml cosXml,string sourceAppid,string sourceBucketName,string sourceRegion, string sourceObject, string destBucketName, string destObjectName)
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();
            //手动设置分块复制阈值，小于阈值的对象使用简单复制，大于阈值的对象使用分块复制，不设定则默认为5MB
            transferConfig.DivisionForUpload = 5242880;
            //手动设置高级接口的自动分块大小，不设定则默认为2MB
            transferConfig.SliceSizeForCopy = 2097152;
            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucketName,sourceRegion, sourceObject);
            COSXMLCopyTask copytask = new COSXMLCopyTask(destBucketName, destObjectName, copySource);
            try
            {
                COSXML.Transfer.COSXMLCopyTask.CopyTaskResult result = await
                  transferManager.CopyAsync(copytask);
                Console.WriteLine(result.GetResultInfo());
                string eTag = result.eTag;
            }
            catch (Exception e)
            {
                Console.WriteLine("CosException: " + e);
            }
        }

        #endregion

        #region 简单操作
        /// <summary>
        /// 简单上传本地文件
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public string SimpleFile(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            try
            {
                PutObjectRequest request = new PutObjectRequest(bucketName, objectName, localFilename);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PutObjectResult result = cosXml.PutObject(request);
                //对象的 eTag
                string eTag = result.eTag;
                return eTag;
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
                return clientEx.Message;
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
                return serverEx.Message;
            }
        }

        /// <summary>
        /// 表单上传对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void SimpleFileWithForm(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            try
            {
                PostObjectRequest request = new PostObjectRequest(bucketName, objectName, localFilename);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                PostObjectResult result = cosXml.PostObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 复制对象（修改属性）
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid"></param>
        /// <param name="sourceBucket"></param>
        /// <param name="sourceRegion"></param>
        /// <param name="sourceKey"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        public void CopyFileToOther(CosXml cosXml, string sourceAppid, string sourceBucket, string sourceRegion, string sourceKey, string destBucketName, string destObjectName)
        {
            try
            {
                CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucket,
                  sourceRegion, sourceKey);
                CopyObjectRequest request = new CopyObjectRequest(destBucketName, destObjectName);
                //设置拷贝源
                request.SetCopySource(copySource);
                //设置是否拷贝还是更新,此处是拷贝
                request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.Copy);
                //执行请求
                CopyObjectResult result = cosXml.CopyObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 复制对象时替换对象属性
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid">账号 appid</param>
        /// <param name="sourceBucket">源对象所在的存储桶</param>
        /// <param name="sourceRegion">源对象的存储桶所在的地域</param>
        /// <param name="sourceKey">源对象键</param>
        /// <param name="destBucketName">目标存储桶</param>
        /// <param name="destObjectName">目标对象键</param>
        public void CopyFileToOtherReplaceAttribute(CosXml cosXml, string sourceAppid, string sourceBucket, string sourceRegion, string sourceKey, string destBucketName, string destObjectName)
        {
            try
            {
                CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucket,
                  sourceRegion, sourceKey);
                CopyObjectRequest request = new CopyObjectRequest(destBucketName, destObjectName);
                //设置拷贝源
                request.SetCopySource(copySource);
                //设置是否拷贝还是更新,此处是拷贝
                request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.Replaced);
                // 替换元数据
                request.SetRequestHeader("Content-Disposition", "attachment; filename=example.jpg");
                //执行请求
                CopyObjectResult result = cosXml.CopyObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 修改对象元数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid">账号 appid</param>
        /// <param name="sourceBucket">源对象所在的存储桶</param>
        /// <param name="sourceRegion">源对象的存储桶所在的地域</param>
        /// <param name="sourceKey">源对象键</param>
        /// <param name="destBucketName">目标存储桶</param>
        /// <param name="destObjectName">目标对象键</param>
        public void CopyFileToOtherModifyObjectMetadata(CosXml cosXml, string sourceAppid, string sourceBucket, string sourceRegion, string sourceKey, string destBucketName, string destObjectName)
        {
            try
            {
                //构造对象属性
                CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucket,
                  sourceRegion, sourceKey);

                CopyObjectRequest request = new CopyObjectRequest(destBucketName, destObjectName);
                //设置拷贝源
                request.SetCopySource(copySource);
                //设置是否拷贝还是更新,此处是拷贝
                request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.Replaced);
                // 替换元数据
                request.SetRequestHeader("Content-Disposition", "attachment; filename=example.jpg");
                request.SetRequestHeader("Content-Type", "image/png");
                //执行请求
                CopyObjectResult result = cosXml.CopyObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 修改对象存储类型
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid">账号 appid</param>
        /// <param name="sourceBucket">源对象所在的存储桶</param>
        /// <param name="sourceRegion">源对象的存储桶所在的地域</param>
        /// <param name="sourceKey">源对象键</param>
        /// <param name="destBucketName">目标存储桶</param>
        /// <param name="destObjectName">目标对象键</param>
        public void CopyFileToOtherModifyStorageType(CosXml cosXml, string sourceAppid, string sourceBucket, string sourceRegion, string sourceKey, string destBucketName, string destObjectName)
        {
            try
            {
                //构造对象属性
                CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucket,
                  sourceRegion, sourceKey);

                CopyObjectRequest request = new CopyObjectRequest(destBucketName, destObjectName);
                //设置拷贝源
                request.SetCopySource(copySource);
                //设置是否拷贝还是更新,此处是拷贝
                request.SetCopyMetaDataDirective(COSXML.Common.CosMetaDataDirective.Replaced);
                // 修改为归档存储
                request.SetCosStorageClass("ARCHIVE");
                //执行请求
                CopyObjectResult result = cosXml.CopyObject(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 追加上传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void Append(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            try
            {
                //首次append上传,追加位置传0,创建一个appendable对象
                long next_append_position = 0;
                AppendObjectRequest request = new AppendObjectRequest(bucketName, objectName, localFilename, next_append_position);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                AppendObjectResult result = cosXml.AppendObject(request);
                //获取下次追加位置
                next_append_position = result.nextAppendPosition;
                Console.WriteLine(result.GetResultInfo());

                //执行追加,传入上次获取的对象末尾
                request = new AppendObjectRequest(bucketName, objectName, localFilename, next_append_position);
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                result = cosXml.AppendObject(request);
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }
        #endregion

        #region 分块操作 Block
        /// <summary>
        /// 查询分块上传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        public void BlockQuery(CosXml cosXml, string bucketName)
        {
            try
            {
                ListMultiUploadsRequest request = new ListMultiUploadsRequest(bucketName);
                //执行请求
                ListMultiUploadsResult result = cosXml.ListMultiUploads(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 初始化分块上传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void BlockInitialization(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                InitMultipartUploadRequest request = new InitMultipartUploadRequest(bucketName, objectName);
                //执行请求
                InitMultipartUploadResult result = cosXml.InitMultipartUpload(request);
                //请求成功
                this.uploadId = result.initMultipartUpload.uploadId; //用于后续分块上传的 uploadId
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 上传分块
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void BlockUpload(CosXml cosXml, string bucketName, string objectName, string localFilename)
        {
            try
            {
                string uploadId = "exampleUploadId"; //初始化分块上传返回的uploadId
                int partNumber = 1; //分块编号，必须从1开始递增
                UploadPartRequest request = new UploadPartRequest(bucketName, objectName, partNumber,uploadId, localFilename, 0, -1);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                UploadPartResult result = cosXml.UploadPart(request);
                //请求成功
                //获取返回分块的eTag,用于后续CompleteMultiUploads
                this.eTag = result.eTag;
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 复制分块
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid"></param>
        /// <param name="sourceBucketName"></param>
        /// <param name="sourceRegion"></param>
        /// <param name="sourceObjectName"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <returns></returns>
        public void BlockCopy(CosXml cosXml,string sourceAppid,string sourceBucketName,string sourceRegion,string sourceObjectName, string destBucketName, string destObjectName)
        {
            try
            {
                COSXML.Model.Tag.CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucketName, sourceRegion, sourceObjectName);
                string uploadId = this.uploadId; //初始化分块上传返回的uploadId
                int partNumber = 1; //分块编号，必须从1开始递增
                UploadPartCopyRequest request = new UploadPartCopyRequest(destBucketName, destObjectName, partNumber, uploadId);
                //设置拷贝源
                request.SetCopySource(copySource);
                //设置复制分块（指定块的范围，如 0 ~ 1M）
                request.SetCopyRange(0, 1024 * 1024);
                //执行请求
                UploadPartCopyResult result = cosXml.PartCopy(request);
                //请求成功
                //获取返回分块的eTag,用于后续CompleteMultiUploads
                this.eTag = result.copyPart.eTag;
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 查询已上传的分块
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void BlockQueryUploaded(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                string uploadId = "exampleUploadId"; //初始化分块上传返回的uploadId
                ListPartsRequest request = new ListPartsRequest(bucketName, objectName, uploadId);
                //执行请求
                ListPartsResult result = cosXml.ListParts(request);
                //请求成功
                //列举已上传的分块
                List<COSXML.Model.Tag.ListParts.Part> alreadyUploadParts = result.listParts.parts;
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 完成分块上传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void BlockFinishedUpload(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                string uploadId = "exampleUploadId"; //初始化分块上传返回的uploadId
                CompleteMultipartUploadRequest request = new CompleteMultipartUploadRequest(bucketName, objectName, uploadId);
                //设置已上传的parts,必须有序，按照partNumber递增
                request.SetPartNumberAndETag(1, this.eTag);
                //执行请求
                CompleteMultipartUploadResult result = cosXml.CompleteMultiUpload(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }

        /// <summary>
        /// 终止分块上传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void BlockTerminationUpload(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                string uploadId = "exampleUploadId"; //初始化分块上传返回的uploadId
                AbortMultipartUploadRequest request = new AbortMultipartUploadRequest(bucketName, objectName, uploadId);
                //执行请求
                AbortMultipartUploadResult result = cosXml.AbortMultiUpload(request);
                //请求成功
                Console.WriteLine(result.GetResultInfo());
            }
            catch (COSXML.CosException.CosClientException clientEx)
            {
                //请求失败
                Console.WriteLine("CosClientException: " + clientEx);
            }
            catch (COSXML.CosException.CosServerException serverEx)
            {
                //请求失败
                Console.WriteLine("CosServerException: " + serverEx.GetInfo());
            }
        }
        #endregion
    }
}
