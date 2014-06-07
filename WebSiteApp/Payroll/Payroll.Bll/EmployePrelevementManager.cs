using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal.Core;
using Payroll.Dal;


namespace Payroll.Bll
{
    public class EmployePrelevementManager
    {
        public bool IsValid(IEnumerable<EmployePrelevementDto> pre, ref List<string> ErrorMessages)
        {
            //Les conditions pouvant être validées dans le formulaire lui-même
            // ne sont pas à priori testés ici

            bool isValid = true;

            // Sélection les prélèvements proprement dits (sans les type de période et les pourcentages)
            var prelevements = (from EmployePrelevementDto c in pre
                         select new { c.PrelevementID, c.Prelevement, c.TypeSalaireID }).Distinct();

            // Vérifier la conformité de chaque prélèvement en fonction des pourcentages par période choisis
            foreach (var c in prelevements)
            {
                //Types de période et pourcentages pour prélevement en cours
                var query = (from p in pre
                             where p.PrelevementID == c.PrelevementID
                             select new { p.TypePeriodeID, p.Pourcentage }).ToList();

                //Validation
                if (query.Count() > 1)
                {
                    if (query[0].TypePeriodeID == query[1].TypePeriodeID)
                    {
                        isValid = false;
                        ErrorMessages.Add(String.Format("{0} : les périodes doivent être differents", c.Prelevement));
                    }
                    if ((query[0].Pourcentage + query[1].Pourcentage) != 1)
                    {
                        isValid = false;
                        ErrorMessages.Add(String.Format("{0} : la somme des pourcentages des périodes doit être égale à 100%", c.Prelevement));
                    }
                }
                else
                {
                    if (query[0].Pourcentage != 1)
                    {
                        isValid = false;
                        ErrorMessages.Add(String.Format("{0} : le pourcentage prélevé pour la période doit être égal à 100%", c.Prelevement));
                    }
                }
            }
            return isValid;
        }
    }
}
