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

        public void Append(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void BreakPointContinue(BosClient client, string bucketName, string objectName, string localFilename)
        {
            throw new NotImplementedException();
        }

        public void PutObject(BosClient client, String bucketName, String objectKey, byte[] byte1, String string1)
        {

            // 获取指定文件
            FileInfo file = new FileInfo("");    //指定文件路径

            // 以文件形式上传Object
            PutObjectResponse putObjectFromFileResponse = client.PutObject(bucketName, objectKey, file);

            // 获取数据流
            Stream inputStream = file.OpenRead();

            // 以数据流形式上传Object
            PutObjectResponse putObjectResponseFromInputStream = client.PutObject(bucketName, objectKey, inputStream);

            // 以二进制串上传Object
            PutObjectResponse putObjectResponseFromByte = client.PutObject(bucketName, objectKey, Encoding.Default.GetBytes("sampledata"));

            // 以字符串上传Object
            PutObjectResponse putObjectResponseFromString = client.PutObject(bucketName, objectKey, "sampledata");

            // 打印ETag
            Console.WriteLine(putObjectFromFileResponse.ETAG);

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
