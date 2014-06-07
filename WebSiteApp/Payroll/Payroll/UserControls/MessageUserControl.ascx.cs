using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Payroll.Helpers;

namespace Payroll.UserControls
{
    public enum MessageType
    {
        Info = 1,
        Error = 2,
        Notice = 3,
        Success = 4,
    }

    public partial class MessageUserControl : System.Web.UI.UserControl
    {
        public void Show(MessageType messageType, String message)
        {
                        
            // Dépendamment du type de message à afficher, cacher ou rendre visible les panels 
            switch (messageType)
            {
                case MessageType.Info:
                    InfoPanel.Visible = true;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = false;
                    InfoLabel.Text = message;
                    break;
                case MessageType.Error:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = true;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = false;
                    ErrorLabel.Text = message;
                    break;
                case MessageType.Notice:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = true;
                    SuccessPanel.Visible = false;
                    NoticeLabel.Text = message;
                    break;
                case MessageType.Success:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = true;
                    SuccessLabel.Text = message;
                    break;
            }

        }

        public void Show(MessageType messageType, String messageTitle, List<String> messages)
        {

            // Dépendamment du type de message à afficher, cacher ou rendre visible les panels 
            // et montrer le titre et les messages
            switch (messageType)
            {
                case MessageType.Info:
                    InfoPanel.Visible = true;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = false;
                    InfoLabel.Text = messageTitle;
                    InfoBulletedList.DataSource = messages;
                    InfoBulletedList.DataBind();
                    break;
                case MessageType.Error:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = true;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = false;
                    ErrorLabel.Text = messageTitle;
                    ErrorBulletedList.DataSource = messages;
                    ErrorBulletedList.DataBind();
                    break;
                case MessageType.Notice:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = true;
                    SuccessPanel.Visible = false;
                    NoticeLabel.Text = messageTitle;
                    NoticeBulletedList.DataSource = messages;
                    NoticeBulletedList.DataBind();
                    break;
                case MessageType.Success:
                    InfoPanel.Visible = false;
                    ErrorPanel.Visible = false;
                    NoticePanel.Visible = false;
                    SuccessPanel.Visible = true;
                    SuccessLabel.Text = messageTitle;
                    SuccessBulletedList.DataSource = messages;
                    SuccessBulletedList.DataBind();
                    break;
            }

        }

        public void Hide()
        {
            //Cacher tous les Panels s'il n'y a aucun message à afficher
            InfoPanel.Visible = false;
            ErrorPanel.Visible = false;
            NoticePanel.Visible = false;
            SuccessPanel.Visible = false;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}