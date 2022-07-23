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
    public static bool IsInstructor(this ClaimsPrincipal claimsPrincipal){
        return claimsPrincipal.IsInRole(Roles.Instructor);
    }
    public static bool IsAdmin(this ClaimsPrincipal claimsPrincipal){
        return claimsPrincipal.IsInRole(Roles.Admin);
    }
    public static bool IsRater(this ClaimsPrincipal claimsPrincipal){
        return claimsPrincipal.IsInRole(Roles.Rater);
    }
    public static bool IsSocialWorker(this ClaimsPrincipal claimsPrincipal){
        return claimsPrincipal.IsInRole(Roles.SocialWorker);
    }
}
