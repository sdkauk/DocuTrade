using System.Security.Claims;

namespace PaperTrade.BusinessLogic.Services.Claims
{
    public interface IClaimsService
    {
        string GetObjectId(ClaimsPrincipal user);
        string GetEmail(ClaimsPrincipal user);
        string GetDisplayName(ClaimsPrincipal user);
    }
}