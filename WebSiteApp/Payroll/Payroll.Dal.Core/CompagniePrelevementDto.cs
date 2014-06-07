using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class CompagniePrelevementDto
    {
        public int CompagnieID { get; set; }
        public int TypeSalaireID { get; set; }
        public int PrelevementID { get; set; }
        public string Prelevement { get; set; }
        public int TypePeriodeID { get; set; }
        public decimal Pourcentage { get; set; }
    }
}
