using Ardalis.Result;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Saas.Domain;
using Saas.Tests.Fakes;

namespace Saas.Infrastructure.Data.Seeding;

internal sealed class Seeder(UniCollabContext context) : ISeeder
{
    public async Task<Result> SeedDatabase()
    {
        if (await context.Users.AnyAsync())
            return Result.Conflict("The database needs to be empty before seeding.");
        
        var faker = new Faker();
        var users = FakeUsers.Generate(10);
        var subjects = new List<Subject>
        {
            new("Software Engineering"),
            new("Applied Mathematics"),
            new("Cryptography"),
            new("Cybersecurity"),
            new("Calculus"),
            new("UI/UX"),
            new("Game Development")
        };

        var epicDeveloperKonsklav = new User("konsklav", "123", [], []);
        var epicDeveloperNove = new User("nove", "123", [epicDeveloperKonsklav], []);

        epicDeveloperKonsklav.AddFriend(epicDeveloperNove);

        var chatRoom = ChatRoom.Create("Developers", [epicDeveloperKonsklav, epicDeveloperNove]);

        var posts = users.SelectMany(user =>
        {
            var subject = faker.PickRandom(subjects);
            var userPosts = FakePosts.GetForUser(user, faker.Random.Number(min: 0, max: 10), subjects: [subject]);
            return userPosts;
        });
        
        context.Subjects.AddRange(subjects);
        context.Users.AddRange([..users, epicDeveloperKonsklav, epicDeveloperNove]);
        context.ChatRooms.Add(chatRoom);
        context.Posts.AddRange(posts);

        var changesMade = await context.SaveChangesAsync();
        return Result.SuccessWithMessage($"Successfully added {changesMade} entities.");
    }
}