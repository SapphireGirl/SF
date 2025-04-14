using SF.Model;
using Bogus;
// Bogus is a popular library for generating fake data

namespace SF.ServerTests
{
    internal static class SeedTestData
    {
        public static List<Home> GenerateFakeHomes(int count)
        {
            var faker = new Faker<Home>()
                .RuleFor(h => h.ID, f => f.IndexFaker + 1) // Id should be unique
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(h => h.ZipCode, f => f.Random.Int(10000, 99999)) // Generate a random zip code between 10000 and 99999
                .RuleFor(h => h.Price, f => f.Random.Decimal(100, 200));
            return faker.Generate(count);
        }

        internal static Home GenerateFakeHome()
        {
            var faker = new Faker<Home>()
                .RuleFor(h => h.ID, f => f.IndexFaker + 100) // Id should be unique
                .RuleForType(typeof(string), f => f.Lorem.Word())
                .RuleFor(h => h.ZipCode, f => f.Random.Int(10000, 99999)) // Generate a random zip code between 10000 and 99999
                .RuleFor(h => h.Price, f => f.Random.Decimal(100, 200));
            
            var home = faker.Generate(1).FirstOrDefault();

            return home;
        }
    }
}
