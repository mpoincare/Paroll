using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Threading.Tasks;
using Payroll.Dal.Core;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.Diagnostics;

namespace Payroll.Dal
{
    public class EmployeRepository
    {
        public int GetEmployeCountByStatut(int companyID, int statutEmployeID)
        {
            //Nombre d'employés pour une compangie et un statut donnés
            int query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.Employes
                         where t.CompagnieID == companyID
                         && t.StatutEmployeID == statutEmployeID
                         select t).Count();
            }
            return query;
        }

        public void AddEmploye(EmployeDto empDto, IEnumerable<EmployeSalaireDto> empSalairesDto, 
            IEnumerable<CongeDto> congesDto, IEnumerable<EmployePrelevementDto> empPrelDto)
        {
            //Ajouter un nouvel employé avec son ou ses salaires,ses congés et ses prélèvements
            
            var emp = new Employe();
            emp.AdresseRue1 = empDto.AdresseRue1;
            emp.AdresseRue2 = empDto.AdresseRue2;
            emp.AdresseVille = empDto.AdresseVille;
            emp.Cin = empDto.CIN;
            emp.CompagnieID = empDto.CompagnieID;
            emp.DateEmbauche = empDto.DateEmbauche;
            emp.DateNaissance = empDto.DateNaissance;
            emp.DepartementID = empDto.DepartementID;
            emp.Email = empDto.Email;
            emp.Extension = empDto.Extension;
            emp.ModifiePar = empDto.ModifiePar;
            emp.Nif = empDto.NIF;
            emp.Nom = empDto.Nom;
            emp.NumeroEmploye = empDto.NumeroEmploye;
            emp.Phone1 = empDto.Phone1;
            emp.Phone2 = empDto.Phone2;
            emp.PosteID = empDto.PosteID;
            emp.Prenom = empDto.Prenom;
            emp.Sexe = empDto.Sexe;
            emp.StatutEmployeID = empDto.StatutEmployeID;
            emp.StatutMatrimonialID = empDto.StatutMatrimonialID;
            emp.TypeEmployeID = empDto.TypeEmployeID;

            
            foreach (EmployeSalaireDto salDto in empSalairesDto)
            {
                var sal = new EmployeSalaire();
                sal.BanqueID = salDto.BanqueID;
                sal.BonusFinAnnee = salDto.BonusFinAnnee;
                sal.CompteBancaire = salDto.CompteBancaire;
                sal.HeureSupPaye = salDto.HeureSupPaye;
                sal.ModifiePar = salDto.ModifiePar;
                sal.MonnaieID = salDto.MonnaieID;
                sal.Salaire = salDto.Salaire;
                sal.TypePaiementID = salDto.TypePaiementID;
                sal.TypeSalaireID = salDto.TypeSalaireID;

                emp.EmployeSalaires.Add(sal);
            }

            foreach (CongeDto congDto in congesDto)
            {
                var cong = new Conge();
                cong.AccumulableParAnnee = congDto.AccumulableParAnnee;
                cong.IncrementableParMois = congDto.IncrementableParMois;
                cong.JourBalance = congDto.JourBalance;
                cong.JourParAnnee = congDto.JourParAnnee;
                cong.ModifiePar = congDto.ModifiePar;
                cong.TypeCongeID = congDto.TypeCongeID;

                emp.Conges.Add(cong);
            }
            
            foreach (EmployePrelevementDto prelDto in empPrelDto)
            {
                var prel = new EmployePrelevement();
                prel.PrelevementID = prelDto.PrelevementID;
                prel.TypePeriodeID = prelDto.TypePeriodeID;
                prel.TypeSalaireID = prelDto.TypeSalaireID;
                prel.Pourcentage = prelDto.Pourcentage;

                emp.EmployePrelevements.Add(prel);
            }
            
            try
            {
                //ajouter les informations du nouvel employé au context et sauvegarder
                using (var context = new PayrollEntities())
                {
                    context.Employes.Add(emp);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public void UpdateEmploye(EmployeDto empDto, IEnumerable<EmployeSalaireDto> empSalairesDto,
            IEnumerable<CongeDto> congesDto, IEnumerable<EmployePrelevementDto> empPrelDto)
        {
            //Mettre à jour les informations d'un employé existant y compris ses salaires,ses congés et ses prélèvements
            //Attention: la mise à jour peut occasionner à des insertions, des suppressions et des mises à jour

            //Copier les données des Dto dans des Entity Objects
            var emp = new Employe();
            emp.EmployeID = empDto.EmployeID;
            emp.AdresseRue1 = empDto.AdresseRue1;
            emp.AdresseRue2 = empDto.AdresseRue2;
            emp.AdresseVille = empDto.AdresseVille;
            emp.Cin = empDto.CIN;
            emp.CompagnieID = empDto.CompagnieID;
            emp.DateEmbauche = empDto.DateEmbauche;
            emp.DateNaissance = empDto.DateNaissance;
            emp.DepartementID = empDto.DepartementID;
            emp.Email = empDto.Email;
            emp.Extension = empDto.Extension;
            emp.ModifiePar = empDto.ModifiePar;
            emp.Nif = empDto.NIF;
            emp.Nom = empDto.Nom;
            emp.NumeroEmploye = empDto.NumeroEmploye;
            emp.Phone1 = empDto.Phone1;
            emp.Phone2 = empDto.Phone2;
            emp.PosteID = empDto.PosteID;
            emp.Prenom = empDto.Prenom;
            emp.Sexe = empDto.Sexe;
            emp.StatutEmployeID = empDto.StatutEmployeID;
            emp.StatutMatrimonialID = empDto.StatutMatrimonialID;
            emp.TypeEmployeID = empDto.TypeEmployeID;

            foreach (EmployeSalaireDto salDto in empSalairesDto)
            {
                var sal = new EmployeSalaire();
                sal.BanqueID = salDto.BanqueID;
                sal.BonusFinAnnee = salDto.BonusFinAnnee;
                sal.CompteBancaire = salDto.CompteBancaire;
                sal.HeureSupPaye = salDto.HeureSupPaye;
                sal.ModifiePar = salDto.ModifiePar;
                sal.MonnaieID = salDto.MonnaieID;
                sal.Salaire = salDto.Salaire;
                sal.TypePaiementID = salDto.TypePaiementID;
                sal.TypeSalaireID = salDto.TypeSalaireID;
                emp.EmployeSalaires.Add(sal);
            }

            foreach (CongeDto congDto in congesDto)
            {
                var cong = new Conge();
                cong.AccumulableParAnnee = congDto.AccumulableParAnnee;
                cong.IncrementableParMois = congDto.IncrementableParMois;
                cong.JourBalance = congDto.JourBalance;
                cong.JourParAnnee = congDto.JourParAnnee;
                cong.ModifiePar = congDto.ModifiePar;
                cong.TypeCongeID = congDto.TypeCongeID;
                emp.Conges.Add(cong);
            }

            foreach (EmployePrelevementDto prelDto in empPrelDto)
            {
                var prel = new EmployePrelevement();
                prel.PrelevementID = prelDto.PrelevementID;
                prel.TypePeriodeID = prelDto.TypePeriodeID;
                prel.TypeSalaireID = prelDto.TypeSalaireID;
                prel.Pourcentage = prelDto.Pourcentage;
                emp.EmployePrelevements.Add(prel);
            }

            //Construire le "object graph" détaché de l'employé à mettre à jour

            try
            {
                //Mettre à jour, insérer ou supprimmer les données
                using (var context = new PayrollEntities())
                {
                    // Charger les données de l'employé se trouvant dans la base avec ses congés, ses prélevements et ses salaires 
                    Employe existingEmp = context.Employes
                                                .Where(e => e.EmployeID == emp.EmployeID)
                                                .Include(e => e.Conges)
                                                .Include(e => e.EmployePrelevements)
                                                .Include(e => e.EmployeSalaires)
                                                .FirstOrDefault();
                    // Si l'employé a été trouvé, procéder à la mise à jour des ses données personnelles et annexes
                    if (existingEmp != null)
                    {
                        // ***Mettre à jour les données personnelles de l'employé***
                        var empEntry = context.Entry(existingEmp);
                        empEntry.CurrentValues.SetValues(emp);

                        // ***Mettre à jour les salaires de l'employé***

                        // Créer une instance du comparateur pour les salaires
                        EmployeSalaireEqualityComparer empSalEqC = new EmployeSalaireEqualityComparer();
                        // Chercher les nouvellement ajoutés
                        var addedEmpSals = emp.EmployeSalaires.Except(existingEmp.EmployeSalaires, empSalEqC);
                        // Chercher les supprimés
                        var deletedEmpSals = existingEmp.EmployeSalaires.Except(emp.EmployeSalaires, empSalEqC);
                        // Chercher les mis à jour
                        var modifiedEmpSals = emp.EmployeSalaires.Except(addedEmpSals, empSalEqC);
                        // Marquer tous les ajouts comme tels dans le contexte
                        addedEmpSals.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Added);
                        // Marquer toutes les suppressions comme telles dans le contexte
                        deletedEmpSals.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Deleted);
                        // Mettre à jour les propriétés modifiées par le client
                        foreach (EmployeSalaire epS in modifiedEmpSals)
                        {
                            var existingEmpSal = context.EmployeSalaires.Where(e => e.EmployeID == epS.EmployeID && e.TypeSalaireID == epS.TypeSalaireID);
                            if (existingEmpSal != null)
                            {
                                var empSalEntry = context.Entry(existingEmpSal);
                                empSalEntry.CurrentValues.SetValues(epS);
                            }
                        }

                        // ***Mettre à jour les congés de l'employé***

                        // Créer une instance du comparateur pour les congés
                        CongeEqualityComparer CongEqC = new CongeEqualityComparer();
                        // Chercher les nouvellement ajoutés
                        var addedCongs = emp.Conges.Except(existingEmp.Conges, CongEqC);
                        // Chercher les supprimés
                        var deletedCongs = existingEmp.Conges.Except(emp.Conges, CongEqC);
                        // Chercher les mis à jour
                        var modifiedCongs = emp.Conges.Except(addedCongs, CongEqC);
                        // Marquer tous les ajouts comme tels dans le contexte
                        addedCongs.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Added);
                        // Marquer toutes les suppressions comme telles dans le contexte
                        deletedCongs.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Deleted);
                        // Mettre à jour les propriétés modifiées par le client
                        foreach (Conge epS in modifiedCongs)
                        {
                            var existingCong = context.Conges.Where(e => e.EmployeID == epS.EmployeID && e.TypeCongeID == epS.TypeCongeID);
                            if (existingCong != null)
                            {
                                var CongEntry = context.Entry(existingCong);
                                CongEntry.CurrentValues.SetValues(epS);
                            }
                        }

                        // ***Mettre à jour les prélèvements de l'employé***

                        // Créer une instance du comparateur pour les prélèvements
                        EmployePrelevementEqualityComparer EmpPrelEqC = new EmployePrelevementEqualityComparer();
                        // Chercher les nouvellement ajoutés
                        var addedEmpPrels = emp.EmployePrelevements.Except(existingEmp.EmployePrelevements, EmpPrelEqC);
                        // Chercher les supprimés
                        var deletedEmpPrels = existingEmp.EmployePrelevements.Except(emp.EmployePrelevements, EmpPrelEqC);
                        // Chercher les mis à jour
                        var modifiedEmpPrels = emp.EmployePrelevements.Except(addedEmpPrels, EmpPrelEqC);
                        // Marquer tous les ajouts comme tels dans le contexte
                        addedEmpPrels.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Added);
                        // Marquer toutes les suppressions comme telles dans le contexte
                        deletedEmpPrels.ToList().ForEach(sal => context.Entry(sal).State = System.Data.EntityState.Deleted);
                        // Mettre à jour les propriétés modifiées par le client
                        foreach (EmployePrelevement epS in modifiedEmpPrels)
                        {
                            var existingEmpPrel = context.EmployePrelevements
                                                    .Where(e => e.EmployeID == epS.EmployeID && e.PrelevementID == epS.PrelevementID
                                                    && e.TypePeriodeID == epS.TypePeriodeID && e.TypeSalaireID == epS.TypeSalaireID);
                            if (existingEmpPrel != null)
                            {
                                var EmpPrelEntry = context.Entry(existingEmpPrel);
                                EmpPrelEntry.CurrentValues.SetValues(epS);
                            }
                        }
                    }
                }
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public int GetNewNumeroInterne(int companyID)
        {
            // Renvoyer un nouveau numero interne pour un nouvel employé d'une compagnie
            int empNum;
            using (var context = new PayrollEntities())
            {
                var oEmpNum = new ObjectParameter("NumeroEmploye", typeof(int));
                var result = context.GetEmployeInternalNumber(companyID, oEmpNum);
                empNum = (int)oEmpNum.Value;
            }
            return empNum;
        }

        public int GetSearchCount(EmployeSearchDto emp)
        {
            //Nombre d'employés pour une compangie par rapport aux critères de recherche précisés
            using (var context = new PayrollEntities())
            {
                //Convertir l'ensemble des employés en IQueryable (équivaut à faire un "select * from <table>" en sql)
                var queryableEmp = context.Employes.AsQueryable();
                //Choisir les employés actifs de la compagnie (la base n'est pas intérrogée tant que "ToList" n'est pas appelée)
                queryableEmp = queryableEmp.Where(x => (x.StatutEmployeID == emp.StatutEmployeID) && (x.CompagnieID == emp.CompagnieID));
                // Ajouter les autres critères un par un s'ils on été renseignés dans le formulaire
                if (!String.IsNullOrEmpty(emp.Phone1))
                    queryableEmp = queryableEmp.Where(x => x.Phone1.Contains(emp.Phone1));
                if (!String.IsNullOrEmpty(emp.Nom))
                    queryableEmp = queryableEmp.Where(x => x.Nom.Contains(emp.Nom));
                if (!String.IsNullOrEmpty(emp.Prenom))
                    queryableEmp = queryableEmp.Where(x => x.Prenom.Contains(emp.Prenom));
                if (!String.IsNullOrEmpty(emp.CIN))
                    queryableEmp = queryableEmp.Where(x => x.Cin.Contains(emp.CIN));
                // Renvoyer le nombre d'enregistrements trouvé
                return queryableEmp.Count();
            }
        }
        
        public IEnumerable<EmployeSearchDto> GetSearchResult(EmployeSearchDto emp)
        {
            //Retrouve les employés d'une compangie correspondant aux critères de recherche précisés
            // en utilisant des "Query Expressions" à la place de "LINQ to Entities" (synthaxe différente, mêmes effets)
            using (var context = new PayrollEntities())
            {
                //Convertir l'ensemble des employés en IQueryable (équivaut à faire un "select * from <table>" en sql)
                var queryableEmp = context.Employes.AsQueryable();
                //Choisir les employés actifs de la compagnie (la base n'est pas intérrogée tant que "ToList" n'est pas appelée)
                queryableEmp = queryableEmp.Where(x => (x.StatutEmployeID == emp.StatutEmployeID) && (x.CompagnieID == emp.CompagnieID));
                // Ajouter les autres critères un par un s'ils on été renseignés dans le formulaire
                if (!String.IsNullOrEmpty(emp.Phone1))
                    queryableEmp = queryableEmp.Where(x => x.Phone1.Contains(emp.Phone1));
                if (!String.IsNullOrEmpty(emp.Nom))
                    queryableEmp = queryableEmp.Where(x => x.Nom.Contains(emp.Nom));
                if (!String.IsNullOrEmpty(emp.Prenom))
                    queryableEmp = queryableEmp.Where(x => x.Prenom.Contains(emp.Prenom));
                if (!String.IsNullOrEmpty(emp.CIN))
                    queryableEmp = queryableEmp.Where(x => x.Cin.Contains(emp.CIN));
                // Trier par ordre alphabétique du Nom et du Prenom
                queryableEmp.OrderBy(x => x.Nom).ThenBy(x => x.Prenom);
                // Renvoyer le résultat sous forme d'une liste d'objets de type EmployeSearchDto
                return queryableEmp.Select(y => new EmployeSearchDto
                {
                    Nom = y.Nom,
                    Prenom = y.Prenom,
                    NumeroEmploye = y.NumeroEmploye,
                    Phone1 = y.Phone1,
                    CIN = y.Cin,
                    CompagnieID = y.CompagnieID,
                    StatutEmployeID = y.TypeEmployeID,
                    EmployeID = y.EmployeID
                }).ToList();
            }
        }

        public Int64? GetEmployeId(int CompagnieId, int NumeroEmploye)
        {
            // À partir du numéro interne de l'employé et du code de sa compagnie, retrouver le EmployeID
            using (var context = new PayrollEntities())
            {
                var queryableEmp = context.Employes.Where(x => (x.CompagnieID == CompagnieId && x.NumeroEmploye == NumeroEmploye)).FirstOrDefault();
                return queryableEmp.EmployeID;
            }
        }

        public EmployeDto GetEmployeById(long employeID)
        {
            using (var context = new PayrollEntities())
            {
                EmployeDto query = (from t in context.Employes
                                    where t.EmployeID == employeID
                                    select new EmployeDto
                                    {
                                        AdresseRue1 = t.AdresseRue1,
                                        AdresseRue2 = t.AdresseRue2,
                                        AdresseVille = t.AdresseVille,
                                        CIN = t.Cin,
                                        CompagnieID = t.CompagnieID,
                                        DateEmbauche = t.DateEmbauche,
                                        DateNaissance = t.DateNaissance,
                                        DepartementID = t.DepartementID,
                                        Email = t.Email,
                                        Extension = t.Extension,
                                        ModifiePar = t.ModifiePar,
                                        NIF = t.Nif,
                                        Nom = t.Nom,
                                        NumeroEmploye = t.NumeroEmploye,
                                        Phone1 = t.Phone1,
                                        Phone2 = t.Phone2,
                                        PosteID = t.PosteID,
                                        Prenom = t.Prenom,
                                        Sexe = t.Sexe,
                                        StatutEmployeID = t.StatutEmployeID,
                                        StatutMatrimonialID = t.StatutMatrimonialID,
                                        TypeEmployeID = t.TypeEmployeID
                                    }).FirstOrDefault();
                return query;
            }
        }

        public EmployeSalaireDto GetSalaireByType(long employeID, int typeSalaire)
        {
            using (PayrollEntities context = new PayrollEntities())
            {
                EmployeSalaireDto query = (from s in context.EmployeSalaires
                             where s.EmployeID == employeID
                             && s.TypeSalaireID == typeSalaire
                             select new EmployeSalaireDto
                             {
                                 BanqueID = s.BanqueID,
                                 BonusFinAnnee = (bool)s.BonusFinAnnee,
                                 CompteBancaire = s.CompteBancaire,
                                 EmployeID = s.EmployeID,
                                 HeureSupPaye = (bool)s.HeureSupPaye,
                                 MonnaieID = s.MonnaieID,
                                 Salaire = s.Salaire,
                                 TypePaiementID = s.TypePaiementID,
                                 TypeSalaireID = s.TypeSalaireID
                             }).FirstOrDefault();

                return query;
            }
        }

        public IEnumerable<CongeDto> GetConge(long employeID)
        {
            using (PayrollEntities context = new PayrollEntities())
            {
                IEnumerable<CongeDto> query = (from c in context.Conges
                                               where c.EmployeID == employeID
                                               select new CongeDto
                                               {
                                                   AccumulableParAnnee = c.AccumulableParAnnee,
                                                   EmployeID = c.EmployeID,
                                                   IncrementableParMois = c.IncrementableParMois,
                                                   JourBalance = c.JourBalance,
                                                   JourParAnnee = c.JourParAnnee,
                                                   TypeCongeID = c.TypeCongeID,
                                                   TypeConge = c.TypeConge.Nom,
                                               }).ToList();
                return query;
            }
        }

        public IEnumerable<EmployePrelevementDto> GetPrelevementByTypeSalaire(long employeID, int typeSalaire)
        {
            using (PayrollEntities context = new PayrollEntities())
            {
                IEnumerable<EmployePrelevementDto> query = (from p in context.EmployePrelevements
                                                            where p.EmployeID == employeID
                                                            && p.TypeSalaireID == typeSalaire
                                                            select new EmployePrelevementDto
                                                            {
                                                                EmployeID = p.EmployeID,
                                                                Pourcentage = p.Pourcentage,
                                                                PrelevementID = p.PrelevementID,
                                                                TypePeriodeID = p.TypePeriodeID,
                                                                TypeSalaireID = p.TypeSalaireID
                                                            }).ToList();
                return query;
            }
        }

    }
}
    