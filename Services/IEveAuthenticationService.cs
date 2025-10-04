using StellarEve_API.Services.EveAuthenticationServiceObjects;

namespace StellarEve_API.Services
{
    public interface IEveAuthenticationService
    {
        Task<StartAuthorizeCharacterResponse> StartAuthorizeCharacterAsync(StartAuthorizeCharacterRequest request);
        Task<ExchangeAuthorizationCodeForTokensResponse> ExchangeAuthorizationCodeForTokensAsync(ExchangeAuthorizationCodeForTokensRequest request);
    }
}