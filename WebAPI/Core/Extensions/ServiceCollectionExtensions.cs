using System.Text;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Core.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddAuth(this IServiceCollection services)
  {
    services.AddAuthentication(x =>
    {
      x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
      x.TokenValidationParameters = new TokenValidationParameters
      {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VeryVeryReliableSecretKeyWhichIsLongEnough")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
      };
    });

    services.AddAuthorization(opts =>
    {
      opts.AddPolicy("Admin", policyBuilder => policyBuilder.RequireClaim("type", "admin"));
      opts.AddPolicy("Public", policyBuilder => policyBuilder.RequireClaim("type", "public"));
    });


    return services;
  }

  public static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.SwaggerDocument(opts =>
    {
      opts.EndpointFilter = endpoint => endpoint.EndpointTags?.Contains("Admin") == true;
      opts.DocumentSettings = docSettings => docSettings.DocumentName = "admin";
      opts.AutoTagPathSegmentIndex = 0;
    });

    services.SwaggerDocument(opts =>
    {
      opts.EndpointFilter = endpoint => endpoint.EndpointTags?.Contains("Public") == true;
      opts.DocumentSettings = docSettings => docSettings.DocumentName = "public";
      opts.AutoTagPathSegmentIndex = 0;
    });

    return services;
  }
}