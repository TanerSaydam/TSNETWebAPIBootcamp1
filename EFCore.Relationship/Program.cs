using EFCore.Relationship.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseLazyLoadingProxies(); //bu iliþki tablolarýnýn otomatik liste çekilirken joinlenmesini saðlar. Best practices bunu kullanmamamýzý önerir.
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    options.LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

using (var scoped = app.Services.CreateScope())
{
    var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();
