using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Payroll.UserControls
{
    public partial class CalendarCombo : System.Web.UI.UserControl
    {
        private int _startYear = DateTime.Now.Year - 80;
        private int _endYear = DateTime.Now.Year + 5;
        private DateTime _initialDate  = DateTime.Now;
        private bool _showBlankEntry = false;
        

        public int StartYear
        {
            get
            {
                return _startYear;
            }
            set
            {
                if (value > this.EndYear)
                {
                    throw new Exception("L'année du début doit être inférieure à l'année de fin.");
                }
                else
                {
                    _startYear = value;
                }
            }
        }

        public int EndYear
        {
            get
            {
                return _endYear;
            }
            set
            {
                if (value < this.StartYear)
                {
                    throw new
                        Exception("L'année de fin doit être supérieure à l'année de début.");
                }
                else
                {
                    _endYear = value;
                }
            }
        }

        
        public String StartDate
        {
            set
            {
                // Set a preselected date in the combo
                DateTime dateValue;
                if (!(DateTime.TryParse(value, out dateValue)))
                {
                    throw new
                        Exception("Date à préselectionner incorrecte");
                }
                else
                {
                    _initialDate = dateValue;
                }
            }
        }

        public bool ShowBlankEntry
        {
            get;
            set;
        }

        public bool IsSelectedDateCorrect()
        {
            //Check if date selected is a correct one
            DateTime dateValue;
                        
            return (DateTime.TryParse(Annee.SelectedValue + "-" + Mois.SelectedValue + "-" + Jour.SelectedValue, out dateValue));
        }

        public DateTime SelectedDate
        {
            get
            {
                DateTime dateValue;
                bool isDate;
                isDate = DateTime.TryParse(Annee.SelectedValue + "-" + Mois.SelectedValue + "-" + Jour.SelectedValue, out dateValue);
                return dateValue;
            }
            set
            {
                Annee.SelectedValue = value.Year.ToString();
                Mois.SelectedValue = value.Month.ToString();
                Jour.SelectedValue = value.Day.ToString();
            }

        }

        public string SelectedDateString
        {
            get
            {
                return Annee.SelectedValue + "-" + Mois.SelectedValue + "-" + Jour.SelectedValue;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        public void SetDate(DateTime date)
        {
            Annee.SelectedValue = date.Year.ToString();
            Mois.SelectedValue = date.Month.ToString();
            Jour.SelectedValue = date.Day.ToString();
        }

        public void FillList()
        {
            FillAnnee();
            FillMois(); 
            FillJour();
        }

        private void FillAnnee()
        {
            if (_showBlankEntry)
            {
                Annee.Items.Add(new ListItem("aaaa", "aaaa"));
            }
            for (int i = _startYear; i <= _endYear; i++)
            {
                Annee.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            if (_showBlankEntry)
            {
                Annee.SelectedIndex = 0;
            }
            else
            {
                Annee.SelectedValue = _initialDate.Year.ToString();
            }
        }

        private void FillMois()
        {
            if (_showBlankEntry)
            {
                Mois.Items.Add(new ListItem("mmm", "mmm"));
            }
            Mois.Items.Add(new ListItem("Janvier", "1"));
            Mois.Items.Add(new ListItem("Février", "2"));
            Mois.Items.Add(new ListItem("Mars", "3"));
            Mois.Items.Add(new ListItem("Avril", "4"));
            Mois.Items.Add(new ListItem("Mai", "5"));
            Mois.Items.Add(new ListItem("Juin", "6"));
            Mois.Items.Add(new ListItem("Juillet", "7"));
            Mois.Items.Add(new ListItem("Août", "8"));
            Mois.Items.Add(new ListItem("Septembre", "9"));
            Mois.Items.Add(new ListItem("Octobre", "10"));
            Mois.Items.Add(new ListItem("Novembre", "11"));
            Mois.Items.Add(new ListItem("Décembre", "12"));
            if (_showBlankEntry)
            {
                Mois.SelectedIndex = 0;
            }
            else
            {
                Mois.SelectedValue = _initialDate.Month.ToString();
            }
        }

        private void FillJour()
        {
            if (_showBlankEntry)
            {
                Jour.Items.Add(new ListItem("jj", "jj"));
            }
            for (int i = 1; i <= 31; i++)
            {
                Jour.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            if (_showBlankEntry)
            {
                Jour.SelectedIndex = 0;
            }
            else
            {
                Jour.SelectedValue = _initialDate.Day.ToString();
            }
        }


    }
}