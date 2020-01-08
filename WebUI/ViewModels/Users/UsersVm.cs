using System.Collections.Generic;
using System.Linq;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels.Users
{
    public class UsersVm
    {
        public string SearchName;

        public IEnumerable<UserVm> Users;

        public PaginationVm Pagination;

        private const int PerPage = 5;

        public UsersVm(in IEnumerable<User> models, string name, int? page)
        {
            SearchName = name;

            int selectedPage = page ?? 1;
            Pagination = new PaginationVm(models.Count(), PerPage, selectedPage, nameof(name), name);

            Users = models
                .Skip(Pagination.Skip)
                .Take(Pagination.Take)
                .Select(model => new UserVm(model));
        }
    }
}