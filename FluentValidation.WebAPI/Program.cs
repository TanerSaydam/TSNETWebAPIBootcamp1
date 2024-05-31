using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.WebAPI.Context;
using FluentValidation.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly).AddProblemDetails();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddSingleton<ApplicationDbContext>();
builder.Services.AddSingleton<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {

        throw;
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
