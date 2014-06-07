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
    
    public partial class EmployeSalaire
    {
        public EmployeSalaire()
        {
            this.Avances = new HashSet<Avance>();
            this.Bonuses = new HashSet<Bonus>();
            this.Prets = new HashSet<Pret>();
            this.Payes = new HashSet<Paye>();
            this.PayeAjustements = new HashSet<PayeAjustement>();
        }
    
        public decimal Salaire { get; set; }
        public string CompteBancaire { get; set; }
        public Nullable<bool> BonusFinAnnee { get; set; }
        public Nullable<bool> HeureSupPaye { get; set; }
        public string ModifiePar { get; set; }
        public Nullable<System.DateTime> ModifieDate { get; set; }
        public int TypePaiementID { get; set; }
        public Nullable<int> BanqueID { get; set; }
        public long EmployeID { get; set; }
        public int TypeSalaireID { get; set; }
        public int MonnaieID { get; set; }
    
        public virtual ICollection<Avance> Avances { get; set; }
        public virtual Banque Banque { get; set; }
        public virtual ICollection<Bonus> Bonuses { get; set; }
        public virtual Employe Employe { get; set; }
        public virtual TypePaiement TypePaiement { get; set; }
        public virtual TypeSalaire TypeSalaire { get; set; }
        public virtual Monnaie Monnaie { get; set; }
        public virtual ICollection<Pret> Prets { get; set; }
        public virtual ICollection<Paye> Payes { get; set; }
        public virtual ICollection<PayeAjustement> PayeAjustements { get; set; }
    }
}
