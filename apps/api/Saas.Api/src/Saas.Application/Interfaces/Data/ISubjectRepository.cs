using Saas.Domain;

namespace Saas.Application.Interfaces.Data;

public interface ISubjectRepository : IUnitOfWork
{
    Task<List<Subject>> GetAllAsync();
    Task<List<Subject>> GetByIdsAsync(List<Guid> subjectIds);
}