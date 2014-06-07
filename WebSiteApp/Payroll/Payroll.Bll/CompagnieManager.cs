using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;

namespace Payroll.Bll
{
    public class CompagnieManager
    {
        public IEnumerable<TypePeriodeDto> GetTypePeriodeForCompany(int companyID)
        {
            CompagnieRepository cr = new CompagnieRepository();
            return cr.GetAllTypePeriode(companyID);
        }

        public int GetDefaultTypePeriodeForCompany(int companyID, int prelevementID, int typeSalaireID)
        {
            CompagnieRepository cr = new CompagnieRepository();
            return cr.GetDefaultTypePeriodeForPrelevement(companyID, prelevementID, typeSalaireID);
        }

        public IEnumerable<CompagniePrelevementDto> GetAllPrelevementForCompany(int companyID, int typeSalaireID)
        {
            CompagnieRepository cr = new CompagnieRepository();
            return cr.GetAllPrelevementForCompany(companyID, typeSalaireID);
        }

        public IEnumerable<CompagnieTypeCongeDto> GetAllCongeForCompany(int companyID)
        {
            CompagnieRepository cr = new CompagnieRepository();
            return cr.GetAllCongeForCompany(companyID);
        }
    }
}
