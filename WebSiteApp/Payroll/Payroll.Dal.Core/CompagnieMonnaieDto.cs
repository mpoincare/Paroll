using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class CompagnieMonnaieDto
    {
        public int CompagnieID { get; set; }
        public int MonnaieID { get; set; }
        public decimal SalaireNormalMax { get; set; }
    }
}
