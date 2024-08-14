using EnglishTutorAI.Api.Extensions;
using EnglishTutorAI.Domain.Entities;
using EnglishTutorAI.Infrastructure;
using EnglishTutorAI.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.InstallServicesInAssembly(builder.Configuration);
configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

services.AddAuthorizationBuilder();
services.AddIdentityCore<User>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;
    })
    .AddErrorDescriber<AuthErrorDescriber>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

services.AddHttpContextAccessor();
services.AddCors();

var app = builder.Build();

app.UseCors(
    options => options.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!)
        .AllowAnyMethod()
        .AllowAnyHeader());

app.MapGroup("api/identity/")
    .WithTags("Identity")
    .MapMyIdentityApi<User>();

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