using Query.Common.Domain;
using Query.Products.DTOs;

namespace Query.Wishlists.DTOs;

public class WishlistDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public ProductDto? Product { get; set; }
}
