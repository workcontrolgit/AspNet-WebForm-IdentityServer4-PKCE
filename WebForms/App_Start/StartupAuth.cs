using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace WebForms
{
    public partial class Startup
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["Oidc:ClientId"];
        private readonly string _secret = ConfigurationManager.AppSettings["Oidc:Secret"];
        private readonly string _redirectUri = ConfigurationManager.AppSettings["Oidc:RedirectUri"];
        private readonly string _scope = ConfigurationManager.AppSettings["Oidc:Scope"];
        private readonly string _authority = ConfigurationManager.AppSettings["Oidc:Authority"];
        private readonly string _postLogoutRedirectUri = ConfigurationManager.AppSettings["Oidc:PostLogoutRedirectUri"];
        private readonly string _responseType = ConfigurationManager.AppSettings["Oidc:ResponseType"];
        private readonly string _authenticationType = ConfigurationManager.AppSettings["Oidc:AuthenticationType"];
        private readonly string _signInAsAuthenticationType = ConfigurationManager.AppSettings["Oidc:SignInAsAuthenticationType"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = _authenticationType,
                ExpireTimeSpan = TimeSpan.FromMinutes(10),
                SlidingExpiration = true
            });

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {

                AuthenticationType = _authenticationType,
                SignInAsAuthenticationType = _signInAsAuthenticationType,

                Authority = _authority,

                ClientId = _clientId,
                ClientSecret = _secret,

                RedirectUri = _redirectUri,
                PostLogoutRedirectUri = _postLogoutRedirectUri,

                ResponseType = _responseType,

                Scope = _scope,


                UseTokenLifetime = false,
                SaveTokens = true,
                RedeemCode = true,
                UsePkce = true,
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = SetIdTokenHintOnLogout
                }
            });

        }

        // Set the id_token_hint parameter during logout so that
        // IdentityServer can safely redirect back here after
        // logout. Unlike .NET Core authentication handler, the Owin
        // middleware doesn't do this automatically.
        private async Task SetIdTokenHintOnLogout(
            RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            if (notification.ProtocolMessage.PostLogoutRedirectUri != null)
            {
                var auth = await notification.OwinContext.Authentication.AuthenticateAsync("cookies");
                if (auth.Properties.Dictionary.TryGetValue("id_token", out var idToken))
                {
                    notification.ProtocolMessage.IdTokenHint = idToken;
                }
            }
        }
    }
}
