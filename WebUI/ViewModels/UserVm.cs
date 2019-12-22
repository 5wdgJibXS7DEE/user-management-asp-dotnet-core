using System;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels
{
    public class UserVm
    {
        public Guid? ExternalId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public UserVm From(User model)
        {
            ExternalId = model.ExternalId;
            Name = model.Name;
            Age = model.Age;
            Gender = model.Gender;

            return this;
        }
    }
}