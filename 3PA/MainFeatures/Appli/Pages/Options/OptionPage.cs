﻿#region header
// ========================================================================
// Copyright (c) 2017 - Julien Caillon (julien.caillon@gmail.com)
// This file (OptionPage.cs) is part of 3P.
// 
// 3P is a free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// 3P is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with 3P. If not, see <http://www.gnu.org/licenses/>.
// ========================================================================
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using YamuiFramework.Animations.Transitions;
using YamuiFramework.Controls;
using YamuiFramework.Fonts;
using YamuiFramework.HtmlRenderer.WinForms;
using YamuiFramework.Themes;
using _3PA.Images;
using _3PA.Lib;
using _3PA.MainFeatures.AutoCompletionFeature;
using _3PA.NppCore;

namespace _3PA.MainFeatures.Appli.Pages.Options {
    /// <summary>
    /// This page is built programatically
    /// </summary>
    internal partial class OptionPage : YamuiPage {
        #region fields

        private List<string> _allowedGroups;

        private YamuiButton _btSave;

        #endregion

        #region constructor

        public OptionPage(List<string> allowedGroups) {
            InitializeComponent();

            _allowedGroups = allowedGroups;

            GeneratePage();
        }

        #endregion

        #region private methods

        private void GeneratePage() {
            var lastCategory = "";
            var yPos = 0;
            var configInstance = Config.Instance;

            ForEachConfigPropertyWithDisplayAttribute((property, attribute) => {
                var valObj = property.GetValue(configInstance);

                // new group
                if (!lastCategory.EqualsCi(attribute.GroupName)) {
                    if (!string.IsNullOrEmpty(lastCategory))
                        // ReSharper disable once AccessToModifiedClosure
                        yPos += 10;
                    lastCategory = attribute.GroupName;
                    scrollPanel.ContentPanel.Controls.Add(new YamuiLabel {
                        AutoSize = true,
                        Function = FontFunction.Heading,
                        Location = new Point(0, yPos),
                        Text = lastCategory.ToUpper()
                    });
                    yPos += 30;
                }

                // name of the field
                var label = new HtmlLabel {
                    AutoSizeHeightOnly = true,
                    BackColor = Color.Transparent,
                    Location = new Point(30, yPos),
                    Size = new Size(190, 10),
                    IsSelectionEnabled = false,
                    Text = attribute.Name
                };
                scrollPanel.ContentPanel.Controls.Add(label);

                var listRangeAttr = property.GetCustomAttributes(typeof(RangeAttribute), false);
                var rangeAttr = (listRangeAttr.Any()) ? (RangeAttribute) listRangeAttr.FirstOrDefault() : null;

                if (valObj is string) {
                    // string
                    var strControl = new YamuiTextBox {
                        Location = new Point(240, yPos),
                        Text = (string) property.GetValue(configInstance),
                        Size = new Size(300, 20),
                        Tag = property.Name
                    };

                    scrollPanel.ContentPanel.Controls.Add(strControl);
                    var strButton = new YamuiButtonImage {
                        Text = @"save",
                        BackGrndImage = ImageResources.Save,
                        Size = new Size(20, 20),
                        Location = new Point(545, yPos),
                        Tag = strControl,
                        TabStop = false
                    };
                    strButton.ButtonPressed += SaveButtonOnButtonPressed;
                    scrollPanel.ContentPanel.Controls.Add(strButton);
                    tooltip.SetToolTip(strButton, "Click to <b>set the value</b> of this field<br>Otherwise, your modifications will not be saved");

                    var undoButton = new YamuiButtonImage {
                        BackGrndImage = ImageResources.UndoUserAction,
                        Size = new Size(20, 20),
                        Location = new Point(565, yPos),
                        Tag = strControl,
                        TabStop = false
                    };
                    undoButton.ButtonPressed += UndoButtonOnButtonPressed;
                    scrollPanel.ContentPanel.Controls.Add(undoButton);
                    tooltip.SetToolTip(undoButton, "Click to <b>reset this field</b> to its default value");

                    tooltip.SetToolTip(strControl, attribute.Description + "<br><div class='ToolTipBottomGoTo'>Click on the save button <img src='Save'> to set your modifications</div>");
                }
                if (valObj is int || valObj is double) {
                    // number
                    var numControl = new YamuiTextBox {
                        Location = new Point(240, yPos),
                        Text = ((valObj is int) ? ((int) property.GetValue(configInstance)).ToString() : ((double) property.GetValue(configInstance)).ToString(CultureInfo.CurrentCulture)),
                        Size = new Size(300, 20),
                        Tag = property.Name
                    };

                    scrollPanel.ContentPanel.Controls.Add(numControl);
                    var numButton = new YamuiButtonImage {
                        Text = @"save",
                        BackGrndImage = ImageResources.Save,
                        Size = new Size(20, 20),
                        Location = new Point(545, yPos),
                        Tag = numControl,
                        TabStop = false
                    };
                    numButton.ButtonPressed += SaveButtonOnButtonPressed;
                    scrollPanel.ContentPanel.Controls.Add(numButton);
                    tooltip.SetToolTip(numButton, "Click to <b>set the value</b> of this field<br>Otherwise, your modifications will not be saved");

                    var undoButton = new YamuiButtonImage {
                        BackGrndImage = ImageResources.UndoUserAction,
                        Size = new Size(20, 20),
                        Location = new Point(565, yPos),
                        Tag = numControl,
                        TabStop = false
                    };
                    undoButton.ButtonPressed += UndoButtonOnButtonPressed;
                    scrollPanel.ContentPanel.Controls.Add(undoButton);
                    tooltip.SetToolTip(undoButton, "Click to <b>reset this field</b> to its default value");

                    tooltip.SetToolTip(numControl, attribute.Description + "<br>" + (rangeAttr != null ? "<br><b><i>" + "Min value = " + rangeAttr.Minimum + "<br>Max value = " + rangeAttr.Maximum + "</i></b><br>" : "") + "<div class='ToolTipBottomGoTo'>Click on the save button <img src='Save'> to set your modifications</div>");
                } else if (valObj is bool) {
                    // bool
                    var toggleControl = new YamuiButtonToggle {
                        Location = new Point(240, yPos),
                        Size = new Size(40, 16),
                        Text = null,
                        Checked = (bool) valObj,
                        Tag = property.Name
                    };
                    toggleControl.ButtonPressed += ToggleControlOnCheckedChanged;
                    scrollPanel.ContentPanel.Controls.Add(toggleControl);

                    // tooltip
                    tooltip.SetToolTip(toggleControl, attribute.Description + "<br><div class='ToolTipBottomGoTo'>Click to <b>toggle on/off</b> this feature<br>Your choice is automatically applied</div>");
                }

                yPos += label.Height + 15;
            });

            yPos += 15;
            _btSave = new YamuiButton {
                Location = new Point(30, yPos),
                Size = new Size(120, 24),
                Text = @"Save everything",
                BackGrndImage = ImageResources.Save
            };
            _btSave.ButtonPressed += SaveAllButtonOnButtonPressed;
            tooltip.SetToolTip(_btSave, "Click to <b>save</b> all the options<br><i>This as the same effect than clicking save for each option</i>");
            scrollPanel.ContentPanel.Controls.Add(_btSave);

            var defaultButton = new YamuiButton {
                Location = new Point(155, yPos),
                Size = new Size(120, 24),
                Text = @"Reset to default",
                BackGrndImage = ImageResources.UndoUserAction
            };
            defaultButton.ButtonPressed += DefaultButtonOnButtonPressed;
            tooltip.SetToolTip(defaultButton, "Click to <b>reset</b> all the options to default");
            scrollPanel.ContentPanel.Controls.Add(defaultButton);

            // add a button for the updates
            if (_allowedGroups.Contains("Updates")) {
                var updateButton = new YamuiButton {
                    Location = new Point(280, yPos),
                    Size = new Size(150, 24),
                    Text = @"Check for updates",
                    BackGrndImage = ImageResources.Update
                };
                updateButton.ButtonPressed += (sender, args) => UpdateHandler.CheckForUpdate();
                tooltip.SetToolTip(updateButton, "Click to <b>check for updates</b>");
                scrollPanel.ContentPanel.Controls.Add(updateButton);
            }

            scrollPanel.ContentPanel.Height = yPos + 50;
        }

        #region on events

        private void SaveAllButtonOnButtonPressed(object sender, EventArgs eventArgs) {
            // refresh stuff on screen
            foreach (var control in scrollPanel.ContentPanel.Controls) {
                var ctrl = (Control) control;
                if (ctrl is YamuiButtonImage && ((YamuiButtonImage) ctrl).Text.Equals(@"save")) {
                    SaveButtonOnButtonPressed(ctrl, new EventArgs());
                }
            }
        }

        private void UndoButtonOnButtonPressed(object sender, EventArgs eventArgs) {
            // find the corresponding control
            var textBox = (YamuiTextBox) ((YamuiButtonImage) sender).Tag;
            var propertyName = (string) textBox.Tag;
            textBox.Text = ((new Config.ConfigObject()).GetValueOf(propertyName)).ToString();
            Config.Instance.SetValueOf(propertyName, ((new Config.ConfigObject()).GetValueOf(propertyName)));

            // need to refresh stuff to really apply this option?
            if (Config.Instance.GetAttributeOf<DisplayAttribute>(propertyName).AutoGenerateField) {
                ApplySettings();
            }
            Config.Save();
        }

        private void SaveButtonOnButtonPressed(object sender, EventArgs eventArgs) {
            var textBox = (YamuiTextBox) ((YamuiButtonImage) sender).Tag;
            var propertyName = (string) textBox.Tag;
            if (Config.Instance.GetValueOf(propertyName) is string) {
                // set value
                Config.Instance.SetValueOf(propertyName, textBox.Text);
            } else {
                double ouptut;
                if (!double.TryParse(textBox.Text, out ouptut)) {
                    BlinkTextBox(textBox, ThemeManager.Current.GenericErrorColor);
                    return;
                }
                // check value
                var rangeAttr = Config.Instance.GetAttributeOf<RangeAttribute>(propertyName);
                if (rangeAttr != null) {
                    double maxRange = 9999;
                    double minRange = -9999;
                    try {
                        maxRange = (double) rangeAttr.Maximum;
                        minRange = (double) rangeAttr.Minimum;
                    } catch (Exception e) {
                        if (e is InvalidCastException) {
                            maxRange = (int) rangeAttr.Maximum;
                            minRange = (int) rangeAttr.Minimum;
                        }
                    }
                    if (ouptut > maxRange || ouptut < minRange) {
                        BlinkTextBox(textBox, ThemeManager.Current.GenericErrorColor);
                        return;
                    }
                }
                // set value
                if (Config.Instance.GetValueOf(propertyName) is int)
                    Config.Instance.SetValueOf(propertyName, (int) ouptut);
                else
                    Config.Instance.SetValueOf(propertyName, ouptut);
            }

            // need to refresh stuff to really apply this option?
            if (Config.Instance.GetAttributeOf<DisplayAttribute>(propertyName).AutoGenerateField) {
                ApplySettings();
            }
            Config.Save();
        }

        private void ToggleControlOnCheckedChanged(object sender, EventArgs eventArgs) {
            var toggle = (YamuiButtonToggle) sender;
            var propertyName = (string) toggle.Tag;
            // set value
            Config.Instance.SetValueOf(propertyName, toggle.Checked);

            // need to refresh stuff to really apply this option?
            if (Config.Instance.GetAttributeOf<DisplayAttribute>(propertyName).AutoGenerateField) {
                ApplySettings();
            }
            Config.Save();
        }

        private void DefaultButtonOnButtonPressed(object sender, EventArgs buttonPressedEventArgs) {
            // apply default settings
            var defaultConfig = new Config.ConfigObject();

            // refresh stuff on screen
            foreach (var control in scrollPanel.ContentPanel.Controls) {
                var ctrl = (Control) control;
                if (ctrl.Tag != null && ctrl.Tag is string) {
                    var propertyName = (string) ctrl.Tag;
                    var value = new Config.ConfigObject().GetValueOf(propertyName);
                    if (ctrl is YamuiButtonToggle) {
                        Config.Instance.SetValueOf(propertyName, defaultConfig.GetValueOf(propertyName));
                        ((YamuiButtonToggle) ctrl).Checked = (bool) value;
                    } else if (ctrl is YamuiTextBox) {
                        Config.Instance.SetValueOf(propertyName, defaultConfig.GetValueOf(propertyName));
                        ((YamuiTextBox) ctrl).Text = value.ToString();
                    }
                }
            }
            ApplySettings();
            Config.Save();
        }

        #endregion

        /// <summary>
        /// For certain config properties, we need to refresh stuff to see a difference
        /// </summary>
        private void ApplySettings() {
            YamuiThemeManager.TabAnimationAllowed = Config.Instance.AppliAllowTabAnimation;
            CodeExplorer.CodeExplorer.Instance.ApplyColorSettings();
            FileExplorer.FileExplorer.Instance.ApplyColorSettings();
            AutoCompletion.ForceClose();
            InfoToolTip.InfoToolTip.ForceClose();
            Plug.ApplyOptionsForScintilla();
            Npp.Editor.MouseDwellTime = Config.Instance.ToolTipmsBeforeShowing;
            AutoCompletion.RefreshStaticItems(); // when changing case
        }

        /// <summary>
        /// Makes the given textbox blink
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="blinkColor"></param>
        private void BlinkTextBox(YamuiTextBox textBox, Color blinkColor) {
            textBox.UseCustomBackColor = true;
            Transition.run(textBox, "BackColor", ThemeManager.Current.ButtonNormalBack, blinkColor, new TransitionType_Flash(3, 300), (o, args) => { textBox.UseCustomBackColor = false; });
        }

        /// <summary>
        /// Execute an action for each property of the config object that has a display attribute
        /// </summary>
        /// <param name="action"></param>
        private void ForEachConfigPropertyWithDisplayAttribute(Action<FieldInfo, DisplayAttribute> action) {
            var properties = typeof(Config.ConfigObject).GetFields();

            /* loop through fields */
            foreach (var property in properties) {
                if (property.IsPrivate) {
                    continue;
                }
                var listCustomAttr = property.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (!listCustomAttr.Any()) {
                    continue;
                }
                var displayAttr = (DisplayAttribute) listCustomAttr.FirstOrDefault();
                if (displayAttr == null) {
                    continue;
                }
                if (string.IsNullOrEmpty(displayAttr.Name)) {
                    continue;
                }
                if (!_allowedGroups.Contains(displayAttr.GroupName)) {
                    continue;
                }

                // execute the action with the loop property and display attribute
                action(property, displayAttr);
            }
        }

        #endregion
    }
}