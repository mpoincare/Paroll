using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Dal.Core;

namespace Payroll.Dal
{
    public class CompagnieRepository
    {
        //Sélectionner toutes les types de périodes disponible pour une compagnie
        public IEnumerable<TypePeriodeDto> GetAllTypePeriode(int companyID)
        {
            IEnumerable<TypePeriodeDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.TypePeriodes
                                    from c in t.Compagnies
                                    where c.CompagnieID == companyID
                                    orderby t.Nom ascending
                                    select new TypePeriodeDto
                                    {
                                        TypePeriodeID = t.TypePeriodeID,
                                        Nom = t.Nom
                                    }).ToList();
            }
            return query;
        }

        //Sélectionner le TypePeriodeID pour une Compagnie, un Prelevement and un TypeSalaire donné CompagniePrelevement
        public int GetDefaultTypePeriodeForPrelevement(int companyID, int prelevementID, int typeSalaireID)
        {
            int scalar;
            using (var context = new PayrollEntities())
            {
                var query = (from t in context.CompagniePrelevements
                             where t.CompagnieID == companyID
                             && t.PrelevementID == prelevementID
                             && t.TypeSalaireID == typeSalaireID
                             select new { t.TypePeriodeID }).SingleOrDefault();
                scalar = query.TypePeriodeID;
            }
            return scalar;
        }

        //Sélectionner tous les Prelevement et leurs période d'application par défaut pour une Compagnie et un TypeSalaire
        public IEnumerable<CompagniePrelevementDto> GetAllPrelevementForCompany(int companyID, int typeSalaireID)
        {
            IEnumerable<CompagniePrelevementDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.CompagniePrelevements
                        where t.CompagnieID == companyID
                        && t.TypeSalaireID == typeSalaireID
                        orderby t.Prelevement.TypePrelevementID ascending,
                            t.Prelevement.Nom ascending
                         select new CompagniePrelevementDto
                            {
                                CompagnieID = t.CompagnieID,
                                TypeSalaireID = t.TypeSalaireID,
                                PrelevementID = t.PrelevementID,
                                Prelevement = t.Prelevement.Nom,
                                TypePeriodeID = t.TypePeriodeID,
                                Pourcentage = t.Pourcentage
                            }).ToList();
            }
            return query;
        }

        //Sélectionner tous les Congés et leurs attributs par défaut pour une Compagnie
        public IEnumerable<CompagnieTypeCongeDto> GetAllCongeForCompany(int companyID)
        {
            IEnumerable<CompagnieTypeCongeDto> query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.CompagnieTypeConges
                         where t.CompagnieID == companyID
                         orderby t.TypeCongeID ascending
                         select new CompagnieTypeCongeDto
                         {
                             CompagnieID = t.CompagnieID,
                             TypeCongeID = t.TypeCongeID,
                             Conge = t.TypeConge.Nom,
                             JourParAnnee = t.JourParAnnee,
                             AccumulableParAnnee = t.AccumulableParAnnee,
                             IncrementableParMois = t.IncrementableParMois
                         }).ToList();
            }
            return query;
        }

        public CompagnieDto GetCompagnieById(int compagnieID)
        {
            CompagnieDto query;
            using (var context = new PayrollEntities())
            {
                query = (from t in context.Compagnies
                         where t.CompagnieID == compagnieID
                         select new CompagnieDto
                         {
                             CompagnieID = t.CompagnieID,
                             Nom = t.Nom,
                             Nif = t.Nif,
                             NumeroEmployeSuivant = t.NumeroEmployeSuivant,
                             MensualiteLogiciel = t.MensualiteLogiciel,
                             MonnaieID = t.MonnaieID,
                             ExpirationLogiciel = t.ExpirationLogiciel,
                             MaximumEmploye = t.MaximumEmploye,
                             HoldingID = t.HoldingID,
                             ModifiePar = t.ModifiePar
                         }
                         ).SingleOrDefault();
            }
            return query;
        }

        public decimal GetSalaireNormalMaxForCompany(int compagnieID, int monnaieID)
        {
            decimal sMax;
            using (var context = new PayrollEntities())
            {
                var query = (from t in context.CompagnieMonnaies
                             where t.CompagnieID == compagnieID
                             && t.MonnaieID == monnaieID
                             select new { SalaireNormalMax = t.SalaireNormalMax }).SingleOrDefault();

                sMax = query.SalaireNormalMax;
            }
            return sMax;
        }
    }
}
