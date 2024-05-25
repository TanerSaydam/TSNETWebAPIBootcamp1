using EntityFrameworkCore.WebAPI.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//service registration start here

//builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    options.UseSqlServer(builder.Configuration.GetSection("SqlServer").Value);
    //options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service registration stop here

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
