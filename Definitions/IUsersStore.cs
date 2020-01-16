using System;
using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Definitions
{
    public interface IUsersStore
    {
        IEnumerable<User> All();

        User SingleByExternalId(Guid externalId);

        IEnumerable<User> FindByName(string name);

        Guid Create(User created);

        bool TryUpdate(User updated);

        void Load();

        void Save();
    }
}