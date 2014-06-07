using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class CongeDto
    {
        public Int64 EmployeID { get; set; }
        public int TypeCongeID { get; set; }
        public string TypeConge { get; set; }
        public decimal? JourParAnnee { get; set; }
        public decimal? JourBalance { get; set; }
        public bool IncrementableParMois { get; set; }
        public bool AccumulableParAnnee { get; set; }
        public string ModifiePar { get; set; }
    }
}
