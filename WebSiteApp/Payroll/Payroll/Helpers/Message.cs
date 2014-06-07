using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Payroll.Helpers
{
    public enum MessageType
    {
        Info = 1,
        Error = 2,
        Notice = 3,
        Success = 4,
    }

    
    public static class Message
    {
        public static void Show(MessageType messageType, String message, Page page)
        {
            // Get Panel controls
            var infoPanel = UIControl.FindControlRecursive(page, "InfoPanel") as Panel;
            var errorPanel = UIControl.FindControlRecursive(page, "ErrorPanel") as Panel;
            var noticePanel = UIControl.FindControlRecursive(page, "NoticePanel") as Panel;
            var successPanel = UIControl.FindControlRecursive(page, "SuccessPanel") as Panel;

            // Depending on message type, hide/show panels and message
            switch (messageType)
            {
                case MessageType.Info:
                    infoPanel.Visible = true;
                    errorPanel.Visible = false;
                    noticePanel.Visible = false;
                    successPanel.Visible = false;
                    var infoLabel = UIControl.FindControlRecursive(page, "InfoLabel") as Label;
                    infoLabel.Text = message;
                    break;
                case MessageType.Error:
                    infoPanel.Visible = false;
                    errorPanel.Visible = true;
                    noticePanel.Visible = false;
                    successPanel.Visible = false;
                    var errorLabel = UIControl.FindControlRecursive(page, "ErrorLabel") as Label;
                    errorLabel.Text = message;
                    break;
                case MessageType.Notice:
                    infoPanel.Visible = false;
                    errorPanel.Visible = false;
                    noticePanel.Visible = true;
                    successPanel.Visible = false;
                    var noticeLabel = UIControl.FindControlRecursive(page, "NoticeLabel") as Label;
                    noticeLabel.Text = message;
                    break;
                case MessageType.Success:
                    infoPanel.Visible = false;
                    errorPanel.Visible = false;
                    noticePanel.Visible = false;
                    successPanel.Visible = true;
                    var successLabel = UIControl.FindControlRecursive(page, "SuccessLabel") as Label;
                    successLabel.Text = message;
                    break;
            }

        }

        public static void Hide(Page page)
        {
            // Get Panel controls
            var infoPanel = UIControl.FindControlRecursive(page, "InfoPanel") as Panel;
            var errorPanel = UIControl.FindControlRecursive(page, "ErrorPanel") as Panel;
            var noticePanel = UIControl.FindControlRecursive(page, "NoticePanel") as Panel;
            var successPanel = UIControl.FindControlRecursive(page, "SuccessPanel") as Panel;

            infoPanel.Visible = false;
            errorPanel.Visible = false;
            noticePanel.Visible = false;
            successPanel.Visible = false;
        }
    }
}