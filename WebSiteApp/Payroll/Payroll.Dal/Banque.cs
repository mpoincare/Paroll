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
    
    public partial class Banque
    {
        public Banque()
        {
            this.EmployeSalaires = new HashSet<EmployeSalaire>();
            this.Prets = new HashSet<Pret>();
            this.Compagnies = new HashSet<Compagnie>();
        }
    
        public int BanqueID { get; set; }
        public string Nom { get; set; }
    
        public virtual ICollection<EmployeSalaire> EmployeSalaires { get; set; }
        public virtual ICollection<Pret> Prets { get; set; }
        public virtual ICollection<Compagnie> Compagnies { get; set; }
    }
}
