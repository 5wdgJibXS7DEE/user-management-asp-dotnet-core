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
            UsersRepository.AddRandomUser();

            var vms = UsersRepository.Users.Select(u => new UserVm().From(u));

            return View(vms);
        }
    }
}