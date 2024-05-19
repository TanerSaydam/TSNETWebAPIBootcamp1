using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddApiVersioning(action =>
{
    action.DefaultApiVersion = new ApiVersion(1, 0);
    action.AssumeDefaultVersionWhenUnspecified = true;
    action.ReportApiVersions = true;
    action.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version")
        );
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

//https://localhost:7064/api/values/gettodo?api-version=2

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

app.UseAuthorization();

app.MapControllers();

app.Run();
