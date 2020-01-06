using System;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels
{
    public class UserVm
    {
        public Guid? Id;

        public string Name;

        public int Age;

        public Gender Gender;

        public UserVm(User model)
        {
            Id = model.ExternalId;
            Name = model.Name;
            Age = model.Age;
            Gender = model.Gender;
        }
    }
}