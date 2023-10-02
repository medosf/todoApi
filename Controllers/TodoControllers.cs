using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using todoApi.Models;

namespace todoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly CosmosDbService _cosmosDbService;

        public TodoController(IConfiguration configuration)
        {
            _cosmosDbService = new CosmosDbService(configuration);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            var items = await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(string id)
        {
            var item = await _cosmosDbService.GetItemAsync(id);
            if (item == null)
            {
                return NotFound();
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

                 await _cosmosDbService.AddItemAsync(item);
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
