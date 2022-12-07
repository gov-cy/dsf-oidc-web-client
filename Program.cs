using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AllowAnonymousToPage("/Privacy");
    //options.Conventions.AllowAnonymousToPage("/Signout");
    //options.Conventions.AllowAnonymousToPage("/signout-callback-oidc");
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "oidc"; //OpenIdConnectDefaults.AuthenticationScheme; //"oidc";
})
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.Name = "DsfIdentityServerAuthCookie";
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Authority"];

        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.SaveTokens = true;
        options.UsePkce = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Scope.Add("api1");
        options.Scope.Add("dsf.submission");


        options.ClaimActions.MapJsonKey("unique_identifier", "unique_identifier");
        options.ClaimActions.MapJsonKey("legal_unique_identifier", "legal_unique_identifier");
        options.ClaimActions.MapJsonKey("legal_main_profile", "legal_main_profile");
        //EIDAS
        options.ClaimActions.MapJsonKey("given_name", "given_name");
        options.ClaimActions.MapJsonKey("family_name", "family_name");
        options.ClaimActions.MapJsonKey("birthdate", "birthdate");

        options.Events = new OpenIdConnectEvents
        {
            //OnTokenValidated = context =>
            //{
            //    context.HandleResponse();
            //    context.Response.Redirect("/");
            //    return Task.FromResult(0);
            //},

            OnRemoteFailure = context => {
                context.HandleResponse();
                context.Response.Redirect("/");
                return Task.FromResult(0);
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
