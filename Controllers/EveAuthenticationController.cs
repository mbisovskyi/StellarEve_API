
using Microsoft.AspNetCore.Mvc;
using StellarEve_API.EveAuthenticationDTO;
using StellarEve_API.Services;
using StellarEve_API.Services.EveAuthenticationServiceObjects;

namespace StellarEve_API.Controllers
{
    public class EveAuthenticationController : EveControllerBase
    {
        // My Endpoints
        protected string AuthorizeCharacterEndpoint;
        protected string ExchangeCodeForTokensEndpoint;

        // My Services
        IEveAuthenticationService eveAuthenticationService;

        public EveAuthenticationController(IConfiguration _config, IEveAuthenticationService _eveAuthenticationService) : base(_config)
        {
            // Construct My Endpoints
            AuthorizeCharacterEndpoint = EveAuthorizationBaseAddress + "authorize/";
            ExchangeCodeForTokensEndpoint = EveAuthorizationBaseAddress + "token/";

            // Construct My Services
            eveAuthenticationService = _eveAuthenticationService;

        }

        [HttpGet("authorize/character")] // api/eveauthentication/authorize/character
        public ActionResult<StartAuthorizeCharacterResponse> GetAuthorizeCharacter()
        {
            StartAuthorizeCharacterRequest serviceRequest = new StartAuthorizeCharacterRequest() 
            { 
                ClientBaseAddress = ClientBaseAddress, 
                AuthorizeCharacterEndpoint = AuthorizeCharacterEndpoint,
                EveClientId = EveClientId,
                EveScope = EveScope,
            };

            StartAuthorizeCharacterResponse response = eveAuthenticationService.StartAuthorizeCharacter(serviceRequest);

            return response.Success ? Ok(response) : StatusCode(500, response);
        }

        [HttpPost("authorize/code")] // api/eveauthentication/authorize/code
        public ActionResult PostAuthorizeCode(PostAuthorizeCodeDTORequest requestDto)
        {
            ExchangeAuthorizationCodeForTokensRequest serviceRequest = new ExchangeAuthorizationCodeForTokensRequest()
            {
                ExchangeCodeForTokensEndpoint = ExchangeCodeForTokensEndpoint,
                EveClientId = EveClientId,
                EveClientSecret = EveClientSecret,
                AuthorizationCode = requestDto.AuthorizationCode
            };

            ExchangeAuthorizationCodeForTokensResponse response = eveAuthenticationService.ExchangeAuthorizationCodeForTokens(serviceRequest).Result;
            
            return response.Success ? Ok(response) : BadRequest();
        }
    }
}
