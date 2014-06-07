using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Bll;
using Payroll;
using Payroll.UserControls;
using Payroll.Dal.Core;
using Payroll.Helpers;

namespace Payroll.Personnel
{
    public partial class EmployeNouveau : System.Web.UI.Page
    {
        private bool _isEdit;

        protected void Page_Init(object sender, EventArgs e)
        {
            // Renseigner la variable _isEdit et dans le cas d'une édition, vérifier la conformité du QueryString
            SetEditStatusAndVerifyUrl();

            //Çréer les User Controls dynamiquement ici pour qu'ils puissent être mis à jour durant les Postbacks
            AddPrelevementUserControls(LookupEnum.TypeSalaire.Regulier);
            AddPrelevementUserControls(LookupEnum.TypeSalaire.Special);
            AddCongeUserControls();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Remplir les DropDownList
                FillTypeEmploye();
                FillStatutmatrimonial();
                FillPoste();
                FillDepartement();
                FillTypePaiementSalaireRegulierAndSpecial();
                FillMonnaieSalaireRegulierAndSpecial();
                FillBanqueSalaireRegulierAndSpecial();

                UctDateEmbauche.FillList();
                UctDateNaissance.FillList();

                if (_isEdit)
                {
                    //Si on a à faire à une mise à jour, charger les données de l'employé et renseigner les champs
                    SetEmployeInformation();
                }
            }
        }

        protected void DdlTypeEmploye_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Montrer ou cacher le panel contenant les champs spécifiques aux employés speciaux (avec Ajax)
            if (Int32.Parse(DdlTypeEmploye.SelectedValue) == (int)LookupEnum.TypeEmploye.Special)
            {
                PanelSpecial.Visible = true;
            }
            else
            {
                PanelSpecial.Visible = false;
            }
        }
        protected void CreateEmployeeButton_Click(object sender, EventArgs ev)
        {
            //Vérifier d'abord que les validations au niveau du formulaire sont correctes
            if (IsValid)
            {
                //Remplir les Dto avec les données du formulaire pour permettre la validation et la sauvegarde
                EmployeDto emp = FillEmployeDto();
                EmployeSalaireDto empSalaireRegulier = FillEmployeSalaireRegulierDto();
                List<CongeDto> empConges = FillCongeDto();
                List<EmployePrelevementDto> empPrelevementRegulier = FillPrelevementRegulierDto();

                // Remplir les Dto dans le cas d'un employé spécial
                EmployeSalaireDto empSalaireSpecial = null;
                List<EmployePrelevementDto> empPrelevementSpecial = null;
                if (Int32.Parse(DdlTypeEmploye.SelectedValue) == (int)LookupEnum.TypeEmploye.Special)
                {
                    empSalaireSpecial = FillEmployeSalaireSpecialDto();
                    empPrelevementSpecial = FillPrelevementSpecialDto();
                }

                //Validation des règles (business rules)
                List<String> errorMessages = new List<string>(); // Pour conserver les messages d'erreurs éventuels
                // Validation employé données personnelles
                bool isValidEmp = new EmployeManager().IsValid(emp, ref errorMessages);
                // Validation salaire régulier
                bool isValidSalReg = new EmployeSalaireManager().IsValid(empSalaireRegulier, ref errorMessages);
                // Validation salaire spécial si on a à faire à ce type d'employé
                bool isValidSalSpe = true;
                if (Int32.Parse(DdlTypeEmploye.SelectedValue) == (int)LookupEnum.TypeEmploye.Special)
                {
                    isValidSalSpe = new EmployeSalaireManager().IsValid(empSalaireSpecial, ref errorMessages);
                }
                // Validation congés
                bool isValidConge = true;
                isValidConge = new CongeManager().IsValid(empConges, ref errorMessages);

                //Validation prélèvements régulier
                bool isValidPrelevementRegulier = new EmployePrelevementManager().IsValid(empPrelevementRegulier, ref errorMessages);
                // Validation prélèvements spécial si on a à faire à ce type d'employé
                bool isValidPrelevementSpecial = true;
                if (Int32.Parse(DdlTypeEmploye.SelectedValue) == (int)LookupEnum.TypeEmploye.Special)
                {
                    isValidPrelevementSpecial = new EmployePrelevementManager().IsValid(empPrelevementRegulier, ref errorMessages);
                }

                // Si les données sont correctes, sauvegarder les, sinon afficher les messages d'erreurs
                if (isValidEmp && isValidSalReg && isValidSalSpe && isValidConge && isValidPrelevementRegulier && isValidPrelevementSpecial)
                {
                    EmployeManager empMan = new EmployeManager();
                    //Insérer ou mettre à jour
                    if (_isEdit)
                    {
                        emp.EmployeID = Int64.Parse(Request.QueryString["Id"]);
                        empMan.UpdateEmploye(emp, empSalaireRegulier, empSalaireSpecial, empConges,
                            empPrelevementRegulier, empPrelevementSpecial);
                        UctMessage.Show(UserControls.MessageType.Success,
                            String.Format("L'employé {0} {1} a été mise à jour avec succès.", emp.Prenom, emp.Nom));
                    }
                    else
                    {
                        int numInterne = empMan.AddEmploye(emp, empSalaireRegulier, empSalaireSpecial, empConges,
                            empPrelevementRegulier, empPrelevementSpecial);
                        UctMessage.Show(UserControls.MessageType.Success,
                            String.Format("L'employé {0} {1} a été créé avec succès. Son numéro interne est: {2}", emp.Prenom, emp.Nom, numInterne));
                        //Nettoyer la forme pour prochaine saisie
                        CleanForm();
                    }
                }
                else
                {
                    UctMessage.Show(Payroll.UserControls.MessageType.Error, @"Oops... des erreurs sont survenues:", errorMessages);
                }
            }
        }

        private void AddPrelevementUserControls(LookupEnum.TypeSalaire typeSal)
        {
            IEnumerable<CompagniePrelevementDto> cpd;
            CompagnieManager cm = new CompagnieManager();
            int compagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            cpd = cm.GetAllPrelevementForCompany(compagnieID, (int)typeSal);

            IEnumerable<TypePeriodeDto> tpData;
            tpData = cm.GetTypePeriodeForCompany(compagnieID);

            // Sélection les prélèvements proprement dits (sans les type de période et les pourcentages)
            var prelevements = (from CompagniePrelevementDto c in cpd
                                select new { c.PrelevementID, c.Prelevement, c.CompagnieID, c.TypeSalaireID }).Distinct();

            // Pour chaque prélevement pour le salaire régulier, afficher le user control Prelevement
            foreach (var c in prelevements)
            {
                Prelevement pr;
                pr = (Prelevement)LoadControl("~/UserControls/Prelevement.ascx");
                //Dépendamment du Type de Salaire, ajouter le control au Panel régulier ou au Panel special
                if (typeSal == LookupEnum.TypeSalaire.Regulier)
                {
                    PlaceHolderPrelevementRegulier.Controls.Add(pr);
                }
                else
                {
                    PlaceHolderPrelevementSpecial.Controls.Add(pr);
                }
                pr.ID = c.PrelevementID.ToString() + '_' + c.Prelevement.Replace(' ', '_'); //Il faut rendre le ID du control unique
                if (_isEdit) { pr.PrelevementChecked = true; } else { pr.PrelevementChecked = true; }
                pr.CheckBoxText = c.Prelevement;
                pr.CompagnieID = c.CompagnieID;
                pr.TypeSalaireID = c.TypeSalaireID;
                pr.PrelevementID = c.PrelevementID;
                pr.TypePeriodes = tpData;
                //S'occuper des types de période et des pourcentages
                var query = (from p in cpd
                             where p.PrelevementID == c.PrelevementID
                             select new { p.TypePeriodeID, p.Pourcentage }).ToList();
                pr.TypePeriodeID1 = query[0].TypePeriodeID;
                pr.Pourcentage1 = query[0].Pourcentage;
                if (query.Count > 1)
                {
                    pr.TypePeriodeID2 = query[1].TypePeriodeID;
                    pr.Pourcentage2 = query[1].Pourcentage;
                }

            }
        }

        private void AddCongeUserControls()
        {
            IEnumerable<CompagnieTypeCongeDto> cpd;
            CompagnieManager cm = new CompagnieManager();
            int compagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            cpd = cm.GetAllCongeForCompany(compagnieID);
            foreach (CompagnieTypeCongeDto c in cpd)
            {
                CongeUserControl pr;
                pr = (CongeUserControl)LoadControl("~/UserControls/CongeUserControl.ascx");
                PlaceHolderConge.Controls.Add(pr);
                pr.ID = c.TypeCongeID.ToString() + '_' + c.Conge.Replace(' ', '_'); //Il faut rendre le ID du control unique
                if (_isEdit) { pr.CongeChecked = false; } else { pr.CongeChecked = true; }
                pr.CompagnieID = c.CompagnieID;
                pr.TypeCongeID = c.TypeCongeID;
                pr.TypeConge = c.Conge;
                if (c.JourParAnnee != null) pr.JourParAnnee = c.JourParAnnee.ToString();
                if (c.AccumulableParAnnee != null) pr.AccumulableParAnneeChecked = (bool)c.AccumulableParAnnee;
                if (c.IncrementableParMois != null) pr.IncrementableParMoisChecked = (bool)c.IncrementableParMois;
            }
        }


        private void FillTypeEmploye()
        {
            IEnumerable<TypeEmployeDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllTypeEmployeForCompany((int)Session["CompagnieID"]);
            DdlTypeEmploye.DataSource = data;
            DdlTypeEmploye.DataValueField = "TypeEmployeID";
            DdlTypeEmploye.DataTextField = "Nom";
            DdlTypeEmploye.DataBind();
        }

        private void FillStatutmatrimonial()
        {
            IEnumerable<StatutMatrimonialDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllStatutMatrimonial();
            DdlStatutMatrimonial.DataSource = data;
            DdlStatutMatrimonial.DataValueField = "StatutMatrimonialID";
            DdlStatutMatrimonial.DataTextField = "Nom";
            DdlStatutMatrimonial.DataBind();
            DdlStatutMatrimonial.Items.Insert(0, new ListItem("< Choisir un statut >", "0"));
        }

        private void FillPoste()
        {
            IEnumerable<PosteDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllPosteForCompany((int)Session["CompagnieID"]);
            DdlPoste.DataSource = data;
            DdlPoste.DataValueField = "PosteID";
            DdlPoste.DataTextField = "Nom";
            DdlPoste.DataBind();
            DdlPoste.Items.Insert(0, new ListItem("< Choisir un poste >", "0"));
        }

        private void FillDepartement()
        {
            IEnumerable<DepartementDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllDepartementForCompany((int)Session["CompagnieID"]);
            DdlDepartement.DataSource = data;
            DdlDepartement.DataValueField = "DepartementID";
            DdlDepartement.DataTextField = "Nom";
            DdlDepartement.DataBind();
            DdlDepartement.Items.Insert(0, new ListItem("< Choisir un département >", "0"));
        }

        private void FillTypePaiementSalaireRegulierAndSpecial()
        {
            IEnumerable<TypePaiementDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllTypePaiementForCompany((int)Session["CompagnieID"]);

            //Remplir type paiement salaire régulier
            DdlTypePaiementSalaireRegulier.DataSource = data;
            DdlTypePaiementSalaireRegulier.DataValueField = "TypePaiementID";
            DdlTypePaiementSalaireRegulier.DataTextField = "Nom";
            DdlTypePaiementSalaireRegulier.DataBind();

            //Remplir type paiement salaire spécial
            DdlTypePaiementSalaireSpecial.DataSource = data;
            DdlTypePaiementSalaireSpecial.DataValueField = "TypePaiementID";
            DdlTypePaiementSalaireSpecial.DataTextField = "Nom";
            DdlTypePaiementSalaireSpecial.DataBind();
        }

        private void FillMonnaieSalaireRegulierAndSpecial()
        {
            IEnumerable<MonnaieDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllMonnaieForCompany((int)Session["CompagnieID"]);

            //Remplir monnaie salaire régulier
            DdlMonnaieSalaireRegulier.DataSource = data;
            DdlMonnaieSalaireRegulier.DataValueField = "MonnaieID";
            DdlMonnaieSalaireRegulier.DataTextField = "Nom";
            DdlMonnaieSalaireRegulier.DataBind();

            //Remplir monnaie salaire spécial
            DdlMonnaieSalaireSpecial.DataSource = data;
            DdlMonnaieSalaireSpecial.DataValueField = "MonnaieID";
            DdlMonnaieSalaireSpecial.DataTextField = "Nom";
            DdlMonnaieSalaireSpecial.DataBind();
        }

        private void FillBanqueSalaireRegulierAndSpecial()
        {
            IEnumerable<BanqueDto> data;
            LookupManager lm = new LookupManager();
            data = lm.GetAllBanqueForCompany((int)Session["CompagnieID"]);

            //Remplir banque salaire régulier
            DdlBanqueSalaireRegulier.DataSource = data;
            DdlBanqueSalaireRegulier.DataValueField = "BanqueID";
            DdlBanqueSalaireRegulier.DataTextField = "Nom";
            DdlBanqueSalaireRegulier.DataBind();
            DdlBanqueSalaireRegulier.Items.Insert(0, new ListItem("< Choisir une banque >", "0"));

            //Remplir banque salaire spécial
            DdlBanqueSalaireSpecial.DataSource = data;
            DdlBanqueSalaireSpecial.DataValueField = "BanqueID";
            DdlBanqueSalaireSpecial.DataTextField = "Nom";
            DdlBanqueSalaireSpecial.DataBind();
            DdlBanqueSalaireSpecial.Items.Insert(0, new ListItem("< Choisir une banque >", "0"));
        }

        private EmployeDto FillEmployeDto()
        {
            EmployeDto emp = new EmployeDto();
            emp.AdresseRue1 = TxbAdresseRue1.Text.Trim();
            emp.AdresseRue2 = TxbAdresseRue2.Text.Trim();
            emp.CIN = TxbCin.Text.Trim();
            emp.CompagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            emp.DateEmbaucheString = UctDateEmbauche.SelectedDateString;
            emp.DateNaissanceString = UctDateNaissance.SelectedDateString;
            emp.DepartementID = Int32.Parse(DdlDepartement.SelectedValue);
            emp.Email = TxbEmail.Text.Trim();
            emp.ModifiePar = User.Identity.Name;
            emp.NIF = TxbNif.Text.Trim();
            emp.Nom = TxbNom.Text.ToTitleCase();
            emp.Phone1 = TxbPhone1.Text.Trim();
            emp.Phone2 = TxbPhone2.Text.Trim();
            emp.Prenom = TxbPrenom.Text.ToTitleCase();
            emp.PosteID = Int32.Parse(DdlPoste.SelectedValue);
            if (RbtSexeFeminin.Checked) { emp.Sexe = LookupConst.Feminin; }
            else
            {
                if (RbtSexeMasculin.Checked) { emp.Sexe = LookupConst.Masculin; }
                else { emp.Sexe = null; }
            }
            emp.StatutEmployeID = (int)LookupEnum.StatutEmploye.Actif;
            emp.StatutMatrimonialID = Int32.Parse(DdlStatutMatrimonial.SelectedValue);
            emp.TypeEmployeID = Int32.Parse(DdlTypeEmploye.SelectedValue);
            emp.UrgenceNom = TxbUrgenceNom.Text.Trim();
            emp.UrgencePhone = TxbUrgencePhone.Text.Trim();

            return emp;
        }

        private EmployeSalaireDto FillEmployeSalaireRegulierDto()
        {
            EmployeSalaireDto empSal = new EmployeSalaireDto();
            empSal.BanqueID = Int32.Parse(DdlBanqueSalaireRegulier.SelectedValue);
            if (ChkBonusFinAnneeRegulier.Checked) { empSal.BonusFinAnnee = true; } else { empSal.BonusFinAnnee = false; }
            empSal.CompagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            empSal.CompteBancaire = TxbCompteBancaireSalaireRegulier.Text.Trim();
            if (ChkHeureSupRegulier.Checked) { empSal.HeureSupPaye = true; } else { empSal.HeureSupPaye = false; }
            empSal.ModifiePar = User.Identity.Name;
            empSal.MonnaieID = Int32.Parse(DdlMonnaieSalaireRegulier.SelectedValue);
            empSal.TypePaiementID = Int32.Parse(DdlTypePaiementSalaireRegulier.SelectedValue);

            Decimal salReg;
            if ((!String.IsNullOrEmpty(TxbSalaireRegulier.Text.Trim())) && (Decimal.TryParse(TxbSalaireRegulier.Text.Trim(), out salReg)))
            { empSal.Salaire = salReg; }

            empSal.TypeSalaireID = (Int32)LookupEnum.TypeSalaire.Regulier;

            return empSal;
        }

        private EmployeSalaireDto FillEmployeSalaireSpecialDto()
        {
            EmployeSalaireDto empSal = new EmployeSalaireDto();
            empSal.BanqueID = Int32.Parse(DdlBanqueSalaireSpecial.SelectedValue);
            if (ChkBonusFinAnneeSpecial.Checked) { empSal.BonusFinAnnee = true; } else { empSal.BonusFinAnnee = false; }
            empSal.CompagnieID = SessionHelper.GetCompanyIDFromSession(HttpContext.Current);
            empSal.CompteBancaire = TxbCompteBancaireSalaireSpecial.Text.Trim();
            if (ChkHeureSupSpecial.Checked) { empSal.HeureSupPaye = true; } else { empSal.HeureSupPaye = false; }
            empSal.ModifiePar = User.Identity.Name;
            empSal.MonnaieID = Int32.Parse(DdlMonnaieSalaireSpecial.SelectedValue);
            empSal.TypePaiementID = Int32.Parse(DdlTypePaiementSalaireSpecial.SelectedValue);

            Decimal salReg;
            if ((!String.IsNullOrEmpty(TxbSalaireSpecial.Text.Trim())) && (Decimal.TryParse(TxbSalaireSpecial.Text.Trim(), out salReg)))
            { empSal.Salaire = salReg; }

            empSal.TypeSalaireID = (Int32)LookupEnum.TypeSalaire.Special;

            return empSal;
        }

        private List<CongeDto> FillCongeDto()
        {
            List<CongeDto> lst = new List<CongeDto>();
            foreach (Control c in PlaceHolderConge.Controls)
            {
                //Traiter seulement les congés cochés
                if (c is CongeUserControl)
                {
                    CongeUserControl cuc = (CongeUserControl)c;
                    if (cuc.CongeChecked)
                    {
                        CongeDto conge = new CongeDto();
                        conge.IncrementableParMois = cuc.IncrementableParMoisChecked;
                        conge.AccumulableParAnnee = cuc.IncrementableParMoisChecked;

                        Decimal jourBal;
                        if (Decimal.TryParse(cuc.BalanceJour.Trim(), out jourBal)) { conge.JourBalance = jourBal; }
                        else { conge.JourBalance = null; }

                        Decimal jourAn;
                        if (Decimal.TryParse(cuc.JourParAnnee.Trim(), out jourAn)) { conge.JourParAnnee = jourAn; }
                        else { conge.JourParAnnee = null; }

                        conge.ModifiePar = User.Identity.Name;
                        conge.TypeCongeID = cuc.TypeCongeID;
                        conge.TypeConge = cuc.TypeConge;
                        lst.Add(conge);
                    }
                }
            }
            return lst;
        }

        private List<EmployePrelevementDto> FillPrelevementRegulierDto()
        {
            List<EmployePrelevementDto> lst = new List<EmployePrelevementDto>();
            foreach (Control c in PlaceHolderPrelevementRegulier.Controls)
            {
                //Traiter seulement les prélèvement cochés
                if (c is Prelevement)
                {
                    Prelevement cuc = (Prelevement)c;
                    if (cuc.PrelevementChecked)
                    {
                        EmployePrelevementDto pre = new EmployePrelevementDto();
                        pre.PrelevementID = cuc.PrelevementID;
                        pre.TypePeriodeID = cuc.SelectedPeriode1;
                        pre.TypeSalaireID = cuc.TypeSalaireID;
                        pre.Pourcentage = cuc.SelectedPourcentage1;
                        lst.Add(pre);
                        //Si il y a plus qu'une période, ajouter un enregistrement pour la seconde période
                        if (cuc.TypePeriodes.Count() > 1)
                        {
                            EmployePrelevementDto pre2 = new EmployePrelevementDto();
                            pre2.PrelevementID = cuc.PrelevementID;
                            pre2.TypePeriodeID = cuc.SelectedPeriode2;
                            pre2.TypeSalaireID = cuc.TypeSalaireID;
                            pre2.Pourcentage = cuc.SelectedPourcentage2;
                            lst.Add(pre2);
                        }
                    }
                }
            }
            return lst;
        }

        private List<EmployePrelevementDto> FillPrelevementSpecialDto()
        {
            List<EmployePrelevementDto> lst = new List<EmployePrelevementDto>();
            foreach (Control c in PlaceHolderPrelevementSpecial.Controls)
            {
                //Traiter seulement les prélèvement cochés
                if (c is Prelevement)
                {
                    Prelevement cuc = (Prelevement)c;
                    if (cuc.PrelevementChecked)
                    {
                        EmployePrelevementDto pre = new EmployePrelevementDto();
                        pre.PrelevementID = cuc.PrelevementID;
                        pre.TypePeriodeID = cuc.SelectedPeriode1;
                        pre.TypeSalaireID = cuc.TypeSalaireID;
                        pre.Pourcentage = cuc.SelectedPourcentage1;
                        lst.Add(pre);
                        //Si il y a plus qu'une période, ajouter un enregistrement pour la seconde période
                        if (cuc.TypePeriodes.Count() > 1)
                        {
                            EmployePrelevementDto pre2 = new EmployePrelevementDto();
                            pre2.PrelevementID = cuc.PrelevementID;
                            pre2.TypePeriodeID = cuc.SelectedPeriode2;
                            pre2.TypeSalaireID = cuc.TypeSalaireID;
                            pre2.Pourcentage = cuc.SelectedPourcentage2;
                            lst.Add(pre2);
                        }
                    }
                }
            }
            return lst;
        }

        private void CleanForm()
        {
            //Nettoyer la forme pour l'enregistrement d'un nouvel employé
            TxbAdresseRue1.Text = String.Empty;
            TxbAdresseRue2.Text = String.Empty;
            TxbCin.Text = String.Empty;
            UctDateNaissance.SelectedDate = DateTime.Now;
            TxbEmail.Text = String.Empty;
            TxbNif.Text = String.Empty;
            TxbNom.Text = String.Empty;
            TxbPhone1.Text = String.Empty;
            TxbPhone2.Text = String.Empty;
            RbtSexeFeminin.Checked = false;
            RbtSexeMasculin.Checked = false;
            DdlStatutMatrimonial.SelectedIndex = 0;
            TxbUrgenceNom.Text = String.Empty;
            TxbUrgencePhone.Text = String.Empty;
            TxbCompteBancaireSalaireRegulier.Text = String.Empty;
            TxbSalaireRegulier.Text = String.Empty;
            TxbCompteBancaireSalaireSpecial.Text = String.Empty;
            TxbSalaireSpecial.Text = String.Empty;
        }

        private void SetEditStatusAndVerifyUrl()
        {
            // A-t-on à faire à un ajout ou une mise à jour?
            var employeID = HttpUtility.UrlEncode(Request.QueryString["Id"]);
            if (!String.IsNullOrEmpty(employeID))
            {
                _isEdit = true;
                //Si on a à faire à une mise à jour, vérifier que le QueryString n'a pas été changé par l'utilisateur
                UrlSecurity usec = new UrlSecurity();
                if (!usec.IsUrlNotTampered(String.Format("Id={0}", Request.QueryString["Id"]), Request.QueryString["Digest"]))
                    //Le Querystring a été changé, afficher la page d'erreur avec le message correspondant
                    Response.Redirect("~/Erreur.aspx?ErrorType=1");
            }
            else
                _isEdit = false;
        }

        private void SetEmployeInformation()
        {
            //Charger les données de l'employé dans les différents champs

            BtnCreateEmployee.Text = "Mettre à Jour"; // Changer le libellé du bouton
            long longEmployeID = Int64.Parse(Request.QueryString["Id"]); // Id de l'employé

            // Créer les Dto pour recevoir les informations de l'employé
            EmployeDto emp;
            EmployeSalaireDto empSalReg = new EmployeSalaireDto();
            EmployeSalaireDto empSalSpe = new EmployeSalaireDto();
            IEnumerable<EmployePrelevementDto> empPrelReg;
            IEnumerable<EmployePrelevementDto> empPrelSpe;
            IEnumerable<CongeDto> empConges;

            EmployeManager empMan = new EmployeManager();
            empMan.GetEmployeDetailsByID(longEmployeID, out emp, out empSalReg, out empSalSpe, out empConges,
                out empPrelReg, out empPrelSpe);

            SetInformationPersonnelles(emp);
            SetSalaireRegulier(empSalReg);
            SetConges(empConges);
            SetPrelevementsReguliers(empPrelReg);
            // Renseigner les champs du Salaire Spécial et des Prélèvements correspondant si le type de l'employe est "Special"
            if (emp.TypeEmployeID == (int)LookupEnum.TypeEmploye.Special)
            {
                SetSalaireSpecial(empSalSpe);
                SetPrelevementsSpecials(empPrelSpe);
            }
        }

        private void SetInformationPersonnelles(EmployeDto emp)
        {
            //Renseigner les champs avec les données personnelles de l'employé
            TxbAdresseRue1.Text = emp.AdresseRue1;
            TxbAdresseRue2.Text = emp.AdresseRue2;
            TxbCin.Text = emp.CIN;
            UctDateEmbauche.SelectedDate = emp.DateEmbauche;
            UctDateNaissance.SelectedDate = emp.DateNaissance;
            DdlDepartement.SelectedValue = emp.DepartementID.ToString();
            TxbEmail.Text = emp.Email;
            TxbNif.Text = emp.NIF;
            TxbNom.Text = emp.Nom;
            TxbPhone1.Text = emp.Phone1;
            TxbPhone2.Text = emp.Phone2;
            TxbPrenom.Text = emp.Prenom;
            DdlPoste.SelectedValue = emp.PosteID.ToString();
            if (emp.Sexe == LookupConst.Feminin) { RbtSexeFeminin.Checked = true; }
            else
            {
                if (emp.Sexe == LookupConst.Masculin) { RbtSexeMasculin.Checked = true; }
            }
            DdlStatutMatrimonial.SelectedValue = emp.StatutMatrimonialID.ToString();
            DdlTypeEmploye.SelectedValue = emp.TypeEmployeID.ToString();
            TxbUrgenceNom.Text = emp.UrgenceNom;
            TxbUrgencePhone.Text = emp.UrgencePhone;
        }

        private void SetSalaireRegulier(EmployeSalaireDto empSal)
        {
            DdlBanqueSalaireRegulier.SelectedValue = empSal.BanqueID.ToString();
            if (empSal.BonusFinAnnee) { ChkBonusFinAnneeRegulier.Checked = true; } else { ChkBonusFinAnneeRegulier.Checked = false; }
            TxbCompteBancaireSalaireRegulier.Text = empSal.CompteBancaire;
            if (empSal.HeureSupPaye) { ChkHeureSupRegulier.Checked = true; } else { ChkHeureSupRegulier.Checked = false; }
            DdlMonnaieSalaireRegulier.SelectedValue = empSal.MonnaieID.ToString();
            DdlTypePaiementSalaireRegulier.SelectedValue = empSal.TypePaiementID.ToString();
            TxbSalaireRegulier.Text = empSal.Salaire.ToString();
        }

        private void SetSalaireSpecial(EmployeSalaireDto empSal)
        {
            DdlBanqueSalaireSpecial.SelectedValue = empSal.BanqueID.ToString();
            if (empSal.BonusFinAnnee) { ChkBonusFinAnneeSpecial.Checked = true; } else { ChkBonusFinAnneeSpecial.Checked = false; }
            TxbCompteBancaireSalaireSpecial.Text = empSal.CompteBancaire;
            if (empSal.HeureSupPaye) { ChkHeureSupSpecial.Checked = true; } else { ChkHeureSupSpecial.Checked = false; }
            DdlMonnaieSalaireSpecial.SelectedValue = empSal.MonnaieID.ToString();
            DdlTypePaiementSalaireSpecial.SelectedValue = empSal.TypePaiementID.ToString();
            TxbSalaireSpecial.Text = empSal.Salaire.ToString();
        }

        private void SetConges(IEnumerable<CongeDto> empConges)
        {
            // Pour chaque congé enregistré pour l'employé, trouver le usercontrol correspondant et renseigner les champs
            foreach (CongeDto conge in empConges)
            {
                // Comparer le ID de chaque usercontrol pour retrouver celui correspondant au congé
                foreach (Control ctl in PlaceHolderConge.Controls)
                {
                    //Traiter seulement les usercontols de type Prelevement
                    if (ctl is CongeUserControl)
                    {
                        if (String.Compare(ctl.ID.Split('_')[0], conge.TypeCongeID.ToString()) == 0)
                        {
                            CongeUserControl cuc = (CongeUserControl)ctl;
                            cuc.CongeChecked = true;
                            cuc.AccumulableParAnneeChecked = conge.AccumulableParAnnee;
                            cuc.IncrementableParMoisChecked = conge.IncrementableParMois;
                            cuc.JourParAnnee = conge.JourParAnnee.ToString();
                            cuc.BalanceJour = conge.JourParAnnee.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void SetPrelevementsReguliers(IEnumerable<EmployePrelevementDto> empPrelevements)
        {
            // Sélection les prélèvements proprement dits (sans les type de période et les pourcentages)
            var prelevements = (from EmployePrelevementDto c in empPrelevements
                         select new { c.PrelevementID }).Distinct();

            // Vérifier la conformité de chaque prélèvement en fonction des pourcentages par période choisis
            foreach (var c in prelevements)
            {
                //Types de période et pourcentages pour prélevement en cours
                var query = (from p in empPrelevements
                             where p.PrelevementID == c.PrelevementID
                             select new { p.PrelevementID, p.TypePeriodeID, p.Pourcentage }).ToList();

                //Il ne peut y avoir, au plus, que 2 enregistrements car un prélèvement ne peut 
                // concerner que 2 périodes maximum
                
                // Comparer le ID de chaque usercontrol pour retrouver celui correspondant au prélèvement
                foreach (Control ctl in PlaceHolderPrelevementRegulier.Controls)
                {
                    //Traiter seulement les usercontols de type Prelevement
                    if (ctl is Prelevement)
                    {
                        // Si la 1ère partie de l'ID du control correspond, renseigner les champs correspondant
                        if (String.Compare(ctl.ID.Split('_')[0], query[0].PrelevementID.ToString()) == 0)
                        {
                            Prelevement cuc = (Prelevement)ctl;
                            cuc.PrelevementChecked = true;
                            cuc.TypePeriodeID1 = query[0].TypePeriodeID;
                            cuc.Pourcentage1 = query[0].Pourcentage;
                            // si le prélèvement concerne 2 période, renseigner les champs correspondant
                            if (query.Count() > 1)
                            {
                                cuc.PrelevementChecked = true;
                                cuc.TypePeriodeID2 = query[1].TypePeriodeID;
                                cuc.Pourcentage2 = query[1].Pourcentage;
                            }
                            break; // arrêter la recherche (usercontrol déjà trouvé)
                        }
                    }
                }
            }
        }

        private void SetPrelevementsSpecials(IEnumerable<EmployePrelevementDto> empPrelevements)
        {
            // Sélection des prélèvements proprement dits (sans les type de période et les pourcentages)
            var prelevements = (from EmployePrelevementDto c in empPrelevements
                                select new { c.PrelevementID }).Distinct();

            // Vérifier la conformité de chaque prélèvement en fonction des pourcentages par période choisis
            foreach (var c in prelevements)
            {
                //Types de période et pourcentages pour prélevement en cours
                var query = (from p in empPrelevements
                             where p.PrelevementID == c.PrelevementID
                             select new { p.PrelevementID, p.TypePeriodeID, p.Pourcentage }).ToList();

                //Il ne peut y avoir, au plus, que 2 enregistrements car un prélèvement ne peut 
                // concerner que 2 périodes maximum

                // Comparer le ID de chaque usercontrol pour retrouver celui correspondant au prélèvement
                foreach (Control ctl in PlaceHolderPrelevementSpecial.Controls)
                {
                    //Traiter seulement les usercontols de type Prelevement
                    if (ctl is Prelevement)
                    {
                        // Si la 1ère partie de l'ID du control correspond, renseigner les champs correspondant
                        if (String.Compare(ctl.ID.Split('_')[0], query[0].PrelevementID.ToString()) == 0)
                        {
                            Prelevement cuc = (Prelevement)ctl;
                            cuc.PrelevementChecked = true;
                            cuc.TypePeriodeID1 = query[0].TypePeriodeID;
                            cuc.Pourcentage1 = query[0].Pourcentage;
                            // si le prélèvement concerne 2 période, renseigner les champs correspondant
                            if (query.Count() > 1)
                            {
                                cuc.PrelevementChecked = true;
                                cuc.TypePeriodeID2 = query[1].TypePeriodeID;
                                cuc.Pourcentage2 = query[1].Pourcentage;
                            }
                            break; // arrêter la recherche (usercontrol déjà trouvé)
                        }
                    }
                }
            }
        }
                    
    }
}