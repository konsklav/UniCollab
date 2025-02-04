using Microsoft.EntityFrameworkCore;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Infrastructure.Data.Repositories;

internal sealed class SubjectRepository(UniCollabContext context) : ISubjectRepository
{
    public async Task<List<Subject>> GetAllAsync() =>
        await context.Subjects.ToListAsync();

    public async Task<List<Subject>> GetByNamesAsync(List<Guid> subjectIds) =>
        await context.Subjects
            .Where(s => subjectIds.Contains(s.Id))
            .ToListAsync();
    
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}