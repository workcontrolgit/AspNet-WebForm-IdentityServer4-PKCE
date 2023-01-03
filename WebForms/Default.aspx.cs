using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;

namespace WebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var result = Context.GetOwinContext().Authentication.AuthenticateAsync("cookies").Result;

            //example code to get claims from code behind
            ClaimsPrincipal cp = Page.User as ClaimsPrincipal;

            //((System.Security.Claims.ClaimsPrincipal)User).Claims)

            var claims = cp.Claims.ToList();

            //            grdClaims.DataSource = cp.Claims.ToList();
            grdClaims.DataSource = result.Identity.Claims;
            grdClaims.DataBind();

            grdDictionaries.DataSource = result.Properties.Dictionary;
            grdDictionaries.DataBind();

            //example code to extract access_token using Linq
            var accessTokenClaim = result.Properties.Dictionary
            .FirstOrDefault(x => x.Key == "access_token");
            if (accessTokenClaim.Value != null)
            {
                var accessToken = accessTokenClaim.Value;
                lblAccessToken.Text = accessToken;
            }


            //example code to extract id_token using Linq
            var idTokenClaim = result.Properties.Dictionary
            .FirstOrDefault(x => x.Key == "id_token");

            if (idTokenClaim.Value != null)
            {
                var idToken = idTokenClaim.Value;
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(idToken);
                lblIdToken.Text = idToken;
            }



        }


    }
}