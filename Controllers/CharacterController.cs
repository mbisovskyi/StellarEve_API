using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StellarEve_API.Services;
using StellarEve_API.Services.CharacterServiceObjects;

namespace StellarEve_API.Controllers
{
    public class CharacterController : ControllerBase
    {
        ICharacterService characterService;
        public CharacterController(IConfiguration _config, ICharacterService _characterService) : base(_config) 
        {
            characterService = _characterService;
        }

        [Authorize]
        [HttpGet("verify")] // api/character/verify
        public async Task<IActionResult> VerifyAuthorizedCharacter()
        {
            AuthorizedCharacterInfoResponse response = await characterService.GetAuthorizedCharacterInfo();
            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}
