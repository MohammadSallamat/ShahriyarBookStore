using Application.CommonApplication;
using Domain.UserAggregate.Repository;

namespace Application.UsersApp.RemoveToken;

public class RemoveUserTokenCommandHandler : IBaseCommandHandler<RemoveUserTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(RemoveUserTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetTracking(request.UserId);
        if (user == null)
            return OperationResult.NotFound("کاربر یافت نشد.");

        user.RemoveToken(request.TokenId);

        await _userRepository.Save();
        return OperationResult.Success();
    }
}
