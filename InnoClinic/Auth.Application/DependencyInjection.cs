using Auth.Application.Common.Abstractions;
using Auth.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Applications
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddMediatR(conf => conf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddTokens(configuration);
            return services;
        }

        public static IServiceCollection AddTokens(this IServiceCollection services, ConfigurationManager configuration)
        {
            JwtSettings jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton<ITokenGenerator, TokenGenerator>()
             .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(opt =>
                 opt.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuer = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = jwtSettings.Issuer,
                     ValidAudience = jwtSettings.Audience,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    )
                 });

            return services;
        }
    }
}
