using Application.CommonApplication.Validation;
using FluentValidation;

namespace Application.Sellers.AddInventory;

public class AddSellerInventoryCommandValidator : AbstractValidator<AddSellerInventoryCommand>
{
    public AddSellerInventoryCommandValidator()
    {

        RuleFor(b => b.Count)
           .NotNull().WithMessage(ValidationMessages.required("تعداد"))
            .GreaterThanOrEqualTo(0).WithMessage(ValidationMessages.MinLength);

        RuleFor(b => b.Price)
           .NotNull().WithMessage(ValidationMessages.required("قیمت"))
            .GreaterThanOrEqualTo(1000).WithMessage(ValidationMessages.minLength("قیمت",1000));
    }
}

