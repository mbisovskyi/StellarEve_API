using StellarEve_API.Services.ServiceBaseObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StellarEve_API.Services.EveAuthenticationServiceObjects
{
    public class ProcessEveAuthorizationCodesRequest
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

    public class ProcessEveAuthorizationCodesResponse : ServiceBaseResponse 
    {
        [Required]
        public string NavigateToAddress { get; set; }
        [Required]
        public string CallbackCode { get; set; }
    }

    public class ProcessAuthorizeCharacterRequest
    {
        [Required]
        public string ExchangeCodeForTokensEndpoint { get; set; }
        [Required]
        public string EveClientId { get; set; }
        [Required]
        public string EveClientSecret { get; set; }
        [Required]
        public string AuthorizationCode { get; set; }
    }

    public class ProcessAuthorizeCharacterResponse : ServiceBaseResponse
    {
        [Required]
        public int CharacterId { get; set; }
        [Required]
        public string CharacterName { get; set; }
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string TokenType { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public DateTime ExpiresOn { get; set; }
    }

    public class TokensSerializationObject
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
