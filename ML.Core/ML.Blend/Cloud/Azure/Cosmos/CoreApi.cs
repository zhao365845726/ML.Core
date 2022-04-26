using Microsoft.Azure.Cosmos;
using ML.Blend.Cloud.Azure.Demo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ML.Blend.Cloud.Azure.Cosmos
{
    public class CoreApi : ClientBase
    {
        public CoreApi(string endpointUri, string primaryKey, string applicationName) : base()
        {
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions() { ApplicationName = applicationName });
        }

        #region 配置后自动生效的方法
        public void SetConfig(string databaseName, string containerName)
        {
            this.databaseName = databaseName;
            this.containerName = containerName;
        }
        #endregion

        public async Task CreateDatabaseIfNotExistsAsync()
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(this.databaseName);
        }
        
        public async Task CreateDatabaseIfNotExistsAsync(string databaseName)
        {
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);
        }

        public async Task CreateContainerAsync()
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(this.containerName, "/partitionKey");
        }

        public async Task CreateContainerAsync(string containerName)
        {
            // Create a new container
            this.container = await this.database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey");
            Console.WriteLine("Created Container: {0}\n", this.container.Id);
        }

        public async Task CreateItemAsync<T>(T entity, string id, string key)
        {
            try
            {
                await CreateDatabaseIfNotExistsAsync();
                await CreateContainerAsync();
                // Read the item to see if it exists.  
                ItemResponse<T> andersenFamilyResponse = await this.container.ReadItemAsync<T>(id, new PartitionKey(key));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<T> andersenFamilyResponse = await this.container.CreateItemAsync<T>(entity, new PartitionKey(key));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource, andersenFamilyResponse.RequestCharge);
            }
        }

        public async Task UpsertItemAsync(string key, string value)
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateContainerAsync();
            await this.container.UpsertItemAsync(key, new PartitionKey(value));
        }

        public async Task GetItemQueryIterator(string key, string value)
        {
            await CreateDatabaseIfNotExistsAsync();
            await CreateContainerAsync();
            //await this.container.GetItemQueryIterator<string>(key);
        }

        //public async Task DeleteAsync(string key, string value)
        //{
        //    await CreateDatabaseIfNotExistsAsync();
        //    await CreateContainerAsync();
        //    await this.container.UpsertItemAsync(key, new PartitionKey(value));
        //}

        public async Task ScaleContainerAsync()
        {
            // Read the current throughput
            try
            {
                int? throughput = await this.container.ReadThroughputAsync();
                if (throughput.HasValue)
                {
                    Console.WriteLine("Current provisioned throughput : {0}\n", throughput.Value);
                    int newThroughput = throughput.Value + 100;
                    // Update throughput
                    await this.container.ReplaceThroughputAsync(newThroughput);
                    Console.WriteLine("New provisioned throughput : {0}\n", newThroughput);
                }
            }
            catch (CosmosException cosmosException) when (cosmosException.StatusCode == HttpStatusCode.BadRequest)
            {
                Console.WriteLine("Cannot read container throuthput.");
                Console.WriteLine(cosmosException.ResponseBody);
            }

        }

        public async Task AddItemsToContainerAsync<T>(T entity,string id,string key)
        {
            try
            {
                // Read the item to see if it exists.  
                ItemResponse<T> andersenFamilyResponse = await this.container.ReadItemAsync<T>(id, new PartitionKey(key));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<T> andersenFamilyResponse = await this.container.CreateItemAsync<T>(entity, new PartitionKey(key));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource, andersenFamilyResponse.RequestCharge);
            }
        }


        public async Task AddItemsToContainerAsync()
        {
            // Create a family object for the Andersen family
            Family andersenFamily = new Family
            {
                Id = "Andersen.1",
                PartitionKey = "Andersen",
                LastName = "Andersen",
                Parents = new Parent[]
                {
                    new Parent { FirstName = "Thomas" },
                    new Parent { FirstName = "Mary Kay" }
                },
                Children = new Child[]
                {
                    new Child
                    {
                        FirstName = "Henriette Thaulow",
                        Gender = "female",
                        Grade = 5,
                        Pets = new Pet[]
                        {
                            new Pet { GivenName = "Fluffy" }
                        }
                    }
                },
                Address = new Address { State = "WA", County = "King", City = "Seattle" },
                IsRegistered = false
            };

            try
            {
                // Read the item to see if it exists.  
                ItemResponse<Family> andersenFamilyResponse = await this.container.ReadItemAsync<Family>(andersenFamily.Id, new PartitionKey(andersenFamily.PartitionKey));
                Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
                ItemResponse<Family> andersenFamilyResponse = await this.container.CreateItemAsync<Family>(andersenFamily, new PartitionKey(andersenFamily.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
            }

            // Create a family object for the Wakefield family
            Family wakefieldFamily = new Family
            {
                Id = "Wakefield.7",
                PartitionKey = "Wakefield",
                LastName = "Wakefield",
                Parents = new Parent[]
                {
                    new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
                    new Parent { FamilyName = "Miller", FirstName = "Ben" }
                },
                Children = new Child[]
                {
                    new Child
                    {
                        FamilyName = "Merriam",
                        FirstName = "Jesse",
                        Gender = "female",
                        Grade = 8,
                        Pets = new Pet[]
                        {
                            new Pet { GivenName = "Goofy" },
                            new Pet { GivenName = "Shadow" }
                        }
                    },
                    new Child
                    {
                        FamilyName = "Miller",
                        FirstName = "Lisa",
                        Gender = "female",
                        Grade = 1
                    }
                },
                Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
                IsRegistered = true
            };

            try
            {
                // Read the item to see if it exists
                ItemResponse<Family> wakefieldFamilyResponse = await this.container.ReadItemAsync<Family>(wakefieldFamily.Id, new PartitionKey(wakefieldFamily.PartitionKey));
                Console.WriteLine("Item in database with id: {0} already exists\n", wakefieldFamilyResponse.Resource.Id);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
                ItemResponse<Family> wakefieldFamilyResponse = await this.container.CreateItemAsync<Family>(wakefieldFamily, new PartitionKey(wakefieldFamily.PartitionKey));

                // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
                Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", wakefieldFamilyResponse.Resource.Id, wakefieldFamilyResponse.RequestCharge);
            }
        }

        public async Task QueryItemsAsync()
        {
            var sqlQueryText = "SELECT * FROM c WHERE c.PartitionKey = 'Andersen'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Family> queryResultSetIterator = this.container.GetItemQueryIterator<Family>(queryDefinition);

            List<Family> families = new List<Family>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Family family in currentResultSet)
                {
                    families.Add(family);
                    Console.WriteLine("\tRead {0}\n", family);
                }
            }
        }

        public async Task ReplaceFamilyItemAsync()
        {
            ItemResponse<Family> wakefieldFamilyResponse = await this.container.ReadItemAsync<Family>("Wakefield.7", new PartitionKey("Wakefield"));
            var itemBody = wakefieldFamilyResponse.Resource;

            // update registration status from false to true
            itemBody.IsRegistered = true;
            // update grade of child
            itemBody.Children[0].Grade = 6;

            // replace the item with the updated content
            wakefieldFamilyResponse = await this.container.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.PartitionKey));
            Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.LastName, itemBody.Id, wakefieldFamilyResponse.Resource);
        }

        public async Task DeleteFamilyItemAsync()
        {
            var partitionKeyValue = "Wakefield";
            var familyId = "Wakefield.7";

            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<Family> wakefieldFamilyResponse = await this.container.DeleteItemAsync<Family>(familyId, new PartitionKey(partitionKeyValue));
            Console.WriteLine("Deleted Family [{0},{1}]\n", partitionKeyValue, familyId);
        }

        public async Task DeleteDatabaseAndCleanupAsync()
        {
            DatabaseResponse databaseResourceResponse = await this.database.DeleteAsync();
            // Also valid: await this.cosmosClient.Databases["FamilyDatabase"].DeleteAsync();

            Console.WriteLine("Deleted Database: {0}\n", this.database.Id);

            //Dispose of CosmosClient
            this.cosmosClient.Dispose();
        }
    }
}
