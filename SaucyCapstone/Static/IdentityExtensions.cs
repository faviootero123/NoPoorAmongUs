using SaucyCapstone.Constants;
using System.Security.Claims;

namespace SaucyCapstone.Static;

public static class IdentityExtensions
{
    public static string UserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);      
    }
    public static string Email(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Email);
    }
    public static string Username(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.FindFirstValue(ClaimTypes.Name);
    }
    public static bool IsStaff(this ClaimsPrincipal claimsPrincipal)
    {
        return (claimsPrincipal.IsInRole(Roles.Instructor) || claimsPrincipal.IsInRole(Roles.Admin) || claimsPrincipal.IsInRole(Roles.Rater) || claimsPrincipal.IsInRole(Roles.SocialWorker));
    }

}
