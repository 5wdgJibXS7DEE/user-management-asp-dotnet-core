using System;

namespace UserManagement.Models
{
    public class User
    {
        public Guid? InternalId { get; set; }

        public Guid? ExternalId { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }
    }
}