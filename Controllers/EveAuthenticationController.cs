using Microsoft.AspNetCore.Mvc;
using StellarEve_API.EveAuthenticationDTO;
using StellarEve_API.Services.EveAuthenticationServiceObjects;
using StellarEve_API.Services.EveServices;

namespace StellarEve_API.Controllers
{
    public class EveAuthenticationController : ControllerBase
    {
        // My Endpoints
        protected string AuthorizeCharacterEndpoint;
        protected string ExchangeCodeForTokensEndpoint;

        // My Services
        IEveAuthenticationService eveAuthenticationService;

        public EveAuthenticationController(IConfiguration _config, IEveAuthenticationService _eveAuthenticationService, IHttpContextAccessor _httpContextAccessor) : base(_config)
        {
            // Construct My Endpoints
            AuthorizeCharacterEndpoint = EveAuthorizationBaseAddress + "authorize/";
            ExchangeCodeForTokensEndpoint = EveAuthorizationBaseAddress + "token/";

            // Construct My Services
            eveAuthenticationService = _eveAuthenticationService;

        }

        [HttpGet("authorization_codes")] // api/eveauthentication/authorization_codes
        public async Task<IActionResult> GetEveAuthorizationCodes()
        {
            ProcessEveAuthorizationCodesRequest serviceRequest = new ProcessEveAuthorizationCodesRequest() 
            { 
                ClientBaseAddress = ClientBaseAddress, 
                AuthorizeCharacterEndpoint = AuthorizeCharacterEndpoint,
                EveClientId = EveClientId,
                EveScope = EveScope,
            };

            ProcessEveAuthorizationCodesResponse response = await eveAuthenticationService.ProcessEveAuthorizationCodesAsync(serviceRequest);

            return response.Success ? Ok(response) : BadRequest();
        }

        [HttpPost("authorize_character")] // api/eveauthentication/authorize_character
        public async Task<IActionResult> AuthorizeCharacter(AuthorizeCharacterRequestDto requestDto)
        {
            ProcessAuthorizeCharacterRequest serviceRequest = new ProcessAuthorizeCharacterRequest()
            {
                ExchangeCodeForTokensEndpoint = ExchangeCodeForTokensEndpoint,
                EveClientId = EveClientId,
                EveClientSecret = EveClientSecret,
                AuthorizationCode = requestDto.AuthorizationCode
            };

            ProcessAuthorizeCharacterResponse response = await eveAuthenticationService.ProcessAuthorizeCharacterAsync(serviceRequest);
            
            return response.Success ? Ok(response) : BadRequest();
        }
    }
}
