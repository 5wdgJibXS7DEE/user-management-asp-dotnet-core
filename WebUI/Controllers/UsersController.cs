using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserManagement.Models;

namespace UserManagement.WebUI.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
        }

        public IActionResult Index()
        {
            var users = new List<User>();
            users.Add(new User() { Name = "John", Age = 18, Gender = Gender.Male });
            users.Add(new User() { Name = "Kate", Age = 21, Gender = Gender.Female });

            return View(users);
        }
    }
}