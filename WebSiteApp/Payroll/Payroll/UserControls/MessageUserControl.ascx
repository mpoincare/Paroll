<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageUserControl.ascx.cs" ClassName="MessageUserControl" Inherits="Payroll.UserControls.MessageUserControl" %>
<div class="span-24">
    <asp:Panel runat="server" ID="InfoPanel" Visible="false">
        <div class="info">
            <asp:Label runat="server" ID="InfoLabel"></asp:Label>
            <asp:BulletedList ID="InfoBulletedList" BulletStyle="Disc" DisplayMode="Text" runat="server"></asp:BulletedList>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="ErrorPanel" Visible="false">
        <div class="error">
            <asp:Label runat="server" ID="ErrorLabel"></asp:Label>
            <asp:BulletedList ID="ErrorBulletedList" BulletStyle="Disc" DisplayMode="Text" runat="server"></asp:BulletedList>
        </div>
        </asp:Panel>
    <asp:Panel runat="server" ID="NoticePanel" Visible="false">
        <div class="notice">
            <asp:Label runat="server" ID="NoticeLabel"></asp:Label>
            <asp:BulletedList ID="NoticeBulletedList" BulletStyle="Disc" DisplayMode="Text" runat="server"></asp:BulletedList>
        </div>
        </asp:Panel>
    <asp:Panel runat="server" ID="SuccessPanel" Visible="false">
        <div class="success">
            <asp:Label runat="server" ID="SuccessLabel"></asp:Label>
            <asp:BulletedList ID="SuccessBulletedList" BulletStyle="Disc" DisplayMode="Text" runat="server"></asp:BulletedList>
        </div>
    </asp:Panel>
</div>
