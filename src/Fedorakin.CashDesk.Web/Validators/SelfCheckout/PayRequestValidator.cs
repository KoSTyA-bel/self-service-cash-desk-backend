using Fedorakin.CashDesk.Web.Contracts.Requests.SelfCheckout;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.SelfCheckout;

public class PayRequestValidator : AbstractValidator<PayRequest>
{
	public PayRequestValidator()
	{
		RuleFor(x => x.CartNumber)
            .NotEqual(Guid.Empty).WithMessage("Cart number can`t be empty");
    }
}
