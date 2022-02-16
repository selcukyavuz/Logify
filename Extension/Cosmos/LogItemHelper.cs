namespace Logify.Cosmos;

using System;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Azure.Cosmos;

public class LogItemHelper
{
    private CosmosClient cosmosClient;
    private Database database;
    private Container container;
    private string databaseId = "LogItem";
    private string containerId = "Items";

    private const string _partitionKey = "LogItem";

    public LogItemHelper(string EndpointUri,string PrimaryKey)
    {
        this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" });
        this.database =  this.cosmosClient.GetDatabase(databaseId);
        this.container = this.database.GetContainer(containerId);
        CreateDatabaseAsync().Wait();
        CreateContainerAsync().Wait();
    }

    private async Task CreateDatabaseAsync()
    {
        this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
    }

    private async Task CreateContainerAsync()
    {
        this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/partitionKey");
    }    

    public async Task AddItemsToContainerAsync(LogItem item)
    {
        item.Id = Guid.NewGuid().ToString();
        item.PartitionKey = _partitionKey;
            
        try
        {
            ItemResponse<LogItem> entryResponse = await this.container.ReadItemAsync<LogItem>(item.Id, new PartitionKey(item.PartitionKey));
        }
        catch(CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            ItemResponse<LogItem> entryResponse = await this.container.CreateItemAsync<LogItem>(item, new PartitionKey(item.PartitionKey));
        }
    }
}
