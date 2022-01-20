using Microsoft.AspNetCore.Authorization;

namespace PM.CloudPlatform.ForkliftManager.Apis.Authorization
{
    /// <summary>
    /// Token
    /// </summary>
    public class TokenParameter : IAuthorizationRequirement
    {
        public string Name { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;

        public int AccessExpiration { get; set; }
        public int RefreshExpiration { get; set; }
    }
}