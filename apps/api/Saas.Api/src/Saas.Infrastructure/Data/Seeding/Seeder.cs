using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Seeding;

internal sealed class Seeder(UniCollabContext context) : ISeeder
{
    public async Task<Result> SeedDatabase()
    {
        await context.Database.MigrateAsync();
        
        if (await context.Users.AnyAsync())
            return Result.Conflict("The database needs to be empty before seeding.");
        
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

        context.Subjects.AddRange(subjects);
        context.Users.AddRange(epicDeveloperKonsklav, epicDeveloperNove);
        context.ChatRooms.Add(chatRoom);

        var changesMade = await context.SaveChangesAsync();
        return Result.SuccessWithMessage($"Successfully added {changesMade} entities.");
    }
}