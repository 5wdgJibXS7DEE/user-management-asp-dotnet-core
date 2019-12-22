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
        public static ICollection<User> Users { get; private set; }

        public static readonly string Filepath = "users.json";

        public static void Import()
        {
            string json = ReadJsonFromFile(Filepath);

            if (json == null)
            {
                Users = new Collection<User>();
            }
            else
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

        private static void Export()
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

        public static void AddRandomUser()
        {
            var random = new Random();

            var user = new User()
            {
                InternalId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid(),
                Age = random.Next(1, 100),
                Name = "Randomly generated",
                Gender = Enum.Parse<Gender>(random.Next(0, 2).ToString())
            };

            Users.Add(user);

            Export();
        }
    }
}