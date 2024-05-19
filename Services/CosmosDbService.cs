using Microsoft.Azure.Cosmos;

using todoApi.Models;

namespace todoApi.Service;

public class CosmosDbService : ICosmosDbService
{
    private Container _container;

    public CosmosDbService(
        string databaseName,
        string containerName,
        string account,
        string key)
    {
        var client = new CosmosClient(account, key);
        _container = client.GetContainer(databaseName, containerName);
    }

    public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync(string queryString)
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

      public async Task<TodoItem> GetTodoItemAsync(string id)
    {
        var queryString = $"SELECT * FROM c WHERE c.id = '{id}'";
        var query = _container.GetItemQueryIterator<TodoItem>(new QueryDefinition(queryString));
        while (query.HasMoreResults)
        {
        var response = await query.ReadNextAsync();
        if (response.Count > 0)
            return response?.FirstOrDefault() ?? null;
        }    
        return null; 
    }

    public async Task AddTodoItemAsync(TodoItem item)
    {
        await _container.CreateItemAsync<TodoItem>(item, new PartitionKey(item.Id));
    }

    public async Task UpdateTodoItemAsync(string id, TodoItem item)
    {
        await _container.UpsertItemAsync<TodoItem>(item, new PartitionKey(id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<TodoItem>(id, new PartitionKey(id));
    }

}
