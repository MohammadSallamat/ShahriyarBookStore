using Query.Common.Domain;
using Query.Wishlists.DTOs;
using System.Collections.Generic;

namespace Query.Wishlists.GetByUserId;

public record GetUserWishlistQuery(long UserId) : IBaseQuery<List<WishlistDto>>;
