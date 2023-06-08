using FluentValidation;

namespace Order.Application.Features.Order.Commands;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(a => a.UserName)
            .NotEmpty().WithMessage("{UserName} is required!")
            .NotNull()
            .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters!");
    }
}

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(a => a.UserName)
            .NotEmpty().WithMessage("{UserName} is required!")
            .NotNull()
            .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters!");
    }
}
