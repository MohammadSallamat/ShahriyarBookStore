using FluentValidation;

namespace Application.Orders.AddItem;

public class AddOrderItemCommanValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommanValidator()
    {
        RuleFor(f => f.Count)
          .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
    }
}
