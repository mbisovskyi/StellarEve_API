using System.ComponentModel.DataAnnotations;

namespace StellarEve_API.EveAuthenticationDTO
{
    public class AuthorizeCharacterRequestDto
    {
        [Required]
        public string AuthorizationCode { get; set; }
    }
}
