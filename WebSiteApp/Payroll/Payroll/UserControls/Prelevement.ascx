<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Prelevement.ascx.cs" ClassName="Prelevement" Inherits="Payroll.UserControls.Prelevement" %>
<table>
    <tr>
        <td>&nbsp</td>
        <td><asp:Label ID="LblPeriode1" runat="server" Text="Période"></asp:Label></td>
        <td><asp:Label ID="LblPourcentage1" runat="server" Text="Pct."></asp:Label></td>
        <td><asp:Label ID="LblPeriode2" runat="server" Text="Période"></asp:Label></td>
        <td><asp:Label ID="LblPourcentage2" runat="server" Text="Pct."></asp:Label></td>
    </tr>
    <tr>
        <td><asp:CheckBox ID="ChkPrelevement" Checked="false" runat="server" Width="200px"/></td>
        <td><asp:DropDownList ID="DdlTypePeriode1" runat="server" Width="120px"></asp:DropDownList></td>
        <td><asp:DropDownList ID="DdlPourcentage1" runat="server" Width="60px"></asp:DropDownList></td>
        <td><asp:DropDownList ID="DdlTypePeriode2" runat="server" Width="120px"></asp:DropDownList></td>
        <td><asp:DropDownList ID="DdlPourcentage2" runat="server" Width="60px"></asp:DropDownList></td>
    </tr>
</table>

