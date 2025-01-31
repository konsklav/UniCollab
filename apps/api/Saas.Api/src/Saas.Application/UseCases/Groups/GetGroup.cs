using Ardalis.Result;
using Saas.Application.Interfaces;
using Saas.Application.Interfaces.Data;
using Saas.Domain;

namespace Saas.Application.UseCases.Groups;

public class GetGroup(IGroupRepository repository) : IApplicationUseCase
{
    public async Task<Result<Group>> Handle(Guid groupId)
    {
        var group = await repository.GetByIdAsync(groupId);
        if (group is null) 
            return Result<Group>.NotFound();

        return group;
    }
}