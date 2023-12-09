using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
