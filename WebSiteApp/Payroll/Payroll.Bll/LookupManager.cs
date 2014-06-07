using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;

namespace Payroll.Bll
{
    public class LookupManager
    {
        public IEnumerable<TypeEmployeDto> GetAllTypeEmployeForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllTypeEmployeForCompany(companyID);
        }

        public IEnumerable<StatutMatrimonialDto> GetAllStatutMatrimonial()
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllStatutMatrimonial();
        }

        public IEnumerable<PosteDto> GetAllPosteForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllPosteForCompany(companyID);
        }

        public IEnumerable<DepartementDto> GetAllDepartementForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllDepartementForCompany(companyID);
        }

        public IEnumerable<TypePaiementDto> GetAllTypePaiementForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllTypePaiementForCompany(companyID);
        }

        public IEnumerable<MonnaieDto> GetAllMonnaieForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllMonnaieForCompany(companyID);
        }

        public IEnumerable<BanqueDto> GetAllBanqueForCompany(int companyID)
        {
            LookupRepository lp = new LookupRepository();
            return lp.GetAllBanqueForCompany(companyID);
        }
    }
}
