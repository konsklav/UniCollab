using Microsoft.AspNetCore.Mvc;
using Saas.Api.Contracts;
using Saas.Api.Extensions;
using Saas.Application.UseCases.Subjects;

namespace Saas.Api.Endpoints.Subjects;

[ApiController]
[Route("/subjects")]
public class SubjectsController : ControllerBase
{
    [HttpGet(Name = "Get All Subjects")]
    public async Task<IResult> GetAll([FromServices] GetAllSubjects getAllSubjects)
    {
        return await getAllSubjects.HandleAsync().ToHttp(
            onSuccess: subjects => subjects.Select(SubjectDto.From));
    }
}