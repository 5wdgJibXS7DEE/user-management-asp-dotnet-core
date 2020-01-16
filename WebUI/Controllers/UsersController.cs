using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Definitions;
using UserManagement.Models;
using UserManagement.WebUI.ViewModels.Users;

namespace UserManagement.WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersStore _usersStore;

        public UsersController(IUsersStore usersStore)
        {
            _usersStore = usersStore;
        }

        public IActionResult Index(string name, int? page)
        {
            IEnumerable<User> users = _usersStore.FindByName(name);

            var vm = new UsersVm(users, name, page);

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            User user = _usersStore.SingleByExternalId(id);

            var vm = new EditVm(user);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditVm input)
        {
            if (ModelState.IsValid == false)
                return View(nameof(Edit), input);

            if (_usersStore.TryUpdate(input.ToModel()))
                TempData["Success"] = "The changes were saved.";
            else
                TempData["Error"] = "An error occured and the changes were NOT saved.";

            return RedirectToAction(nameof(Edit), new { input.Id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateVm());
        }

        [HttpPost]
        public IActionResult Create(CreateVm input)
        {
            if (ModelState.IsValid == false)
                return View(nameof(Create), input);

            Guid createdId = _usersStore.Create(input.ToModel());
            TempData["Success"] = "The user was created.";

            return RedirectToAction(nameof(Edit), new { Id = createdId });
        }

        public IActionResult CreateRandom()
        {
            User generated = Models.User.GenerateRandom();

            _usersStore.Create(generated);

            TempData["Success"] = "A randomly generated user was created.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Load()
        {
            _usersStore.Load();

            TempData["Success"] = "Users were loaded.";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Save()
        {
            _usersStore.Save();

            TempData["Success"] = "Users were saved.";

            return RedirectToAction(nameof(Index));
        }
    }
}