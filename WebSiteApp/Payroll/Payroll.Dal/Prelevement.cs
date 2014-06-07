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
    
    public partial class Prelevement
    {
        public Prelevement()
        {
            this.BonusPrelevements = new HashSet<BonusPrelevement>();
            this.CessationPrelevements = new HashSet<CessationPrelevement>();
            this.CompagniePrelevements = new HashSet<CompagniePrelevement>();
            this.EmployePrelevements = new HashSet<EmployePrelevement>();
            this.PayePrelevements = new HashSet<PayePrelevement>();
            this.PrelevementDetails = new HashSet<PrelevementDetail>();
        }
    
        public int PrelevementID { get; set; }
        public string Nom { get; set; }
        public string Abreviation { get; set; }
        public int TypePrelevementID { get; set; }
        public int NaturePrelevementID { get; set; }
        public int PrelevementApplicationID { get; set; }
    
        public virtual ICollection<BonusPrelevement> BonusPrelevements { get; set; }
        public virtual ICollection<CessationPrelevement> CessationPrelevements { get; set; }
        public virtual ICollection<CompagniePrelevement> CompagniePrelevements { get; set; }
        public virtual ICollection<EmployePrelevement> EmployePrelevements { get; set; }
        public virtual NaturePrelevement NaturePrelevement { get; set; }
        public virtual ICollection<PayePrelevement> PayePrelevements { get; set; }
        public virtual ICollection<PrelevementDetail> PrelevementDetails { get; set; }
        public virtual TypePrelevement TypePrelevement { get; set; }
        public virtual PrelevementApplication PrelevementApplication { get; set; }
    }
}
