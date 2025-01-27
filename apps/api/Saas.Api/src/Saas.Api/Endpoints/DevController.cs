using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Saas.Infrastructure.Data.Seeding;

namespace Saas.Api.Endpoints;

/// <summary>
/// Endpoints that allow the developer to easily manipulate the database, such as seeding the database with fake data.
/// </summary>
[ApiController]
[Route("/dev/database")]
public class DevController(ISeeder seeder)
{
    /// <summary>
    /// Seeds the database with
    /// </summary>
    /// <returns></returns>
    [HttpPut("seed")]
    public async Task<IResult> SeedDatabase()
    {
        var seedResult = await seeder.SeedDatabase();
        return seedResult.ToMinimalApiResult();
    }
}