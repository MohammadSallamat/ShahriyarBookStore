using Application.CommonApplication;

namespace Application.Wishlists.Remove;

public record RemoveWishlistItemCommand(long WishlistId, long UserId) : IBaseCommand;
