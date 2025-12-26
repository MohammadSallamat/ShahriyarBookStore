using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.Products.AddImage;

public class AddImageProductCommandValidator:AbstractValidator<AddImageProductCommand>
{
    public AddImageProductCommandValidator()
    {
        RuleFor(b => b.ImageName)
           .NotNull().WithMessage(ValidationMessages.required("عکس"))
           .JustImageFile();

        RuleFor(b => b.Sequence)
            .GreaterThanOrEqualTo(0);
    }
}