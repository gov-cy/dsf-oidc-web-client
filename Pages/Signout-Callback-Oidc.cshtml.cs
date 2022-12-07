using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class Signout_Callback_OidcModel : PageModel
    {
        public void OnGet()
        {
            SignOut("Cookies", "oidc");
        }
    }
}
