using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using UserManagement.Definitions;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class UsersStore : IUsersStore
    {
        private List<User> _users = new List<User>();

        private readonly string _filepath;

        private readonly object _fileLocker = new object();

        public UsersStore(string filepath)
        {
            _filepath = filepath;
        }

        public IEnumerable<User> All()
        {
            lock (_users)
                return _users.ToArray();
        }

        public User SingleByExternalId(Guid externalId)
        {
            return All()
                .Single(m => m.ExternalId == externalId);
        }

        public IEnumerable<User> FindByName(string name)
        {
            string nameFilter = name != null
                ? name.Trim().ToLowerInvariant()
                : "";

            return All()
                .Where(m => m.Name.ToLowerInvariant().Contains(nameFilter))
                .ToArray();
        }

        public Guid Create(User created)
        {
            created.InternalId = Guid.NewGuid();
            created.ExternalId = Guid.NewGuid();

            lock (_users)
                _users.Add(created);

            return created.ExternalId.Value;
        }

        public bool TryUpdate(User updated)
        {
            lock (_users)
            {
                IEnumerable<User> match = _users.Where(u => u.ExternalId == updated.ExternalId);

                if (match.Count() != 1)
                    return false;

                User old = match.Single();

                updated.InternalId = old.InternalId;

                _users.Remove(old);

                _users.Add(updated);
            }

            return true;
        }

        public void Load()
        {
            string json = ReadJsonFromFile(_filepath);

            var users = new List<User>(JsonSerializer.Deserialize<IEnumerable<User>>(json));

            lock (_users)
                _users = users;
        }

        private string ReadJsonFromFile(string jsonPath)
        {
            lock (_fileLocker)
            {
                using (FileStream fs = File.OpenRead(jsonPath))
                using (TextReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public void Save()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string serialized = "";

            lock (_users)
                serialized = JsonSerializer.Serialize(_users, options);

            WriteInFile(serialized, _filepath);
        }

        private void WriteInFile(string content, string filepath)
        {
            lock (_fileLocker)
            {
                using (FileStream fs = File.Create(filepath))
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                }
            }
        }

        public void CreateRandom()
        {
            Random random = new Random();
            Gender gender = (Gender)random.Next(0, 2); // todo GSA max value of enum Gender must be computed at runtime

            User user = new User()
            {
                InternalId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid(),
                Age = random.Next(1, 100),
                Name = GenerateRandomName(gender, random),
                Gender = gender
            };

            lock (_users)
                _users.Add(user);
        }

        private static string GenerateRandomName(Gender gender, Random random)
        {
            string[] males = new string[] { "Wei", "Adrien", "Mathieu", "Morgan", "Marco", "Yahia", "Cédric", "Fabrice", "Nathan", "Hackett" };
            string[] females = new string[] { "Jayanti", "Raja", "Diane", "Caitlin", "Amélie", "Priya", "Sara", "Leia" };
            string name = null;

            switch (gender)
            {
                case Gender.Male:
                    name = males[random.Next(0, males.Length - 1)];
                    break;

                case Gender.Female:
                    name = females[random.Next(0, females.Length - 1)];
                    break;

                default:
                    throw new NotSupportedException(gender + " not a correct value for " + nameof(Gender));
            }

            return name;
        }
    }
}