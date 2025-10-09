using StellarEve_API.Services.CharacterServiceObjects;
using System.Text.Json;
using StellarEve_API.SystemObjects;
using StellarEve_API.Services.AppServices;

namespace StellarEve_API.Services.EveServices
{
    public class EveCharacterService : IEveCharacterService
    {
        HttpClient http;

        public EveCharacterService(HttpClient _http)
        {
            http = _http;
        }

        public async Task<AuthorizedCharacterInfoResponse?> GetAuthorizedCharacterInfoAsync(string? accessToken)
        {
            HttpResponseMessage httpResponse;
            AuthorizedCharacterInfoResponse? response = new AuthorizedCharacterInfoResponse();

            try
            {
                if (string.IsNullOrWhiteSpace(accessToken))
                {
                    accessToken = AuthorizationService.RetrieveAccessTokenFromHttpContextAsync().Result;
                }

                if (accessToken != null)
                {
                    AuthorizationService.AddBearerAuthorizationHeader(http, accessToken);
                    httpResponse = http.GetAsync(EveApiConstants.Endpoints.GetCharacterPublicData).Result;
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        response = JsonSerializer.Deserialize<AuthorizedCharacterInfoResponse>(await httpResponse.Content.ReadAsStringAsync());

                        if (response != null)
                        {
                            response.Success = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                response = new AuthorizedCharacterInfoResponse()
                {
                    Success = false,
                    Error = exception.Message,
                    StackTrace = exception.StackTrace
                };
            }

            return response;
        }
    }
}
