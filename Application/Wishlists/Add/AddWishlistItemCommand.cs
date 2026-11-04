using Application.CommonApplication;

namespace Application.Wishlists.Add;

public record AddWishlistItemCommand(long UserId, long ProductId) : IBaseCommand<long>;
