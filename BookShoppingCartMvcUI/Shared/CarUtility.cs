using Microsoft.AspNetCore.Identity;

namespace BookShoppingCartMvcUI.Shared;

public static class CartUtility
{
    public static string GetUserId(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        var principal = httpContextAccessor.HttpContext.User;
        string userId = userManager.GetUserId(principal);
        return userId;
    }
}