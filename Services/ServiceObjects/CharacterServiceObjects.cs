using StellarEve_API.Services.ServiceBaseObjects;

namespace StellarEve_API.Services.CharacterServiceObjects
{
    public class AuthorizedCharacterInfoResponse : ServiceBaseResponse
    {
        public int CharacterID { get; set; }
        public string CharacterName { get; set; }
        public DateTime ExpiresOn { get; set; }
        public string Scopes { get; set; }
        public string TokenType { get; set; }
        public string CharacterOwnerHash { get; set; }
        public string IntellectualProperty { get; set; }
    }
}
