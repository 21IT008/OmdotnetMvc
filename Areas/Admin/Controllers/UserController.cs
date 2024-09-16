using Microsoft.AspNetCore.Mvc;
using OmdotnetMvc.Models;
using System.Data.SqlClient;

namespace OmdotnetMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        List<User> users = new List<User>();
        public IActionResult Index()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection= new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command=new SqlCommand("SELECT Id,Name,Email,Gender,Country FROM Users", connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) {
                        users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            Gender = reader.GetString(3),
                            Country = reader.GetString(4)
                        });
                    }
                }
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            User user = null;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command=new SqlCommand("SELECT Name,Email,Gender,Country FROM Users WHERE Id=@Id",connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) {
                        user = new User
                        {
                            Id=Id,
                            Name = reader.GetString(0), 
                            Email = reader.GetString(1),
                            Gender = reader.GetString(2),
                            Country = reader.GetString(3)
                        };
                    }
                }
                connection.Close();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            string connectionString=_configuration.GetConnectionString("DefaultConnection");
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand command=new SqlCommand("UPDATE Users SET Name=@Name,Email=@Email,Gender=@Gender,Country=@Country WHERE Id=@Id",connection))
                {
                    command.Parameters.AddWithValue("@Name",user.Name);
                    command.Parameters.AddWithValue("@Email",user.Email);
                    command.Parameters.AddWithValue("@Gender",user.Gender);
                    command.Parameters.AddWithValue("@Country",user.Country);
                    command.Parameters.AddWithValue("@Id",user.Id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id) { 
            String connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id=@Id",connection))
                {
                    command.Parameters.AddWithValue("@Id",Id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return RedirectToAction("Index");
        }
    }
}
