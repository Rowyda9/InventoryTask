

using BackCore.BLL.ViewModels;
using FluentValidation;

namespace BackCore.Validtor
{
	public  class ProductViewModelValidator : AbstractValidator<ProductFormViewModel>
	{
		public ProductViewModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(200);
			RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(1000);
			RuleFor(x => x.Barcode).NotEmpty().NotNull().MaximumLength(1000);
		}
	}
}
