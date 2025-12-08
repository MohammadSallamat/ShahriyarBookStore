using Application.CommonApplication.Validation;
using FluentValidation;

namespace Application.Comments.Edit;

public class EditCommentCommandValidator : AbstractValidator<EditCommentCommand>
{
    public EditCommentCommandValidator()
    {

        RuleFor(r => r.Text)
            .NotNull()
            .MinimumLength(10).WithMessage(ValidationMessages.minLength(" کامنت شما", 10));
    }

}

