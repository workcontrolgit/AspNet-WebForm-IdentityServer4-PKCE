using System;
using System.Web;

namespace WebForms
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Context.GetOwinContext().Authentication.SignOut("oidc", "cookies");
        }
    }
}