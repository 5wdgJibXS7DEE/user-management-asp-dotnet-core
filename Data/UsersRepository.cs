using System.Collections.ObjectModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using UserManagement.Models;

namespace UserManagement.Data
{
    public class UsersRepository
    {
        public static ICollection<User> Users { get; private set; } = new Collection<User>();

        public static readonly string Filepath = "users.json";

        public static void Load()
        {
            string json = ReadJsonFromFile(Filepath);

            if (json != null)
            {
                Users = JsonSerializer.Deserialize<ICollection<User>>(json);
            }
        }

        private static string ReadJsonFromFile(string jsonPath)
        {
            try
            {
                using (FileStream fs = File.OpenRead(jsonPath))
                using (TextReader reader = new StreamReader(fs))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public static void Save()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;

            string json = JsonSerializer.Serialize(Users, options);

            WriteJsonInFile(json, Filepath);
        }

        private static void WriteJsonInFile(string json, string filepath)
        {
            using (FileStream fs = File.Create(filepath))
            using (TextWriter writer = new StreamWriter(fs))
            {
                writer.Write(json);
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