using StellarEve_API.Services.EveAuthenticationServiceObjects;

namespace StellarEve_API.Services
{
    public interface IEveAuthenticationService
    {
        StartAuthorizeCharacterResponse StartAuthorizeCharacter(StartAuthorizeCharacterRequest request);
        Task<ExchangeAuthorizationCodeForTokensResponse> ExchangeAuthorizationCodeForTokens(ExchangeAuthorizationCodeForTokensRequest request);
    }
}