using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class TypePaiementDto
    {
        public int TypePaiementID { get; set; }
        public string Nom { get; set; }
        public int CompagnieID { get; set; }
    }
}
