using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Payroll.Dal
{
    public class AppMenuManager
    {
        public List<Menu> GetMenuData(string[] userRoles)
        {
            List<Menu> menuItems;
            using (var context = new AppMenuEntities())
            {
                var menuData = from menu in context.Menus
                               where (menu.MenuRoles.Any(role => userRoles.Contains(role.RoleName)))
                               orderby menu.OrderNumber ascending
                               select menu;
                menuItems = menuData.ToList();
            }
            return menuItems;
        }

        public List<SubMenu> GetSubMenuData(int MenuID, string[] userRoles)
        {
            List<SubMenu> subMenuItems;
            using (var context = new AppMenuEntities())
            {
                var subMenuData = from subMenu in context.SubMenus
                               where ((subMenu.SubMenuRoles.Any(role => userRoles.Contains(role.RoleName)))
                               && (subMenu.MenuID == MenuID))
                               orderby subMenu.OrderNumber ascending
                               select subMenu;
                subMenuItems = subMenuData.ToList();
            }
            return subMenuItems;
        }

        //Get sub menu links data by sub menu ID
        public List<SubMenuLink> GetSubMenuLinkData(int SubMenuID, string[] userRoles)
        {
            List<SubMenuLink> subMenuLinkItems;
            using (var context = new AppMenuEntities())
            {
                var subMenuLinkData = from subMenuLink in context.SubMenuLinks
                                      where ((subMenuLink.SubMenuLinkRoles.Any(role => userRoles.Contains(role.RoleName)))
                                      && (subMenuLink.SubMenuID == SubMenuID))
                                      orderby subMenuLink.OrderNumber ascending
                                      select subMenuLink;
                subMenuLinkItems = subMenuLinkData.ToList();
            }
            return subMenuLinkItems;
        }

        //Get all sub menu links data by top menu ID
        public List<SubMenuLink> GetAllSubMenuLinkData(int TopMenuID, string[] userRoles)
        {
            List<SubMenuLink> subMenuLinkItems;
            using (var context = new AppMenuEntities())
            {
                var subMenuLinkData = from subMenuLink in context.SubMenuLinks
                                      where ((subMenuLink.SubMenuLinkRoles.Any(role => userRoles.Contains(role.RoleName)))
                                      && (subMenuLink.SubMenu.MenuID == TopMenuID))
                                      orderby subMenuLink.OrderNumber ascending
                                      select subMenuLink;
                subMenuLinkItems = subMenuLinkData.ToList();
            }
            return subMenuLinkItems;
        }
    }
}
