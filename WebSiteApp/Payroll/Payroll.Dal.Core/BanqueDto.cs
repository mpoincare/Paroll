using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class BanqueDto
    {
        public int BanqueID { get; set; }
        public string Nom { get; set; }
        public int CompagnieID { get; set; }
    }
}
