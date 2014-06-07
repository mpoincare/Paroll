using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal.Core;

namespace Payroll.Dal
{
    public class LookupRepository
    {
        //Sélectionner les types d'employés définis pour une compagnie donnée
        public IEnumerable<TypeEmployeDto> GetAllTypeEmployeForCompany(int companyID)
        {
            IEnumerable<TypeEmployeDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.CompagnieTypeEmployes
                         where t.CompagnieID == companyID
                         orderby t.TypeEmployeID ascending
                         select new TypeEmployeDto
                         {
                             TypeEmployeID = t.TypeEmployeID,
                             Nom = t.TypeEmploye.Nom
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les statuts matrimoniaux
        public IEnumerable<StatutMatrimonialDto> GetAllStatutMatrimonial()
        {
            IEnumerable<StatutMatrimonialDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.StatutMatrimonials
                         orderby t.Nom ascending
                         select new StatutMatrimonialDto
                         {
                             StatutMatrimonialID = t.StatutMatrimonialID,
                             Nom = t.Nom
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les postes définis pour une compagnie donnée
        public IEnumerable<PosteDto> GetAllPosteForCompany(int companyID)
        {
            IEnumerable<PosteDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.Postes
                         where t.CompagnieID == companyID
                         orderby t.Nom ascending
                         select new PosteDto
                         {
                             PosteID = t.PosteID,
                             Nom = t.Nom,
                             CompagnieID = t.CompagnieID,
                             ModifiePar = t.ModifiePar
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les départements définis pour une compagnie donnée
        public IEnumerable<DepartementDto> GetAllDepartementForCompany(int companyID)
        {
            IEnumerable<DepartementDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.Departements
                         where t.CompagnieID == companyID
                         orderby t.Nom ascending
                         select new DepartementDto
                         {
                             DepartementID = t.DepartementID,
                             Nom = t.Nom,
                             CompagnieID = t.CompagnieID,
                             ModifiePar = t.ModifiePar,
                             DepartementParentID = t.DepartementID
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les types de paiement définis pour une compagnie donnée
        public IEnumerable<TypePaiementDto> GetAllTypePaiementForCompany(int companyID)
        {
            IEnumerable<TypePaiementDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.CompagnieTypePaiements
                         where t.CompagnieID == companyID
                         orderby t.TypePaiement.Nom ascending
                         select new TypePaiementDto
                         {
                             TypePaiementID = t.TypePaiementID,
                             Nom = t.TypePaiement.Nom,
                             CompagnieID = t.CompagnieID,
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les monnaies définies pour une compagnie donnée
        public IEnumerable<MonnaieDto> GetAllMonnaieForCompany(int companyID)
        {
            IEnumerable<MonnaieDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.CompagnieMonnaies
                         where t.CompagnieID == companyID
                         orderby t.Monnaie.Nom ascending
                         select new MonnaieDto
                         {
                             MonnaieID = t.MonnaieID,
                             Nom = t.Monnaie.Nom,
                             CompagnieID = t.CompagnieID,
                             Abreviation = t.Monnaie.Abreviation,
                             Symbol = t.Monnaie.Symbol,
                             Locale = t.Monnaie.Locale
                         }).ToList();
            }
            return query;
        }

        //Sélectionner les banques définies pour une compagnie donnée
        public IEnumerable<BanqueDto> GetAllBanqueForCompany(int companyID)
        {
            IEnumerable<BanqueDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.Banques
                         from c in t.Compagnies
                         where c.CompagnieID == companyID
                         orderby t.Nom ascending
                         select new BanqueDto
                         {
                             BanqueID = t.BanqueID,
                             Nom = t.Nom,
                             CompagnieID = c.CompagnieID
                         }).ToList();
            }
            return query;
        }
    }
}
