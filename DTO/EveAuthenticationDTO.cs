using System.ComponentModel.DataAnnotations;

namespace StellarEve_API.EveAuthenticationDTO
{
    public class PostAuthorizeCodeDTORequest
    {
        [Required]
        public string AuthorizationCode { get; set; }
    }
}
