using Autofac;
using Autofac.Extensions.DependencyInjection;
using EnglishTutorAI.Api.Constants;
using EnglishTutorAI.Api.Extensions;
using EnglishTutorAI.Api.Health;
using EnglishTutorAI.Api.Middlewares;
using EnglishTutorAI.Application.Hubs;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Infrastructure;
using EnglishTutorAI.Infrastructure.DependencyInjection;
using EnglishTutorAI.Infrastructure.Identity;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

if (builder.Environment.IsProduction())
{
    services.ConfigureAwsServices(configuration);
}

services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 5;
    })
    .AddErrorDescriber<AuthErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacModule());
});

services.InstallServicesInAssembly(builder.Configuration);
services.AddAuthorizationBuilder();
services.AddSignalR();
services.AddMemoryCache();
services.AddHealthChecks()
    .AddCheck<DatabaseConnectivityHealthCheck>("DatabaseConnectivity")
    .AddCheck<DatabaseDataHealthCheck>("DatabaseData");

var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()!;

services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders(CustomHeaders.ExceptionTraceId);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.ApplyMigrationsAndSeedAsync();

app.UseCors();
app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();

app.MapHealthChecks("/_health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<AssistantHub>("/assistantHub");

app.Run();