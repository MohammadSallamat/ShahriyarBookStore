using Domain.OrderAggregate.Enums;
using Domain.SellerAggregate.Enums;
using Infrastructure.Persistent.EF;
using Microsoft.EntityFrameworkCore;
using Query.Admin.DTOs;
using Query.Common.Domain;

namespace Query.Admin.GetDashboardSummary;

public class GetAdminDashboardSummaryQueryHandler : IBaseQueryHandler<GetAdminDashboardSummaryQuery, AdminDashboardSummaryDto>
{
    private readonly ShahriarBookStoreContext _context;

    public GetAdminDashboardSummaryQueryHandler(ShahriarBookStoreContext context)
    {
        _context = context;
    }

    public async Task<AdminDashboardSummaryDto> Handle(GetAdminDashboardSummaryQuery request, CancellationToken cancellationToken)
    {
        var totalUsers = await _context.Users.CountAsync(cancellationToken);
        var totalSellers = await _context.Sellers.CountAsync(cancellationToken);
        var pendingSellers = await _context.Sellers.CountAsync(s => s.Status == SellerStatus.New, cancellationToken);
        var acceptedSellers = await _context.Sellers.CountAsync(s => s.Status == SellerStatus.Accepted, cancellationToken);
        var rejectedSellers = await _context.Sellers.CountAsync(s => s.Status == SellerStatus.Rejected, cancellationToken);

        var totalProducts = await _context.Products.CountAsync(cancellationToken);
        var totalOrders = await _context.Orders.CountAsync(cancellationToken);

        var totalRevenue = await _context.Orders
            .Where(o => o.Status == OrderStatus.Finally || o.Status == OrderStatus.Shipping)
            .SumAsync(o => (long?)o.TotalPrice, cancellationToken) ?? 0;

        var categories = await _context.Categories.ToListAsync(cancellationToken);
        var products = await _context.Products.Select(p => p.CategoryId).ToListAsync(cancellationToken);

        var categoryStats = categories.Select(c => new CategoryBookCountDto
        {
            CategoryId = c.Id,
            CategoryTitle = c.Title,
            Slug = c.Slug,
            BookCount = products.Count(p => p == c.Id)
        }).OrderByDescending(c => c.BookCount).ToList();

        return new AdminDashboardSummaryDto
        {
            TotalUsers = totalUsers,
            TotalSellers = totalSellers,
            PendingSellersCount = pendingSellers,
            AcceptedSellersCount = acceptedSellers,
            RejectedSellersCount = rejectedSellers,
            TotalProducts = totalProducts,
            TotalOrders = totalOrders,
            TotalRevenue = totalRevenue,
            CategoryStats = categoryStats
        };
    }
}
