using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class MonnaieDto
    {
        public int MonnaieID { get; set; }
        public string Nom { get; set; }
        public int CompagnieID { get; set; }
        public string Abreviation { get; set; }
        public string Symbol { get; set; }
        public bool Locale { get; set; }
    }
}
