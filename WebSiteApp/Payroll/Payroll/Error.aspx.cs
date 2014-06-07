using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Payroll
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Afficher un message d'erreur qui dépend de la valeur de la variable ErrorType
            if (!IsPostBack)
            {
                var errorType = HttpUtility.UrlEncode(Request.QueryString["ErrorType"]);
                if (!String.IsNullOrEmpty(errorType))
                {
                    string errorMessage = String.Empty;
                    int intErrorType;
                    if (Int32.TryParse(errorType, out intErrorType))
                    {
                        switch (intErrorType)
                        {
                            case 1:
                                errorMessage = @"Les données attendues n'ont pas été reconnues";
                                break;

                        }
                     }
                    UctMessage.Show(UserControls.MessageType.Error, errorMessage);
                }
            }
        }
    }
}