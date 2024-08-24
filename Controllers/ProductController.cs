using Microsoft.AspNetCore.Mvc;

namespace OmdotnetMvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var Email=HttpContext.Session.GetString("Email");
            if(Email==null)
            {
                return RedirectToAction("Login","User");
            }
            return View();
        }
    }
}
