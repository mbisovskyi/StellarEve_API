using StellarEve_API.Services.AppServices;
using StellarEve_API.Services.EveAuthenticationServiceObjects;
using StellarEve_API.Utilities;
using System.Text.Json;

namespace StellarEve_API.Services.EveServices
{
    public class EveAuthenticationService : IEveAuthenticationService
    {

        private readonly HttpClient http;

        public EveAuthenticationService(HttpClient _http)
        {
            http = _http;
        }

        public async Task<StartAuthorizeCharacterResponse> StartAuthorizeCharacterAsync(StartAuthorizeCharacterRequest request)
        {
            StartAuthorizeCharacterResponse myResponse = new StartAuthorizeCharacterResponse();
            string callbackCode;
            string navigateToAddress;

            try
            {
                callbackCode = Strings.GetRandomString(10);
                navigateToAddress = request.AuthorizeCharacterEndpoint +
                    $"?response_type=code" +
                    $"&redirect_uri={request.ClientBaseAddress}eve-auth" +
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
                myResponse = new StartAuthorizeCharacterResponse();
                myResponse.Success = false;
                myResponse.Error = exception.Message;
                myResponse.StackTrace = exception.StackTrace;
            }

            return myResponse;
        }

        public async Task<ExchangeAuthorizationCodeForTokensResponse> ExchangeAuthorizationCodeForTokensAsync(ExchangeAuthorizationCodeForTokensRequest request)
        {
            ExchangeAuthorizationCodeForTokensResponse myResponse = new ExchangeAuthorizationCodeForTokensResponse();
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
                        // Compile myResponse
                        myResponse.Success = true;
                        myResponse.AccessToken = deserializedTokens.AccessToken;
                        myResponse.ExpiresIn = deserializedTokens.ExpiresIn;
                        myResponse.TokenType = deserializedTokens.TokenType;
                        myResponse.RefreshToken = deserializedTokens.RefreshToken;
                    }
                } else
                {
                    myResponse.Success = false;
                }
            }
            catch (Exception exception)
            {
                // Reseting response object, so no data is exposed due to exception.
                myResponse = new ExchangeAuthorizationCodeForTokensResponse();
                myResponse.Success = false;
                myResponse.Error = exception.Message;
                myResponse.StackTrace = exception.StackTrace;
            }
            return myResponse;
        }
    }
}
