using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.WebUI.ViewModels;

namespace UserManagement.WebUI.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
        }

        public IActionResult Index(string name, int? page)
        {
            IEnumerable<User> users = UsersRepository.FindByName(name);

            var vm = new UsersVm(users, name, page);

            return View(vm);
        }

        public IActionResult CreateRandom()
        {
            UsersRepository.CreateRandom();

            TempData["Success"] = "A randomly generated user was created.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Load()
        {
            UsersRepository.Load();

            TempData["Success"] = "Users were loaded.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Save()
        {
            UsersRepository.Save();

            TempData["Success"] = "Users were saved.";

            return RedirectToAction(nameof(Index));
        }
    }
}