using System.Security.Claims;

namespace KorepetycjeNaJuz.Core.Helpers
{
    public static class Extensions
    {
        public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirst("UserId")?.Value;
            return userId?.ToNullableInt();
        }

        public static int? ToNullableInt(this string str)
        {
            if (int.TryParse(str, out int i)) return i;
            return null;
        }
    }
}
