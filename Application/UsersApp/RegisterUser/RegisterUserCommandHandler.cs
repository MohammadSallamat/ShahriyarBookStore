using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.RegisterUser;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IDomainUserService _domainUserService;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IDomainUserService domainUserService, IUserRepository userRepository)
    {
        _domainUserService = domainUserService;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userPassword = Password.Create(request.Password);
        var userPhoneNumber = new PhoneNumber(request.PhoneNumber);

        var user = User.RegisterUser(userPhoneNumber, userPassword, _domainUserService);

        _userRepository.Add(user);
        await _userRepository.Save();
        return OperationResult.Success();

    }
}
