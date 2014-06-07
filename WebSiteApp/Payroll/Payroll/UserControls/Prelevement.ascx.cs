using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Bll;
using Payroll.Dal.Core;

namespace Payroll.UserControls
{
    public partial class Prelevement : System.Web.UI.UserControl
    {
        private int _compagnieID;
        private int _typeSalaireID;
        private int _prelevementID;
        private int _typePeriodeID1;
        private int _typePeriodeID2;
        private decimal _pourcentage1;
        private decimal _pourcentage2;

        public bool PrelevementChecked
        {
            get { return ChkPrelevement.Checked; }
            set { ChkPrelevement.Checked = value; }
        }

        public string CheckBoxText
        {
            get { return ChkPrelevement.Text;}
            set { ChkPrelevement.Text = value;}
        }

        public int CompagnieID 
        {
            get
            {
                return _compagnieID;
            }
            set
            {
                _compagnieID = value;
            }
        }

        public int TypeSalaireID
        {
            get
            {
                return _typeSalaireID;
            }
            set
            {
                _typeSalaireID = value;
            }
        }

        
        public int PrelevementID
        {
            get
            {
                return _prelevementID;
            }
            set
            {
                _prelevementID = value;

            }
        }

        public int TypePeriodeID1
        {
            get
            {
                return _typePeriodeID1;
            }
            set
            {
                _typePeriodeID1 = value;

            }
        }

        public int TypePeriodeID2
        {
            get
            {
                return _typePeriodeID2;
            }
            set
            {
                _typePeriodeID2 = value;

            }
        }

        public decimal Pourcentage1
        {
            get
            {
                return _pourcentage1;
            }
            set
            {
                _pourcentage1 = value;

            }
        }

        public decimal Pourcentage2
        {
            get
            {
                return _pourcentage2;
            }
            set
            {
                _pourcentage2 = value;

            }
        }

        public IEnumerable<TypePeriodeDto> TypePeriodes { get;  set; }

        public int SelectedPeriode1
        {
            get
            {
                return Int32.Parse(DdlTypePeriode1.SelectedValue);
            }
            set
            {
                DdlTypePeriode1.SelectedValue = value.ToString();
            }
        }

        public int SelectedPeriode2
        {
            get
            {
                return Int32.Parse(DdlTypePeriode2.SelectedValue);
            }
            set
            {
                DdlTypePeriode2.SelectedValue = value.ToString();
            }
        }

        public decimal SelectedPourcentage1
        {
            get
            {
                return Decimal.Parse(DdlPourcentage1.SelectedValue);
            }
            set
            {
                DdlPourcentage1.SelectedValue = value.ToString();
            }
        }

        public decimal SelectedPourcentage2
        {
            get
            {
                return Decimal.Parse(DdlPourcentage2.SelectedValue);
            }
            set
            {
                DdlPourcentage2.SelectedValue = value.ToString();
            }
        }

            

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDdlTypePeriode1();
                DdlTypePeriode1.SelectedValue = _typePeriodeID1.ToString(); //Pré-sélection  du Type de Periode dans la liste
                FillDdlPourcentage1();
                DdlPourcentage1.SelectedValue = _pourcentage1.ToString(); //Pré-sélection  du Pourcentage dans la liste
                
                if (TypePeriodes.Count() > 1)
                {
                    FillDdlTypePeriode2();
                    DdlTypePeriode2.SelectedValue = _typePeriodeID2.ToString(); //Pré-sélection  du Type de Periode dans la liste
                    FillDdlPourcentage2();
                    DdlPourcentage2.SelectedValue = _pourcentage2.ToString(); //Pré-sélection  du Pourcentage dans la liste
                }
                else
                {
                    //Si il y a qu'une période de paie dans le mois (paiement mensuel), 
                    //cacher les champs de la 2ème période
                    LblPeriode2.Visible = false;
                    LblPourcentage2.Visible = false;
                    DdlTypePeriode2.Visible = false;
                    DdlPourcentage2.Visible= false;
                }
            }
        }

        private void FillDdlTypePeriode1()
        {
            /*   
            IEnumerable<TypePeriodeDto> tpData;
            CompagnieManager cm = new CompagnieManager();
            tpData = cm.GetTypePeriodeForCompany(_compagnieID);
             */

            DdlTypePeriode1.DataSource = TypePeriodes;
            DdlTypePeriode1.DataTextField = "Nom";
            DdlTypePeriode1.DataValueField = "TypePeriodeID";
            DdlTypePeriode1.DataBind();
        }

        private void FillDdlTypePeriode2()
        {
            DdlTypePeriode2.DataSource = TypePeriodes;
            DdlTypePeriode2.DataTextField = "Nom";
            DdlTypePeriode2.DataValueField = "TypePeriodeID";
            DdlTypePeriode2.DataBind();
        }

        private void FillDdlPourcentage1()
        {
            DdlPourcentage1.Items.Add(new ListItem("0%", "0.00"));
            DdlPourcentage1.Items.Add(new ListItem("5%", "0.05"));
            DdlPourcentage1.Items.Add(new ListItem("10%", "0.10"));
            DdlPourcentage1.Items.Add(new ListItem("15%", "0.15"));
            DdlPourcentage1.Items.Add(new ListItem("20%", "0.20"));
            DdlPourcentage1.Items.Add(new ListItem("25%", "0.25"));
            DdlPourcentage1.Items.Add(new ListItem("30%", "0.30"));
            DdlPourcentage1.Items.Add(new ListItem("35%", "0.35"));
            DdlPourcentage1.Items.Add(new ListItem("40%", "0.40"));
            DdlPourcentage1.Items.Add(new ListItem("45%", "0.45"));
            DdlPourcentage1.Items.Add(new ListItem("50%", "0.50"));
            DdlPourcentage1.Items.Add(new ListItem("55%", "0.55"));
            DdlPourcentage1.Items.Add(new ListItem("60%", "0.60"));
            DdlPourcentage1.Items.Add(new ListItem("65%", "0.65"));
            DdlPourcentage1.Items.Add(new ListItem("70%", "0.70"));
            DdlPourcentage1.Items.Add(new ListItem("75%", "0.75"));
            DdlPourcentage1.Items.Add(new ListItem("80%", "0.80"));
            DdlPourcentage1.Items.Add(new ListItem("85%", "0.85"));
            DdlPourcentage1.Items.Add(new ListItem("90%", "0.90"));
            DdlPourcentage1.Items.Add(new ListItem("95%", "0.95"));
            DdlPourcentage1.Items.Add(new ListItem("100%", "1.00"));
        }

        private void FillDdlPourcentage2()
        {
            DdlPourcentage2.Items.Add(new ListItem("0%", "0.00"));
            DdlPourcentage2.Items.Add(new ListItem("5%", "0.05"));
            DdlPourcentage2.Items.Add(new ListItem("10%", "0.10"));
            DdlPourcentage2.Items.Add(new ListItem("15%", "0.15"));
            DdlPourcentage2.Items.Add(new ListItem("20%", "0.20"));
            DdlPourcentage2.Items.Add(new ListItem("25%", "0.25"));
            DdlPourcentage2.Items.Add(new ListItem("30%", "0.30"));
            DdlPourcentage2.Items.Add(new ListItem("35%", "0.35"));
            DdlPourcentage2.Items.Add(new ListItem("40%", "0.40"));
            DdlPourcentage2.Items.Add(new ListItem("45%", "0.45"));
            DdlPourcentage2.Items.Add(new ListItem("50%", "0.50"));
            DdlPourcentage2.Items.Add(new ListItem("55%", "0.55"));
            DdlPourcentage2.Items.Add(new ListItem("60%", "0.60"));
            DdlPourcentage2.Items.Add(new ListItem("65%", "0.65"));
            DdlPourcentage2.Items.Add(new ListItem("70%", "0.70"));
            DdlPourcentage2.Items.Add(new ListItem("75%", "0.75"));
            DdlPourcentage2.Items.Add(new ListItem("80%", "0.80"));
            DdlPourcentage2.Items.Add(new ListItem("85%", "0.85"));
            DdlPourcentage2.Items.Add(new ListItem("90%", "0.90"));
            DdlPourcentage2.Items.Add(new ListItem("95%", "0.95"));
            DdlPourcentage2.Items.Add(new ListItem("100%", "1.00"));
        }
    }
}