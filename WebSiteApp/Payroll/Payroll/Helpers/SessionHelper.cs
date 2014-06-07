using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Profile;
using System.Web.Security;
using Payroll.Account;

namespace Payroll.Helpers
{
    public static class SessionHelper
    {
        public static int GetCompanyIDFromSession(HttpContext context)
        {

            if (context.Session["CompagnieID"] == null)
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    //Enregistrer la compagnie de l'utilisateur (Code et nom) dans des variables de Session
                    UserProfile currentUserProfile = UserProfile.GetUserProfile(context.User.Identity.Name);
                    context.Session["CompagnieID"] = currentUserProfile.CompagnieID;
                    context.Session["Compagnie"] = currentUserProfile.Compagnie;
                    return (int)currentUserProfile.CompagnieID;
                }
                else
                {
                    context.Response.Redirect("~/Account/Login.aspx");
                    return 0;
                }
            }
           else
            {
                return (int)context.Session["CompagnieID"];
            }

        }
    }
}