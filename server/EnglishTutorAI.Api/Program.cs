using Autofac;
using Autofac.Extensions.DependencyInjection;
using EnglishTutorAI.Api.Extensions;
using EnglishTutorAI.Api.Middlewares;
using EnglishTutorAI.Application.Hubs;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Infrastructure;
using EnglishTutorAI.Infrastructure.DependencyInjection;
using EnglishTutorAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddIdentity<User, IdentityRole<Guid>>(options =>
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

var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()!;
services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("X-Trace-Id");
    });
});

var app = builder.Build();

app.UseCors();
app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<AssistantHub>("/assistantHub");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();