using Application.CommonApplication;

namespace Application.UsersApp.RemoveToken;

public record RemoveUserTokenCommand(long UserId, long TokenId) : IBaseCommand;
