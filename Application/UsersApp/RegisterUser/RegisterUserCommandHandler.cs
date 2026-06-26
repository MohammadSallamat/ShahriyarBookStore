using Application.CommonApplication;
using Domain.Common.Domain.ValueObjects;
using Domain.UserAggregate;
using Domain.UserAggregate.DomainServices;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.RegisterUser;

public class RegisterUserCommandHandler : IBaseCommandHandler<RegisterUserCommand>
{
    private readonly IUserDomainService _domainUserService;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserDomainService domainUserService, IUserRepository userRepository)
    {
        _domainUserService = domainUserService;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userPassword = Password.Create(request.Password);
        var userPhoneNumber = request.PhoneNumber;

        var user = User.RegisterUser(userPhoneNumber, userPassword, _domainUserService);

        await _userRepository.AddAsync(user);
        await _userRepository.Save();
        return OperationResult.Success();

    }
}
