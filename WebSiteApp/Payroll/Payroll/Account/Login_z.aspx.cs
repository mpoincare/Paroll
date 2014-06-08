using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Payroll.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Si la page login est affichée à la suite d'une requête pour une autre page du site,
            // naviguer vers cette page une fois le login validé, autrement afficher la page par défaut du site
            
            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                Login1.DestinationPageUrl = returnUrl;

            }
            else
            {
                Login1.DestinationPageUrl = "~/default.aspx";
            }
            
        }

        private bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            //return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            return true;
        }

        protected void OnLoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
        {
            if (!IsValidEmail(Login1.UserName))
            {
                Login1.InstructionText = "Taper une adresse email valide.";
                Login1.InstructionTextStyle.ForeColor = System.Drawing.Color.RosyBrown;
                e.Cancel = true;
            }
            else
            {
                Login1.InstructionText = String.Empty;
            }
        }

        protected void OnLoginError(object sender, EventArgs e)
        {

        }

        protected void OnLoggedIn(object sender, EventArgs e)
        {
            //Enregistrer la compagnie de l'utilisateur (Code et nom) dans des variables de Session
            UserProfile currentUserProfile = UserProfile.GetUserProfile(Login1.UserName);
            Session["CompagnieID"] = currentUserProfile.CompagnieID;
            Session["Compagnie"] = currentUserProfile.Compagnie;
        }

    }
}