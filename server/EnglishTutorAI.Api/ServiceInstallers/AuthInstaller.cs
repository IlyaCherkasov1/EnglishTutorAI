using System.Text;
using EnglishTutorAI.Api.Interfaces;
using EnglishTutorAI.Application.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EnglishTutorAI.Api.ServiceInstallers;

public class AuthInstaller : IServiceInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()!;
        var googleKeys = configuration.GetSection(nameof(GoogleKeys)).Get<GoogleKeys>()!;

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    RequireExpirationTime = true,
                };
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                options.ClientId = googleKeys.ClientId;
                options.ClientSecret = googleKeys.ClientSecret;
                options.SignInScheme = IdentityConstants.ExternalScheme;

                options.Events = new OAuthEvents()
                {
                    OnRedirectToAuthorizationEndpoint = context =>
                    {
                        context.Response.Redirect(context.RedirectUri + "&prompt=consent select_account");
                        return Task.CompletedTask;
                    },
                };
            });
    }
}