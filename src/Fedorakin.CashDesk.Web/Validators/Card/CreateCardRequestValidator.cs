﻿using Fedorakin.CashDesk.Web.Contracts.Requests.Card;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Card;

public class CreateCardRequestValidator : AbstractValidator<CreateCardRequest>
{
	public CreateCardRequestValidator()
	{
		RuleFor(x => x.Code)
			.Length(16).WithMessage("Card code should be equal 16");

		RuleFor(x => x.Discount)
			.GreaterThanOrEqualTo(0).WithMessage("Discount shold be greater or equal 0")
			.LessThan(100).WithMessage("Discount shold be lesss than 100");

		RuleFor(x => x.CVV)
			.Length(3).WithMessage("Card CVV should contains 3 symbols");
	}
}
