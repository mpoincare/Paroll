using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class EmployePrelevementDto
    {
        public Int64 EmployeID { get; set; }
        public int TypeSalaireID { get; set; }
        public string TypeSalaire { get; set; }
        public int PrelevementID { get; set; }
        public string Prelevement { get; set; }
        public int TypePeriodeID { get; set; }
        public decimal Pourcentage { get; set; }
    }
}
