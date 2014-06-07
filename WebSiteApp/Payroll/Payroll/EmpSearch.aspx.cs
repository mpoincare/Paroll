using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Bll;
using Payroll.Dal.Core;
using Payroll.Helpers;

namespace Payroll
{
    public partial class EmpSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GridViewEmployes.Visible = false;

        }

        protected void FindAndContinueButton_Click(object sender, EventArgs ev)
        {
            // Cette recherche retrouve l'employé directement à partir du numéro interne
            // et charge la forme initialement prévue directement
    
            Int64? EmployeId;
            // Vérifier que le url de la forme pour laquelle on recherche l'employé est correct
            var transferUrl = HttpUtility.UrlEncode(Request.QueryString["TransferUrl"]);
            if (String.IsNullOrEmpty(transferUrl))
            {
                UctMessage.Show(UserControls.MessageType.Error, @"Oops... aucune suite n'est prévue après la recherche. Recommencer à partir du menu principal.");
            }
            else
            {
                // Valider le contenu du champ du numéro interne de l'employé
                List<String> errorMessages = new List<string>(); // Pour conserver les messages d'erreurs éventuels
                EmployeManager empMan = new EmployeManager();
                int compagnieId = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
                int numeroEmploye;
                if (empMan.IsSearchValid(TxbNumeroInterne.Text.Trim(), out numeroEmploye, ref errorMessages))
                {
                    EmployeId = empMan.GetEmployeID(compagnieId, numeroEmploye);
                    // Si EmployeId n'est pas null continuer, autrement afficher message erreur
                    if (EmployeId != null)
                    {
                        // Transférer vers la page qui avait été appelée au préalable
                        Response.Redirect(transferUrl + "?EmployeID=" + EmployeId.ToString());
                    }
                    else
                    {
                        HideGridView();
                        UctMessage.Show(UserControls.MessageType.Error, @"Oops... aucun employé ne correspond à ce numéro.");
                    }
                }
                else
                {
                    HideGridView();
                    UctMessage.Show(UserControls.MessageType.Error, @"Oops... des erreurs sont survenues:", errorMessages);
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs ev)
        {
            // Cette recherche affichera dans une grille le ou les employés correspondant aux critères
 
            // Vérifier que le url de la forme à charger après la recherche existe
            //var transferUrl = HttpUtility.UrlEncode(Request.QueryString["TransferUrl"]);
            var transferUrl = Request.QueryString["TransferUrl"];
            if (String.IsNullOrEmpty(transferUrl))
            {
                UctMessage.Show(UserControls.MessageType.Error, @"Aucune suite n'est prévue après la recherche. Recommencer à partir du menu principal.");
            }
            else
            {
                // Remplir Dto
                EmployeSearchDto emp = FillEmpSearchDto(transferUrl);
                // Vérifier que la recherche est valide
                List<String> errorMessages = new List<string>(); // Pour conserver les messages d'erreurs éventuels
                EmployeManager empMan = new EmployeManager();
                if (empMan.IsSearchValid(emp, ref errorMessages))
                {
                    //Effacer des éventuels message précédents
                    UctMessage.Hide();
                    //Afficher les résultat dans le GridView
                    GridViewEmployes.Visible = true;
                    GridViewEmployes.DataSource = empMan.GetSearchResult(emp);
                    GridViewEmployes.DataBind();
                }
                else
                {
                    HideGridView();
                    UctMessage.Show(UserControls.MessageType.Error, @"Oops... quelques problèmes sont survenues:", errorMessages);
                }
            }
        }
            
        private EmployeSearchDto FillEmpSearchDto(String transferUrl)
        {
            EmployeSearchDto emp = new EmployeSearchDto();
            emp.Phone1 = TxbPhone1.Text.Trim();
            emp.Nom = TxbNom.Text.Trim();
            emp.Prenom = TxbPrenom.Text.Trim();
            emp.CIN = TxbCin.Text.Trim();
            emp.CompagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            emp.StatutEmployeID = (int)LookupEnum.StatutEmploye.Actif;
            emp.Url = transferUrl; // page de transfer une fois l'employé trouvé
            return emp;
        }

        private void HideGridView()
        {
            GridViewEmployes.DataSource = null;
            GridViewEmployes.DataBind();
        }
    }
}