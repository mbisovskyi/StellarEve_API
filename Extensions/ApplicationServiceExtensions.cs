using StellarEve_API.Services;

namespace StellarEve_API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddHttpContextAccessor();
            services.AddSingleton<IEveAuthenticationService, EveAuthenticationService>();
            services.AddSingleton<ICharacterService, CharacterService>();

            return services;
        }
    }
}
