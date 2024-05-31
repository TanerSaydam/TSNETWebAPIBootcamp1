using FluentValidation.Results;
using FluentValidation.WebAPI.DTOs;
using FluentValidation.WebAPI.Models;
using FluentValidation.WebAPI.Services;
using FluentValidation.WebAPI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FluentValidation.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController(ProductService productService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto request, CancellationToken cancellationToken)
    {
        CreateProductDtoValidator validator = new(productService);
        ValidationResult result = validator.Validate(request, options => options.IncludeRuleSets("string kontrol"));

        if (!result.IsValid)
        {
            List<string> errors = result.Errors.Select(s => s.ErrorMessage).ToList();
            return StatusCode(428, errors);
        }

        //bool isNameExists = await productService.IsNameExistsAsync(request.Name, cancellationToken);
        //if (isNameExists)
        //{
        //    return StatusCode(406, new { Message = "Ürün adı daha önce oluşturulmuş" });
        //}


        Product product = new()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Quantity = request.Quantity,
        };


        await productService.CreateAsync(product, cancellationToken);

        //if (!request.Name.CheckNullEmptyAndMinLength(4))
        //{
        //    return StatusCode(428, new { ErrorMessage = "Name alanı boş olamaz ve en az 4 karakter olmalıdır" });
        //}

        //if (!request.Description.CheckNullEmptyAndMinLength(4))
        //{
        //    return StatusCode(428, new { ErrorMessage = "Description alanı boş olamaz ve en az 4 karakter olmalıdır" });
        //}

        return Ok();
    }
}
