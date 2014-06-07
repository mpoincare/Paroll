using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Dal.Core
{
    public class EmployeDto
    {
        public Int64 EmployeID { get; set; }
        public int CompagnieID { get; set; }
        public int NumeroEmploye { get; set; }
        public string CIN { get; set; }
        public string NIF { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string DateNaissanceString { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Sexe { get; set; }
        public string AdresseRue1 { get; set; }
        public string AdresseRue2 { get; set; }
        public string AdresseVille { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Extension { get; set; }
        public string Email { get; set; }
        public string UrgenceNom { get; set; }
        public string UrgencePhone { get; set; }
        public string DateEmbaucheString { get; set; }
        public DateTime DateEmbauche { get; set; }
        public string DateFinEmbaucheString { get; set; }
        public DateTime DateFinEmbauche { get; set; }
        public int DepartementID { get; set; }
        public int StatutEmployeID { get; set; }
        public int StatutMatrimonialID { get; set; }
        public int? PosteID { get; set; }
        public int TypeEmployeID { get; set; }
        public string ModifiePar { get; set; }
    }
}
