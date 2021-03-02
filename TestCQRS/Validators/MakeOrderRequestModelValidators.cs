using FluentValidation;
using TestCQRS.Models;

namespace TestCQRS.Validators
{
    public class MakeOrderRequestModelValidators : AbstractValidator<MakeOrderRequestModel>
    {
        public MakeOrderRequestModelValidators()
        {
            RuleFor(x => x.OrderName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1);
        }
    }
}
