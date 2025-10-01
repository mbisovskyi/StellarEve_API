
using Microsoft.AspNetCore.Mvc;
using StellarEve_API.Services;
using StellarEve_API.Services.EveAuthenticationServiceObjects;

namespace StellarEve_API.Controllers
{
    public class EveAuthenticationController : EveControllerBase
    {
        // My Endpoints
        protected string AuthorizeCharacterEndpoint;
        protected string AuthorizeTokensEndpoint;

        // My Services
        IEveAuthenticationService eveAuthenticationService;

        public EveAuthenticationController(IConfiguration _config, IEveAuthenticationService _eveAuthenticationService) : base(_config)
        {
            // Construct My Endpoints
            AuthorizeCharacterEndpoint = EveAuthorizationBaseAddress + "authorize/";
            AuthorizeTokensEndpoint = EveAuthorizationBaseAddress + "token/";

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

            return Ok(eveAuthenticationService.StartAuthorizeCharacter(serviceRequest));
        }
    }
}
