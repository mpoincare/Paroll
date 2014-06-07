using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Dal;

namespace Payroll.Bll
{
    public enum TopMenuID
    {
        Personnel = 2,
        Administration = 3,

    }
    
    public class AppMenu
    {
        //Compose top menu with or without submenus
        public void ComposeAppMenu(ref System.Web.UI.WebControls.Menu appMenu, string[] userRoles, bool TopMenuOnly)
        {
            AppMenuManager appMenuManager = new AppMenuManager();
            List<Payroll.Dal.Menu> menuList = appMenuManager.GetMenuData(userRoles);
            List<Payroll.Dal.SubMenu> subMenuList;

            foreach (Payroll.Dal.Menu menu in menuList)
            {
                //Adding top menu item
                MenuItem newMenuItem = new MenuItem(menu.Name, menu.MenuID.ToString(), null, menu.Url);
                appMenu.Items.Add(newMenuItem);

                if (!TopMenuOnly)
                {
                    // Adding sub menu items
                    subMenuList = appMenuManager.GetSubMenuData(menu.MenuID, userRoles);
                    foreach (SubMenu subMenu in subMenuList)
                    {
                        MenuItem newSubMenuItem = new MenuItem(subMenu.Name, subMenu.MenuID.ToString(), string.Empty, subMenu.Url);
                        newMenuItem.ChildItems.Add(newSubMenuItem);
                    }
                }
            }
        }

        //Return in a string array the necessary html to show the submenu links per column
        public String[] GetSubMenuLinks(int menuID,  string[] userRoles, int columnCount)
        {
            String[] subMenuLinks = new string[columnCount];

            //Get all submenus and submenu links data (to avoid several trips to data store)
            AppMenuManager appMenuManager = new AppMenuManager();
            List<Payroll.Dal.SubMenu> subMenuList = appMenuManager.GetSubMenuData(menuID, userRoles);
            List<Payroll.Dal.SubMenuLink> subMenuLinkList = appMenuManager.GetAllSubMenuLinkData(menuID, userRoles);

            //Get relevant data per column
            for (int i=0; i < columnCount; i++)
            {
                string htmlString = string.Empty;
                var columnSubMenus = from subMenu in subMenuList
                                     where subMenu.ColumnNumber == i + 1
                                     orderby subMenu.OrderNumber
                                     select subMenu;
                //Get submenus
                foreach (SubMenu subMenu in columnSubMenus)
                {
                    //Write header
                    htmlString = htmlString + "<br /><strong>" + subMenu.Name + "</strong><br />";
                    var columnSubMenuLinks = from subMenuLink in subMenuLinkList
                                             where subMenuLink.SubMenuID == subMenu.SubMenuID
                                             orderby subMenuLink.OrderNumber ascending
                                             select subMenuLink;
                    //Get submenu links
                    foreach (SubMenuLink subMenuLink in columnSubMenuLinks)
                    {
                        //Write submenu links
                        htmlString = htmlString + "<a href='" + subMenuLink.Url + "'>" + subMenuLink.Name + "</a><br />";
                    }
                }
                //Write column html to current string array
                subMenuLinks[i] = htmlString;
            }
            
            return subMenuLinks;
        }
    }
}
