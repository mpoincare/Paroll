<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Payroll.WebUI.Personnel.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span-24">
        <h3>Personnel</h3>
        <hr class="gray-bar" />
    </div>
    <div class="span-23">
        <div runat="server" id="Column1" class="span-7-lmargin"></div>
        <div runat="server" id="Column2" class="span-7"></div>
        <div runat="server" id="Column3" class="span-7"></div>
    </div>
</asp:Content>
