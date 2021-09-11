//using BaiduBce.Services.Bos;
//using ML.Blend.Cloud.Interface.ObjectStorageService;
//using ML.Blend.Unified.Enum;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace ML.Blend.Cloud.Baidu.ObjectStorageService
//{
//    public class ManageObject : ClientBase, IManageObject<BosClient>
//    {
//        public ManageObject(string akId, string akSecret, string endpoint) : base(akId, akSecret, endpoint)
//        {
//        }

//        public void AllList(BosClient client, string bucketName)
//        {
//            throw new NotImplementedException();
//        }

//        public void BatchDelete(BosClient client, string bucketName)
//        {
//            throw new NotImplementedException();
//        }

//        public void CopyBigObject(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
//        {
//            throw new NotImplementedException();
//        }

//        public void CopyBreakPointContinue(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
//        {
//            throw new NotImplementedException();
//        }

//        public void CopySmallObject(BosClient client, string sourceBucket, string sourceObject, string targetBucket, string targetObject)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(BosClient client, string bucketName, string objectName)
//        {
//            throw new NotImplementedException();
//        }

//        public bool DoesExist(BosClient client, string bucketName, string objectName)
//        {
//            throw new NotImplementedException();
//        }

//        public void List(BosClient client, string bucketName)
//        {
//            throw new NotImplementedException();
//        }

//        public void ObjectMetaData(BosClient client, string bucketName, string objectName, string localFilename)
//        {
//            throw new NotImplementedException();
//        }

//        public void SetObjectAcl(BosClient client, string bucketName, string objectName, AccessControlType cannedAccess)
//        {
//            throw new NotImplementedException();
//        }

//        public void SpecifyMarkerList(BosClient client, string bucketName, string marker)
//        {
//            throw new NotImplementedException();
//        }

//        public void SpecifyNumberList(BosClient client, string bucketName, int number)
//        {
//            throw new NotImplementedException();
//        }

//        public void SpecifyPrefixList(BosClient client, string bucketName, int number, string prefix)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
