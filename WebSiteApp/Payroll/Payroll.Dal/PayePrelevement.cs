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
    
    public partial class PayePrelevement
    {
        public decimal Prelevement { get; set; }
        public int PrelevementID { get; set; }
        public long PayeID { get; set; }
        public int TypePeriodeID { get; set; }
        public decimal Pourcentage { get; set; }
    
        public virtual Paye Paye { get; set; }
        public virtual Prelevement Prelevement1 { get; set; }
    }
}
