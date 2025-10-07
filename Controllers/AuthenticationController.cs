using Microsoft.AspNetCore.Mvc;
using StellarEve_API.EveAuthenticationDTO;
using StellarEve_API.Services.EveAuthenticationServiceObjects;
using StellarEve_API.Services.EveServices;

namespace StellarEve_API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        // My Endpoints
        protected string AuthorizeCharacterEndpoint;
        protected string ExchangeCodeForTokensEndpoint;

        // My Services
        IEveAuthenticationService eveAuthenticationService;

        public AuthenticationController(IConfiguration _config, IEveAuthenticationService _eveAuthenticationService, IHttpContextAccessor _httpContextAccessor) : base(_config)
        {
            // Construct My Endpoints
            AuthorizeCharacterEndpoint = EveAuthorizationBaseAddress + "authorize/";
            ExchangeCodeForTokensEndpoint = EveAuthorizationBaseAddress + "token/";

            // Construct My Services
            eveAuthenticationService = _eveAuthenticationService;

        }

        [HttpGet("authorize/character")] // api/authentication/authorize/character
        public async Task<IActionResult> GetAuthorizeCharacter()
        {
            StartAuthorizeCharacterRequest serviceRequest = new StartAuthorizeCharacterRequest() 
            { 
                ClientBaseAddress = ClientBaseAddress, 
                AuthorizeCharacterEndpoint = AuthorizeCharacterEndpoint,
                EveClientId = EveClientId,
                EveScope = EveScope,
            };

            StartAuthorizeCharacterResponse response = await eveAuthenticationService.StartAuthorizeCharacterAsync(serviceRequest);

            return response.Success ? Ok(response) : BadRequest();
        }

        [HttpPost("authorize/code")] // api/authentication/authorize/code
        public async Task<IActionResult> PostAuthorizeCode(PostAuthorizeCodeDTORequest requestDto)
        {
            ExchangeAuthorizationCodeForTokensRequest serviceRequest = new ExchangeAuthorizationCodeForTokensRequest()
            {
                ExchangeCodeForTokensEndpoint = ExchangeCodeForTokensEndpoint,
                EveClientId = EveClientId,
                EveClientSecret = EveClientSecret,
                AuthorizationCode = requestDto.AuthorizationCode
            };

            ExchangeAuthorizationCodeForTokensResponse response = await eveAuthenticationService.ExchangeAuthorizationCodeForTokensAsync(serviceRequest);
            
            return response.Success ? Ok(response) : BadRequest();
        }
    }
}
