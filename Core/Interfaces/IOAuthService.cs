namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IOAuthService
    {
        string GetUserAuthToken(string userName, string userId);
    }
}
