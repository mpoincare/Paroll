using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;

namespace Payroll.Bll
{
    public class CongeManager
    {
        public bool IsValid(CongeDto cong, ref List<string> errorMessages)
        {
            //Les conditions pouvant être validées dans le formulaire lui-même
            // ne sont pas à priori testées ici

            bool isValid = true;

            if (cong.JourParAnnee == null)
            {
                isValid = false;
                errorMessages.Add(String.Format("Le nombre de jours par année pour le congé {0} est obligatoire", cong.TypeConge));
            }
            if (cong.JourBalance == null)
            {
                isValid = false;
                errorMessages.Add(String.Format("La balance des jours pour le congé {0} est obligatoire", cong.TypeConge));
            }
            return isValid;
        }

        public bool IsValid(IEnumerable<CongeDto> conges, ref List<string> errorMessages)
        {
            //Les conditions pouvant être validées dans le formulaire lui-même
            // ne sont pas à priori testées ici

            bool isValid = true;
            foreach (CongeDto cong in conges)
            {
                bool isValidTemp = true;

                if (cong.JourParAnnee == null)
                {
                    isValidTemp = false;
                    errorMessages.Add(String.Format("Le nombre de jours par année pour le congé {0} est obligatoire", cong.TypeConge));
                }
                if (cong.JourBalance == null)
                {
                    isValidTemp = false;
                    errorMessages.Add(String.Format("La balance des jours pour le congé {0} est obligatoire", cong.TypeConge));
                }
                if (!isValidTemp) { isValid = false; }
            }
            return isValid;
        }
    }
}
