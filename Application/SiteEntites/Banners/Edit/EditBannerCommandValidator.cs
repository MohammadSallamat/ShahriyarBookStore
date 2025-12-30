using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.SiteEntites.Banners.Edit;

public class EditBannerCommandValidator:AbstractValidator<EditBannerCommand>
{
    public EditBannerCommandValidator()
    {

        RuleFor(r => r.ImageFile)
            .JustImageFile();

        RuleFor(r => r.Link)
            .NotNull()
            .NotEmpty().WithMessage(ValidationMessages.required("لینک"));

    }
}

