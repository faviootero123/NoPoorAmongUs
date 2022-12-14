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
    public static List<string> UserRoles(this ClaimsPrincipal claimsPrincipal)
    {
        List<string> roles = new List<string>();

        if (claimsPrincipal.IsInRole(Roles.Instructor))
            roles.Add(Roles.Instructor);
        if (claimsPrincipal.IsInRole(Roles.Admin))
            roles.Add(Roles.Admin);
        if (claimsPrincipal.IsInRole(Roles.Rater))
            roles.Add(Roles.Rater);
        if (claimsPrincipal.IsInRole(Roles.SocialWorker))
            roles.Add(Roles.SocialWorker);

        return roles;
    }
    public static List<string> GetAllRoles(this ClaimsPrincipal claimsPrincipal)
    {
        List<string> roles = new List<string>();

        roles.Add(Roles.Instructor);
        roles.Add(Roles.Admin);     
        roles.Add(Roles.Rater);
        roles.Add(Roles.SocialWorker);

        return roles;
    }
}
