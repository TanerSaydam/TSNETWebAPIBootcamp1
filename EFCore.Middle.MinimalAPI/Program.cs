using EFCore.Middle.MinimalAPI;
using EFCore.Middle.MinimalAPI.Context;
using EFCore.Middle.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetSection("SqlServer").Value);
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

//var provider = builder.Services.BuildServiceProvider();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/Create", (Product product, ApplicationDbContext context) =>
{
    context.Add(product);
    context.SaveChanges();

    return Results.Created();
});

app.MapGet("/GetAll", (ApplicationDbContext context) =>
{
    return Results.Ok(context.Products.Include(p => p.MyCategory).ToList());
});

app.MapGet("/GetAllCategories", (ApplicationDbContext context) =>
{
    var result = (from category in context.Categories
                  select new
                  {
                      Id = category.Id,
                      Name = category.Name,
                      Products = context.Products.Where(p => p.MyCategoryId == category.Id).Select(s => new
                      {
                          Id = s.Id,
                          Name = s.Name,
                      }).ToList()
                  }).ToList();

    return Results.Ok(result);
});

app.MapGet("/migrate", (ApplicationDbContext context) =>
{
    context.Database.Migrate();
    return Results.NoContent();
});

ExtensionMethods.MigrateDatabase(builder.Services);

app.Run();
