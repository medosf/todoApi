using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todoApi.Models;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var client = new CosmosClient(configuration["CosmosDb:Endpoint"], configuration["CosmosDb:PrimaryKey"]);
        _container = client.GetContainer(configuration["CosmosDb:DatabaseName"], configuration["CosmosDb:ContainerName"]);
    }

 // Add a new ToDo item
public async Task AddItemAsync(TodoItem item)
{
    await _container.CreateItemAsync(item, new PartitionKey(item.Id));
}

// Get a ToDo item by Id
public async Task<TodoItem> GetItemAsync(string id)
{
    try
    {
        ItemResponse<TodoItem> response = await _container.ReadItemAsync<TodoItem>(id, new PartitionKey(id));
        return response.Resource;
    }
    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
    {
        return null;
    }
}

// Update an existing ToDo item
public async Task UpdateItemAsync(string id, TodoItem item)
{
    await _container.UpsertItemAsync(item, new PartitionKey(id));
}

// Delete a ToDo item
public async Task DeleteItemAsync(string id)
{
    await _container.DeleteItemAsync<TodoItem>(id, new PartitionKey(id));
}

// Get all ToDo items
public async Task<IEnumerable<TodoItem>> GetItemsAsync(string queryString)
{
    var query = _container.GetItemQueryIterator<TodoItem>(new QueryDefinition(queryString));
    List<TodoItem> results = new List<TodoItem>();
    while (query.HasMoreResults)
    {
        var response = await query.ReadNextAsync();
        results.AddRange(response.ToList());
    }
    return results;
}

}
