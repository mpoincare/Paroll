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
    
    public partial class TypePeriode
    {
        public TypePeriode()
        {
            this.CompagniePrelevements = new HashSet<CompagniePrelevement>();
            this.EmployePrelevements = new HashSet<EmployePrelevement>();
            this.Periodes = new HashSet<Periode>();
            this.PretPeriodes = new HashSet<PretPeriode>();
            this.Compagnies = new HashSet<Compagnie>();
        }
    
        public int TypePeriodeID { get; set; }
        public string Nom { get; set; }
    
        public virtual ICollection<CompagniePrelevement> CompagniePrelevements { get; set; }
        public virtual ICollection<EmployePrelevement> EmployePrelevements { get; set; }
        public virtual ICollection<Periode> Periodes { get; set; }
        public virtual ICollection<PretPeriode> PretPeriodes { get; set; }
        public virtual ICollection<Compagnie> Compagnies { get; set; }
    }
}