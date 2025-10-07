using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace StellarEve_API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Authority = config.GetValue<string>("EveAuthority");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudiences = new[] { config.GetValue<string>("EveClientId"), "EVE Online" },
                    ValidateIssuer = true,
                    ValidIssuer = config.GetValue<string>("EveValidIssuer")
                };
            });

            return services;
        }
    }
}
