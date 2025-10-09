using StellarEve_API.Services.AppServices;
using StellarEve_API.Services.CharacterServiceObjects;
using StellarEve_API.Services.EveAuthenticationServiceObjects;
using StellarEve_API.Utilities;
using System.Text.Json;

namespace StellarEve_API.Services.EveServices
{
    public class EveAuthenticationService : IEveAuthenticationService
    {

        private readonly HttpClient http;
        private readonly IEveCharacterService eveCharacterService;

        public EveAuthenticationService(HttpClient _http, IEveCharacterService _eveCharacterService)
        {
            http = _http;
            eveCharacterService = _eveCharacterService;
        }

        public async Task<ProcessEveAuthorizationCodesResponse> ProcessEveAuthorizationCodesAsync(ProcessEveAuthorizationCodesRequest request)
        {
            ProcessEveAuthorizationCodesResponse myResponse = new ProcessEveAuthorizationCodesResponse();
            string callbackCode;
            string navigateToAddress;

            try
            {
                callbackCode = Strings.GetRandomString(10);
                navigateToAddress = request.AuthorizeCharacterEndpoint +
                    $"?response_type=code" +
                    $"&redirect_uri={request.ClientBaseAddress}" +
                    $"&client_id={request.EveClientId}" +
                    $"&scope={request.EveScope}" +
                    $"&state={callbackCode}";

                // Compile myResponse
                myResponse.Success = true;
                myResponse.CallbackCode = callbackCode;
                myResponse.NavigateToAddress = navigateToAddress;
            }
            catch (Exception exception)
            {
                // Reseting response object, so no data is exposed due to exception.
                myResponse = new ProcessEveAuthorizationCodesResponse();
                myResponse.Success = false;
                myResponse.Error = exception.Message;
                myResponse.StackTrace = exception.StackTrace;
            }

            return myResponse;
        }

        public async Task<ProcessAuthorizeCharacterResponse> ProcessAuthorizeCharacterAsync(ProcessAuthorizeCharacterRequest request)
        {
            ProcessAuthorizeCharacterResponse myResponse = new ProcessAuthorizeCharacterResponse();
            Dictionary<string, string> data;

            try
            {
                AuthorizationService.AddBasicAuthorizationHeader(http, request.EveClientId, request.EveClientSecret);
                // Post to Eve's token endpoint
                data = new Dictionary<string, string>
                {
                    { "grant_type", "authorization_code" },
                    { "code", request.AuthorizationCode }
                };
                using FormUrlEncodedContent content = new FormUrlEncodedContent(data);
                HttpResponseMessage httpResponse = await http.PostAsync(request.ExchangeCodeForTokensEndpoint, content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    TokensSerializationObject? deserializedTokens = JsonSerializer.Deserialize<TokensSerializationObject>(await httpResponse.Content.ReadAsStringAsync());

                    if (deserializedTokens != null)
                    {
                        AuthorizedCharacterInfoResponse authorizedCharacterResponse = this.eveCharacterService.GetAuthorizedCharacterInfoAsync(deserializedTokens.AccessToken).Result;

                        if (authorizedCharacterResponse != null)
                        {

                            // Compile myResponse
                            myResponse.CharacterId = authorizedCharacterResponse.CharacterID;
                            myResponse.CharacterName = authorizedCharacterResponse.CharacterName;
                            myResponse.ExpiresOn = authorizedCharacterResponse.ExpiresOn;
                            myResponse.AccessToken = deserializedTokens.AccessToken;
                            myResponse.TokenType = deserializedTokens.TokenType;
                            myResponse.RefreshToken = deserializedTokens.RefreshToken;
                            myResponse.Success = true;
                        }
                    }
                } else
                {
                    myResponse.Success = false;
                }
            }
            catch (Exception exception)
            {
                // Reseting response object, so no data is exposed due to exception.
                myResponse = new ProcessAuthorizeCharacterResponse();
                myResponse.Success = false;
                myResponse.Error = exception.Message;
                myResponse.StackTrace = exception.StackTrace;
            }
            return myResponse;
        }
    }
}
