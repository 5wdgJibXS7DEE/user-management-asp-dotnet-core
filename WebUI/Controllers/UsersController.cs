using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.WebUI.ViewModels.Users;

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

        public IActionResult Display(Guid id)
        {
            User user = UsersRepository.SingleByExternalId(id);

            var vm = new UserVm(user);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(UserVm input)
        {
            if (ModelState.IsValid == false)
                return View(nameof(Display), input);
            
            if (UsersRepository.TryUpdate(input.ToModel()))
                TempData["Success"] = "The changes were saved.";
            else
                TempData["Error"] = "An error occured and the changes were NOT saved.";

            return RedirectToAction(nameof(Display), new { Id = input.Id });
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