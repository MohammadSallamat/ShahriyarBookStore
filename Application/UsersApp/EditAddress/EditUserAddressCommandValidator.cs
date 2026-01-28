using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.UsersApp.EditAddress;

internal partial class EditUserAddressCommandHandler
{
    public class EditUserAddressCommandValidator : AbstractValidator<EditUserAddressCommand>
    {
        public EditUserAddressCommandValidator()
        {
            RuleFor(f => f.City)
                .NotEmpty().WithMessage(ValidationMessages.required("شهر"));

            RuleFor(f => f.Province)
                .NotEmpty().WithMessage(ValidationMessages.required("استان"));

            RuleFor(f => f.Name)
                .NotEmpty().WithMessage(ValidationMessages.required("نام"));

            RuleFor(f => f.Family)
                .NotEmpty().WithMessage(ValidationMessages.required("نام خانوادگی"));

            RuleFor(f => f.NationalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("کدملی"))
                .ValidNationalId();

            RuleFor(f => f.PostalAddress)
                .NotEmpty().WithMessage(ValidationMessages.required("آدرس پستی"));

            RuleFor(f => f.PostalCode)
                .NotEmpty().WithMessage(ValidationMessages.required("کد پستی"));
        }
    }
}