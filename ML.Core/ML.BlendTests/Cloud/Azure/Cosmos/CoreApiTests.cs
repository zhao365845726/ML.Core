using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Azure.Cosmos;
using ML.Blend.Cloud.Azure.Demo;
using ML.BlendTests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ML.Blend.Cloud.Azure.Cosmos.Tests
{
    [TestClass()]
    public class CoreApiTests : TestBase<ML.BlendTests.Model.Azure>
    {
        [TestMethod()]
        public async Task CreateDatabaseIfNotExistsAsyncTestAsync()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            await coreApi.CreateDatabaseIfNotExistsAsync("ToDoList-001");
            await coreApi.CreateContainerAsync("item01");
            await coreApi.ScaleContainerAsync();
            await coreApi.AddItemsToContainerAsync();
            await coreApi.QueryItemsAsync();
            await coreApi.ReplaceFamilyItemAsync();
            await coreApi.DeleteFamilyItemAsync();
            await coreApi.DeleteDatabaseAndCleanupAsync();
            //Console.WriteLine($"结果  {JsonConvert.SerializeObject(response)}");
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            await coreApi.CreateDatabaseIfNotExistsAsync("db01");
            await coreApi.CreateContainerAsync("c01");
            await coreApi.ScaleContainerAsync();
            Company company = new Company()
            {
                Id = "1",
                Name = "n1",
                Address = "a1",
                PartitionKey = "pk1"
            };
            await coreApi.AddItemsToContainerAsync<Company>(company,company.Id,company.PartitionKey);
            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync1()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig("db01", "c01");
            Company company = new Company()
            {
                Id = "2",
                Name = "n1",
                Address = "a1",
                PartitionKey = "pk2"
            };
            await coreApi.CreateItemAsync<Company>(company, company.Id, company.PartitionKey);
            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync2()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig("db01", "c01");
            Company company = new Company()
            {
                Id = "3",
                Name = "n1",
                Address = "a1",
                PartitionKey = "pk3"
            };
            await coreApi.CreateItemAsync<Company>(company, company.Id, company.PartitionKey);
            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync3()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig("db01", "c01");
            for(int i = 0;i < 10; i++)
            {
                Company company = new Company()
                {
                    Id = "3",
                    Name = "n1",
                    Address = "a1",
                    PartitionKey = $"pk{4+i}"
                };
                await coreApi.CreateItemAsync<Company>(company, company.Id, company.PartitionKey);
            }
            
            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task UpsertItemAsync()
        {
            string filePath = @"C:\Code\02-Github\ML.Core\ML.Core\ConsoleApp1\config.json";
            var entity = GetEntities(filePath, "azure-martyzane1988");
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig("db01", "c01");
            Company company = new Company()
            {
                Id = "3",
                Name = "n1",
                Address = "a1",
                PartitionKey = $"pk demo"
            };
            await coreApi.UpsertItemAsync("3", "pk3"); 

            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }
    }
}