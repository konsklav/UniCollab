using Ardalis.Result;

namespace Saas.Infrastructure.Data.Seeding;

public interface ISeeder
{
    Task<Result> SeedDatabase();
}