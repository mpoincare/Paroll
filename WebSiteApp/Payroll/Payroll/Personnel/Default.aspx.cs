using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Bll;

namespace Payroll.WebUI.Personnel
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowSubMenuLinks();
            }
        }

        private void ShowSubMenuLinks()
        {
            //Get current user roles
            string[] userRoles;
            userRoles = Roles.GetRolesForUser();

            // Get submenus and submenu links per column
            string[] subMenuLinks;
            AppMenu appMenu = new AppMenu();
            subMenuLinks = appMenu.GetSubMenuLinks((int)TopMenuID.Personnel, userRoles, 3);

            //Assign submenu and submenu links to their column in the page
            Column1.InnerHtml = subMenuLinks[0];
            Column2.InnerHtml = subMenuLinks[1];
            Column3.InnerHtml = subMenuLinks[2];
        }
    }
}