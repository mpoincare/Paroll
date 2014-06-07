using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Payroll.UserControls
{
    public partial class CongeUserControl : System.Web.UI.UserControl
    {
        private int _compagnieID;
        private int _typeCongeID;

        public int CompagnieID
        {
            get { return _compagnieID; }
            set { _compagnieID = value; }
        }

        public int TypeCongeID
        {
            get { return _typeCongeID; }
            set { _typeCongeID = value; }
        }
        
        public bool CongeChecked
        {
            get { return ChkConge.Checked; }
            set { ChkConge.Checked = value; }
        }

        public bool AccumulableParAnneeChecked
        {
            get { return ChkAccumulableParAnnee.Checked; }
            set { ChkAccumulableParAnnee.Checked = value; }
        }

        public bool IncrementableParMoisChecked
        {
            get { return ChkIncrementableParMois.Checked; }
            set { ChkIncrementableParMois.Checked = value; }
        }

        public string TypeConge
        {
            get { return ChkConge.Text; }
            set { ChkConge.Text = value;}
        }

        public string JourParAnnee
        {
            get { return TxbJourParAnnee.Text; }
            set { TxbJourParAnnee.Text = value; }
        }

        public string BalanceJour
        {
            get { return TxbBalanceJour.Text; }
            set { TxbBalanceJour.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}