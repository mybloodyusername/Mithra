using Mithra.Application.Services;
using Mithra.Domain.Exceptions;
using Mithra.Domain.Interfaces.Services;
using Mithra.Infra.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddConsole();

builder.Services.AddLogging(options => options.SetMinimumLevel(LogLevel.Trace).AddConsole());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddMithraDbContext(builder.Configuration);
builder.Services.AddIdentityDbContext(builder.Configuration);
builder.Services.AddCookieSetting(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicies(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IAuthService, AuthService>();


var app = builder.Build();

await app.InitializeDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.UseCors("DevelopmentPolicy");
}
else
{
    app.UseCors("ProductionPolicy");
}

// Configure the HTTP request pipeline.

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();