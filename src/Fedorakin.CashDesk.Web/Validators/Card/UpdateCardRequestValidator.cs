using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Card;

public class UpdateCardRequestValidator : AbstractValidator<UpdateCardRequest>
{
    public UpdateCardRequestValidator()
    {
        RuleFor(x => x.Discount)
            .GreaterThanOrEqualTo(0).WithMessage("Discount shold be greater or equal 0")
            .LessThan(100).WithMessage("Discount shold be lesss than 100");
    }
}
