using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Helpers;

namespace Payroll.Account
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        string[] userRolesArray;
        MembershipUserCollection users;
        String messageText;
                
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                messageText = "";
                BindUsersListBox();
                Message.Hide(this);

                DeleteAccountButton.Attributes.Add("onclick", "return confirm('Êtes-vous sûr de vouloir supprimmer le compte sélectionné?');");
            }
        }
        
        protected void UsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UsersListBox.SelectedItem != null)
            {
                BindUserRolesListBox();
                BindAvailableRolesListBox();
                Message.Hide(this);
            }

        }

        protected void ResetPasswordButton_Click(object sender, EventArgs ev)
        {
            if (UsersListBox.SelectedItem != null)
            {
                string newPassword = "";

                try
                {
                    newPassword = Membership.Provider.ResetPassword(UsersListBox.SelectedItem.Value, "");
                }
                catch (NotSupportedException e)
                {
                    messageText = "Une erreur est survenue: " + e.Message + "." +
                               "Vérifier et essayez à nouveau.";
                    Message.Show(MessageType.Error, messageText, this);
                }
                catch (MembershipPasswordException e)
                {
                    messageText = "Réponse invalide. Essayez à nouveau.";
                    Message.Show(MessageType.Error, messageText, this);
                    return;
                }
                catch (System.Configuration.Provider.ProviderException e)
                {
                    messageText = "L'utilisateur n'existe pas.";
                    Message.Show(MessageType.Error, messageText, this);
                }

                if (newPassword != "")
                {
                    messageText = "Le mot de passe a été changé. Le nouveau mot de passe est: " + Server.HtmlEncode(newPassword);
                    Message.Show(MessageType.Success, messageText, this);
                }
                else
                {
                    messageText = "L'opération n'a pas réussi. Essayez à nouveau.";
                    Message.Show(MessageType.Error, messageText, this);
                }
            }
            else
            {
                messageText = "Aucun utilisateur n'est sélectionné.";
                Message.Show(MessageType.Error, messageText, this);
            }

        }

        protected void ActivateDeactivateButton_Click(object sender, EventArgs e)
        {
            if (UsersListBox.SelectedItem != null)
            {
                MembershipUser user = Membership.GetUser(UsersListBox.SelectedItem.Value);

                if (user.IsLockedOut)
                {
                    user.UnlockUser();
                    messageText = "Le compte a été débloqué.";
                    Message.Show(MessageType.Success, messageText, this);
                }
                else
                {
                    messageText = "Le compte n'était pas bloqué.";
                    Message.Show(MessageType.Error, messageText, this);
                }
            }
            else
            {
                messageText = "Aucun utilisateur sélectionné.";
                Message.Show(MessageType.Error, messageText, this);
            }
        }

        protected void DeleteAccountButton_Click(object sender, EventArgs e)
        {
            if (UsersListBox.SelectedItem != null)
            {
                DeleteUser(UsersListBox.SelectedItem.Value);
                BindUsersListBox();
                Message.Hide(this);
            }
            else
            {
                messageText = "Aucun utilisateur sélectionné.";
                Message.Show(MessageType.Error, messageText, this);
            }
        }

        protected void AddRolesToUserButton_Click(object sender, EventArgs e)
        {
            List<string> selectedRoles = new List<string>();
            if (AvailableRolesListBox.SelectedItem != null)
            {
                foreach (ListItem item in AvailableRolesListBox.Items)
                {
                    if (item.Selected)
                    {
                        selectedRoles.Add(item.Value);
                    }
                }
                Roles.AddUserToRoles(UsersListBox.SelectedItem.Value, selectedRoles.ToArray());

                //Re-bind role listboxes
                BindUserRolesListBox();
                BindAvailableRolesListBox();
            }
            else
            {
                messageText = "Il n'y a aucun rôle à ajouter sélectionné";
                Message.Show(MessageType.Error, messageText, this);
            }
        }

        protected void RemoveRolesFromUserButton_Click(object sender, EventArgs e)
        {
            List<string> selectedRoles = new List<string>();
            if (UserRolesListBox.SelectedItem != null)
            {
                foreach (ListItem item in UserRolesListBox.Items)
                {
                    if (item.Selected)
                    {
                        selectedRoles.Add(item.Value);
                    }
                }
                Roles.RemoveUserFromRoles(UsersListBox.SelectedItem.Value, selectedRoles.ToArray());

                //Re-bind role listboxes
                BindUserRolesListBox();
                BindAvailableRolesListBox();
            }
            else
            {
                messageText = "Il n'y a aucun rôle à enlever sélectionné";
                Message.Show(MessageType.Error, messageText, this);
            }
        }


        private List<String> GetRemainingRoles(string[] userRolesArray)
        {
            String[] allRolesArray = Roles.GetAllRoles();

            List<String> remainingRolesQuery =
                (from arole in allRolesArray
                let userRoles = from u in userRolesArray
                                select u
                where userRoles.Contains(arole) == false
                orderby arole ascending
                select arole).ToList();

            return remainingRolesQuery;
         }

        private List<UserListBox> GetAllCompanyUsers()
        {
            //Get all Membership Users
            users = Membership.GetAllUsers();
            IEnumerable<MembershipUser> allUsers = users.Cast<MembershipUser>();
            
            //Get all Profiles
            List<UserProfile> allUserProfiles = new List<UserProfile>();
            foreach (MembershipUser u in users)
            {
                UserProfile registeredUserProfile = UserProfile.GetUserProfile(u.UserName);
                allUserProfiles.Add(registeredUserProfile);
            }

            //Get the CompanyID of the currently logged user
            UserProfile currentUserProfile = UserProfile.GetUserProfile();
            int? companyID = currentUserProfile.CompagnieID;

            //Join users and their profiles through query where CompanyID equals current logged user CompanyID
            var userListQuery =
                (from u in allUsers
                 join p in allUserProfiles on u.UserName equals p.UserName
                 where p.CompagnieID == companyID
                 orderby p.Prenom, p.Nom ascending
                 select new UserListBox { Text = p.Prenom + " " + p.Nom + " (" + p.UserName + ")",
                              Value = p.UserName}).ToList();

            return userListQuery;
        }

        private void BindUsersListBox()
        {
            // Bind users to ListBox.
            UsersListBox.DataSource = GetAllCompanyUsers();
            UsersListBox.DataTextField = "Text";
            UsersListBox.DataValueField = "Value";
            UsersListBox.DataBind();
        }

        private void BindAvailableRolesListBox()
        {
            //Bind available roles to AvailableRolesListBox
            AvailableRolesListBox.DataSource = GetRemainingRoles(userRolesArray);
            AvailableRolesListBox.DataBind();
        }

        private void BindUserRolesListBox()
        {
            // Bind user roles to UserRolesListBox.
            userRolesArray = Roles.GetRolesForUser(UsersListBox.SelectedItem.Value);
            UserRolesListBox.DataSource = userRolesArray;
            UserRolesListBox.DataBind();
        }

        private void DeleteUser(string userName)
        {
            Membership.DeleteUser(userName, true);
        }

        
    }
}