global using FastEndpoints;
using WebAPI;
using WebAPI.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddNpgsql<CardinarDbContext>("Host=localhost;Port=5432;Username=postgres;Password=1234;Database=cardinar;");

builder.Services.AddAuth();
builder.Services.AddSwagger();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();

app.UseOpenApi();
app.UseSwaggerUi(opts => opts.Path = "/swagger/{documentName}");

app.Run();