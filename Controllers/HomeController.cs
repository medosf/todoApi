using Microsoft.AspNetCore.Mvc;

namespace todoApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return File("~/index.html", "text/html");
        }
    }
}
