<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Payroll.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Login.css" rel="stylesheet" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left; margin-left:40px;margin-top:35px;">
            <div class="horizontal-stripe"> </div>
            <div id="login-rectangle">
                <div class="login-bg">
                    
                    <div id="control-frame">
                        <table style="width: 100%;">
                            <tr>
                                <td>&nbsp;<div id="login-label">Login</div></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:Label ID="Label1" runat="server" Text="Identifiant"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:TextBox ID="TextBox1" runat="server" Width="150px" CssClass="text-bg"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:Label ID="Label2" runat="server" Text="Mot de passe"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="150px" CssClass="text-bg"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                
                                
                            </tr>
                            <tr>
                                <td>&nbsp;<asp:Button ID="Button2" runat="server" Text="Valider" CssClass="button-bg" Width="120px" /></td>
                                
                                
                            </tr>
                        </table>

                    </div>
                </div>

            </div>
            <div class="bottom-stripe"></div>
    </div>
    </form>
</body>
</html>
