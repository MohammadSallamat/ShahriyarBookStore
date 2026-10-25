using Application.CommonApplication.Validation;
using Domain.DiscountAggregate.Enums;
using FluentValidation;

namespace Application.Discounts.Edit;

public class EditDiscountCommandValidator : AbstractValidator<EditDiscountCommand>
{
    public EditDiscountCommandValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty().WithMessage(ValidationMessages.required("کد تخفیف"));

        RuleFor(c => c.DiscountAmount)
            .GreaterThan(0).WithMessage("مقدار تخفیف باید بزرگتر از صفر باشد.");

        RuleFor(c => c.DiscountAmount)
            .LessThanOrEqualTo(100)
            .When(c => c.DiscountType == DiscountType.Percentage)
            .WithMessage("درصد تخفیف نمی‌تواند بیشتر از ۱۰۰ باشد.");
    }
}
