// 
// DotNetNuke?- http://www.dotnetnuke.com 
// Copyright (c) 2002-2018 
// by DotNetNuke Corporation 
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software. 
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE. 
// 

using System;
using System.Web.UI;

using DotNetNuke;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Controllers;
namespace CMI_News
{

    /// ----------------------------------------------------------------------------- 
    /// <summary> 
    /// The Settings class manages Module Settings 
    /// </summary> 
    /// <remarks> 
    /// </remarks> 
    /// <history> 
    /// </history> 
    /// ----------------------------------------------------------------------------- 
    partial class Settings : ModuleSettingsBase
    {

            /// ----------------------------------------------------------------------------- 
            /// <summary> 
            /// LoadSettings loads the settings from the Database and displays them 
            /// </summary> 
            /// <remarks> 
            /// </remarks> 
            /// <history> 
            /// </history> 
            /// ----------------------------------------------------------------------------- 
            public override void LoadSettings()
            {
                try
                {
                    if (!IsPostBack)
                    {
                        InitializeDataTypeSettings();
                        ddlDataType.SelectedValue = NewsCommon.GetDataTypeSetting(TabModuleId);

                        txtCMINewsSearchShowNum.Text = HostController.Instance.GetString("CMINewsSearchShowNum");
                        txtCMINewsMaximumDownloadNum.Text = HostController.Instance.GetString("CMINewsMaximumDownloadNum");
                        txtCMINewsTableShowNum.Text = HostController.Instance.GetString("CMINewsTableShowNum");
                        txtCMINewsTop3ShowNum.Text = HostController.Instance.GetString("CMINewsTop3ShowNum");
                        txtCMINewsLatestShowNum.Text = HostController.Instance.GetString("CMINewsLatestShowNum");
/*
                        txtSubscribeNowTitle.Text = HostController.Instance.GetString("CMINewsSubscribeNowTitle");
                        txtSubscribeNowlink.Text = HostController.Instance.GetString("CMINewsSubscribeNowLink");
                        txt7daysTrialTitle.Text = HostController.Instance.GetString("CMINews7daysTrialTitle");
                        txt7daysTrialLink.Text = HostController.Instance.GetString("CMINews7daysTrialLink");
 */
                        if (ModuleSettings["CMINewsSubscribeNowTitle"] != null && (string)ModuleSettings["CMINewsSubscribeNowTitle"] != "")
                            txtSubscribeNowTitle.Text = (string)ModuleSettings["CMINewsSubscribeNowTitle"];

                        if (ModuleSettings["CMINewsSubscribeNowLink"] != null && (string)ModuleSettings["CMINewsSubscribeNowLink"] != "")
                            txtSubscribeNowlink.Text = (string)ModuleSettings["CMINewsSubscribeNowLink"];

                        if (ModuleSettings["CMINews7daysTrialTitle"] != null && (string)ModuleSettings["CMINews7daysTrialTitle"] != "")
                            txt7daysTrialTitle.Text = (string)ModuleSettings["CMINews7daysTrialTitle"];

                        if (ModuleSettings["CMINews7daysTrialLink"] != null && (string)ModuleSettings["CMINews7daysTrialLink"] != "")
                            txt7daysTrialLink.Text = (string)ModuleSettings["CMINews7daysTrialLink"];

                        if (ModuleSettings["CMINewsTableViewInstruction"] != null && (string)ModuleSettings["CMINewsTableViewInstruction"] != "")
                            txtTableViewInstruction.Text = (string)ModuleSettings["CMINewsTableViewInstruction"];

                        if (ModuleSettings["CMINewsNewsSearchTitle"] != null && (string)ModuleSettings["CMINewsNewsSearchTitle"] != "")
                            txtNewsSearchTitle.Text = (string)ModuleSettings["CMINewsNewsSearchTitle"];

                        if (ModuleSettings["CMINewsNewsSearchLink"] != null && (string)ModuleSettings["CMINewsNewsSearchLink"] != "")
                            txtNewsSearchLink.Text = (string)ModuleSettings["CMINewsNewsSearchLink"];

                    }

                }
                catch (Exception exc)
                {
                    //Module failed to load 
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }

            /// ----------------------------------------------------------------------------- 
            /// <summary> 
            /// UpdateSettings saves the modified settings to the Database 
            /// </summary> 
            /// <remarks> 
            /// </remarks> 
            /// <history> 
            /// </history> 
            /// ----------------------------------------------------------------------------- 
            public override void UpdateSettings()
            {
                try
                {
                    NewsCommon.UpdateDataTypeSetting(TabModuleId, ddlDataType.SelectedValue);

                    HostController.Instance.Update("CMINewsSearchShowNum", txtCMINewsSearchShowNum.Text);
                    HostController.Instance.Update("CMINewsTableShowNum", txtCMINewsTableShowNum.Text);
                    HostController.Instance.Update("CMINewsMaximumDownloadNum", txtCMINewsMaximumDownloadNum.Text);
                    HostController.Instance.Update("CMINewsTop3ShowNum", txtCMINewsTop3ShowNum.Text);
                    HostController.Instance.Update("CMINewsLatestShowNum", txtCMINewsLatestShowNum.Text);
                    /*
                    HostController.Instance.Update("CMINewsSubscribeNowTitle", txtSubscribeNowTitle.Text);
                    HostController.Instance.Update("CMINewsSubscribeNowLink", txtSubscribeNowlink.Text);
                    HostController.Instance.Update("CMINews7daysTrialTitle", txt7daysTrialTitle.Text);
                    HostController.Instance.Update("CMINews7daysTrialLink", txt7daysTrialLink.Text);
                    */
                    ModuleController objModules = new ModuleController();
                    objModules.UpdateModuleSetting(ModuleId, "CMINewsSubscribeNowTitle", txtSubscribeNowTitle.Text);
                    objModules.UpdateModuleSetting(ModuleId, "CMINewsSubscribeNowLink", txtSubscribeNowlink.Text);
                    objModules.UpdateModuleSetting(ModuleId, "CMINews7daysTrialTitle", txt7daysTrialTitle.Text);
                    objModules.UpdateModuleSetting(ModuleId, "CMINews7daysTrialLink", txt7daysTrialLink.Text);

                    objModules.UpdateModuleSetting(ModuleId, "CMINewsTableViewInstruction", txtTableViewInstruction.Text);
                    objModules.UpdateModuleSetting(ModuleId, "CMINewsNewsSearchTitle", txtNewsSearchTitle.Text);
                    objModules.UpdateModuleSetting(ModuleId, "CMINewsNewsSearchLink", txtNewsSearchLink.Text);
            }

                catch (Exception exc)
                {
                    //Module failed to load 
                    Exceptions.ProcessModuleLoadException(this, exc);
                }
            }


            /// <summary>
            /// Page Init event handler
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void Page_Init(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    NewsCommon.FillDataTypeList(ddlDataType);
                }
            }

            private void InitializeDataTypeSettings()
            {
                NewsCommon.FillDataTypeList(ddlDataType);
            }
        }

    }
