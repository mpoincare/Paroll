using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class CompagnieDto
    {
        public int CompagnieID{ get; set; }
        public string Nom { get; set; }
        public string Nif { get; set; }
        public int NumeroEmployeSuivant { get; set; }
        public decimal MensualiteLogiciel { get; set; }
        public int? MonnaieID { get; set; }
        public DateTime ExpirationLogiciel { get; set; }
        public int MaximumEmploye { get; set; }
        public int? HoldingID { get; set; }
        public string ModifiePar { get; set; }
    }
}
