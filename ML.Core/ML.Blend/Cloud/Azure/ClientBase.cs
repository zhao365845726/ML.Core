using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ML.Blend.Cloud.Azure
{
    public class ClientBase : Base
    {
        public ClientBase() : base()
        {
        }

        /// <summary>
        /// 创建CosmosDB Client
        /// </summary>
        /// <returns></returns>
        public CosmosClient CreateCosmosClient(string endpointUri, string primaryKey, string applicationName)
        {
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
            return cosmosClient;
        }
    }
}
