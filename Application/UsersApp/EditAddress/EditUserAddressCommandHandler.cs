using Application.CommonApplication;
using Domain.UserAggregate;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.EditAddress;

internal partial class EditUserAddressCommandHandler : IBaseCommandHandler<EditUserAddressCommand>
{
    private readonly IUserRepository _repository;

    public EditUserAddressCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditUserAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        var address = new UserAddress(request.Name, request.Family, request.PhoneNumber, request.Province
            , request.City, request.PostalCode, request.PostalAddress, request.NationalCode);

        user.EditAddress(address);
        await _repository.Save();

        return OperationResult.Success();
    }
}