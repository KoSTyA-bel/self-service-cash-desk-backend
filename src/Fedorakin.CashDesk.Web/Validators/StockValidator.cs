using Fedorakin.CashDesk.Data.Models;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators;

public class StockValidator : AbstractValidator<Stock>
{
	public StockValidator()
	{
		RuleFor(x => x.Count)
			.GreaterThan(0).WithMessage("Count should be greater then 0");
    }
}
