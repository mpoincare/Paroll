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
    
    public partial class CompagnieContact
    {
        public int CompagnieContactID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public int CompagnieID { get; set; }
        public string Description { get; set; }
    
        public virtual Compagnie Compagnie { get; set; }
    }
}