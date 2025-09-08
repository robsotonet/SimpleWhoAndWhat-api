using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using WhoAndWhat.Application;
using WhoAndWhat.Application.Features.Users.Commands;
using WhoAndWhat.Application.Features.Users.Queries;

// Configure Serilog for bootstrap logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

    // Add services to the container.
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>());
    builder.Services.AddValidatorsFromAssemblyContaining<IApplicationMarker>();

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!))
        };
    });

    builder.Services.AddAuthorization();

    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<LogContextMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGet("/health", () => "Healthy")
    .WithName("HealthCheck")
    .WithTags("Health");

    var authGroup = app.MapGroup("/auth");

    authGroup.MapPost("/register", async (RegisterUserCommand command, IMediator mediator) =>
    {
        var userId = await mediator.Send(command);
        return Results.Ok(new { UserId = userId });
    })
    .WithName("RegisterUser")
    .WithTags("Authentication");

    authGroup.MapPost("/login", async (LoginQuery query, IMediator mediator) =>
    {
        var tokens = await mediator.Send(query);
        return Results.Ok(tokens);
    })
    .WithName("LoginUser")
    .WithTags("Authentication");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}
