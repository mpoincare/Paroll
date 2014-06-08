<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login_z.aspx.cs" Inherits="Payroll.Account.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <section id="loginForm">
        <div class="span-18">
            <asp:Login runat="server" ID="Login1"  RenderOuterTable="true"
                BorderStyle="Solid" 
                BackColor="#F7F7DE" 
                BorderWidth="1px"
                BorderColor="#CCCC99" 
                EnableViewState="true"
                Font-Size="10pt" 
                Font-Names="Verdana" 
                FailureAction="Refresh"
                FailureText="Votre E-mail et votre mot de passe n'ont pas pu être validés."
                LoginButtonText="Se Connecter"
                LoginButtonType="Link"
                PasswordLabelText="Mot de passe"
                PasswordRequiredErrorMessage="Le mot de passe est obligatoire"
                RememberMeSet="False"
                RememberMeText="Me valider automatiquement la prochaine fois"
                TextLayout="TextOnTop"
                TitleText="Formulaire de connection"
                UserNameLabelText="Adresse E-mail:" 
                UserNameRequiredErrorMessage="L'adresse email est obligatoire"
                OnLoggingIn="OnLoggingIn"
                OnLoginError="OnLoginError" 
                OnLoggedIn="OnLoggedIn"
            >
                <TitleTextStyle Font-Bold="True" 
                    ForeColor="#FFFFFF" 
                    BackColor="#6B696B">
                </TitleTextStyle>
                <LoginButtonStyle CssClass="command-button" />
                
            </asp:Login>
        </div>
    </section>

</asp:Content>
