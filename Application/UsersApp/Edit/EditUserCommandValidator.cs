using Application.CommonApplication.Validation.FluentValidations;
using FluentValidation;

namespace Application.UsersApp.Edit;

internal partial class EditUserCommandHandler
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(r => r.PhoneNumber.Value)
                .ValidPhoneNumber();

            RuleFor(r => r.Email)
                .EmailAddress().WithMessage("ایمیل نامعتبر است");


            RuleFor(f => f.Avatar)
                .JustImageFile();
        }
    }
}