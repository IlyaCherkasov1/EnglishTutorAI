using EnglishTutorAI.Api.Extensions;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Infrastructure;
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

services.InstallServicesInAssembly(builder.Configuration);
configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

services.AddAuthorizationBuilder();

var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()!;
services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();
app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();