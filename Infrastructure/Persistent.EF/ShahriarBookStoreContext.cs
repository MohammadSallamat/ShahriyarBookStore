using Domain.CategoryAggregate;
using Domain.CommentAggregate;
using Domain.OrderAggregate;
using Domain.ProductAggregate;
using Domain.RoleAggregate;
using Domain.SellerAggregate;
using Domain.SiteEntities;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistent.EF;

public class ShahriarBookStoreContext : DbContext
{
    public ShahriarBookStoreContext(DbContextOptions<ShahriarBookStoreContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Seller> Sellers { get; set; }
    public DbSet<SellerInventory> SellerInventories { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShahriarBookStoreContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}   