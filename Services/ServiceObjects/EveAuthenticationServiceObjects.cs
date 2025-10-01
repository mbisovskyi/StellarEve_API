using StellarEve_API.Services.ServiceBaseObjects;
using System.ComponentModel.DataAnnotations;

namespace StellarEve_API.Services.EveAuthenticationServiceObjects
{
    public class StartAuthorizeCharacterRequest
    {
        [Required]
        public string ClientBaseAddress { get; set; }
        [Required]
        public string AuthorizeCharacterEndpoint { get; set; }
        [Required]
        public string EveClientId { get; set; }
        [Required]
        public string EveScope { get; set; }
    }

    public class StartAuthorizeCharacterResponse : ServiceBaseResponse 
    {
        [Required]
        public string NavigateToAddress { get; set; }
        [Required]
        public string CallbackCode { get; set; }
    }
}
