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

        public static User GenerateRandom()
        {
            Random random = new Random();
            Gender gender = (Gender)random.Next(0, 2);

            return new User()
            {
                InternalId = Guid.NewGuid(),
                ExternalId = Guid.NewGuid(),
                Age = random.Next(1, 100),
                Name = GenerateRandomName(gender, random),
                Gender = gender
            };
        }

        private static string GenerateRandomName(Gender gender, Random random)
        {
            string[] males = new string[] { "Wei", "Adrien", "Mathieu", "Morgan", "Marco", "Yahia", "Cédric", "Fabrice", "Nathan", "Hackett" };
            string[] females = new string[] { "Jayanti", "Raja", "Diane", "Caitlin", "Amélie", "Priya", "Sara", "Leia" };

            var name = gender switch
            {
                Gender.Male => males[random.Next(0, males.Length - 1)],

                Gender.Female => females[random.Next(0, females.Length - 1)],

                _ => throw new NotSupportedException(gender + " not a correct value for " + nameof(Gender)),
            };

            return name;
        }
    }
}