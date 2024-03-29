﻿using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Cosmos;

namespace azure_web_api.Data
{
    public class EngineerService : IEngineerService
    {
        //private readonly IKeyVaultManager _secretManager;
        //private readonly IConfiguration _configuration;
        public readonly string CosmosDbConnectionString = "AccountEndpoint=https://azure-dev-cosmos-db.documents.azure.com:443/;AccountKey=UrMiRa7zkfilj5k6DhgAdSXZJQHOMpVed8Kb4fUUQiagiISx9Nx2pNHqnNNVCDirb0hPtXXUhz09ACDbCTQQjw==;";
        public readonly string CosmosDbName = "Constractors";
        public readonly string CosmosDbContainerName = "Engineers";

        //public EngineerService(IKeyVaultManager secretManager, IConfiguration configuration)
        //{
        //    _secretManager = secretManager;
        //    _configuration = configuration;
        //}
        private Container GetContainerClient()
        {
            //string secretValue = _secretManager.GetSecret("cosmos-db-connectionstring");
            //string connectionString = _configuration["cosmos-db-connectionstring"];

            SecretClient secretClient = new SecretClient(new Uri("https://az-learning-key-vault.vault.azure.net/"),
                new DefaultAzureCredential());

            KeyVaultSecret keyVaultSecret = secretClient.GetSecret("cosmos-db-connectionstring");

            //var cosmosDbClient = new CosmosClient(CosmosDbConnectionString);
            var cosmosDbClient = new CosmosClient(keyVaultSecret.Value);
            var container = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return container;
        }
        public async Task<Engineer> AddEngineer(Engineer engineer)
        {
            try
            {
                engineer.id = Guid.NewGuid();
                var container = GetContainerClient();
                var response = await container.CreateItemAsync(engineer, new PartitionKey(engineer.id.ToString()));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Engineer> UpdateEngineer(Engineer engineer)
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.UpsertItemAsync(engineer, new PartitionKey(engineer.id.ToString()));
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<string> DeleteEngineer(string? id, string? partitionKey)
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.DeleteItemAsync<Engineer>(id, new PartitionKey(partitionKey));
                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public async Task<List<Engineer>> GetEngineerDetails()
        {
            List<Engineer> engineers = new List<Engineer>();
            try
            {
                var container = GetContainerClient();
                var sqlQuery = "select * from c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<Engineer> queryResultSetIterator = container.GetItemQueryIterator<Engineer>(queryDefinition);

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<Engineer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (Engineer engineer in currentResultSet)
                    {
                        engineers.Add(engineer);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return engineers;
        }
        public async Task<Engineer> GetEngineerDetailsById(string? id, string? partitionKey)
        {
            try
            {
                var container = GetContainerClient();
                ItemResponse<Engineer> response = await container.ReadItemAsync<Engineer>(id, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (Exception ex)
            {
                throw new Exception("Exception", ex);
            }
        }
    }
}
