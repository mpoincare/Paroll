using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal
{
    // Comparateur pour EmployeSalaire
    public class EmployeSalaireEqualityComparer : IEqualityComparer<EmployeSalaire>
    {
        public bool Equals(EmployeSalaire e1, EmployeSalaire e2)
        {
            if (e1.EmployeID == e2.EmployeID && e1.TypeSalaireID == e2.TypeSalaireID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(EmployeSalaire e)
        {
            long hCode = e.EmployeID ^ e.TypeSalaireID;
            return hCode.GetHashCode();
        }

    }
    
    //Comparateur pour Conge
    public class CongeEqualityComparer : IEqualityComparer<Conge>
    {
        public bool Equals(Conge e1, Conge e2)
        {
            if (e1.EmployeID == e2.EmployeID && e1.TypeCongeID == e2.TypeCongeID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(Conge e)
        {
            long hCode = e.EmployeID ^ e.TypeCongeID;
            return hCode.GetHashCode();
        }

    }

    //Comparateur pour EmployePrelevement
    public class EmployePrelevementEqualityComparer : IEqualityComparer<EmployePrelevement>
    {
        public bool Equals(EmployePrelevement e1, EmployePrelevement e2)
        {
            if (e1.EmployeID == e2.EmployeID && e1.PrelevementID == e2.PrelevementID 
                && e1.TypeSalaireID == e2.TypeSalaireID && e1.TypePeriodeID == e2.TypePeriodeID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(EmployePrelevement e)
        {
            long hCode = e.EmployeID ^ e.PrelevementID ^ e.TypeSalaireID ^ e.TypePeriodeID;
            return hCode.GetHashCode();
        }

    }
}
