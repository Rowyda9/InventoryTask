

using BackCore.BLL.ViewModels;
using FluentValidation;

namespace BackCore.Validtor
{
	public  class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
	{
		public CategoryViewModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(500);
		}
	}
}
