<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpSearch.aspx.cs" Inherits="Payroll.EmpSearch" %>
<%@ Register TagPrefix="uc" TagName="Message" Src="~/UserControls/MessageUserControl.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc:Message  ID="UctMessage" runat="server" />

    <div class="span-12">
        <fieldset>
            <legend>Recherche par numéro interne</legend>
            <ol>
                <li>
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="TxbNumeroInterne">Numéro interne</asp:Label>
                    <asp:TextBox runat="server" ID="TxbNumeroInterne" MaxLength="20" />
                </li>
                <li>
                     <asp:LinkButton runat="server" ID="FindAndContinueButton" CssClass="command-button" OnClick="FindAndContinueButton_Click">Trouver et Continuer</asp:LinkButton>
                </li>
            </ol>
        </fieldset>
    </div>

    <div class="span-12-last">
        <fieldset>
            <legend>Recherche par tout ou partie des champs suivants:</legend>
            <ol>
                <li>
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="TxbPhone1">Téléphone principal</asp:Label>
                    <asp:TextBox runat="server" ID="TxbPhone1" MaxLength="16" />
                </li>
                <li>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="TxbNom">Nom</asp:Label>
                    <asp:TextBox runat="server" ID="TxbNom" MaxLength="30" />
                </li>
                <li>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="TxbPrenom">Prénom</asp:Label>
                    <asp:TextBox runat="server" ID="TxbPrenom" MaxLength="30" />
                </li>
                <li>
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="TxbCin">CIN</asp:Label>
                    <asp:TextBox runat="server" ID="TxbCin" MaxLength="20" />
                </li>
                <li>
                     <asp:LinkButton runat="server" ID="SearchButton" CssClass="command-button" OnClick="SearchButton_Click">Chercher</asp:LinkButton>
                </li>
            </ol>
        </fieldset>
    </div>

    <div class="span-24">
        <asp:GridView ID="GridViewEmployes" runat="server"
            AutoGenerateColumns="false"
            BorderWidth="1px" BackColor="White" GridLines="Vertical"
            CellPadding ="4" BorderStyle="None"
            BorderColor="#DEDFDE" ForeColor="Black">
            <FooterStyle BackColor="#CCCC99" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Right"
                 BackColor="#6B696B" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:HyperLinkField Text="Select..."
                     HeaderText="Employé"
                     DataTextField="NomPrenom"
                     DataNavigateUrlFields="Url" />
                <asp:BoundField HeaderText="Numéro" DataField="NumeroEmploye" />
                <asp:BoundField HeaderText="CIN" DataField="CIN" />
                <asp:BoundField HeaderText="Téléphone" DataField="Phone1" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
