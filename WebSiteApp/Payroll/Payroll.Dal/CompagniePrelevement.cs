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
    
    public partial class CompagniePrelevement
    {
        public int CompagnieID { get; set; }
        public int PrelevementID { get; set; }
        public int TypePeriodeID { get; set; }
        public int TypeSalaireID { get; set; }
        public decimal Pourcentage { get; set; }
    
        public virtual Compagnie Compagnie { get; set; }
        public virtual Prelevement Prelevement { get; set; }
        public virtual TypePeriode TypePeriode { get; set; }
        public virtual TypeSalaire TypeSalaire { get; set; }
    }
}