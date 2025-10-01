using Microsoft.AspNetCore.Components;
using StellarEve_API.Services.EveAuthenticationServiceObjects;
using StellarEve_API.Utilities;

namespace StellarEve_API.Services
{
    public class EveAuthenticationService : IEveAuthenticationService
    {
        public StartAuthorizeCharacterResponse StartAuthorizeCharacter(StartAuthorizeCharacterRequest request)
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

                // Compile myResponse;
                myResponse.Success = true;
                myResponse.CallbackCode = callbackCode;
                myResponse.NavigateToAddress = navigateToAddress;
            }
            catch (Exception exception)
            {
                myResponse.Success = false;
                myResponse.Error = exception.Message;
                myResponse.StackTrace = exception.StackTrace;
            }

            return myResponse;
        }
    }
}
