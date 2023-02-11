using Fedorakin.CashDesk.Web.Contracts.Requests.Product;
using FluentValidation;

namespace Fedorakin.CashDesk.Web.Validators.Product;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
	public UpdateProductRequestValidator()
	{
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name can`t be empty")
            .Length(1, 50).WithMessage("Invalid name length");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description can`t be empty")
            .Length(1, 50).WithMessage("Invalid description length");

        RuleFor(x => x.Barcode)
            .NotEmpty().WithMessage("Product barcode can`t be empty")
            .Length(1, 50).WithMessage("Invalid barcode length");

        RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Invalid weight");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Invalid price");
    }
}
