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
    
    public partial class CompagnieTypeConge
    {
        public int CompagnieID { get; set; }
        public int TypeCongeID { get; set; }
        public Nullable<decimal> JourParAnnee { get; set; }
        public Nullable<bool> AccumulableParAnnee { get; set; }
        public Nullable<bool> IncrementableParMois { get; set; }
    
        public virtual Compagnie Compagnie { get; set; }
        public virtual TypeConge TypeConge { get; set; }
    }
}