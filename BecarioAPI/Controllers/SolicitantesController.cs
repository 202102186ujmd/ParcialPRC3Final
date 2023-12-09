using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitantesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
