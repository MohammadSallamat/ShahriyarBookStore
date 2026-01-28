using Application.CommonApplication.Validation;
using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;


namespace Application.UsersApp.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .ValidPhoneNumber();

        RuleFor(r => r.Email)
            .EmailAddress().WithMessage("ایمیل نامعتبر است");

        RuleFor(f => f.Password)
            .NotEmpty().WithMessage(ValidationMessages.required("کلمه عبور"))
            .NotNull().WithMessage(ValidationMessages.required("کلمه عبور"))
            .MinimumLength(4).WithMessage("کلمه عبور باید بشتر از 4 کارکتر باشد");
    }
}