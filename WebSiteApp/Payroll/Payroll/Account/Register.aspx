<%@ Page Title="Utilisateur" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Payroll.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="span-24">
        <h2>Création d'utilisateurs</h2>
        <hr />
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

    <div class="span-18">
        <asp:CreateUserWizard runat="server" ID="RegisterUser" ViewStateMode="Disabled" OnCreatedUser="RegisterUser_CreatedUser" ContinueDestinationPageUrl="/default.aspx">
            <LayoutTemplate>
                <asp:PlaceHolder runat="server" ID="wizardStepPlaceholder" />
                <asp:PlaceHolder runat="server" ID="navigationPlaceholder" />
            </LayoutTemplate>
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" ID="RegisterUserWizardStep">
                    <ContentTemplate>
                        <p class="message-info">
                            Les mots de passe doivent avoir un minimum de <%: Membership.MinRequiredPasswordLength %> caractères.
                        </p>

                        <p class="validation-summary-errors">
                            <asp:Literal runat="server" ID="ErrorMessage" />
                        </p>

                        <fieldset>
                            <legend>Formulaire de création d'utilisateur</legend>
                            <p>
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="Prenom">Prénom</asp:Label><br />
                                <asp:TextBox runat="server" ID="Prenom" MaxLength="30" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Prenom"
                                    CssClass="field-validation-error" ErrorMessage="Le prénom est obligatoire." />
                            </p>
                            <p>
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="Nom">Nom</asp:Label><br />
                                <asp:TextBox runat="server" ID="Nom" MaxLength="30" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Nom"
                                    CssClass="field-validation-error" ErrorMessage="Le nom est obligatoire." />
                            </p>
                            <p>
                                <asp:Label runat="server" AssociatedControlID="UserName">Adresse email</asp:Label><br />
                                <asp:TextBox runat="server" ID="UserName" TextMode="Email" MaxLength="30" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                                    CssClass="field-validation-error" ErrorMessage="L'adresse email est obligatoire." />
                            </p>
                            <p>
                                <asp:Label runat="server" AssociatedControlID="Email">Confirmez l'adresse email</asp:Label><br />
                                <asp:TextBox runat="server" ID="Email" TextMode="Email" MaxLength="30" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                    CssClass="field-validation-error" ErrorMessage="La confirmation de l'adresse email est obligatoire." />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="UserName" ControlToValidate="Email"
                                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="L'email et la confirmation ne correspondent pas." />
                            </p>
                            <p>
                                <asp:Label runat="server" AssociatedControlID="Password" >Mot de passe</asp:Label><br />
                                <asp:TextBox runat="server" ID="Password" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                                    CssClass="field-validation-error" ErrorMessage="Le mot de passe est obligatoire." />
                            </p>
                            <p>
                                <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirmez le mot de passe</asp:Label><br />
                                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La confirmation du mot de passe est obligatoire." />
                                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                                        CssClass="field-validation-error" Display="Dynamic" ErrorMessage="Le mot de passe et la confirmation ne correspondent pas." />
                            </p>
                        
                        <asp:Label ID="Label3" runat="server" AssociatedControlID="RoleCheckBoxList">Rôles</asp:Label><br />
                        <asp:CheckBoxList ID="RoleCheckBoxList" runat="server" Width="600px" RepeatLayout="Table"  ViewStateMode="Enabled"
                             Height="26px" >
                        </asp:CheckBoxList>    
                        <asp:LinkButton runat="server" ID="CreateButton" CssClass="command-button" CommandName="MoveNext">Créer</asp:LinkButton>
                    </fieldset>
                    </ContentTemplate>
                    <CustomNavigationTemplate />
                </asp:CreateUserWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
</asp:Content>