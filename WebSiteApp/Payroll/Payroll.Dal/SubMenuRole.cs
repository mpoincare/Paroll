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
    
    public partial class SubMenuRole
    {
        public int SubMenuID { get; set; }
        public string RoleName { get; set; }
    
        public virtual SubMenu SubMenu { get; set; }
    }
}