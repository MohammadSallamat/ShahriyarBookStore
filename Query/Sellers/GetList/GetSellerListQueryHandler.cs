using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Sellers.DTOs;
using Query.Sellers.GetById;

namespace Query.Sellers.GetList;

public class GetSellerListQueryHandler : IBaseQueryHandler<GetSellerListQuery, List<SellerDto>>
{
    private readonly ShahriarBookStoreContext _context;

    public GetSellerListQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<List<SellerDto>> Handle(GetSellerListQuery request, CancellationToken cancellationToken)
    {
        var sellers = await _context.Sellers
            .Include(s => s.Inventories)
            .ToListAsync(cancellationToken);

        return sellers.Select(GetSellerByIdQueryHandler.MapSeller).ToList();
    }
}
