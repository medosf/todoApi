using Microsoft.AspNetCore.Mvc;
using todoApi.Models;
using todoApi.Service;

namespace todoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;

        public TodoController(ICosmosDbService cosmosDbService)
        {
          _cosmosDbService = cosmosDbService;
         }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            var items = await _cosmosDbService.GetTodoItemsAsync("SELECT * FROM c");
               if (items == null)
            {
                return NotFound("Somthing went wrong");
            }
          
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(string id)
        {
            var item = await _cosmosDbService.GetTodoItemAsync(id);
            if (item == null)
            {
                return NotFound("Somthing went wrong");
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateItem(TodoItem item)
        {
            if (string.IsNullOrEmpty(item.Id))
                 {
                    item.Id = Guid.NewGuid().ToString(); // Generating a new unique string ID if not provided
                 }

                 await _cosmosDbService.AddTodoItemAsync(item);
             return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
        
        // Add the Update (PUT) method here
    }
}
