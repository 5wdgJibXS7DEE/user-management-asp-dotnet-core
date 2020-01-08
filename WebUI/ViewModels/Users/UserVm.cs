using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels.Users
{
    public class UserVm
    {
        [Required]
        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 130)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public UserVm() { }

        public UserVm(User model)
        {
            Id = model.ExternalId.Value;
            Name = model.Name;
            Age = model.Age;
            Gender = model.Gender;
        }

        public User ToModel()
        {
            var model = new User();

            model.ExternalId = Id;
            model.Name = Name;
            model.Age = Age;
            model.Gender = Gender;

            return model;
        }
    }
}