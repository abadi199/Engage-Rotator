<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RotatorSettings.ascx.cs" Inherits="Engage.Dnn.ContentRotator.RotatorSettings" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/labelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Url" Src="~/controls/URLControl.ascx" %>
<div class="TabContainer">
    <ul>
        <li><a href="#template-settings"><asp:Label runat="server" resourcekey="Templates.Header"/></a></li>
        <li><a href="#header-settings"><asp:Label runat="server" resourcekey="Header.Header" /></a></li>
        <li><a href="#content-settings"><asp:Label runat="server" resourcekey="Content.Header"/></a></li>
        <li><a href="#position-settings"><asp:Label runat="server" resourcekey="Position.Header"/></a></li>
        <li><a href="#rotation-settings"><asp:Label runat="server" resourcekey="Rotation.Header"/></a></li>
        </ul>
    <div id="template-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable">
                    <tr><th colspan="2"><asp:Label resourcekey="lblTemplatesHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblStyleTemplates" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:DropDownList ID="TemplatesDropDownList" runat="server" CssClass="NormalTextBox" AutoPostBack="true" />
                            <asp:Button ID="ApplyTemplateButton" runat="server" resourcekey="btnApplyStyleTemplate" />
                            
                            <fieldset id="TemplateDescriptionPanel" runat="server"><legend><asp:Label runat="server" resourcekey="StyleDescription" /></legend>
                                <asp:Label ID="TemplateDescriptionLabel" runat="server" />
                            </fieldset>
                            <asp:Image ID="TemplatePreviewImage" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="header-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable">
                    <tr><th colspan="2"><asp:Label resourcekey="lblHeaderHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblShowContentHeaderTitle" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Checkbox ID="ShowContentHeaderTitleCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr id="contentHeaderTitleRow" runat="server">
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentHeaderTitle" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentHeaderTitleTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/>
                            <asp:RequiredFieldValidator id="ContentHeaderTitleRequiredValidator" runat="server" ControlToValidate="ContentHeaderTitleTextBox" Display="None" EnableClientScript="false" resourcekey="rfvContentHeaderTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblShowContentHeaderLink" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Checkbox ID="ShowContentHeaderLinkCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr id="contentHeaderLinkTextRow" runat="server">
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentHeaderLinkText" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentHeaderLinkTextTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/>
                            <asp:RequiredFieldValidator id="ContentHeaderLinkTextRequiredValidator" runat="server" ControlToValidate="ContentHeaderLinkTextTextBox" Display="None" EnableClientScript="false" resourcekey="rfvContentHeaderLinkText" />
                        </td>
                    </tr>
                    <tr id="contentHeaderLinkRow" runat="server">
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentHeaderLink" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign"><%-- UrlType is T for Tab --%>
                            <dnn:Url ID="ContentHeaderLinkUrlControl" runat="server" UrlType="T" ShowTrack="false" ShowLog="false" ShowNewWindow="false" ShowUsers="false" ShowNone="false" ShowDatabase="false" ShowSecure="false" ShowUpLoad="false" ShowFiles="false" ShowTabs="true" ShowUrls="true"/>
                            <asp:CustomValidator id="ContentHeaderLinkRequiredValidator" runat="server" Display="None" resourcekey="valContentHeaderLink" EnableClientScript="false" />
                        </td>
                    </tr>
                </table>
    	    </ContentTemplate>
    	    <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
    	</asp:UpdatePanel>
    </div>
    <div id="content-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable">
                    <tr><th colspan="2"><asp:Label resourcekey="lblContentHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ContentDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ContentHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ContentHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valContentHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ContentWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ContentWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ContentWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valContentWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblContentTitleDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ContentTitleDisplayRadioButtonList" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="ThumbnailDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ThumbnailHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="ThumbnailHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ThumbnailHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valThumbnailHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblThumbnailWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="ThumbnailWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                            <asp:CompareValidator id="ThumbnailWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="ThumbnailWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valThumbnailWidth" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="position-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable">
                    <tr><th colspan="2"><asp:Label resourcekey="lblPositionHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionTitleDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="PositionTitleDisplayRadioButtonList" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailDisplay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:RadioButtonList id="PositionThumbnailDisplayRadioButtonList" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailHeight" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="PositionThumbnailHeightTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" />
                            <asp:CompareValidator id="PositionThumbnailHeightIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="PositionThumbnailHeightTextBox" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailHeight" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPositionThumbnailWidth" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="PositionThumbnailWidthTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="pixels" /><%-- TODO: Allow user to select unit --%>
                            <asp:CompareValidator id="PositionThumbnailWidthIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="PositionThumbnailWidthTextBox" Display="None" EnableClientScript="false" resourcekey="valPositionThumbnailWidth" />
                        </td>
                    </tr>
                </table> 
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <div id="rotation-settings">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table class="settingsTable">
                    <tr><th colspan="2"><asp:Label resourcekey="lblRotationHeader" CssClass="Head" runat="server" EnableViewState="false" /></th></tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorDelay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorDelayTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorDelay" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="RotatorDelayTextBox" Display="None" EnableClientScript="false" resourcekey="rfvRotatorDelay" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblPauseOnMouseOver" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:CheckBox id="PauseOnMouseOverCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblRotatorPauseDelay" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="RotatorPauseDelayTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator id="RotatorPauseDelayIntegerValidator" runat="server" Type="Integer" Operator="DataTypeCheck" ControlToValidate="RotatorPauseDelayTextBox" Display="None" EnableClientScript="false" resourcekey="valRotatorPauseDelay" />
                            <asp:RequiredFieldValidator id="RotatorPauseDelayRequiredValidtor" runat="server" ControlToValidate="RotatorPauseDelayTextBox" Display="None" EnableClientScript="false" resourcekey="rfvRotatorPauseDelay" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblUseAnimations" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Checkbox ID="UseAnimationsCheckBox" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="SubHead nowrap rightAlign"><dnn:Label ResourceKey="lblAnimationDuration" runat="server" EnableViewState="false" /></td>
                        <td class="contentColumn leftAlign">
                            <asp:Textbox ID="AnimationDurationTextBox" runat="server" CssClass="NormalTextBox" AutoCompleteType="Disabled"/><asp:Label runat="server" resourcekey="seconds" />
                            <asp:CompareValidator id="AnimationDurationIntegerValidator" runat="server" Type="Double" Operator="DataTypeCheck" ControlToValidate="AnimationDurationTextBox" Display="None" EnableClientScript="false" resourcekey="valAnimationDuration"/>
                            <asp:RequiredFieldValidator id="AnimationDurationRequiredValidator" runat="server" ControlToValidate="AnimationDurationTextBox" Display="None" EnableClientScript="false" resourcekey="rfvAnimationDuration"/>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /></Triggers>
        </asp:UpdatePanel>
    </div>
</div>

<div style="clear:both;">
    <asp:UpdatePanel runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:ValidationSummary runat="server" ShowMessageBox="true" ShowSummary="true" CssClass="NormalRed" />
            <asp:Panel ID="ManifestValidationErrorsPanel" runat="server" CssClass="NormalRed"/>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="SubmitButton" /><asp:AsyncPostBackTrigger ControlID="TemplatesDropDownList" /></Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False" RenderMode="Inline" >
        <ContentTemplate>
            <asp:Button ID="SubmitButton" runat="server" resourcekey="btnSubmit" EnableViewState="false" />&nbsp;
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="TemplatesDropDownList" /></Triggers>
    </asp:UpdatePanel>
    <asp:Button ID="CancelButton" runat="server" resourcekey="btnCancel" CausesValidation="false" EnableViewState="false" />
</div>
<script type="text/javascript">
    jQuery(function() { jQuery('.TabContainer').tabs(); });
</script>