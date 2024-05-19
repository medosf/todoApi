// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace todoApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class NewsController : ControllerBase
//     {
//         private readonly NewsService _newsService;

//         public NewsController(NewsService newsService)
//         {
//             _newsService = newsService;
//         }

//         [HttpGet]
//         public async Task<IActionResult> Get()
//         {
//             var news = await _newsService.GetNewsAsync();
//             return Ok(news);
//         }
//     }

// }

