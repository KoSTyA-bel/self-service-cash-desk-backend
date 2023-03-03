using Fedorakin.CashDesk.Web.Contracts.Requests.Stock;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Stock;

public class CreateStockRequestValidator : AbstractValidator<CreateStockRequest>
{
	public CreateStockRequestValidator()
	{
		RuleFor(x => x.Count)
			.GreaterThan(0).WithMessage("Count of product shoud be greater than 0");
	}
}
