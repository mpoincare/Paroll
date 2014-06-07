using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;

namespace Payroll.Bll
{
    public class EmployeSalaireManager
    {
        public bool IsValid(EmployeSalaireDto emp, ref List<string> errorMessages)
        {
            //Les conditions pouvant être validées dans le formulaire lui-même
            // ne sont pas à priori testées ici

            //Déterminer si on a à faire à un salaire régulier ou spécial 
            //pour personnaliser les messages d'erreurs
            string typeSalaire;
            if (emp.TypeSalaireID == (int)LookupEnum.TypeSalaire.Regulier) { typeSalaire = @"régulier"; }
            else { typeSalaire = @"spécial"; }

            bool isValid = true;

            // Salaire
            if (new CompagnieRepository().GetSalaireNormalMaxForCompany(emp.CompagnieID, emp.MonnaieID) < emp.Salaire)
            {
                isValid = false;
                errorMessages.Add(String.Format("Vous n'avez pas l'autorisation d'assigner un salaire {0} de ce montant", typeSalaire));
            }

            // Type Paiement
            if (emp.TypeSalaireID == (int)LookupEnum.TypePaiement.Virement)
            {
                if (emp.BanqueID == 0)
                {
                    isValid = false;
                    errorMessages.Add(String.Format("Il faut choisir une banque pour le virement du salaire {0} ", typeSalaire));
                }
                if (String.IsNullOrEmpty(emp.CompteBancaire))
                {
                    isValid = false;
                    errorMessages.Add(String.Format("Le compte bancaire pour le virement du salaire {0} est obligatoire", typeSalaire));
                }
            }
            return isValid;
        }
    }
}
