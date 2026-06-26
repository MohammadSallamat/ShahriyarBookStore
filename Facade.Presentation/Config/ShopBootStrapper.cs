using Application.CommonApplication._Utilities;
using Application.Roles.Create;
using Domain.CategoryAggregate.Services;
using Domain.ProductAggregate.Services;
using Domain.SellerAggregate.Services;
using Domain.UserAggregate.DomainServices;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Query.Users.List_of_users;
using FluentValidation;
using Application.Products;
using Application.UsersApp;
using Application.Categories0;
using Application.Sellers;

namespace Facade.Presentation.Config;

public static class ShopBootStrapper
{
    public static void RegisterShopDependencies
        (this IServiceCollection services, string connectionString)
    {
        InfrastructureBootStrapper.Init(services, connectionString);

        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Directories).Assembly));
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(GetUserListQuery).Assembly));

        services.AddTransient<IProductDomainService, ProductDomainService>();
        services.AddTransient<IUserDomainService, UserDomainService>();
        services.AddTransient<ICategoryDomainService, CategoryDomainService>();
        services.AddTransient<ISellerDomainService, SellerDomainService>();

        services.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);
    }
}
