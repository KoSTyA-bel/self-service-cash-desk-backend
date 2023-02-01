using Fedorakin.CashDesk.Data.Models;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators;

public class CardValidator : AbstractValidator<Card>
{
	public CardValidator()
	{
		RuleFor(x => x.Code)
			.NotEmpty().WithMessage("Card code can`t be empty")
            .Length(1, 50).WithMessage("Invalid code length");

		RuleFor(x => x.Discount)
			.GreaterThan(0).WithMessage("Discount cannot be less than 0")
			.LessThan(100).WithMessage("Discount cannot be more than 100");
    }
}
