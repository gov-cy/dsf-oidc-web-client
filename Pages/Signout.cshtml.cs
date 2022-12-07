using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebClient.Pages;

public class SignoutModel : PageModel
{
    public IActionResult OnGet()
    {
        return SignOut("Cookies", "oidc");
    }

    ////Ariadni requirement so that they can logout the user with a get call to the account countroller
    //public void OidcSignOut(string sid)
    //{
    //    var cp = (ClaimsPrincipal)User;
    //    var sidClaim = cp.FindFirst("sid");
    //    if (sidClaim?.Value == sid)
    //    {
    //        // logout
    //        SignOut(CookieAuthenticationDefaults.AuthenticationScheme,
    //            OpenIdConnectDefaults.AuthenticationScheme);
    //    }
    //}
}