using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Azure
{
    /// <summary>
    /// Azure云基类
    /// 参考地址：https://cloud.tencent.com/document/product
    /// </summary>
    public class Base
    {
        // The Cosmos client instance
        protected CosmosClient cosmosClient;

        // The database we will create
        protected Database database;

        // The container we will create.
        protected Container container;

        public string databaseName;
        public string containerName;

        public Base()
        {
        }
    }
}
