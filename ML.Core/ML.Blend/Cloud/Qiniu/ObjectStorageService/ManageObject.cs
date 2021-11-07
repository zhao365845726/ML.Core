using ML.Blend.Cloud.Interface.ObjectStorageService;
using ML.Blend.Unified.Enum;
using Qiniu.Common;
using Qiniu.Http;
using Qiniu.RS;
using Qiniu.RS.Model;
using Qiniu.RSF;
using Qiniu.RSF.Model;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ML.Blend.Cloud.Qiniu.ObjectStorageService
{
    public class ManageObject : ClientBase
    {
        public ManageObject(string akId, string akSecret) : base(akId, akSecret)
        {
        }

        /// <summary>
        /// 获取文件基本信息
        /// </summary>
        public StatResult Stat(string bucketName, string objectName,ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            BucketManager bm = new BucketManager(client);
            StatResult result = bm.Stat(bucketName, objectName);
            //Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        public BatchResult Batch(string bucketName, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            // 批量操作类似于
            // op=<op1>&op=<op2>&op=<op3>...
            string batchOps = "op=OP1&op=OP2";
            BucketManager bm = new BucketManager(client);
            var result = bm.Batch(batchOps);
            // 或者
            //string[] batch_ops={"<op1>","<op2>","<op3>",...};
            //bm.Batch(batch_ops);
            //Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// 获取空间文件列表
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="marker">首次请求时marker必须为空</param>
        /// <param name="prefix">按文件名前缀保留搜索结果</param>
        /// <param name="delimiter">目录分割字符(比如"/")</param>
        /// <param name="zoneId"></param>
        public void ListFiles(string bucketName, string marker, string prefix,string delimiter, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            int limit = 100; // 单次列举数量限制(最大值为1000)
            BucketManager bm = new BucketManager(client);
            //List<FileDesc> items = new List<FileDesc>();
            //List<string> commonPrefixes = new List<string>();
            do
            {
                ListResult result = bm.ListFiles(bucketName, prefix, marker, limit, delimiter);
                Console.WriteLine(result);
                marker = result.Result.Marker;
                //if (result.Result.Items != null)
                //{
                //    items.AddRange(result.Result.Items);
                //}
                //if (result.Result.CommonPrefixes != null)
                //{
                //    commonPrefixes.AddRange(result.Result.CommonPrefixes);
                //}
            } while (!string.IsNullOrEmpty(marker));
            //foreach (string cp in commonPrefixes)
            //{
            //    Console.WriteLine(cp);
            //}
            //foreach(var item in items)
            //{
            //    Console.WriteLine(item.Key);
            //}
        }


        /// <summary>
        /// 设置或更新文件生命周期
        /// </summary>
        public HttpResult UpdateLifecycle(string bucketName, string objectName, ZoneID zoneId = ZoneID.CN_East)
        {
            var client = CreateClient(bucketName, zoneId);
            BucketManager bm = new BucketManager(client);
            // 新的deleteAfterDays，设置为0表示取消lifecycle
            int deleteAfterDays = 1;
            var result = bm.UpdateLifecycle(bucketName, objectName, deleteAfterDays);
            //Console.WriteLine(result);
            return result;
        }

        ///// <summary>
        ///// 持久化并保存处理结果
        ///// </summary>
        //public PfopResult PfopAndSave(string bucketName, string objectName, ZoneID zoneId = ZoneID.CN_East)
        //{
        //    // 队列名称，如果没有，请设置为null
        //    // 另请参阅https://qiniu.kf5.com/hc/kb/article/112978/
        //    string pipeline = "MEDIAPROC_PIPELINE";
        //    // 接收处理结果通知的URL，另请参阅
        //    // http://developer.qiniu.com/code/v6/api/dora-api/pfop/pfop.html#pfop-notification
        //    string notifyUrl = "NOTIFY_URL";
        //    bool force = false;
        //    // 要保存的目标空间
        //    string dstBucket = "dest-bucket";
        //    string dstKey = "2.mp4";
        //    string saveAsUri = StringHelper.urlSafeBase64Encode(dstBucket + ":" + dstKey);
        //    // 需要执行的数据处理,例如视频转码
        //    string fopM = "FILE_OPS"; //示例: "avthumb/mp4";
        //    // 使用管道'|'命令，将处理结果saveas另存
        //    string fops = fopM + "|saveas/" + saveAsUri;
        //    var client = CreateClient(bucketName, zoneId);
        //    OperationManager ox = new OperationManager(client);
        //    PfopResult result = ox.Pfop(bucketName, objectName, fops, pipeline, notifyUrl, force);
        //    // 稍后可以根据PersistentId查询处理进度/结果
        //    //string persistentId = result.PersistentId;
        //    //HttpResult pr = ox.Prefop(persistentId);
        //    //Console.WriteLine(pr);
        //    //Console.WriteLine(result);
        //    return result;
        //}
    }
}
