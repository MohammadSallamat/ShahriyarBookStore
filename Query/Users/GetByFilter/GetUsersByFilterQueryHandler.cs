using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Users.DTOs;
using Query.Users.GetById;

namespace Query.Users.GetByFilter;

public class GetUsersByFilterQueryHandler : IBaseQueryHandler<GetUsersByFilterQuery, UserFilterResult>
{
    private readonly ShahriarBookStoreContext _context;

    public GetUsersByFilterQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<UserFilterResult> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
    {
        var filter = request.FilterParams;
        var query = _context.Users
            .Include(u => u.Roles)
            .Include(u => u.UserAddresses)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(u => u.Name.Contains(filter.Search) ||
                                     u.Family.Contains(filter.Search) ||
                                     u.Email.Contains(filter.Search) ||
                                     u.PhoneNumber.Value.Contains(filter.Search));
        }

        if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
        {
            query = query.Where(u => u.PhoneNumber.Value.Contains(filter.PhoneNumber));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var skip = (filter.PageId - 1) * filter.Take;
        var users = await query
            .OrderByDescending(u => u.Id)
            .Skip(skip)
            .Take(filter.Take)
            .ToListAsync(cancellationToken);

        return new UserFilterResult
        {
            TotalCount = totalCount,
            PageId = filter.PageId,
            Take = filter.Take,
            Users = users.Select(GetUserByIdQueryHandler.MapUser).ToList()
        };
    }
}
