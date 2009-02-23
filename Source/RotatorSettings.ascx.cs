// <copyright file="RotatorSettings.ascx.cs" company="Engage Software">
// Engage: Rotator - http://www.engagemodules.com
// Copyright (c) 2004-2009
// by Engage Software ( http://www.engagesoftware.com )
// </copyright>
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.

namespace Engage.Dnn.ContentRotator
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Web.Hosting;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Xml;
    using System.Xml.Schema;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.UI.Utilities;
    using Globals = DotNetNuke.Common.Globals;

    /// <summary>
    /// Code-behind for the settings control for Rotator
    /// </summary>
    public partial class RotatorSettings : ModuleBase
    {
        /// <summary>
        /// The CSS class to use for disabled text boxes
        /// </summary>
        private const string DisabledTextBoxCssClass = "NormalDisabled";

        /// <summary>
        /// An array of <see cref="ListItem"/>s for the common <see cref="DisplayType"/> options
        /// </summary>
        private ListItem[] displayTypeItems;

        /// <summary>
        /// Gets the duration of the transition animation.
        /// </summary>
        /// <value>The duration of the animation (in seconds).</value>
        private decimal AnimationDuration
        {
            get
            {
                return Dnn.Utility.GetDecimalSetting(this.Settings, "AnimationDuration", 0.3m);
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the main content.
        /// </summary>
        /// <value>The content display mode.</value>
        private DisplayType ContentDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentDisplayMode", DisplayType.Content);
            }
        }

        /// <summary>
        /// Gets the content header link.
        /// </summary>
        /// <value>The content header link.</value>
        private string ContentHeaderLink
        {
            get
            {
                return this.Settings["ContentHeaderLink"] as string;
            }
        }

        /// <summary>
        /// Gets the content header link text.
        /// </summary>
        /// <value>The content header link text.</value>
        private string ContentHeaderLinkText
        {
            get
            {
                return this.Settings["ContentHeaderLinkText"] as string;
            }
        }

        /// <summary>
        /// Gets the content header text.
        /// </summary>
        /// <value>The content header text.</value>
        private string ContentHeaderText
        {
            get
            {
                return this.Settings["ContentHeaderText"] as string;
            }
        }

        /// <summary>
        /// Gets the setting for the height of the main content.
        /// </summary>
        /// <value>The height of the main content (in <c>px</c>).</value>
        private int? ContentHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the content title.
        /// </summary>
        /// <value>The content title display mode.</value>
        private DisplayType ContentTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ContentTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the width of the main content.
        /// </summary>
        /// <value>The width of the main content (in <c>px</c>).</value>
        private int? ContentWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ContentWidth");
            }
        }

        /// <summary>
        /// Gets a value indicating whether to pause rotation when the content is moused over.
        /// </summary>
        /// <value><c>true</c> if the module is set to pause rotation when the content is moused over; otherwise, <c>false</c>.</value>
        private bool PauseOnMouseOver
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "AnimationPauseOnMouseOver", true);
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the position thumbnail.
        /// </summary>
        /// <value>The position thumbnail display mode.</value>
        private DisplayType PositionThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "PositionThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the position thumbnail.
        /// </summary>
        /// <value>The height of the position thumbnail (in <c>px</c>).</value>
        private int? PositionThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the position thumbnail.
        /// </summary>
        /// <value>The width of the position thumbnail (in <c>px</c>).</value>
        private int? PositionThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "PositionThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets the setting for the display mode of the position title.
        /// </summary>
        /// <value>The position title display mode.</value>
        private DisplayType PositionTitleDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ControlsTitleDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the delay between each item.
        /// </summary>
        /// <value>The rotator delay (in seconds).</value>
        private int RotatorDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorDelay", 8);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the entire rotator.
        /// </summary>
        /// <value>The height of the rotator (in <c>px</c>).</value>
        private int? RotatorHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the delay before continuing rotation after the mouse leaves the content area (is <see cref="PauseOnMouseOver"/> is set).
        /// </summary>
        /// <value>The rotator pause delay (in seconds).</value>
        private int RotatorPauseDelay
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorPauseDelay", 3);
            }
        }

        /// <summary>
        /// Gets the setting for the width of the entire rotator.
        /// </summary>
        /// <value>The width of the rotator (in <c>px</c>).</value>
        private int? RotatorWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "RotatorWidth");
            }
        }

        /// <summary>
        /// Gets a value indicating whether to show the content header.
        /// </summary>
        /// <value>
        /// <c>true</c> if the content header is set to be displayed; otherwise, <c>false</c>.
        /// </value>
        private bool ShowContentHeader
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowContentHeader", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to show the content header link.
        /// </summary>
        /// <value>
        /// <c>true</c> if the content header link is set to be displayed; otherwise, <c>false</c>.
        /// </value>
        private bool ShowContentHeaderLink
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "ShowContentHeaderLink", false);
            }
        }

        /// <summary>
        /// Gets the setting for the display mode for the thumbnail.
        /// </summary>
        /// <value>The thumbnail display mode.</value>
        private DisplayType ThumbnailDisplayMode
        {
            get
            {
                return Dnn.Utility.GetEnumSetting(this.Settings, "ThumbnailDisplayMode", DisplayType.Link);
            }
        }

        /// <summary>
        /// Gets the setting for the height of the thumbnail.
        /// </summary>
        /// <value>The height of the thumbnail (in <c>px</c>).</value>
        private int? ThumbnailHeight
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailHeight");
            }
        }

        /// <summary>
        /// Gets the setting for the width of the thumbnail.
        /// </summary>
        /// <value>The width of the thumbnail (in <c>px</c>).</value>
        private int? ThumbnailWidth
        {
            get
            {
                return Dnn.Utility.GetIntSetting(this.Settings, "ThumbnailWidth");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance of the module is set to use animations.
        /// </summary>
        /// <value><c>true</c> if this instance of the module is set to use animations; otherwise, <c>false</c>.</value>
        private bool UseAnimations
        {
            get
            {
                return Dnn.Utility.GetBoolSetting(this.Settings, "UseAnimations", true);
            }
        }

        /// <summary>
        /// Gets the setting for the selected style template.
        /// </summary>
        /// <value>The selected style template.</value>
        private string StyleTemplate
        {
            get
            {
                return this.Settings["StyleTemplate"] as string;
            }
        }

        /// <summary>
        /// Gets an array of <see cref="ListItem"/>s for the common <see cref="DisplayType"/> options
        /// </summary>
        private ListItem[] DisplayTypeItems
        {
            get
            {
                if (this.displayTypeItems == null)
                {
                    this.displayTypeItems = new ListItem[]
                                        {
                                                new ListItem(
                                                    Localization.GetString(DisplayType.None.ToString(), this.LocalResourceFile),
                                                    DisplayType.None.ToString()),
                                                new ListItem(
                                                    Localization.GetString(DisplayType.Content.ToString(), this.LocalResourceFile),
                                                    DisplayType.Content.ToString()),
                                                new ListItem(
                                                    Localization.GetString(DisplayType.Link.ToString(), this.LocalResourceFile),
                                                    DisplayType.Link.ToString())
                                        };
                }

                return this.displayTypeItems;
            }
        }

        /// <summary>
        /// Raises the <see cref="Control.Init"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += this.Page_Load;
            this.ApplyTemplateButton.Click += this.ApplyTemplateButton_Click;
            this.CancelButton.Click += this.CancelButton_Click;
            this.SubmitButton.Click += this.SubmitButton_Click;
            this.PauseOnMouseOverCheckBox.CheckedChanged += this.PauseOnMouseOverCheckBox_CheckedChanged;
            this.ShowContentHeaderLinkCheckBox.CheckedChanged += this.ShowContentHeaderLinkCheckBox_CheckedChanged;
            this.ShowContentHeaderTitleCheckBox.CheckedChanged += this.ShowContentHeaderTitleCheckBox_CheckedChanged;
            this.UseAnimationsCheckBox.CheckedChanged += this.UseAnimationsCheckBox_CheckedChanged;
            this.TemplatesDropDownList.SelectedIndexChanged += this.TemplatesDropDownList_SelectedIndexChanged;
            this.ContentDisplayRadioButtonList.SelectedIndexChanged += this.ContentDisplayRadioButtonList_SelectedIndexChanged;
            this.PositionThumbnailDisplayRadioButtonList.SelectedIndexChanged += this.PositionThumbnailDisplayRadioButtonList_SelectedIndexChanged;
            this.ThumbnailDisplayRadioButtonList.SelectedIndexChanged += this.ThumbnailDisplayRadioButtonList_SelectedIndexChanged;
            this.ContentHeaderLinkRequiredValidator.ServerValidate += this.ContentHeaderLinkRequiredValidator_ServerValidate;
        }

        /// <summary>
        /// Gets the text representation of a <see cref="Nullable{T}"/> <see cref="int"/> <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A culture-aware representation of the given <paramref name="value"/></returns>
        private static string GetValueText(int? value)
        {
            return value.HasValue
                           ? value.Value.ToString(CultureInfo.CurrentCulture)
                           : string.Empty;
        }

        /// <summary>
        /// Adds the <see cref="DisabledTextBoxCssClass"/> to the given 
        /// <see cref="textbox"/> if it is not <see cref="TextBox.Enabled"/>; otherwise
        /// removes the <see cref="DisabledTextBoxCssClass"/>.
        /// </summary>
        /// <param name="textbox">The textbox on which to set the CSS class.</param>
        private static void SetDisabledCssClass(TextBox textbox)
        {
            textbox.CssClass = !textbox.Enabled
                                       ? Engage.Utility.AddCssClass(textbox.CssClass, DisabledTextBoxCssClass)
                                       : Engage.Utility.RemoveCssClass(textbox.CssClass, DisabledTextBoxCssClass);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsPostBack)
                {
                    this.FillDisplayTypeListControl(this.PositionTitleDisplayRadioButtonList, true);
                    this.FillDisplayTypeListControl(this.PositionThumbnailDisplayRadioButtonList, true);
                    this.FillDisplayTypeListControl(this.ContentTitleDisplayRadioButtonList, false);
                    this.FillDisplayTypeListControl(this.ContentDisplayRadioButtonList, false);
                    this.FillDisplayTypeListControl(this.ThumbnailDisplayRadioButtonList, false);

                    this.FillTemplatesListControl();

                    this.RotatorWidthTextBox.Text = GetValueText(this.RotatorWidth);
                    this.RotatorHeightTextBox.Text = GetValueText(this.RotatorHeight);

                    this.PositionTitleDisplayRadioButtonList.SelectedValue = this.PositionTitleDisplayMode.ToString();
                    this.PositionThumbnailDisplayRadioButtonList.SelectedValue = this.PositionThumbnailDisplayMode.ToString();
                    this.ContentTitleDisplayRadioButtonList.SelectedValue = this.ContentTitleDisplayMode.ToString();

                    this.ContentDisplayRadioButtonList.SelectedValue = this.ContentDisplayMode.ToString();
                    this.ContentWidthTextBox.Text = GetValueText(this.ContentWidth);
                    this.ContentHeightTextBox.Text = GetValueText(this.ContentHeight);
                    this.ProcessContentVisibility();

                    this.ThumbnailDisplayRadioButtonList.SelectedValue = this.ThumbnailDisplayMode.ToString();
                    this.ThumbnailWidthTextBox.Text = GetValueText(this.ThumbnailWidth);
                    this.ThumbnailHeightTextBox.Text = GetValueText(this.ThumbnailHeight);
                    this.ProcessThumbnailVisibility();

                    this.PositionThumbnailWidthTextBox.Text = GetValueText(this.PositionThumbnailWidth);
                    this.PositionThumbnailHeightTextBox.Text = GetValueText(this.PositionThumbnailHeight);
                    this.ProcessPositionThumbnailVisibility();

                    this.ShowContentHeaderTitleCheckBox.Checked = this.ShowContentHeader;
                    this.ContentHeaderTitleTextBox.Text = this.ContentHeaderText;
                    this.ProcessContentHeaderVisiblity();

                    this.ShowContentHeaderLinkCheckBox.Checked = this.ShowContentHeaderLink;
                    this.ContentHeaderLinkTextTextBox.Text = this.ContentHeaderLinkText;
                    this.ContentHeaderLinkUrlControl.Url = this.ContentHeaderLink;

                    // Show tabs if there is no url, show as a url if there is anything.  BD
                    this.ContentHeaderLinkUrlControl.UrlType = string.IsNullOrEmpty(this.ContentHeaderLink) ? "T" : "U";
                    this.ProcessContentHeaderLinkVisiblity();

                    this.PauseOnMouseOverCheckBox.Checked = this.PauseOnMouseOver;
                    this.RotatorDelayTextBox.Text = this.RotatorDelay.ToString(CultureInfo.CurrentCulture);
                    this.RotatorPauseDelayTextBox.Text = this.RotatorPauseDelay.ToString(CultureInfo.CurrentCulture);
                    this.ProcessMouseOverVisibility();

                    this.UseAnimationsCheckBox.Checked = this.UseAnimations;
                    this.AnimationDurationTextBox.Text = this.AnimationDuration.ToString(CultureInfo.CurrentCulture);
                    this.PauseOnMouseOverCheckBox.Checked = this.PauseOnMouseOver;
                    this.ProcessAnimationsVisiblity();

                    this.TemplatesDropDownList.SelectedValue = this.StyleTemplate;
                    this.TemplatesDropDownList.Attributes.Add("OriginalStyleTemplate", this.StyleTemplate);
                    this.FillTemplateTab();
                }

                this.RegisterTabsContainer();
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the Click event of the ApplyTemplateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ApplyTemplateButton_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "StyleTemplate", this.TemplatesDropDownList.SelectedValue);

                try
                {
                    TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(this.TemplatesDropDownList.SelectedValue);
                    if (manifest.Settings != null && manifest.Settings.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> setting in manifest.Settings)
                        {
                            modules.UpdateTabModuleSetting(this.TabModuleId, setting.Key, setting.Value);
                        }
                    }

                    // return to this page with the new settings applied
                    this.Response.Redirect(this.EditUrl("ModSettings"), false);
                }
                catch (XmlSchemaValidationException)
                {
                    this.ShowManifestValidationErrorMessage();
                }
                catch (XmlException)
                {
                    this.ShowManifestValidationErrorMessage();
                }
            }
            catch (Exception exc)
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
        }

        /// <summary>
        /// Handles the Click event of the SubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (this.Page.IsValid)
            {
                ModuleController modules = new ModuleController();
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorWidth", this.RotatorWidthTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorHeight", this.RotatorHeightTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentWidth", this.ContentWidthTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeight", this.ContentHeightTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailWidth", this.ThumbnailWidthTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailHeight", this.ThumbnailHeightTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailWidth", this.PositionThumbnailWidthTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailHeight", this.PositionThumbnailHeightTextBox.Text);

                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorDelay", this.RotatorDelayTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "RotatorPauseDelay", this.RotatorPauseDelayTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationDuration", this.AnimationDurationTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeaderText", this.ContentHeaderTitleTextBox.Text);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeaderLink", Dnn.Utility.CreateUrlFromControl(this.ContentHeaderLinkUrlControl, this.PortalSettings));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentHeaderLinkText", this.ContentHeaderLinkTextTextBox.Text);

                modules.UpdateTabModuleSetting(this.TabModuleId, "ControlsTitleDisplayMode", this.PositionTitleDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentTitleDisplayMode", this.ContentTitleDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ContentDisplayMode", this.ContentDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "ThumbnailDisplayMode", this.ThumbnailDisplayRadioButtonList.SelectedValue);

                modules.UpdateTabModuleSetting(this.TabModuleId, "ShowContentHeader", this.ShowContentHeaderTitleCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "ShowContentHeaderLink", this.ShowContentHeaderLinkCheckBox.Checked.ToString(CultureInfo.InvariantCulture));

                modules.UpdateTabModuleSetting(this.TabModuleId, "PositionThumbnailDisplayMode", this.PositionThumbnailDisplayRadioButtonList.SelectedValue);
                modules.UpdateTabModuleSetting(this.TabModuleId, "UseAnimations", this.UseAnimationsCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                modules.UpdateTabModuleSetting(this.TabModuleId, "AnimationPauseOnMouseOver", this.PauseOnMouseOverCheckBox.Checked.ToString(CultureInfo.InvariantCulture));
                this.Response.Redirect(Globals.NavigateURL(this.TabId), false);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the PauseOnMouseOverCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PauseOnMouseOverCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessMouseOverVisibility();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ShowContentHeaderLinkCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowContentHeaderLinkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessContentHeaderLinkVisiblity();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ShowContentHeaderTitleCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ShowContentHeaderTitleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessContentHeaderVisiblity();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the UseAnimationsCheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void UseAnimationsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ProcessAnimationsVisiblity();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the TemplatesDropDownList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TemplatesDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TemplatesDropDownList.Attributes["OriginalStyleTemplate"] != this.TemplatesDropDownList.SelectedValue)
            {
                ClientAPI.AddButtonConfirm(this.SubmitButton, Localization.GetString("TemplateChangedConfirm", this.LocalResourceFile));
            }

            this.FillTemplateTab();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ContentDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ContentDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessContentVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the PositionThumbnailDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PositionThumbnailDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessPositionThumbnailVisibility();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ThumbnailDisplayRadioButtonList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ThumbnailDisplayRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ProcessThumbnailVisibility();
        }

        /// <summary>
        /// Handles the ServerValidate event of the ContentHeaderLinkRequiredValidator control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Web.UI.WebControls.ServerValidateEventArgs"/> instance containing the event data.</param>
        private void ContentHeaderLinkRequiredValidator_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (e != null)
            {
                // valid if there is a url, or we aren't using the header link.
                // the validator should be disabled if ShowContentHeaderLinkCheckBox is unchecked, this is just a doublecheck.  BD
                e.IsValid = !string.IsNullOrEmpty(this.ContentHeaderLinkUrlControl.Url)
                            || !this.ShowContentHeaderLinkCheckBox.Checked;
            }
        }

        /// <summary>
        /// Fills the given <paramref name="list"/> with the <see cref="DisplayType"/> options
        /// </summary>
        /// <param name="list">The list to fill.</param>
        /// <param name="showRotateContentOption">if set to <c>true</c> adds the <see cref="DisplayType.RotateContent"/> option.</param>
        private void FillDisplayTypeListControl(ListControl list, bool showRotateContentOption)
        {
            list.Items.Clear();
            list.Items.AddRange(this.DisplayTypeItems);

            if (showRotateContentOption)
            {
                list.Items.Add(new ListItem(
                    Localization.GetString(DisplayType.RotateContent.ToString(), this.LocalResourceFile),
                    DisplayType.RotateContent.ToString()));
            }
        }

        /// <summary>
        /// Fills <see cref="TemplatesDropDownList"/>.
        /// </summary>
        private void FillTemplatesListControl()
        {
            this.TemplatesDropDownList.Items.Clear();
            this.TemplatesDropDownList.Items.Add(new ListItem(Localization.GetString("None", this.LocalResourceFile), string.Empty));

            string templatesDirectory = HostingEnvironment.MapPath(Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName);
            if (!string.IsNullOrEmpty(templatesDirectory))
            {
                foreach (string directory in Directory.GetDirectories(templatesDirectory))
                {
                    this.TemplatesDropDownList.Items.Add(new ListItem(directory.Substring(directory.LastIndexOf(Path.DirectorySeparatorChar) + 1)));
                }
            }
        }

        /// <summary>
        /// Displays information about the selected template
        /// </summary>
        private void FillTemplateTab()
        {
            try
            {
                this.ApplyTemplateButton.Enabled = true;
                TemplateManifest manifest = TemplateManifest.CreateTemplateManifest(this.TemplatesDropDownList.SelectedValue);
                this.TemplateDescriptionLabel.Text = manifest.Description;
                this.TemplateDescriptionPanel.Visible = Engage.Utility.HasValue(this.TemplateDescriptionLabel.Text);
                string templateFolder = Utility.DesktopModuleVirtualPath + Utility.StyleTemplatesFolderName + this.TemplatesDropDownList.SelectedValue;
                this.TemplatePreviewImage.ImageUrl = templateFolder + "/" + manifest.PreviewImageFilename;
                this.TemplatePreviewImage.Visible = File.Exists(HostingEnvironment.MapPath(this.TemplatePreviewImage.ImageUrl));
            }
            catch (XmlSchemaValidationException)
            {
                this.ShowManifestValidationErrorMessage();
            }
        }

        /// <summary>
        /// Hides and shows controls based on whether the Use Animations setting is selected
        /// </summary>
        private void ProcessAnimationsVisiblity()
        {
            this.AnimationDurationTextBox.Enabled =
                    this.AnimationDurationIntegerValidator.Enabled =
                    this.AnimationDurationRequiredValidator.Enabled = this.UseAnimationsCheckBox.Checked;

            SetDisabledCssClass(this.AnimationDurationTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the content header link is set to be shown
        /// </summary>
        private void ProcessContentHeaderLinkVisiblity()
        {
            this.ContentHeaderLinkTextTextBox.Enabled =
                    this.ContentHeaderLinkTextRequiredValidator.Enabled =
                    this.ContentHeaderLinkUrlControl.Visible =
                    this.ContentHeaderLinkRequiredValidator.Enabled = this.ShowContentHeaderLinkCheckBox.Checked;

            SetDisabledCssClass(this.ContentHeaderLinkTextTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the content header is set to be shown
        /// </summary>
        private void ProcessContentHeaderVisiblity()
        {
            this.ContentHeaderTitleTextBox.Enabled =
                    this.ContentHeaderTitleRequiredValidator.Enabled = this.ShowContentHeaderTitleCheckBox.Checked;

            SetDisabledCssClass(this.ContentHeaderTitleTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the main content
        /// </summary>
        private void ProcessContentVisibility()
        {
            this.ContentHeightTextBox.Enabled =
                    this.ContentHeightIntegerValidator.Enabled =
                    this.ContentWidthTextBox.Enabled =
                    this.ContentWidthIntegerValidator.Enabled =
                    this.ContentDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();

            SetDisabledCssClass(this.ContentHeightTextBox);
            SetDisabledCssClass(this.ContentWidthTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on whether the <see cref="PauseOnMouseOver"/> setting is selected
        /// </summary>
        private void ProcessMouseOverVisibility()
        {
            this.RotatorPauseDelayTextBox.Enabled =
                    this.RotatorPauseDelayIntegerValidator.Enabled =
                    this.RotatorPauseDelayRequiredValidtor.Enabled = this.PauseOnMouseOverCheckBox.Checked;

            SetDisabledCssClass(this.RotatorPauseDelayTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the position thumbnail
        /// </summary>
        private void ProcessPositionThumbnailVisibility()
        {
            DisplayType positionThumbnailDisplayMode = (DisplayType)Enum.Parse(typeof(DisplayType), this.PositionThumbnailDisplayRadioButtonList.SelectedValue, true);
            this.PositionThumbnailHeightTextBox.Enabled =
                    this.PositionThumbnailHeightIntegerValidator.Enabled =
                    this.PositionThumbnailWidthTextBox.Enabled =
                    this.PositionThumbnailWidthIntegerValidator.Enabled = positionThumbnailDisplayMode != DisplayType.None;

            SetDisabledCssClass(this.PositionThumbnailHeightTextBox);
            SetDisabledCssClass(this.PositionThumbnailWidthTextBox);
        }

        /// <summary>
        /// Hides and shows controls based on the selected display type for the thumbnail
        /// </summary>
        private void ProcessThumbnailVisibility()
        {
            this.ThumbnailHeightTextBox.Enabled = this.ThumbnailDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();
            this.ThumbnailHeightIntegerValidator.Enabled = this.ThumbnailHeightTextBox.Enabled;

            this.ThumbnailWidthTextBox.Enabled = this.ThumbnailDisplayRadioButtonList.SelectedValue != DisplayType.None.ToString();
            this.ThumbnailWidthIntegerValidator.Enabled = this.ThumbnailWidthTextBox.Enabled;

            SetDisabledCssClass(this.ThumbnailHeightTextBox);
            SetDisabledCssClass(this.ThumbnailWidthTextBox);
        }

        /// <summary>
        /// Displays the error message that the selected template's manifest does not pass validation
        /// </summary>
        private void ShowManifestValidationErrorMessage()
        {
            this.ManifestValidationErrorsPanel.Controls.Add(new LiteralControl("<ul><li>" + Localization.GetString("ManifestValidation", this.LocalResourceFile) + "</li></ul>"));
            this.TemplateDescriptionPanel.Visible = false;
            this.TemplatePreviewImage.Visible = false;
            this.ApplyTemplateButton.Enabled = false;
        }

        /// <summary>
        /// Registers the JavaScript to create the tabs container.
        /// </summary>
        private void RegisterTabsContainer()
        {
            this.AddJQueryReference();

#if DEBUG
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui-1.5.3.js");
#else
            this.Page.ClientScript.RegisterClientScriptResource(typeof(RotatorEdit), "Engage.Dnn.ContentRotator.JavaScript.jquery-ui-1.5.3.min.js");
#endif
        }
    }
}