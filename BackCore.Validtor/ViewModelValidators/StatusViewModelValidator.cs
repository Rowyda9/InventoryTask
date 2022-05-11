

using BackCore.BLL.ViewModels;
using FluentValidation;

namespace BackCore.Validtor
{
	public  class StatusViewModelValidator : AbstractValidator<StatusViewModel>
	{
		public StatusViewModelValidator()
		{
			RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(100);
			RuleFor(x => x.Description).MaximumLength(1000);
		
		}
	}
}
