using Fedorakin.CashDesk.Data.Models;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators;

public class ProfileValidator : AbstractValidator<Profile>
{
	public ProfileValidator()
	{
		RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Profile name can`t be empty")
            .Length(1, 50).WithMessage("Invalid name length");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("Role id should be greater than 0");
    }
}
