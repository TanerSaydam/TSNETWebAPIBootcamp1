using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using AuthenticationVeAuthorization.WebAPI.Options;
using AuthenticationVeAuthorization.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var secret = await GetSecret();

var config = new ConfigurationBuilder()
    .AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(secret)))
    .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});

//builder.Services.Configure<Jwt>(builder.Configuration.GetSection("JWT"));
builder.Services.ConfigureOptions<JwtOptions>(); //IConfigureOptions

var provider = builder.Services.BuildServiceProvider();
var jwt = provider.GetRequiredService<IOptionsMonitor<Jwt>>();

builder.Services.AddScoped<JwtProvider>();

//builder.Services.AddAuthentication("MyAuthScheme")
//    .AddScheme<ApiKeyAuthSchemeOptions, ApiKeyAuthHandler>("MyAuthScheme", _ => { });
string securityKeyValue = jwt.CurrentValue.SecretKey; //builder.Configuration.GetSection("JWT:SecretKey").Value!;
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyValue));
//JwtBearerDefaults.AuthenticationScheme = "Bearer"
builder.Services.ConfigureOptions<JwtOptionsSetup>(); //IPostConfigureOptions
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();


static async Task<string> GetSecret()
{
    IAmazonSecretsManager client = new AmazonSecretsManagerClient();

    GetSecretValueRequest request = new()
    {
        SecretId = "JWT2"
    };

    GetSecretValueResponse response;

    try
    {
        response = await client.GetSecretValueAsync(request);
    }
    catch (Exception)
    {

        throw;
    }

    return response.SecretString;
}