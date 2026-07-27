global using FastEndpoints;
using WebAPI;
using WebAPI.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddNpgsql<CardinarDbContext>("Host=localhost;Port=5432;Username=postgres;Password=123;Database=cardinar;");
builder.Services.AddAuth();
builder.Services.AddSwagger();

var app = builder.Build();
app.UseFastEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.UseOpenApi();
app.UseSwaggerUi(opts => opts.Path = "/swagger/{documentName}");

app.Run();