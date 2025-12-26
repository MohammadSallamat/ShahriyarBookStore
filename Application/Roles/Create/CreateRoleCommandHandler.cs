using Application.CommonApplication;
using Domain.RoleAggregate;
using Domain.RoleAggregate.Repository;

namespace Application.Roles.Create;

public class CreateRoleCommandHandler : IBaseCommandHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _repository;

    public CreateRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {

        var role = new Role(request.title);

        await _repository.Add(role);
        await _repository.Save();
        return OperationResult.Success();
    }
}

