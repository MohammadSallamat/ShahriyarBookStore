using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.DecreaseItemCount;


public class DecreaseOrderItemCountCommandValidator
    : AbstractValidator<DecreaseOrderItemCountCommand>
{
    public DecreaseOrderItemCountCommandValidator()
    {
        RuleFor(f => f.Count)
            .GreaterThanOrEqualTo(1).WithMessage("تعداد باید بیشتر از 0 باشد");
    }
}
