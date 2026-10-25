using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Discounts.DTOs;
using Query.Discounts.GetById;
using System.Threading;
using System.Threading.Tasks;

namespace Query.Discounts.GetByCode;

public class GetDiscountByCodeQueryHandler : IBaseQueryHandler<GetDiscountByCodeQuery, DiscountDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetDiscountByCodeQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<DiscountDto?> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
    {
        var code = request.Code.Trim().ToUpper();
        var discount = await _context.Discounts
            .FirstOrDefaultAsync(d => d.Code == code, cancellationToken);

        if (discount == null)
            return null;

        return GetDiscountByIdQueryHandler.MapDiscount(discount);
    }
}
