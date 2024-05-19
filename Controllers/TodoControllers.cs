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
                return NotFound("No items found");
            }
          
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(string id)
        {
            var item = await _cosmosDbService.GetTodoItemAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateItem(TodoItem item)
        {
     
            if(item.Id == null){
                return BadRequest("Id is required");
            }
        
                 await _cosmosDbService.AddTodoItemAsync(item);
             return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string id)
        {
            await _cosmosDbService.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
