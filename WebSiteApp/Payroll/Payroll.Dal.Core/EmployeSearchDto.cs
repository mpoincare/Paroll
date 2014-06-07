using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class EmployeSearchDto
    {
        public Int64 EmployeID { get; set; }
        public int CompagnieID { get; set; }
        public int NumeroEmploye { get; set; }
        public string CIN { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomPrenom { get; set; }
        public string Phone1 { get; set; }
        public int StatutEmployeID { get; set; }
        public string Url { get; set; }
    }
}
