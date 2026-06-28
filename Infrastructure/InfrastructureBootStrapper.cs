using Domain.CategoryAggregate;
using Domain.CommentAggregate;
using Domain.OrderAggregate.Repository;
using Domain.ProductAggregate.Repository;
using Domain.RoleAggregate.Repository;
using Domain.SellerAggregate.Repository;
using Domain.SiteEntities.Repositories;
using Domain.UserAggregate.Repository;
using Infrastructure.Persistent.Dapper;
using Infrastructure.Persistent.EF;
using Infrastructure.Persistent.EF.CategoryAgg;
using Infrastructure.Persistent.EF.CommentAgg;
using Infrastructure.Persistent.EF.OrderAgg;
using Infrastructure.Persistent.EF.ProductAgg;
using Infrastructure.Persistent.EF.RoleAgg;
using Infrastructure.Persistent.EF.SellerAgg;
using Infrastructure.Persistent.EF.SiteAgg;
using Infrastructure.Persistent.EF.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure;

public static class InfrastructureBootStrapper
{
    public static void Init(this IServiceCollection services,string connectionString)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<ISellerRepository, SellerRepository>();

        services.AddScoped<IBannerRepository, BannerRepository>();
        services.AddScoped<ISliderRepository, SliderRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddTransient(_=>new DapperContext(connectionString));

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("connection string is not configured.");
        }

        services.AddDbContext<ShahriarBookStoreContext>(option =>
        {
            option.UseSqlServer(connectionString);
        });
    }
}
