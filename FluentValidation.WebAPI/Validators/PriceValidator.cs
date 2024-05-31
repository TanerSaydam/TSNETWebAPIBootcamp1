namespace FluentValidation.WebAPI.Validators;

public sealed class PriceValidator : AbstractValidator<decimal>
{
    public PriceValidator()
    {
        RuleFor(p => p).GreaterThan(0);
    }
}
