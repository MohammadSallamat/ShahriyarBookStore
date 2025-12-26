using Application.CommonApplication.Validation;
using FluentValidation;

namespace Application.Roles.Edit;

public class EditRoleCommandValidator : AbstractValidator<EditRoleCommand>
{
    public EditRoleCommandValidator()
    {

        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

    }
}

