using Dapper;
using Infrastructure.Persistent.Dapper;
using Query.Common.Domain;

namespace Query.Users.List_of_users;

internal class GetUserListQueryHandler : IBaseQueryHandler<GetUserListQuery, IEnumerable<UserListItemDto>>
{
    private readonly DapperContext _context;

    public GetUserListQueryHandler(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserListItemDto>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        using var connection = _context.CreateConnection();
        var  query = "select Id, Name, Family, PhoneNumber , Email ,Gender from Users";
        return await connection.QueryAsync<UserListItemDto>(query, cancellationToken);
    }
}

