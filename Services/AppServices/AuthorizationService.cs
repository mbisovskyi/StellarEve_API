using Microsoft.AspNetCore.Authentication;

namespace StellarEve_API.Services.AppServices
{
    public class AuthorizationService
    {
        public async static Task<string?> RetrieveAccessTokenFromHttpContextAsync()
        {
            IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
            return await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }

        public static void AddBasicAuthorizationHeader(HttpClient http, string clientId, string clientSecret)
        {
            string credentials = $"{clientId}:{clientSecret}";
            byte[] credentialsBytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            string base64Credentials = Convert.ToBase64String(credentialsBytes);
            string authorizationHeader = $"Basic {base64Credentials}";
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
        }

        public static void AddBearerAuthorizationHeader(HttpClient http, string accessToken)
        {
            string authorizationHeader = $"Bearer {accessToken}";
            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("Authorization", authorizationHeader);
        }
    }
}
