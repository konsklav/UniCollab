using Ardalis.Result;

namespace Saas.Infrastructure.Seeding;

public interface ISeeder
{
    Task<Result> SeedDatabase();
}