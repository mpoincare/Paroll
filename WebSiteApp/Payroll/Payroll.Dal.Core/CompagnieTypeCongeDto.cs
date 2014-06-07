using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class CompagnieTypeCongeDto
    {
        public int CompagnieID { get; set; }
        public int TypeCongeID { get; set; }
        public string Conge { get; set; }
        public decimal? JourParAnnee { get; set; }
        public bool? AccumulableParAnnee { get; set; }
        public bool? IncrementableParMois { get; set; }
    }
}
