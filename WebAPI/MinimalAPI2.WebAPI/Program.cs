using MinimalAPI2.WebAPI.Enpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseTodo1EndPoint();
app.UseTodo2EndPoint();

app.MapControllers();

app.Run();
