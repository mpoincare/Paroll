//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Payroll.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubMenuLink
    {
        public SubMenuLink()
        {
            this.SubMenuLinkRoles = new HashSet<SubMenuLinkRole>();
        }
    
        public int SubMenuLinkID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int SubMenuID { get; set; }
        public int ColumnNumber { get; set; }
        public int OrderNumber { get; set; }
    
        public virtual SubMenu SubMenu { get; set; }
        public virtual ICollection<SubMenuLinkRole> SubMenuLinkRoles { get; set; }
    }
}
