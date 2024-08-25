using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace OmdotnetMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        public AdminController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email AND Password=@Password", connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        HttpContext.Session.SetString("AdminEmail", email);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Invalid credentials";
                        return View();
                    }
                }
            }
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminEmail") == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminEmail");
            return RedirectToAction("Login");
        }
    }
}
