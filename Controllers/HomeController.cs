using Microsoft.AspNetCore.Mvc;

namespace todoApi.Controllers
{
    [ApiController]
    [Route("/")] // Set the route to the root
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Welcome to Todo API!";
        }
    }
}
