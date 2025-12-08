using Application.CommonApplication.Validation;
using FluentValidation;

namespace Application.Comments.Create;

public class CreateCommentCommandValidator :AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(r => r.text)
            .NotNull()
            .MinimumLength(10).WithMessage(ValidationMessages.minLength(" کامنت شما", 10));
    }
}
