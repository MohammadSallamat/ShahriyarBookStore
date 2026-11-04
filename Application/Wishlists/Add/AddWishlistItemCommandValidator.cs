using FluentValidation;

namespace Application.Wishlists.Add;

public class AddWishlistItemCommandValidator : AbstractValidator<AddWishlistItemCommand>
{
    public AddWishlistItemCommandValidator()
    {
        RuleFor(w => w.UserId)
            .GreaterThan(0).WithMessage("شناسه کاربر نامعتبر است.");

        RuleFor(w => w.ProductId)
            .GreaterThan(0).WithMessage("شناسه محصول نامعتبر است.");
    }
}
