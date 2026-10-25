using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;
using Query.Users.GetById;

namespace Query.Users.GetByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler : IBaseQueryHandler<GetUserByPhoneNumberQuery, UserDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUserByPhoneNumberQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserDto?> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.UserAddresses)
            .FirstOrDefaultAsync(u => u.PhoneNumber.Value == request.PhoneNumber, cancellationToken);

        if (user == null)
            return null;

        return GetUserByIdQueryHandler.MapUser(user);
    }
}
