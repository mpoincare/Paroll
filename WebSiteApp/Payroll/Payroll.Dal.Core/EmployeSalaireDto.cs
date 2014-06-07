using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class EmployeSalaireDto
    {
        public Int64 EmployeID { get; set; }
        public int CompagnieID { get; set; }
        public int TypeSalaireID { get; set; }
        public int TypePaiementID { get; set; }
        public decimal Salaire { get; set; }
        public string CompteBancaire { get; set; }
        public int? BanqueID { get; set; }
        public int MonnaieID { get; set; }
        public bool BonusFinAnnee { get; set; }
        public bool HeureSupPaye { get; set; }
        public string ModifiePar { get; set; }
    }
}
