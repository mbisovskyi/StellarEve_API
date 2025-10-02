using Microsoft.AspNetCore.Mvc;

namespace StellarEve_API.Controllers
{
    public class EveControllerBase : BaseApiController
    {
        protected string ClientBaseAddress;
        protected string EveAuthorizationBaseAddress;
        protected string EveClientId;
        protected string EveClientSecret;
        protected string EveScope;

        public EveControllerBase(IConfiguration _config) 
        {
            ClientBaseAddress = _config.GetValue<string>("ClientBaseAddress") ?? String.Empty;
            EveAuthorizationBaseAddress = _config.GetValue<string>("EveAuthorizationBaseAddress") ?? String.Empty;
            EveClientId = _config.GetValue<string>("EveClientId") ?? String.Empty;
            EveClientSecret = _config.GetValue<string>("EveClientSecret") ?? String.Empty;
            EveScope = _config.GetValue<string>("EveScope") ?? String.Empty;
        }
    }
}
