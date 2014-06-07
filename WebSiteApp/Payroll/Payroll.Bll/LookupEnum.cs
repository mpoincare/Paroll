using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Bll
{
    public static class LookupEnum
    {
        //Énumérations reflétant le contenu des tables de référence portant les mêmes noms.
        // Elles servent dans la couche Bll afin d'éviter l'utilisation des nombres eux-mêmes (magic numbers)
        public enum TypeEmploye
        {
            Regulier = 1,
            Journalier = 2,
            Special = 3
        }
        
        public enum StatutEmploye
        {
            Actif = 1,
            Termine = 2,
            Suspendu = 3
        }

        public enum TypeSalaire
        {
            Regulier = 1,
            Special = 2,
        }

        public enum TypePaiement
        {
            Cash = 1,
            Cheque = 2,
            Virement = 3
        }

        public enum TypeConge
        {
            Annuel = 1,
            Maladie = 2,
            Maternite = 3,
            SansSolde = 4
        }
        

    }
}
