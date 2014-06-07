using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;

namespace Payroll.Bll
{
    public class AbonnementLogicielManager
    {
        
        public bool IsSubscriptionValid(int companyID)
        {
            //Vérifier que la date d'expiration de l'abonnement est supérieur
            // à la date du jour
            CompagnieRepository cr = new CompagnieRepository();
            CompagnieDto cd = cr.GetCompagnieById(companyID);
            return (cd.ExpirationLogiciel >= DateTime.Now);
        }

        public bool CanAddEmploye(int companyID)
        {
            //Vérifier que le nombre d'employé maximum permis n'est pas dépassé
            CompagnieRepository cr = new CompagnieRepository();
            CompagnieDto cd = cr.GetCompagnieById(companyID);
            int compMax = cd.MaximumEmploye;
            int compActive = new EmployeRepository().GetEmployeCountByStatut(companyID, (int)LookupEnum.StatutEmploye.Actif);
            return (compActive < compMax);
        }
    }
}
