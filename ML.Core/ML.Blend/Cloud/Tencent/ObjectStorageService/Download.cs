using COSXML;
using COSXML.Model.Object;
using COSXML.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ML.Blend.Cloud.Tencent.ObjectStorageService
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class Download : ClientBase
    {
        public Download(string akId, string akSecret, string region) : base(akId, akSecret, region)
        {
        }

        #region 高级接口  Senior
        /// <summary>
        /// 下载对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="savelocalFilename"></param>
        public async Task SeniorDownloadObjectAsync(CosXml cosXml, string bucketName, string objectName, string savelocalFilename)
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            string localDir = System.IO.Path.GetTempPath();//本地文件夹

            // 下载对象
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(bucketName, objectName, localDir, savelocalFilename);

            downloadTask.progressCallback = delegate (long completed, long total)
            {
                Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
            };

            try
            {
                COSXML.Transfer.COSXMLDownloadTask.DownloadTaskResult result = await transferManager.DownloadAsync(downloadTask);
                Console.WriteLine(result.GetResultInfo());
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
        /// 设置下载支持断点续传
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="savelocalFilename"></param>
        public async Task SeniorDownloadSupportBreakpointContinuationAsync(CosXml cosXml, string bucketName, string objectName, string savelocalFilename)
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            string localDir = System.IO.Path.GetTempPath();//本地文件夹
            GetObjectRequest request = new GetObjectRequest(bucketName,objectName,localDir,savelocalFilename);
            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(request);
            //开启断点续传，当本地存在未下载完成文件时，追加下载到文件末尾
            //本地文件已存在部分不符合本次下载的内容，可能导致下载失败，请删除文件重试
            downloadTask.SetResumableDownload(true);
            try
            {
                COSXML.Transfer.COSXMLDownloadTask.DownloadTaskResult result = await transferManager.DownloadAsync(downloadTask);
                Console.WriteLine(result.GetResultInfo());
                string eTag = result.eTag;
            }
            catch (Exception e)
            {
                Console.WriteLine("CosException: " + e);
            }
        }

        /// <summary>
        /// 批量下载
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="savelocalFilename"></param>
        public async Task SeniorBatchDownloadAsync(CosXml cosXml, string bucketName, string objectName, string savelocalFilename)
        {
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            string localDir = System.IO.Path.GetTempPath();//本地文件夹

            for (int i = 0; i < 5; i++)
            {
                // 下载对象
                string cosPath = objectName + i; //对象在存储桶中的位置标识符，即称对象键
                COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(bucketName, cosPath, localDir, savelocalFilename);
                await transferManager.DownloadAsync(downloadTask);
            }
        }

        /// <summary>
        /// 单链接限速下载
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="savelocalFilename"></param>
        public async Task SeniorLimitDownloadSingleLink(CosXml cosXml, string bucketName, string objectName, string savelocalFilename)
        {
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);
            string localDir = System.IO.Path.GetTempPath();//本地文件夹

            GetObjectRequest request = new GetObjectRequest(bucketName, objectName, localDir, savelocalFilename);
            request.LimitTraffic(8 * 1000 * 1024); // 限制为1MB/s

            COSXMLDownloadTask downloadTask = new COSXMLDownloadTask(request);
            await transferManager.DownloadAsync(downloadTask);
        }
        #endregion

        #region 简单接口  Simple
        /// <summary>
        /// 单对象简单下载
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="savelocalFilename"></param>
        public void SimpleDownloadObject(CosXml cosXml, string bucketName, string objectName, string savelocalFilename)
        {
            try
            {
                string localDir = System.IO.Path.GetTempPath();//本地文件夹
                GetObjectRequest request = new GetObjectRequest(bucketName, objectName, localDir, savelocalFilename);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                GetObjectResult result = cosXml.GetObject(request);
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
        /// 下载对象到内存中
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void SimpleDownloadObjectToMem(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                GetObjectBytesRequest request = new GetObjectBytesRequest(bucketName, objectName);
                //设置进度回调
                request.SetCosProgressCallback(delegate (long completed, long total)
                {
                    Console.WriteLine(String.Format("progress = {0:##.##}%", completed * 100.0 / total));
                });
                //执行请求
                GetObjectBytesResult result = cosXml.GetObject(request);
                //获取内容到 byte 数组中
                byte[] content = result.content;
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
