using Microsoft.AspNetCore.Mvc;
using OmdotnetMvc.Models;
using System.Data.SqlClient;

namespace OmdotnetMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    if(user.AcceptTerms==false)
                    {
                        ModelState.AddModelError("AcceptTerms", "Please accept terms and conditions");
                        return View(user);
                    }
                    using (SqlCommand Checkcommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email=@Email", connection))
                    {
                        Checkcommand.Parameters.AddWithValue("@Email", user.Email);
                        int count = (int)Checkcommand.ExecuteScalar();
                        if (count > 0)
                        {
                            ModelState.AddModelError("Email", "Email already exists");
                            return View(user);
                        }
                    }
                    using(SqlCommand InsertCommand =new SqlCommand("INSERT INTO USERS (Name,Email,Password,Gender,AcceptTerms,Country) VALUES (@Name,@Email,@Password,@Gender,@AcceptTerms,@Country)",connection))
                    {
                        InsertCommand.Parameters.AddWithValue("@Name", user.Name);
                        InsertCommand.Parameters.AddWithValue("@Email", user.Email);
                        InsertCommand.Parameters.AddWithValue("@Password", user.Password);
                        InsertCommand.Parameters.AddWithValue("@Gender",user.Gender);
                        InsertCommand.Parameters.AddWithValue("@AcceptTerms",user.AcceptTerms);
                        InsertCommand.Parameters.AddWithValue("@Country",user.Country);
                        InsertCommand.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Success");
            }
            return View(user);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
