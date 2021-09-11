//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using Aliyun.OSS;
//using Aliyun.OSS.Common;
//using Aliyun.OSS.Util;

//namespace ML.Blend.Cloud.Interface.ObjectStorageService
//{
//    /// <summary>
//    /// 上传文件
//    /// </summary>
//    public interface IUpload
//    {

//        /// <summary>
//        /// 简单上传本地文件
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="localFileName"></param>
//        public void SimpleFile(OssClient client, string bucketName,string objectName,string localFilename)
//        {
//            try
//            {
//                // 上传文件。
//                var result = client.PutObject(bucketName, objectName, localFilename);
//                Console.WriteLine("Put object succeeded, ETag: {0} ", result.ETag);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Put object failed, {0}", ex.Message);
//            }
//        }

//        /// <summary>
//        /// 简单上传字符串
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="objectContent"></param>
//        public void SimpleString(OssClient client, string bucketName, string objectName, string objectContent)
//        {
//            try
//            {
//                byte[] binaryData = Encoding.ASCII.GetBytes(objectContent);
//                MemoryStream requestContent = new MemoryStream(binaryData);
//                // 上传文件。
//                client.PutObject(bucketName, objectName, requestContent);
//                Console.WriteLine("Put object succeeded");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Put object failed, {0}", ex.Message);
//            }
//        }

//        /// <summary>
//        /// 简单上传文件带MD5校验
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="localFilename"></param>
//        public void SimpleFileWithValidate(OssClient client, string bucketName, string objectName, string localFilename)
//        {
//            try
//            {
//                // 计算MD5。
//                string md5;
//                using (var fs = File.Open(localFilename, FileMode.Open))
//                {
//                    md5 = OssUtils.ComputeContentMd5(fs, fs.Length);
//                }
//                var objectMeta = new ObjectMetadata
//                {
//                    ContentMd5 = md5
//                };
//                // 上传文件。
//                client.PutObject(bucketName, objectName, localFilename, objectMeta);
//                Console.WriteLine("Put object succeeded");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Put object failed, {0}", ex.Message);
//            }
//        }

//        /// <summary>
//        /// 追加上传
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="localFilename"></param>
//        public void Append(OssClient client, string bucketName, string objectName, string localFilename)
//        {
//            // 第一次追加的位置是0，返回值为下一次追加的位置。后续追加的位置是追加前文件的长度。
//            long position = 0;
//            try
//            {
//                var metadata = client.GetObjectMetadata(bucketName, objectName);
//                position = metadata.ContentLength;
//            }
//            catch (Exception) { }
//            try
//            {
//                using (var fs = File.Open(localFilename, FileMode.Open))
//                {
//                    var request = new AppendObjectRequest(bucketName, objectName)
//                    {
//                        ObjectMetadata = new ObjectMetadata(),
//                        Content = fs,
//                        Position = position
//                    };
//                    // 追加文件。
//                    var result = client.AppendObject(request);
//                    // 设置文件的追加位置。
//                    position = result.NextAppendPosition;
//                    Console.WriteLine("Append object succeeded, next append position:{0}", position);
//                }
//                // 获取追加位置，再次追加文件。
//                using (var fs = File.Open(localFilename, FileMode.Open))
//                {
//                    var request = new AppendObjectRequest(bucketName, objectName)
//                    {
//                        ObjectMetadata = new ObjectMetadata(),
//                        Content = fs,
//                        Position = position
//                    };
//                    var result = client.AppendObject(request);
//                    position = result.NextAppendPosition;
//                    Console.WriteLine("Append object succeeded, next append position:{0}", position);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Append object failed, {0}", ex.Message);
//            }
//        }

//        /// <summary>
//        /// 断点续传
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="localFilename"></param>
//        public void BreakPointContinue(OssClient client, string bucketName, string objectName, string localFilename)
//        {
//            string checkpointDir = $"checkPoint{objectName}";
//            try
//            {
//                // 通过UploadFileRequest设置多个参数。
//                UploadObjectRequest request = new UploadObjectRequest(bucketName, objectName, localFilename)
//                {
//                    // 指定上传的分片大小。
//                    PartSize = 8 * 1024 * 1024,
//                    // 指定并发线程数。
//                    ParallelThreadCount = 3,
//                    // checkpointDir保存断点续传的中间状态，用于失败后继续上传。如果checkpointDir为null，断点续传功能不会生效，每次失败后都会重新上传。
//                    CheckpointDir = checkpointDir,
//                };
//                // 断点续传上传。
//                client.ResumableUploadObject(request);
//                Console.WriteLine("Resumable upload object:{0} succeeded", objectName);
//            }
//            catch (OssException ex)
//            {
//                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
//                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed with error info: {0}", ex.Message);
//            }
//        }

//        public void Slice(OssClient client, string bucketName, string objectName, string localFilename)
//        {

//        }

//        /// <summary>
//        /// 进度条
//        /// </summary>
//        /// <param name="client"></param>
//        /// <param name="bucketName"></param>
//        /// <param name="objectName"></param>
//        /// <param name="localFilename"></param>
//        public void PutObjectProgress(OssClient client, string bucketName, string objectName, string localFilename)
//        {
//            // 带进度条的上传。
//            try
//            {
//                using (var fs = File.Open(localFilename, FileMode.Open))
//                {
//                    var putObjectRequest = new PutObjectRequest(bucketName, objectName, fs);
//                    putObjectRequest.StreamTransferProgress += streamProgressCallback;
//                    client.PutObject(putObjectRequest);
//                }
//                Console.WriteLine("Put object:{0} succeeded", objectName);
//            }
//            catch (OssException ex)
//            {
//                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID: {2}\tHostID: {3}",
//                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Failed with error info: {0}", ex.Message);
//            }
//        }

//        /// <summary>
//        /// 获取上传进度
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="args"></param>
//        public void streamProgressCallback(object sender, StreamTransferProgressArgs args)
//        {
//            System.Console.WriteLine("ProgressCallback - Progress: {0}%, TotalBytes:{1}, TransferredBytes:{2} ",
//                args.TransferredBytes * 100 / args.TotalBytes, args.TotalBytes, args.TransferredBytes);
//        }
//    }
//}
