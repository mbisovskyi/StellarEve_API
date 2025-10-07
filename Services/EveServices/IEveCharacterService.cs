using StellarEve_API.Services.CharacterServiceObjects;

namespace StellarEve_API.Services.EveServices
{
    public interface IEveCharacterService
    {
        Task<AuthorizedCharacterInfoResponse> GetAuthorizedCharacterInfo();
    }
}
