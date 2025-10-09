using StellarEve_API.Services.EveAuthenticationServiceObjects;

namespace StellarEve_API.Services.EveServices
{
    public interface IEveAuthenticationService
    {
        Task<ProcessEveAuthorizationCodesResponse> ProcessEveAuthorizationCodesAsync(ProcessEveAuthorizationCodesRequest request);
        Task<ProcessAuthorizeCharacterResponse> ProcessAuthorizeCharacterAsync(ProcessAuthorizeCharacterRequest request);
    }
}