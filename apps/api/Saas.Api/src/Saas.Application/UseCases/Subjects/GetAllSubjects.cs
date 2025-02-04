using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Subjects;

public class GetAllSubjects(ISubjectRepository subjectRepository) : IApplicationUseCase
{
    public async Task<Result<List<Subject>>> HandleAsync()
    {
        var subjects = await subjectRepository.GetAllAsync();
        return subjects;
    }
}