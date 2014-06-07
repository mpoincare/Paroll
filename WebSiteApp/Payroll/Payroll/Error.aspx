<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Payroll.Error" %>
<%@ Register TagPrefix="uc" TagName="Message" Src="~/UserControls/MessageUserControl.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1> ERREUR!!1</h1>
    <h2> Une erreur est survenue dans votre application:</h2>
    <uc:Message  ID="UctMessage" runat="server" />
</asp:Content>
