using Microsoft.AspNetCore.Authentication;
using StellarEve_API.Services.CharacterServiceObjects;
using System.Text.Json;

namespace StellarEve_API.Services
{
    public class CharacterService : ICharacterService
    {
        IHttpContextAccessor httpContextAccessor;
        HttpClient http;

        public CharacterService(IHttpContextAccessor _httpContextAccessor, HttpClient _http)
        {
            httpContextAccessor = _httpContextAccessor;
            http = _http;
        }

        public async Task<AuthorizedCharacterInfoResponse?> GetAuthorizedCharacterInfo()
        {
            string? accessToken;
            string authorizationHeader;
            HttpResponseMessage httpResponse;
            AuthorizedCharacterInfoResponse? response = new AuthorizedCharacterInfoResponse();

            try
            {
                accessToken = httpContextAccessor.HttpContext.GetTokenAsync("access_token").Result;
                if (accessToken != null)
                {
                    authorizationHeader = $"Bearer {accessToken}";
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

                    httpResponse = http.GetAsync("https://esi.evetech.net/verify/").Result;
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
