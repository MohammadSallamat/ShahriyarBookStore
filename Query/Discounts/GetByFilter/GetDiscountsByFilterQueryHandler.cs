using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Common.Domain;
using Query.Discounts.DTOs;
using Query.Discounts.GetById;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Query.Discounts.GetByFilter;

public class GetDiscountsByFilterQueryHandler : IBaseQueryHandler<GetDiscountsByFilterQuery, DiscountFilterResult>
{
    private readonly ShahriarBookStoreContext _context;

    public GetDiscountsByFilterQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<DiscountFilterResult> Handle(GetDiscountsByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;
        var query = _context.Discounts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(@params.Code))
        {
            var code = @params.Code.Trim().ToUpper();
            query = query.Where(d => d.Code.Contains(code));
        }

        if (@params.IsActive.HasValue)
        {
            query = query.Where(d => d.IsActive == @params.IsActive.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var skip = (@params.PageId - 1) * @params.Take;
        var discounts = await query
            .OrderByDescending(d => d.Id)
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        return new DiscountFilterResult
        {
            TotalCount = totalCount,
            PageId = @params.PageId,
            Take = @params.Take,
            Discounts = discounts.Select(GetDiscountByIdQueryHandler.MapDiscount).ToList()
        };
    }
}
