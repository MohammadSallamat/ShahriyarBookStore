using Application.CommonApplication;
using Domain.RoleAggregate.Repository;

namespace Application.Roles.Edit;

public class EditRoleCommandHandler : IBaseCommandHandler<EditRoleCommand>
{
    private readonly IRoleRepository _repository;

    public EditRoleCommandHandler(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetTracking(request.RoleId);
        if (role == null)
            return OperationResult.NotFound();
        role.EditTitle(request.Title);

        if (request.AddPermissions != null && request.AddPermissions.Any())
        {
            foreach (var p in request.AddPermissions)
                role.AddPermission(p);
        }

        if (request.RemovePermissions != null && request.RemovePermissions.Any())
        {
            foreach (var p in request.RemovePermissions)
                role.RemovePermission(p);
        }

        await _repository.Save();

        return OperationResult.Success();
    }
}

