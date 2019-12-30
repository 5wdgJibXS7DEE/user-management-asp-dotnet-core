using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using UserManagement.Models;
using System.Linq;

namespace UserManagement.Data
{
    public class UsersRepository
    {
        private static List<User> Users = new List<User>();

        public static readonly string Filepath = "users.json";

        private static readonly object FileLocker = new object();

        public static IEnumerable<User> All()
        {
            lock(Users)
                return Users.ToArray();
        }

        public static IEnumerable<User> FindByName(string name)
        {
            string nameFilter = name != null
                ? name.Trim().ToLowerInvariant()
                : "";

            return All()
                .Where(m => m.Name.ToLowerInvariant().Contains(nameFilter))
                .ToArray();
        }

        public static void Load()
        {
            string json = ReadJsonFromFile(Filepath);

            var users = new List<User>(JsonSerializer.Deserialize<IEnumerable<User>>(json));

            lock(Users)
                Users = users;
        }

        private static string ReadJsonFromFile(string jsonPath)
        {
            lock(FileLocker)
            {
                using (FileStream fs = File.OpenRead(jsonPath))
                using (TextReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void Save()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string serialized = "";

            lock(Users)
                serialized = JsonSerializer.Serialize(Users, options);

            WriteInFile(serialized, Filepath);
        }

        private static void WriteInFile(string content, string filepath)
        {
            lock(FileLocker)
            {
                using (FileStream fs = File.Create(filepath))
                using (TextWriter writer = new StreamWriter(fs))
                {
                    writer.Write(content);
                }
            }
        }

        public static void CreateRandom()
        {
            Random random = new Random();
            Gender gender = (Gender) random.Next(0, 2); // todo GSA max value of enum Gender must be computed at runtime

            User user = new User()
            {
                InternalId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid(),
                Age = random.Next(1, 100),
                Name = GenerateRandomName(gender, random),
                Gender = gender
            };

            lock(Users)
                Users.Add(user);
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