using Microsoft.AspNetCore.Mvc;

namespace OmdotnetMvc.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            var Email=HttpContext.Session.GetString("Email");
            var AdminEmail=HttpContext.Session.GetString("AdminEmail");
            if(Email==null && AdminEmail==null)
            {
                return RedirectToAction("Login","User");
            }
            return View();
        }
    }
}
