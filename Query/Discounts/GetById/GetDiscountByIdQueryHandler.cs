using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Discounts.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Query.Discounts.GetById;

public class GetDiscountByIdQueryHandler : IBaseQueryHandler<GetDiscountByIdQuery, DiscountDto?>
{
    private readonly ShahriarBookStoreContext _context;

    public GetDiscountByIdQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<DiscountDto?> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
    {
        var discount = await _context.Discounts
            .FirstOrDefaultAsync(d => d.Id == request.DiscountId, cancellationToken);

        if (discount == null)
            return null;

        return MapDiscount(discount);
    }

    public static DiscountDto MapDiscount(Domain.DiscountAggregate.Discount discount)
    {
        return new DiscountDto
        {
            Id = discount.Id,
            CreationDate = discount.CreationDate,
            Code = discount.Code,
            DiscountType = discount.DiscountType,
            DiscountAmount = discount.DiscountAmount,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            Capacity = discount.Capacity,
            UsedCount = discount.UsedCount,
            MinOrderAmount = discount.MinOrderAmount,
            MaxDiscountAmount = discount.MaxDiscountAmount,
            IsActive = discount.IsActive
        };
    }
}
