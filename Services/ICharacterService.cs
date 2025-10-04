using StellarEve_API.Services.CharacterServiceObjects;

namespace StellarEve_API.Services
{
    public interface ICharacterService
    {
        Task<AuthorizedCharacterInfoResponse> GetAuthorizedCharacterInfo();
    }
}
