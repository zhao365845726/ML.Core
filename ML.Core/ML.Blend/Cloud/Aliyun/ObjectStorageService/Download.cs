using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aliyun.OSS;
using Aliyun.OSS.Common;

namespace ML.Blend.Cloud.Aliyun.ObjectStorageService
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

        public void Range()
        {

        }

        public void BreakPointContinue()
        {

        }
    }
}
