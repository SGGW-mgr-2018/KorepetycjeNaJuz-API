using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IOAuthService
    {
        string GetUserAuthToken(string userName, string userId);
        IDictionary<string, string> GetClaims(string token);
    }
}
