using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Profile;
using WebControlExtension;
using System.Configuration;
using Payroll.Helpers;

namespace Payroll.Account
{
    public partial class Register : Page
    {
        private string[] rolesArray;
        private List<String> checkedRoles;
        string messageText;


        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            if (!IsPostBack)
            {
                // Bind roles to CheckBoxList.
                rolesArray = Roles.GetAllRoles();
                var roleCheckBoxList = Page.GetControl("RoleCheckBoxList") as CheckBoxList;
                roleCheckBoxList.DataSource = rolesArray;
                roleCheckBoxList.DataBind();

                /*
                //Set Profile for user jcadet
                UserProfile currentUserProfile = UserProfile.GetUserProfile();
                currentUserProfile.Nom = "Cadet";
                currentUserProfile.Prenom = "Jerry";
                currentUserProfile.CompagnieID = 2;
                currentUserProfile.Compagnie = "Elite Consulting";
                currentUserProfile.MustChangePassword = false;
                currentUserProfile.Save();
                */
            }
            
            if (Request.QueryString["success"] == "true")
            {
                messageText = "L'utilisateur a été enregisré avec succès" ;
                Message.Show(MessageType.Success, messageText, this);
            }
            
                        
            
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            //FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);
            
            //Set Roles and Profiles
            SetRegisteredUserProfile();
            SaveRolesCheckBoxState();
            SetRegisteredUserRoles();

            //Add success querystring value and postback
            Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri + "?success=true");
        }

        
        private void SaveRolesCheckBoxState()
        {
            if (IsPostBack)
            {
                checkedRoles = new List<string>();
                CheckBoxList roleChkBoxList = UIControl.FindControlRecursive(RegisterUserWizardStep, "RoleCheckBoxList") as CheckBoxList;

                foreach (ListItem item in roleChkBoxList.Items)
                {
                    if (item.Selected)
                    {
                        checkedRoles.Add(item.Value);
                    }
                }
            }
        }

        private void SetRegisteredUserProfile()
        {
            //Set Profile for newly registered User, the CompanyID and Company are copied from
            // the currently logged user profile

            //Get value of Nom, Prenom textboxes
            var firstnameTextBox = Page.GetControl("Prenom") as TextBox;
            var lastnameTextBox = Page.GetControl("Nom") as TextBox;

            //Get the profile of the currently logged user
            UserProfile currentUserProfile = UserProfile.GetUserProfile();

            UserProfile registeredUserProfile = UserProfile.GetUserProfile(RegisterUser.UserName);
            registeredUserProfile.Nom = StringHelper.ToTitleCase(lastnameTextBox.Text.Trim());
            registeredUserProfile.Prenom = StringHelper.ToTitleCase(firstnameTextBox.Text.Trim());
            registeredUserProfile.CompagnieID = currentUserProfile.CompagnieID;
            registeredUserProfile.Compagnie = currentUserProfile.Compagnie;
            registeredUserProfile.MustChangePassword = true; //Force the user to change his/her password at the next login
            registeredUserProfile.Save();
            
        }

        private void SetRegisteredUserRoles()
        {
            //Assign the checked roles to the newly registered User
            Roles.AddUserToRoles(RegisterUser.UserName, checkedRoles.ToArray());
        }
    }
}