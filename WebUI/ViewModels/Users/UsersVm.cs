using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels
{
    public class UsersVm
    {
        public IEnumerable<UserVm> Users;

        public string Name;

        public PaginationVm Pagination;

        private const int PerPage = 5;

        public UsersVm(in IEnumerable<User> models, string name, int? page)
        {
            Name = name;

            int elements = models.Count();

            int pageFilter = page ?? 1;

            int take = PerPage * pageFilter <= models.Count()
                ? PerPage
                : models.Count() % PerPage;

            Users = models
                .Skip((pageFilter - 1) * PerPage)
                .Take(take)
                .Select(model => new UserVm(model));

            Pagination = new PaginationVm(elements, PerPage, pageFilter, nameof(name), name);
        }
    }
}