using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.Sellers.Create;

public class CreateSellerCommandvalidator : AbstractValidator<CreateSellerCommand>
{
    public CreateSellerCommandvalidator()
    {
        RuleFor(r => r.ShopName)
            .NotEmpty().WithMessage(ValidationMessages.required("نام فروشگاه"));

        RuleFor(r => r.NationalCode)
            .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
            .ValidNationalId();
    }
}