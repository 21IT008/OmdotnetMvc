using Microsoft.AspNetCore.Mvc;

namespace OmdotnetMvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
