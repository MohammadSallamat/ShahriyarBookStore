using Application.CommonApplication;
using Domain.UserAggregate;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.AddAdress;

public class AddAddressUserCommandHandler : IBaseCommandHandler<AddAddressUserCommand>
{
    private readonly IUserRepository _repository;

    public AddAddressUserCommandHandler(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(AddAddressUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound();

        bool isDuplicate = user.UserAddresses.Any(a =>
            a.PostalCode == request.PostalCode &&
            a.PostalAddress == request.PostalAddress);
        if (isDuplicate)
            return OperationResult.Error("این آدرس قبلاً ثبت شده است.");

        var address = new UserAddress(request.Name, request.Family, request.PhoneNumber, request.Province,
            request.City, request.PostalCode, request.PostalAddress, request.NationalCode);
        user.AddAddress(address);

        await _repository.Save();
        return OperationResult.Success();
    }
}
