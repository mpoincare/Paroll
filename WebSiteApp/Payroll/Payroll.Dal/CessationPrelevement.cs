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
    
    public partial class CessationPrelevement
    {
        public decimal Prelevement { get; set; }
        public long CessationID { get; set; }
        public int PrelevementID { get; set; }
    
        public virtual Cessation Cessation { get; set; }
        public virtual Prelevement Prelevement1 { get; set; }
    }
}
