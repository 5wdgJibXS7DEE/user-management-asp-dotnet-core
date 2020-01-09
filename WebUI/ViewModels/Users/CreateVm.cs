using System;
using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.WebUI.ViewModels.Users
{
    public class CreateVm
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(0, 130)]
        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public CreateVm() { }

        public User ToModel()
        {
            return new User()
            {
                Name = Name,
                Age = Age,
                Gender = Gender
            };
        }
    }
}