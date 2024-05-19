using System.Collections.Generic;
using System.Threading.Tasks;
using todoApi.Models;

namespace todoApi.Service;

public interface ICosmosDbService
{
    Task<IEnumerable<TodoItem>> GetTodoItemsAsync(string queryString);
    Task<TodoItem> GetTodoItemAsync(string id);
    Task AddTodoItemAsync(TodoItem item);
    Task UpdateTodoItemAsync(string id, TodoItem item);
    Task DeleteItemAsync(string id);


}
