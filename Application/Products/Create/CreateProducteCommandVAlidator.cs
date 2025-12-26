using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.Products.Create;

public class CreateProducteCommandVAlidator : AbstractValidator<CreateProducteCommand>
{
    public CreateProducteCommandVAlidator()
    {

        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Slug)
           .NotEmpty().WithMessage(ValidationMessages.required("Slug"));

        RuleFor(r => r.Description)
           .NotEmpty().WithMessage(ValidationMessages.required("توضیحات"));

        RuleFor(r => r.ImageFile)
           .JustImageFile();
    }
}

