using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace ML.Blend.Cloud.Baidu.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class Download : ClientBase
    {
        public Download(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 流式下载
        /// </summary>
        /// <param name="client">Oss客户端对象</param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void FlowType(OssClient client, string bucketName, string objectName, string downloadFilename)
        {
            try
            {
                // 下载文件。
                var result = client.GetObject(bucketName, objectName);
                using (var requestStream = result.Content)
                {
                    using (var fs = File.Open(downloadFilename, FileMode.OpenOrCreate))
                    {
                        int length = 4 * 1024;
                        var buf = new byte[length];
                        do
                        {
                            length = requestStream.Read(buf, 0, length);
                            fs.Write(buf, 0, length);
                        } while (length != 0);
                    }
                }
                Console.WriteLine("Get object succeeded");
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
        /// 范围下载
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void Range(OssClient client, string bucketName, string objectName, string downloadFilename)
        {
            try
            {
                var getObjectRequest = new GetObjectRequest(bucketName, objectName);
                // 设置Range，取值范围为第20至第100字节。
                getObjectRequest.SetRange(20, 100);
                // 范围下载。getObjectRequest的setRange可以实现文件的分段下载和断点续传。
                var obj = client.GetObject(getObjectRequest);
                // 下载数据并写入文件。
                using (var requestStream = obj.Content)
                {
                    byte[] buf = new byte[1024];
                    var fs = File.Open(downloadFilename, FileMode.OpenOrCreate);
                    var len = 0;
                    while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                    {
                        fs.Write(buf, 0, len);
                    }
                    fs.Close();
                }
                Console.WriteLine("Get object succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Get object failed. {0}", ex.Message);
            }
        }

        /// <summary>
        /// 断点续传下载
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void BreakPointContinue(OssClient client, string bucketName, string objectName, string downloadFilename)
        {
            string checkpointDir = $"checkPoint{objectName}";
            try
            {
                // 通过DownloadObjectRequest设置多个参数。
                DownloadObjectRequest request = new DownloadObjectRequest(bucketName, objectName, downloadFilename)
                {
                    // 指定下载的分片大小。
                    PartSize = 8 * 1024 * 1024,
                    // 指定并发线程数。
                    ParallelThreadCount = 3,
                    // checkpointDir用于保存断点续传进度信息。如果某一分片下载失败，再次下载时会根据文件中记录的点继续下载。如果checkpointDir为null，断点续传功能不会生效，每次失败后都会重新下载。
                    CheckpointDir = checkpointDir,
                };
                // 断点续传下载。
                client.ResumableDownloadObject(request);
                Console.WriteLine("Resumable download object:{0} succeeded", objectName);
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
        /// 获取进度条
        /// </summary>
        /// <param name="client"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="downloadFilename"></param>
        public void GetObjectProgress(OssClient client, string bucketName, string objectName, string downloadFilename)
        {
            try
            {
                var getObjectRequest = new GetObjectRequest(bucketName, objectName);
                getObjectRequest.StreamTransferProgress += streamProgressCallback;
                // 下载文件。
                var ossObject = client.GetObject(getObjectRequest);
                using (var stream = ossObject.Content)
                {
                    var buffer = new byte[1024 * 1024];
                    var bytesRead = 0;
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 处理读取的数据（此处代码省略）。
                    }
                }
                Console.WriteLine("Get object:{0} succeeded", objectName);
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

        private void streamProgressCallback(object sender, StreamTransferProgressArgs args)
        {
            System.Console.WriteLine("ProgressCallback - Progress: {0}%, TotalBytes:{1}, TransferredBytes:{2} ",
                args.TransferredBytes * 100 / args.TotalBytes, args.TotalBytes, args.TransferredBytes);
        }
    }
}
