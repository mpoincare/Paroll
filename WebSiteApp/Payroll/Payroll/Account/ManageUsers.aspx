<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Payroll.Account.ManageUsers" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="span-24">
        <h3>Gestion des utilisateurs</h3>
        <hr class="gray-bar" />
    </div>
    
    <div class="span-24">
        <asp:Panel runat="server" ID="InfoPanel" Visible="false">
            <div class="info">
                <asp:Label runat="server" ID="InfoLabel"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="ErrorPanel" Visible="false">
            <div class="error">
                <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
            </div>
         </asp:Panel>
        <asp:Panel runat="server" ID="NoticePanel" Visible="false">
            <div class="notice">
                <asp:Label runat="server" ID="NoticeLabel"></asp:Label>
            </div>
         </asp:Panel>
        <asp:Panel runat="server" ID="SuccessPanel" Visible="false">
            <div class="success">
                <asp:Label runat="server" ID="SuccessLabel"></asp:Label>
            </div>
         </asp:Panel>
    </div>
    

    <div class="span-24">
        <div class="float-left">
            <asp:Panel runat="server" ID="ActionPanel" Visible="true">
                <asp:LinkButton runat="server" ID="ResetPasswordButton" OnClick="ResetPasswordButton_Click" CssClass="command-button">Réinitialiser</asp:LinkButton>
                <asp:LinkButton runat="server" ID="ActivateDeactivateButton" OnClick="ActivateDeactivateButton_Click" CssClass="command-button">Débloquer</asp:LinkButton>
                <asp:LinkButton runat="server" ID="DeleteAccountButton" OnClick="DeleteAccountButton_Click" CssClass="command-button">Supprimer</asp:LinkButton>
            </asp:Panel>
        </div>
    </div>

    <div class="span-12">
        <p>
            <asp:Label runat="server" AssociatedControlID="UsersListBox" ID="Label1">Utilisateurs</asp:Label><br />
            <asp:ListBox runat="server" ID="UsersListBox" Rows="15" 
                OnSelectedIndexChanged ="UsersListBox_SelectedIndexChanged" 
                AutoPostBack="true" ViewStateMode="Enabled" Width="400px" CssClass="text">
            </asp:ListBox>
        </p>
    </div>
    
    <div class="span-12 last">
        <asp:Panel runat="server" ID="RolesPanel" Visible="true">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" AssociatedControlID="UserRolesListBox" ID="Label3">Rôles utilisateur</asp:Label>
                    </td>
                    <td>
                        &nbsp
                    </td>
                    <td>
                        <asp:Label runat="server" AssociatedControlID="AvailableRolesListBox" ID="Label2">Rôles disponibles</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2">
                        <p>
                            <asp:ListBox runat="server" ID="UserRolesListBox" Rows="6"  SelectionMode="Multiple" Width="150px"></asp:ListBox>
                        </p>
                    </td>
                    <td>
                        <asp:LinkButton runat="server" ID="AddRolesToUserButton" OnClick="AddRolesToUserButton_Click" CssClass="command-button-small">&#60; Ajouter</asp:LinkButton>
                    </td>
                    <td rowspan="2">
                        <p>
                           <asp:ListBox runat="server" ID="AvailableRolesListBox" Rows="6" SelectionMode="Multiple" Width="150px"></asp:ListBox>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td>
                         <asp:LinkButton runat="server" ID="RemoveRolesFromUserButton" OnClick="RemoveRolesFromUserButton_Click" CssClass="command-button-small">Enlever &#62;</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

</asp:Content>
