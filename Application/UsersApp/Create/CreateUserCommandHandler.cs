using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.Create;

public class CreateUserCommandHandler : IBaseCommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _repository;
    private readonly IDomainUserService _userDomainService;
    public CreateUserCommandHandler(IUserRepository repository, IDomainUserService userDomainService)
    {
        _repository = repository;
        _userDomainService = userDomainService;
    }

    public async Task<OperationResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userPassword = Password.Create(request.Password);
        var userPhone = new PhoneNumber(request.PhoneNumber);
        var user = new User(request.Name, request.Family, userPhone
            , request.Email, userPassword, request.Gender, _userDomainService);

        _repository.Add(user);
        await _repository.Save();
        return OperationResult.Success();
    }
}