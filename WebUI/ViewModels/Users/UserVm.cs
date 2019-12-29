using System;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels
{
    public class UserVm
    {
        public Guid? ExternalId;

        public string Name;

        public int Age;

        public Gender Gender;

        public UserVm(User model)
        {
            ExternalId = model.ExternalId;
            Name = model.Name;
            Age = model.Age;
            Gender = model.Gender;
        }
    }
}