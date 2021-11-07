using ML.Blend.Cloud.Interface.ObjectStorageService;
using Qiniu.Common;
using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class Upload : ClientBase
    {
        public Upload(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 简单上传本地文件
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        /// <param name="zoneId"></param>
        public HttpResult SimpleFile(string bucketName, string objectName, string localFilename, ZoneID zoneId = ZoneID.CN_East)
        {
            try
            {
                var client = CreateClient(bucketName, zoneId);
                // 上传策略，参见 
                // https://developer.qiniu.com/kodo/manual/put-policy
                PutPolicy putPolicy = new PutPolicy();
                // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
                // putPolicy.Scope = bucket + ":" + saveKey;
                putPolicy.Scope = bucketName;
                // 上传策略有效期(对应于生成的凭证的有效期)          
                putPolicy.SetExpires(3600);
                // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
                putPolicy.DeleteAfterDays = 1;
                // 生成上传凭证，参见
                // https://developer.qiniu.com/kodo/manual/upload-token            
                string token = SetAuth(client, Unified.Enum.QiniuAccessTokenType.UPLOAD,putPolicy);
                UploadManager um = new UploadManager();
                HttpResult result = um.UploadFile(localFilename, objectName, token);
                //Console.WriteLine(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Put object failed, {0}", ex.Message);
                throw new Exception();
            }
        }

        /// <summary>
        /// 简单上传-上传字节数据
        /// </summary>
        public HttpResult UploadBytesData(string bucketName, string objectName, string localFilename, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            byte[] data = System.IO.File.ReadAllBytes(localFilename);
            //byte[] data = System.Text.Encoding.UTF8.GetBytes("Hello World!");
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucketName;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string token = SetAuth(client, Unified.Enum.QiniuAccessTokenType.UPLOAD, putPolicy);
            FormUploader fu = new FormUploader();
            HttpResult result = fu.UploadData(data, objectName, token);
            return result;
        }

        /// <summary>
        /// 上传（来自网络回复的）数据流
        /// </summary>
        public HttpResult UploadStream(string bucketName, string objectName, string localFilename, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            // 上传策略，参见 
            // https://developer.qiniu.com/kodo/manual/put-policy
            PutPolicy putPolicy = new PutPolicy();
            // 如果需要设置为"覆盖"上传(如果云端已有同名文件则覆盖)，请使用 SCOPE = "BUCKET:KEY"
            // putPolicy.Scope = bucket + ":" + saveKey;
            putPolicy.Scope = bucketName;
            // 上传策略有效期(对应于生成的凭证的有效期)          
            putPolicy.SetExpires(3600);
            // 上传到云端多少天后自动删除该文件，如果不设置（即保持默认默认）则不删除
            putPolicy.DeleteAfterDays = 1;
            // 生成上传凭证，参见
            // https://developer.qiniu.com/kodo/manual/upload-token            
            string token = SetAuth(client, Unified.Enum.QiniuAccessTokenType.UPLOAD, putPolicy); 
            try
            {
                //string url = "http://img.ivsky.com/img/tupian/pre/201610/09/beifang_shanlin_xuejing-001.jpg";
                var wReq = System.Net.WebRequest.Create(localFilename) as System.Net.HttpWebRequest;
                var resp = wReq.GetResponse() as System.Net.HttpWebResponse;
                using (var stream = resp.GetResponseStream())
                {
                    // 请不要使用UploadManager的UploadStream方法，因为此流不支持查找(无法获取Stream.Length)
                    // 请使用FormUploader或者ResumableUploader的UploadStream方法
                    FormUploader fu = new FormUploader();
                    var result = fu.UploadStream(stream, objectName, token);
                    return result;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception();
            }
        }

        /// <summary>
        /// 上传大文件，可以从上次的断点位置继续上传
        /// </summary>
        public HttpResult UploadBigFile(string bucketName, string objectName, string localFilename, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            // 断点记录文件，可以不用设置，让SDK自动生成，如果出现续上传的情况，SDK会尝试从该文件载入断点记录
            // 对于不同的上传任务，请使用不同的recordFile
            string recordFile = $"{localFilename}.cache";
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = bucketName;
            putPolicy.SetExpires(3600);
            string token = SetAuth(client, Unified.Enum.QiniuAccessTokenType.UPLOAD, putPolicy);
            // 包含两个参数，并且都有默认值
            // 参数1(bool): uploadFromCDN是否从CDN加速上传，默认否
            // 参数2(enum): chunkUnit上传分片大小，可选值128KB,256KB,512KB,1024KB,2048KB,4096KB
            ResumableUploader ru = new ResumableUploader(false, ChunkUnit.U1024K);
            // ResumableUploader.UploadFile有多种形式，您可以根据需要来选择
            //
            // 最简模式，使用默认recordFile和默认uploadProgressHandler
            // UploadFile(localFile,saveKey,token)
            // 
            // 基本模式，使用默认uploadProgressHandler
            // UploadFile(localFile,saveKey,token,recordFile)
            //
            // 一般模式，使用自定义进度处理(可以监视上传进度)
            // UploadFile(localFile,saveKey,token,recordFile,uploadProgressHandler)
            //
            // 高级模式，包含上传控制(可控制暂停/继续 或者强制终止)
            // UploadFile(localFile,saveKey,token,recordFile,uploadProgressHandler,uploadController)
            // 
            // 支持自定义参数
            //var extra = new System.Collections.Generic.Dictionary<string, string>();
            //extra.Add("FileType", "UploadFromLocal");
            //extra.Add("YourKey", "YourValue");
            //uploadFile(...,extra,...)
            //最大尝试次数(有效值1~20)，在上传过程中(如mkblk或者bput操作)如果发生错误，它将自动重试，如果没有错误则无需重试
            int maxTry = 10;
            // 使用默认进度处理，使用自定义上传控制            
            UploadProgressHandler upph = new UploadProgressHandler(ResumableUploader.DefaultUploadProgressHandler);
            UploadController upctl = new UploadController(uploadControl);
            HttpResult result = ru.UploadFile(localFilename, objectName, token, recordFile, maxTry, upph, upctl);
            //Console.WriteLine(result);
            return result;
        }
        // 内部变量，仅作演示
        private static bool paused = false;
        /// <summary>
        /// 上传控制
        /// </summary>
        /// <returns></returns>
        private static UPTS uploadControl()
        {
            // 这个函数只是作为一个演示，实际当中请根据需要来设置
            // 这个演示的实际效果是“走走停停”，也就是停一下又继续，如此重复直至上传结束
            paused = !paused;
            if (paused)
            {
                return UPTS.Suspended;
            }
            else
            {
                return UPTS.Activated;
            }
        }
    }
}
