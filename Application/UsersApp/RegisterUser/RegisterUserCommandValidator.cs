using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.UsersApp.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {

        RuleFor(r => r.PhoneNumber)
           .ValidPhoneNumber();

        RuleFor(f => f.Password)
                .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
                .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
                .MinimumLength(4).WithMessage("کلمه عبور باید بشتر از 4 کارکتر باشد");
    }
}
