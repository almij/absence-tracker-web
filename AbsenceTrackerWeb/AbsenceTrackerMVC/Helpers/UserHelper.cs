using System.Security.Claims;

namespace AbsenceTrackerMVC.Helpers
{
    public static class UserHelper
    {
        public static string Id(this ClaimsPrincipal claimsPrincipal) => 
            claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

        public static string Name(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }
}
