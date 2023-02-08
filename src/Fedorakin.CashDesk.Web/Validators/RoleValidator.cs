using Fedorakin.CashDesk.Data.Models;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators;

public class RoleValidator : AbstractValidator<Role>
{
	public RoleValidator()
	{
		RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Role name can`t be empty")
            .Length(1, 50).WithMessage("Invalid name length");
    }
}
