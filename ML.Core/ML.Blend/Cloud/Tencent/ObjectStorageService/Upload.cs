using COSXML;
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

        #endregion

        #region 简单操作
        /// <summary>
        /// 简单上传本地文件
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        public void SimpleFile(CosXml cosXml, string bucketName, string objectName, string localFilename)
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







        ///// <summary>
        ///// 断点续传
        ///// </summary>
        ///// <param name="client"></param>
        ///// <param name="bucketName"></param>
        ///// <param name="objectName"></param>
        ///// <param name="localFilename"></param>
        //public void BreakPointContinue(OssClient client, string bucketName, string objectName, string localFilename)
        //{
        //    string checkpointDir = $"checkPoint{objectName}";
        //    try
        //    {
        //        // 通过UploadFileRequest设置多个参数。
        //        UploadObjectRequest request = new UploadObjectRequest(bucketName, objectName, localFilename)
        //        {
        //            // 指定上传的分片大小。
        //            PartSize = 8 * 1024 * 1024,
        //            // 指定并发线程数。
        //            ParallelThreadCount = 3,
        //            // checkpointDir保存断点续传的中间状态，用于失败后继续上传。如果checkpointDir为null，断点续传功能不会生效，每次失败后都会重新上传。
        //            CheckpointDir = checkpointDir,
        //        };
        //        // 断点续传上传。
        //        client.ResumableUploadObject(request);
        //        Console.WriteLine("Resumable upload object:{0} succeeded", objectName);
        //    }
        //    catch (OssException ex)
        //    {
        //        Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
        //            ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failed with error info: {0}", ex.Message);
        //    }
        //}

        //public void Slice(OssClient client, string bucketName, string objectName, string localFilename)
        //{

        //}

        ///// <summary>
        ///// 进度条
        ///// </summary>
        ///// <param name="client"></param>
        ///// <param name="bucketName"></param>
        ///// <param name="objectName"></param>
        ///// <param name="localFilename"></param>
        //public void PutObjectProgress(OssClient client, string bucketName, string objectName, string localFilename)
        //{
        //    // 带进度条的上传。
        //    try
        //    {
        //        using (var fs = File.Open(localFilename, FileMode.Open))
        //        {
        //            var putObjectRequest = new PutObjectRequest(bucketName, objectName, fs);
        //            putObjectRequest.StreamTransferProgress += streamProgressCallback;
        //            client.PutObject(putObjectRequest);
        //        }
        //        Console.WriteLine("Put object:{0} succeeded", objectName);
        //    }
        //    catch (OssException ex)
        //    {
        //        Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID: {2}\tHostID: {3}",
        //            ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failed with error info: {0}", ex.Message);
        //    }
        //}

        ///// <summary>
        ///// 获取上传进度
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //public void streamProgressCallback(object sender, StreamTransferProgressArgs args)
        //{
        //    System.Console.WriteLine("ProgressCallback - Progress: {0}%, TotalBytes:{1}, TransferredBytes:{2} ",
        //        args.TransferredBytes * 100 / args.TotalBytes, args.TotalBytes, args.TransferredBytes);
        //}
    }
}
