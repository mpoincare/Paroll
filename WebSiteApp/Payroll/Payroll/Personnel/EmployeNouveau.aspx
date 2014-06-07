<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeNouveau.aspx.cs" Inherits="Payroll.Personnel.EmployeNouveau" %>
<%@ Register TagPrefix="uc" TagName="CalendarCombo" Src="~/UserControls/CalendarCombo.ascx" %>
<%@ Register TagPrefix="uc" TagName="Message" Src="~/UserControls/MessageUserControl.ascx" %>
<%@ Reference Control="~/UserControls/Prelevement.ascx" %>
<%@ Reference Control="~/UserControls/CongeUserControl.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <uc:Message  ID="UctMessage" runat="server" />
    <div class="span-24">
        <fieldset>
            <legend>Type de l'employé</legend>
            <ol>
                <li>
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="DdlTypeEmploye">Type Employé *</asp:Label>
                    <asp:DropDownList ID="DdlTypeEmploye" OnSelectedIndexChanged="DdlTypeEmploye_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend> Informations personnelles</legend>
            <ol>
                <li>
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="TxbPrenom">Prénom *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbPrenom" MaxLength="30" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxbPrenom"
                        CssClass="field-validation-error" ErrorMessage="Le prénom est obligatoire." />
                </li>
                <li>
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="TxbNom">Nom *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbNom" MaxLength="30" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxbNom"
                        CssClass="field-validation-error" ErrorMessage="Le nom est obligatoire." />
                </li>
                <li>
                    <asp:Label ID="Label4" runat="server" AssociatedControlID="UctDateNaissance">Date de Naissance *</asp:Label>
                    <uc:CalendarCombo  ID="UctDateNaissance" runat="server" />
                </li>
                <li>
                    <asp:Label ID="Label5" runat="server" AssociatedControlID="RbtSexeFeminin">Sexe *</asp:Label>
                    <asp:RadioButton ID="RbtSexeFeminin" GroupName="Sexe" runat="server" Text="Masculin"/>
                    <asp:RadioButton ID="RbtSexeMasculin" GroupName="Sexe" runat="server" Text="Féminin"/>
                </li>
                <li>
                    <asp:Label ID="Label6" runat="server" AssociatedControlID="TxbCin">CIN *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbCin" MaxLength="20" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxbCin"
                        CssClass="field-validation-error" ErrorMessage="La CIN est obligatoire." />
                </li>
                <li>
                    <asp:Label ID="Label7" runat="server" AssociatedControlID="TxbNif">NIF</asp:Label>
                    <asp:TextBox runat="server" ID="TxbNif" MaxLength="12" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Contact personnel</legend>
            <ol>
                <li>
                    <asp:Label ID="Label8" runat="server" AssociatedControlID="TxbAdresseRue1">Adresse (ligne 1) *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbAdresseRue1" MaxLength="50" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxbAdresseRue1"
                        CssClass="field-validation-error" ErrorMessage="L'adresse est obligatoire." />
                </li>
                <li>
                    <asp:Label ID="Label9" runat="server" AssociatedControlID="TxbAdresseRue2">Adresse (ligne 2)</asp:Label>
                    <asp:TextBox runat="server" ID="TxbAdresseRue2" MaxLength="50" />
                </li>
                <li>
                    <asp:Label ID="Label14" runat="server" AssociatedControlID="TxbAdresseVille">Ville</asp:Label>
                    <asp:TextBox runat="server" ID="TxbAdresseVille" MaxLength="30" />
                </li>
                <li>
                    <asp:Label ID="Label10" runat="server" AssociatedControlID="TxbPhone1">Téléphone principal *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbPhone1" MaxLength="16" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxbPhone1"
                        CssClass="field-validation-error" ErrorMessage="Le téléphone principal est obligatoire." />
                </li>
                <li>
                    <asp:Label ID="Label11" runat="server" AssociatedControlID="TxbPhone2">Téléphone autre</asp:Label>
                    <asp:TextBox runat="server" ID="TxbPhone2" MaxLength="16" />
                </li>
                <li>
                    <asp:Label ID="Label12" runat="server" AssociatedControlID="TxbExtension">Extension bureau</asp:Label>
                    <asp:TextBox runat="server" ID="TxbExtension" MaxLength="16" />
                </li>
                <li>
                    <asp:Label ID="Label13" runat="server" AssociatedControlID="TxbEmail">Adresse email</asp:Label>
                    <asp:TextBox runat="server" ID="TxbEmail" TextMode="Email" MaxLength="30" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Contact extérieur</legend>
            <ol>
                <li>
                    <asp:Label ID="Label15" runat="server" AssociatedControlID="DdlStatutMatrimonial">Statut matrimonial *</asp:Label>
                    <asp:DropDownList ID="DdlStatutMatrimonial" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="Label16" runat="server" AssociatedControlID="TxbUrgenceNom">Personne à appeler en cas d'urgence</asp:Label>
                    <asp:TextBox runat="server" ID="TxbUrgenceNom" MaxLength="50" />
                </li>
                <li>
                    <asp:Label ID="Label17" runat="server" AssociatedControlID="TxbUrgencePhone">Téléphone urgence</asp:Label>
                    <asp:TextBox runat="server" ID="TxbUrgencePhone" MaxLength="16" />
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Embauche</legend>
            <ol>
                <li>
                    <asp:Label ID="Label18" runat="server" AssociatedControlID="UctDateEmbauche">Date d'embauche *</asp:Label>
                    <uc:CalendarCombo  ID="UctDateEmbauche" runat="server" />
                </li>
                <li>
                    <asp:Label ID="Label19" runat="server" AssociatedControlID="DdlDepartement">Département *</asp:Label>
                    <asp:DropDownList ID="DdlDepartement" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="Label20" runat="server" AssociatedControlID="DdlPoste">Poste</asp:Label>
                    <asp:DropDownList ID="DdlPoste" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:Label ID="Label21" runat="server" AssociatedControlID="TxbSuperviseur">Superviseur</asp:Label>
                    <asp:DropDownList ID="TxbSuperviseur" runat="server"></asp:DropDownList>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Congés</legend>          
            <asp:PlaceHolder ID="PlaceHolderConge" runat="server"></asp:PlaceHolder>
        </fieldset> 
        <fieldset>
            <legend>Conditions salaire régulier</legend>
            <ol>
                <li>
                    <asp:Label ID="Label22" runat="server" AssociatedControlID="DdlTypePaiementSalaireRegulier">Mode paiement salaire régulier</asp:Label>
                    <asp:DropDownList ID="DdlTypePaiementSalaireRegulier" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <fieldset>
                        <legend>Heures supplémentaires et bonus</legend>
                        <ol>
                            <li>
                                <asp:CheckBox ID="ChkHeureSupRegulier" Text="Heures supplémentaires payées" runat="server" />
                            </li>
                            <li>
                                <asp:CheckBox ID="ChkBonusFinAnneeRegulier" Text="Bonus de fin d'année (boni)" runat="server" />
                            </li>
                            <li>
                                <asp:CheckBox ID="ChkBonusTaxableRegulier" Text="Bonus de fin d'année taxable" runat="server" />
                            </li>
                        </ol>
                    </fieldset>
                </li>
                <li>
                    <fieldset>
                        <legend>Taxes/Contributions et pourcentages prélevés par période</legend>
                        <ol>
                            <asp:PlaceHolder ID="PlaceHolderPrelevementRegulier" runat="server"></asp:PlaceHolder>
                        </ol>
                    </fieldset>
                </li>
            </ol>
        </fieldset>
        <fieldset>
            <legend>Salaire régulier</legend>
            <ol>
                <li>
                    <asp:HiddenField ID="HdfTypeSalaireRegulier" runat="server" />
                    <asp:Label ID="LblSalaireRegulier" runat="server" AssociatedControlID="TxbSalaireRegulier">Salaire régulier *</asp:Label>
                    <asp:TextBox runat="server" ID="TxbSalaireRegulier" MaxLength="16" TextMode="Number" />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ControlToValidate="TxbSalaireRegulier" 
                        ErrorMessage="Vous devez taper un nombre" 
                        ValidationExpression="^\d+\.?\d*$" />
                    
                </li>
                <li>
                    <asp:Label ID="Label24" runat="server" AssociatedControlID="DdlMonnaieSalaireRegulier">Monnaie *</asp:Label>
                    <asp:DropDownList ID="DdlMonnaieSalaireRegulier" runat="server"></asp:DropDownList>
                </li>
                <asp:Panel ID="PanelBanqueSalaireRegulier" runat="server">
                    <li>
                        <asp:Label ID="Label26" runat="server" AssociatedControlID="DdlBanqueSalaireRegulier">Banque de virement du salaire régulier</asp:Label>
                        <asp:DropDownList ID="DdlBanqueSalaireRegulier" runat="server"></asp:DropDownList>
                    </li>
                    <li>
                        <asp:Label ID="Label27" runat="server" AssociatedControlID="TxbCompteBancaireSalaireRegulier">No du compte bancaire</asp:Label>
                        <asp:TextBox runat="server" ID="TxbCompteBancaireSalaireRegulier" MaxLength="16" />
                    </li>
                </asp:Panel>
            </ol>
        </fieldset>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DdlTypeEmploye" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="PanelSpecial" Visible="false" runat="server">
                    <fieldset>
                        <legend>Conditions salaire spécial</legend>
                        <ol>
                            <li>
                                <asp:Label ID="Label28" runat="server" AssociatedControlID="DdlTypePaiementSalaireSpecial">Type paiement salaire spécial *</asp:Label>
                                <asp:DropDownList ID="DdlTypePaiementSalaireSpecial" runat="server"></asp:DropDownList>
                            </li>
                            <li>
                                <fieldset>
                                    <legend>Heures supplémentaires et bonus</legend>
                                    <ol>
                                        <li>
                                            <asp:CheckBox ID="ChkHeureSupSpecial"  Text="Heures supplémentaires payées" runat="server" />
                                        </li>
                                        <li>
                                            <asp:CheckBox ID="ChkBonusFinAnneeSpecial" Text="Bonus de fin d'année (boni)" runat="server" />
                                        </li>
                                        <li>
                                            <asp:CheckBox ID="ChkBonusTaxableSpecial" Text="Bonus de fin d'année taxable" runat="server" />
                                        </li>
                                    </ol>
                                </fieldset>
                            </li>
                            <li>
                                <fieldset>
                                    <legend>Taxes/Contributions et pourcentages prélevés par période</legend>
                                    <ol>
                                        <asp:PlaceHolder ID="PlaceHolderPrelevementSpecial" runat="server"></asp:PlaceHolder>
                                    </ol>
                                </fieldset>
                            </li>
                        </ol>
                    </fieldset>
                    <fieldset>
                        <legend>Salaire spécial</legend>
                        <ol>
                            <li>
                                <asp:HiddenField ID="HdfTypeSalaireSpecial" runat="server" />
                                <asp:Label ID="LblSalaireSpecial" runat="server" AssociatedControlID="TxbSalaireSpecial">Salaire spécial *</asp:Label>
                                <asp:TextBox runat="server" ID="TxbSalaireSpecial" MaxLength="20" TextMode="Number"/>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="TxbSalaireSpecial" 
                                    ErrorMessage="Vous devez taper un nombre" 
                                    ValidationExpression="^\d+\.?\d*$" />
                            </li>
                            <li>
                                <asp:Label ID="Label23" runat="server" AssociatedControlID="DdlMonnaieSalaireSpecial">Monnaie</asp:Label>
                                <asp:DropDownList ID="DdlMonnaieSalaireSpecial" runat="server"></asp:DropDownList>
                            </li>
                            <asp:Panel ID="PanelBanqueSalaireSpecial" runat="server">
                                <li>
                                    <asp:Label ID="Label30" runat="server" AssociatedControlID="DdlBanqueSalaireSpecial">Banque de virement du salaire spécial</asp:Label>
                                    <asp:DropDownList ID="DdlBanqueSalaireSpecial" runat="server"></asp:DropDownList>
                                </li>
                                <li>
                                    <asp:Label ID="Label31" runat="server" AssociatedControlID="TxbCompteBancaireSalaireSpecial">No du compte bancaire</asp:Label>
                                    <asp:TextBox runat="server" ID="TxbCompteBancaireSalaireSpecial" MaxLength="16" />
                                </li>
                            </asp:Panel>
                        </ol>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <fieldset>
            <asp:LinkButton runat="server" ID="BtnCreateEmployee" CssClass="command-button" OnClick="CreateEmployeeButton_Click">Créer Employé</asp:LinkButton>
        </fieldset>
    </div>
</asp:Content>
