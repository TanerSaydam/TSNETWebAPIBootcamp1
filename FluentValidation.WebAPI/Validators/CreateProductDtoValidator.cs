using FluentValidation.WebAPI.DTOs;
using FluentValidation.WebAPI.Services;

namespace FluentValidation.WebAPI.Validators;

public sealed class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator(ProductService productService)
    {
        RuleSet("string kontrol", () =>
        {
            RuleFor(p => p.Description)
            .DoluOlmali();

            RuleFor(p => p.Name)
                .DoluOlmali()
                .Matches(@"[a-z]").WithMessage("Name de küçük harf olmalıdır")
                .Matches(@"[A-Z]").WithMessage("Name de büyük harf olmalıdır")
                .Matches(@"[0-9]").WithMessage("Name de sayı olmalıdır");

        });

        //.MustAsync(async (name, cancellation) =>
        //{
        //    return await productService.IsNameExistsAsync(name);
        //}).WithMessage("Ürün adı daha önce oluşturulmuş");

        RuleFor(p => p.Price).GreaterThan(0).When(p => p.Name == "Domates");//.SetValidator(new PriceValidator());
    }
}
