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
    
    public partial class TypeConge
    {
        public TypeConge()
        {
            this.CompagnieTypeConges = new HashSet<CompagnieTypeConge>();
            this.Conges = new HashSet<Conge>();
        }
    
        public int TypeCongeID { get; set; }
        public string Nom { get; set; }
    
        public virtual ICollection<CompagnieTypeConge> CompagnieTypeConges { get; set; }
        public virtual ICollection<Conge> Conges { get; set; }
    }
}