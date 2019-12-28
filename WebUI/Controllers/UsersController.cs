using System.Linq;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.WebUI.ViewModels;

namespace UserManagement.WebUI.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
        }

        public IActionResult Index()
        {
            var vms = UsersRepository.All().Select(u => new UserVm().From(u));

            return View(vms);
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