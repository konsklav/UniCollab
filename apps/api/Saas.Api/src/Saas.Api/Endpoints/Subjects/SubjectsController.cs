using Microsoft.AspNetCore.Mvc;

namespace Saas.Api.Endpoints.Subjects;

[ApiController]
[Route("/subjects")]
public class SubjectsController : ControllerBase
{
    [HttpGet(Name = "Get All Subjects")]
    public async Task<IResult> GetAll()
    {
        
    }
}