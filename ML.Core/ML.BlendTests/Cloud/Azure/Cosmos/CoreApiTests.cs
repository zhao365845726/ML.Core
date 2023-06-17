using Microsoft.VisualStudio.TestTools.UnitTesting;
using ML.Blend.Cloud.Azure.Cosmos;
using ML.Blend.Cloud.Azure.Demo;
using ML.BlendTests;
using ML.BlendTests.Model.Monitor;
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
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            await coreApi.CreateDatabaseIfNotExistsAsync("TODOList01");
            await coreApi.CreateContainerAsync(ContainerName);
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
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            await coreApi.CreateDatabaseIfNotExistsAsync(DatabaseName);
            await coreApi.CreateContainerAsync(ContainerName);
            await coreApi.ScaleContainerAsync();
            Company company = new Company()
            {
                Id = "1",
                Name = "n1",
                Address = "a1",
                PartitionKey = "pk1"
            };
            await coreApi.AddItemsToContainerAsync<Company>(company, company.Id, company.PartitionKey);
            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync1()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
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
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
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
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
            for (int i = 0; i < 10; i++)
            {
                Company company = new Company()
                {
                    Id = "3",
                    Name = "n1",
                    Address = "a1",
                    PartitionKey = $"pk{4 + i}"
                };
                await coreApi.CreateItemAsync<Company>(company, company.Id, company.PartitionKey);
            }

            //await coreApi.QueryItemsAsync();
            //await coreApi.ReplaceFamilyItemAsync();
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync4()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
            for (int i = 0; i < 10; i++)
            {
                Company company = new Company()
                {
                    Id = "3",
                    Name = "n1",
                    Address = "a1",
                    PartitionKey = $"pk{4 + i}"
                };
                await coreApi.CreateItemAsync<Company>(company, company.Id, company.PartitionKey);
            }
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task CreateItemAsyncTestAsync5()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
            Student student = new Student()
            {
                Id = "1",
                Name = "Lili",
                Description = "一个非常优秀的学生",
                PartitionKey = "p1"
            };
            await coreApi.CreateItemAsync<Student>(student, student.Id, student.PartitionKey);
            Console.WriteLine($"操作结果完成");
        }

        [TestMethod()]
        public async Task UpsertItemAsync()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
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

        [TestMethod()]
        public async void QueryItemsAsyncTest()
        {
            string filePath = $"{Data.FilePath}{Data.FileName}";
            var entity = GetEntities(filePath, Data.NodeName);
            //CoreApi coreApi = new CoreApi();
            CoreApi coreApi = new CoreApi(entity.endpointUri, entity.primaryKey, entity.applicationName);
            coreApi.SetConfig(DatabaseName, ContainerName);
            var sqlQueryText = "SELECT * FROM c WHERE c.PartitionKey = 'pk3'";
            await coreApi.QueryItemsAsync<Company>(sqlQueryText);
            //Console.WriteLine(JsonConvert.SerializeObject(result));
        }
    }
}