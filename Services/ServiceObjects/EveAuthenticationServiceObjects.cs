using StellarEve_API.Services.ServiceBaseObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

    public class ExchangeAuthorizationCodeForTokensRequest
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

    public class ExchangeAuthorizationCodeForTokensResponse : ServiceBaseResponse
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public int ExpiresIn { get; set; }
        [Required]
        public string TokenType { get; set; }
        [Required]
        public string RefreshToken { get; set; }
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
