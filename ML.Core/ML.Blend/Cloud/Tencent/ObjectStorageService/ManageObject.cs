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
    public class ManageObject : ClientBase
    {
        private string nextMarker = string.Empty;
        private string keyMarker = string.Empty;
        private string versionIdMarker = string.Empty;

        public ManageObject(string akId, string akSecret, string region) : base(akId, akSecret, region)
        {
        }

        #region 列出对象  List
        /// <summary>
        /// 查询对象列表-获取第一页数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        public void GetFirstPageList(CosXml cosXml, string bucketName)
        {
            try
            {
                GetBucketRequest request = new GetBucketRequest(bucketName);
                //执行请求
                GetBucketResult result = cosXml.GetBucket(request);
                //bucket的相关信息
                ListBucket info = result.listBucket;
                if (info.isTruncated)
                {
                    // 数据被截断，记录下数据下标
                    this.nextMarker = info.nextMarker;
                }
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
        /// 查询对象列表-获取下一页数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        public void GetNextPageList(CosXml cosXml, string bucketName)
        {
            try
            {
                GetBucketRequest request = new GetBucketRequest(bucketName);
                //上一次拉取数据的下标
                request.SetMarker(this.nextMarker);
                //执行请求
                GetBucketResult result = cosXml.GetBucket(request);
                //bucket的相关信息
                ListBucket info = result.listBucket;
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
        /// 查询对象列表-获取对象列表与子目录
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="folderKey"></param>
        public void GetObjectListAndSubFolder(CosXml cosXml, string bucketName, string folderKey)
        {
            try
            {
                GetBucketRequest request = new GetBucketRequest(bucketName);
                //获取 a/ 下的对象以及子目录
                request.SetPrefix($"{folderKey}/");
                request.SetDelimiter("/");
                //执行请求
                GetBucketResult result = cosXml.GetBucket(request);
                //bucket的相关信息
                ListBucket info = result.listBucket;
                // 对象列表
                List<ListBucket.Contents> objects = info.contentsList;
                // 子目录列表
                List<ListBucket.CommonPrefixes> subDirs = info.commonPrefixesList;
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
        /// 查询对象历史版本列表-获取对象历史版本列表第一页数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        public void GetObjectHistoryVersionFirstPageList(CosXml cosXml, string bucketName)
        {
            try
            {
                ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucketName);
                //执行请求
                ListBucketVersionsResult result = cosXml.ListBucketVersions(request);
                //bucket的相关信息
                ListBucketVersions info = result.listBucketVersions;

                List<ListBucketVersions.Version> objects = info.objectVersionList;
                List<ListBucketVersions.CommonPrefixes> prefixes = info.commonPrefixesList;

                if (info.isTruncated)
                {
                    // 数据被截断，记录下数据下标
                    this.keyMarker = info.nextKeyMarker;
                    this.versionIdMarker = info.nextVersionIdMarker;
                }
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
        /// 查询对象历史版本列表-获取对象历史版本列表下一页数据
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        public void GetObjectHistoryVersionNextPageList(CosXml cosXml, string bucketName)
        {
            try
            {
                ListBucketVersionsRequest request = new ListBucketVersionsRequest(bucketName);

                // 上一页的数据结束下标
                request.SetKeyMarker(this.keyMarker);
                request.SetVersionIdMarker(this.versionIdMarker);

                //执行请求
                ListBucketVersionsResult result = cosXml.ListBucketVersions(request);
                ListBucketVersions info = result.listBucketVersions;

                if (info.isTruncated)
                {
                    // 数据被截断，记录下数据下标
                    this.keyMarker = info.nextKeyMarker;
                    this.versionIdMarker = info.nextVersionIdMarker;
                }
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

        #region 删除对象  Delete
        /// <summary>
        /// 删除单个对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void DeleteObject(CosXml cosXml, string bucketName,string objectName)
        {
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest(bucketName, objectName);
                //执行请求
                DeleteObjectResult result = cosXml.DeleteObject(request);
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
        /// 删除多个对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        public void DeleteMultipleObject(CosXml cosXml, string bucketName, string objectName)
        {
            try
            {
                DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucketName);
                //设置返回结果形式
                request.SetDeleteQuiet(false);
                List<string> objects = new List<string>();
                objects.Add(objectName);
                request.SetObjectKeys(objects);
                //执行请求
                DeleteMultiObjectResult result = cosXml.DeleteMultiObjects(request);
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
        /// 删除多个对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="lstObjectName"></param>
        public void DeleteMultipleObject(CosXml cosXml, string bucketName, List<string> lstObjectName)
        {
            try
            {
                DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucketName);
                //设置返回结果形式
                request.SetDeleteQuiet(false);
                request.SetObjectKeys(lstObjectName);
                //执行请求
                DeleteMultiObjectResult result = cosXml.DeleteMultiObjects(request);
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
        /// 删除多个对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="arrObjectName"></param>
        public void DeleteMultipleObject(CosXml cosXml, string bucketName, string[] arrObjectName)
        {
            try
            {
                DeleteMultiObjectRequest request = new DeleteMultiObjectRequest(bucketName);
                //设置返回结果形式
                request.SetDeleteQuiet(false);
                List<string> objects = new List<string>();
                if(arrObjectName.Length > 0)
                {
                    foreach(string obj in arrObjectName)
                    {
                        objects.Add(obj);
                    }
                }
                request.SetObjectKeys(objects);
                //执行请求
                DeleteMultiObjectResult result = cosXml.DeleteMultiObjects(request);
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
        /// 删除指定前缀对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="bucketName"></param>
        /// <param name="prefix"></param>
        public void DeleteAppointPrefixObject(CosXml cosXml, string bucketName, string prefix)
        {
            try
            {
                String nextMarker = null;

                // 循环请求直到没有下一页数据
                do
                {
                    GetBucketRequest listRequest = new GetBucketRequest(bucketName);
                    //获取 folder1/ 下的所有对象以及子目录
                    listRequest.SetPrefix(prefix);
                    listRequest.SetMarker(nextMarker);
                    //执行列出对象请求
                    GetBucketResult listResult = cosXml.GetBucket(listRequest);
                    ListBucket info = listResult.listBucket;
                    // 对象列表
                    List<ListBucket.Contents> objects = info.contentsList;
                    // 下一页的下标
                    nextMarker = info.nextMarker;

                    DeleteMultiObjectRequest deleteRequest = new DeleteMultiObjectRequest(bucketName);
                    //设置返回结果形式
                    deleteRequest.SetDeleteQuiet(false);
                    //对象列表
                    List<string> deleteObjects = new List<string>();
                    foreach (var content in objects)
                    {
                        deleteObjects.Add(content.key);
                    }
                    deleteRequest.SetObjectKeys(deleteObjects);
                    //执行批量删除请求
                    DeleteMultiObjectResult deleteResult = cosXml.DeleteMultiObjects(deleteRequest);
                    //打印请求结果
                    Console.WriteLine(deleteResult.GetResultInfo());
                } while (nextMarker != null);
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

        #region 移动对象  Move
        /// <summary>
        /// 移动对象
        /// </summary>
        /// <param name="cosXml"></param>
        /// <param name="sourceAppid"></param>
        /// <param name="sourceBucketName"></param>
        /// <param name="sourceRegion"></param>
        /// <param name="sourceObject"></param>
        /// <param name="destBucketName"></param>
        /// <param name="destObjectName"></param>
        /// <returns></returns>
        public async Task MoveObjectAsync(CosXml cosXml, string sourceAppid, string sourceBucketName, string sourceRegion, string sourceObject, string destBucketName, string destObjectName)
        {
            // 初始化 TransferConfig
            TransferConfig transferConfig = new TransferConfig();

            // 初始化 TransferManager
            TransferManager transferManager = new TransferManager(cosXml, transferConfig);

            CopySourceStruct copySource = new CopySourceStruct(sourceAppid, sourceBucketName, sourceRegion, sourceObject);
            COSXMLCopyTask copyTask = new COSXMLCopyTask(destBucketName, destObjectName, copySource);

            try
            {
                // 拷贝对象
                COSXML.Transfer.COSXMLCopyTask.CopyTaskResult result = await transferManager.CopyAsync(copyTask);
                Console.WriteLine(result.GetResultInfo());

                // 删除对象
                DeleteObjectRequest request = new DeleteObjectRequest(sourceBucketName, sourceObject);
                DeleteObjectResult deleteResult = cosXml.DeleteObject(request);
                // 打印结果
                Console.WriteLine(deleteResult.GetResultInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine("CosException: " + e);
            }
        }
        #endregion


    }
}
