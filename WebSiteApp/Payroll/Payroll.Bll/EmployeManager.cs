using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal;
using Payroll.Dal.Core;
using System.Text.RegularExpressions;

namespace Payroll.Bll
{
    public class EmployeManager
    {
        
        public bool IsValid( EmployeDto emp, ref List<string> ErrorMessages)
        {
            //Les conditions pouvant être validées dans le formulaire lui-même
            // ne sont pas à priori testés ici

            bool isValid = true;

            // Abonnement
            AbonnementLogicielManager al = new AbonnementLogicielManager();
            if (!al.IsSubscriptionValid(emp.CompagnieID))
            {
                isValid = false;
                ErrorMessages.Add(@"Opération impossible: l'abonnement du logiciel a expiré.");
            }

            if (!al.CanAddEmploye(emp.CompagnieID))
            {
                isValid = false;
                ErrorMessages.Add(@"Opération impossible: le nombre maximum d'employés a été atteint.");
            }

            //Date naissance
            DateTime dateNaissance;
            if (!DateTime.TryParse(emp.DateNaissanceString, out dateNaissance))
            {
                isValid = false;
                ErrorMessages.Add(@"Date de naissance invalide");
            }
            else
            {
                if (dateNaissance.AddYears(18) > DateTime.Now)
                {
                    isValid = false;
                    ErrorMessages.Add(@"La date de naissance fait de l'employé un mineur");
                }
            }

            //Sexe
            if (emp.Sexe == null)
            {
                isValid = false;
                ErrorMessages.Add(@"Le sexe de l'employé n'est pas précisé");
            }

            //Email
            if (!String.IsNullOrEmpty(emp.Email))
            {
                // Renvoi true si l'email est valide.
                if (!Regex.IsMatch(emp.Email.ToString(), @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    isValid = false;
                    ErrorMessages.Add("L'email de l'employé n'est pas correct");
                }
            }

            //Date Embauche
            DateTime dateEmbauche;
            if (!DateTime.TryParse(emp.DateNaissanceString, out dateEmbauche))
            {
                isValid = false;
                ErrorMessages.Add(@"Date d'embauche invalide");
            }

            //Departement
            if (emp.DepartementID == 0)
            {
                isValid = false;
                ErrorMessages.Add(@"Il faut choisir un département");
            }

            //Statut Matrimonial
            if (emp.StatutMatrimonialID == 0)
            {
                isValid = false;
                ErrorMessages.Add(@"Il faut choisir un statut matrimonial");
            }
            return isValid;
        }

        public int AddEmploye(EmployeDto empDto, EmployeSalaireDto empSalReg, EmployeSalaireDto empSalSpe,
            IEnumerable<CongeDto> congesDto, IEnumerable<EmployePrelevementDto> empPrelReg, 
            IEnumerable<EmployePrelevementDto> empPrelSpe)
        {
            //Obtenir un numéro interne pour l'employé
            int numInterne = new EmployeRepository().GetNewNumeroInterne(empDto.CompagnieID);
            //Ajouter numero interne pour l'employé
            empDto.NumeroEmploye = numInterne;
            //Ajouter les dates d'embauche et de naissance de l'employé à partir des champs string correspondant
            empDto.DateEmbauche = DateTime.Parse(empDto.DateEmbaucheString);
            empDto.DateNaissance = DateTime.Parse(empDto.DateNaissanceString);

            //Déterminer si le type de l'employé
            bool isSpecial = false;
            if (empDto.TypeEmployeID == (int)LookupEnum.TypeEmploye.Special) { isSpecial = true; }

            //Ajouter les salaires régulier et spécial dans une liste
            List<EmployeSalaireDto> empSalaires = new List<EmployeSalaireDto>();
            empSalaires.Add(empSalReg);
            if (isSpecial) { empSalaires.Add(empSalSpe); }

            //Fusionner les listes de prélèvement des salaires réguliers et spéciaux s'il ya lieu
            List<EmployePrelevementDto> empPrelList = empPrelReg.ToList();
            if (isSpecial) { empPrelList.AddRange(empPrelSpe); }

            //Ajouter l'employé
            EmployeRepository empRep = new EmployeRepository();
            empRep.AddEmploye(empDto, empSalaires, congesDto, empPrelList);

            //Renvoyer le numéro interne de l'employé
            return numInterne;
        }

        public void UpdateEmploye(EmployeDto empDto, EmployeSalaireDto empSalReg, EmployeSalaireDto empSalSpe,
            IEnumerable<CongeDto> congesDto, IEnumerable<EmployePrelevementDto> empPrelReg,
            IEnumerable<EmployePrelevementDto> empPrelSpe)
        {
            //Ajouter les dates d'embauche et de naissance de l'employé à partir des champs string correspondant
            empDto.DateEmbauche = DateTime.Parse(empDto.DateEmbaucheString);
            empDto.DateNaissance = DateTime.Parse(empDto.DateNaissanceString);

            //Déterminer si le type de l'employé
            bool isSpecial = false;
            if (empDto.TypeEmployeID == (int)LookupEnum.TypeEmploye.Special) { isSpecial = true; }

            //Ajouter les salaires régulier et spécial dans une liste
            List<EmployeSalaireDto> empSalaires = new List<EmployeSalaireDto>();
            empSalaires.Add(empSalReg);
            if (isSpecial) { empSalaires.Add(empSalSpe); }

            //Fusionner les listes de prélèvement des salaires réguliers et spéciaux s'il ya lieu
            List<EmployePrelevementDto> empPrelList = empPrelReg.ToList();
            if (isSpecial) { empPrelList.AddRange(empPrelSpe); }

            //Ajouter l'employé
            EmployeRepository empRep = new EmployeRepository();
            empRep.UpdateEmploye(empDto, empSalaires, congesDto, empPrelList);
        }

        public bool IsSearchValid(EmployeSearchDto emp, ref List<string> ErrorMessages)
        {
            //Vérifier que le résultat de la recherche est en dessous du nombre maximal permis
            bool isValid = true;
            EmployeRepository empRep = new EmployeRepository();
            int resultCount = empRep.GetSearchCount(emp);
            if (resultCount > LookupConst.EmpSearchMax)
            {
                isValid = false;
                ErrorMessages.Add(@"Le nombre d'enregistrements correspondant à la recherche est trop grand. Recommencer avec des critères plus détaillés.");
            }
            else
            {
                if (resultCount == 0)
                {
                    isValid = false;
                    ErrorMessages.Add(@"Aucun enregistrement n'a pu être trouvé. Recommencer avec d'autres critères.");
                }
            }
            return isValid;
        }

        public bool IsSearchValid(String StrNumeroEmploye, out int NumeroEmploye, ref List<string> ErrorMessages)
        {
            //Vérifier que le numéro interne est valide
            bool isValid = Int32.TryParse(StrNumeroEmploye.Trim(), out NumeroEmploye);
            if (!isValid)
            {
                ErrorMessages.Add(@"Le numéro interne de l'employé est incorrect.");
            }
            return isValid;
        }

        public IEnumerable<EmployeSearchDto> GetSearchResult(EmployeSearchDto emp)
        {
            //Renvoyer les résultats de la recherche, on suppose que la recherche est valide
            EmployeRepository empRep = new EmployeRepository();
            IEnumerable<EmployeSearchDto> empResults = empRep.GetSearchResult(emp);
            
            // Renseigner les champs "calculés" et protéger le Url
            UrlSecurity usec = new UrlSecurity();
            foreach ( EmployeSearchDto e in empResults)
            {
                e.NomPrenom = e.Nom + ", " + e.Prenom;
                // Protéger le Url avec un paramètre (Digest)
                e.Url = usec.CreateTamperProofUrl(emp.Url, String.Empty, "Id=" + e.EmployeID.ToString());
                //e.Url = emp.Url + "?Id=" + e.EmployeID.ToString(); // Url de navigation
            }
            return empResults;
        }

        public Int64? GetEmployeID(int CompagnieId, int NumeroEmploye)
        {
            //Renvoyer les résultats de la recherche, on suppose que la recherche est valide
            EmployeRepository empRep = new EmployeRepository();
            return empRep.GetEmployeId(CompagnieId, NumeroEmploye);
        }

        public EmployeDto GetEmployeByID(long employeID)
        {
            //Renvoyer les données personnelles de l'employe à partir de sa clé primaire
            EmployeRepository empRep = new EmployeRepository();
            return empRep.GetEmployeById(employeID);
        }

        public void GetEmployeDetailsByID(long employeID, out EmployeDto emp, out EmployeSalaireDto empSalReg,
            out EmployeSalaireDto empSalSpe, out IEnumerable<CongeDto> empConge, out IEnumerable<EmployePrelevementDto> empPrelReg, 
            out IEnumerable<EmployePrelevementDto> empPrelSpe)
        {
            //Renvoyer toutes les données personnelles de l'employe à partir de sa clé primaire
            EmployeRepository empRep = new EmployeRepository();
            emp = empRep.GetEmployeById(employeID);
            empSalReg = empRep.GetSalaireByType(employeID, (int)LookupEnum.TypeSalaire.Regulier);
            empPrelReg = empRep.GetPrelevementByTypeSalaire(employeID, (int)LookupEnum.TypeSalaire.Regulier);

            if (emp.TypeEmployeID == (int)LookupEnum.TypeEmploye.Special)
            {
                empSalSpe = empRep.GetSalaireByType(employeID, (int)LookupEnum.TypeSalaire.Special);
                empPrelSpe = empRep.GetPrelevementByTypeSalaire(employeID, (int)LookupEnum.TypeSalaire.Special);
            }
            else
            {
                empSalSpe = null;
                empPrelSpe = null;
            }
            empConge = empRep.GetConge(employeID);
        }
    }
}
