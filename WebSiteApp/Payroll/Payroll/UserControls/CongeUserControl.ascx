<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CongeUserControl.ascx.cs" ClassName="CongeUserControl" Inherits="Payroll.UserControls.CongeUserControl" %>
<table>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            Jours par année
        </td>
        <td>
            Balance actuelle
        </td>
        <td>
            Accumulable par année
        </td>
        <td>
            Gagnable mensuellement
        </td>
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="ChkConge" Checked="false" Width="100px" runat="server" />
        </td>
        <td>
            <asp:TextBox ID="TxbJourParAnnee" runat="server" MaxLength="3" Width="50px" TextMode="Number"/>
        </td>
        <td>
            <asp:TextBox ID="TxbBalanceJour" runat="server" MaxLength="3" Width="50px" Text="0" TextMode="Number"/>
        </td>
        <td>
            <asp:CheckBox ID="ChkIncrementableParMois" Checked="false" runat="server" />
        </td>
        <td>
            <asp:CheckBox ID="ChkAccumulableParAnnee" Checked="false" runat="server" />
        </td>
    </tr>
</table>

